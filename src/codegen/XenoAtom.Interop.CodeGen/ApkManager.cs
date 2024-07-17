// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Tar;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XenoAtom.Interop.CodeGen;

public partial class ApkManager
{
    private const string ApkIndex = "APKINDEX.tar.gz";
    private const string PackageFolder = "packages";
    private const string IncludeFolder = "include";
    private const string ManFolder = "man";
    private const string SysIncludeFolder = "system_include";
    private const string PackagesIncludeFolder = "packages_include";
    private Dictionary<string, PackageMap> _archToPackages = new();

    public const string DefaultArch = "x86_64";

    public ApkManager()
    {
        Version = "v3.20";
        Repositories= ["main", "community"];
        Architectures = [DefaultArch];
        CacheRootName = "ApkCache";
    }

    public string Version { get; set; }
    
    public List<string> Repositories { get; }

    public string[] Architectures { get; set; }

    public string CacheRootName { get; set; }

    public PackageMap GetPackages(string arch)
    {
        ValidateArch(arch);
        return _archToPackages[arch];
    }
    
    public async Task Initialize()
    {
        foreach (var arch in Architectures)
        {
            _archToPackages[arch] = new PackageMap();

            foreach (var repository in Repositories)
            {
                var cacheDirectory = GetCacheDirectory(arch, repository);
                if (!Directory.Exists(cacheDirectory))
                {
                    Directory.CreateDirectory(cacheDirectory);
                }
                var packageDirectory = GetPackageDirectory(arch, repository);
                if (!Directory.Exists(packageDirectory))
                {
                    Directory.CreateDirectory(packageDirectory);
                }
                var includeDirectory = GetIncludeDirectory(arch, repository);
                if (!Directory.Exists(includeDirectory))
                {
                    Directory.CreateDirectory(includeDirectory);
                }
                var sysIncludeDirectory = GetSysIncludeDirectory(arch, repository);
                if (!Directory.Exists(sysIncludeDirectory))
                {
                    Directory.CreateDirectory(sysIncludeDirectory);
                }
                var manFolder = GetManDirectory(arch, repository);
                if (!Directory.Exists(manFolder))
                {
                    Directory.CreateDirectory(manFolder);
                }

                var apkIndex = GetLocalPackagePath(arch, repository, ApkIndex);
                var apkUrl = GetApkUrl(arch, repository, ApkIndex);
                var lastModified = await GetLastModifiedForUrl(apkUrl);
                
                if (!File.Exists(apkIndex) || (lastModified.HasValue && File.GetLastWriteTime(apkIndex) < lastModified.Value))
                {
                    await DownloadFile(apkUrl, apkIndex);
                }

                await ReadIndex(arch, repository, apkIndex);
            }
        }
    }

    [GeneratedRegex(@"((?<prefix>\w+):)?(?<name>[^>=]+)((?<range>=|>=|>)(?<version>.+))?", RegexOptions.Multiline)]
    private static partial Regex RegexParseDependency();


    private async Task ReadIndex(string arch, string repository, string apkIndexPath)
    {
        using var fileStream = new FileStream(apkIndexPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress);
        using var reader = new TarReader(gzipStream, true);

        var packages = GetPackages(arch);

        while (true)
        {
            var tarEntry = await reader.GetNextEntryAsync();
            if (tarEntry == null)
            {
                break;
            }

            if (tarEntry.Name != "APKINDEX")
            {
                continue;
            }

            var stream = tarEntry.DataStream!;
            var indexReader = new StreamReader(stream, leaveOpen: true);

            string? currentPackage = null;
            while (await indexReader.ReadLineAsync() is { } line)
            {
                if (line.StartsWith("P:", StringComparison.Ordinal))
                {
                    currentPackage = line.Substring(2);
                    packages.CreatePackage(currentPackage, repository);
                }
                else if (line.StartsWith("V:", StringComparison.Ordinal))
                {
                    var currentPackageVersion = line.Substring(2);
                    packages.SetPackageVersion(currentPackage ?? throw new ArgumentNullException("Version found before package name"), currentPackageVersion);
                }
                else if (currentPackage != null && (line.StartsWith("D:", StringComparison.Ordinal) || line.StartsWith("p:", StringComparison.Ordinal)))
                {
                    // D:pc:skalibs pkgconfig s6-dns=2.3.7.0-r0
                    // D:so:libc.musl-x86_64.so.1 so:libskarnet.so.2.14
                    var parts = line.Substring(2).Split(' ');
                    foreach (var part in parts)
                    {
                        var match = RegexParseDependency().Match(part);
                        if (!match.Success)
                        {
                            throw new InvalidOperationException($"Invalid dependency line part `{part}` in `{line}` from package {currentPackage}");
                        }

                        var name = match.Groups["name"].Value;
                        var prefix = match.Groups["prefix"].Value;
                        var range = match.Groups["range"].Value;
                        var version = match.Groups["version"].Value;

                        var kind = prefix switch
                        {
                            "pc" => PackageProvideKind.PackageConfig,
                            "so" => PackageProvideKind.SharedObject,
                            "cmd" => PackageProvideKind.Command,
                            "dbus" => PackageProvideKind.Dbus,
                            "" => PackageProvideKind.Package,
                            _ => throw new InvalidOperationException($"Invalid prefix {prefix} in dependency line part `{part}` in `{line}` from package {currentPackage}")
                        };

                        var pkgRange = range switch
                        {
                            "=" => PackageVersionRange.Equal,
                            ">" => PackageVersionRange.GreaterThan,
                            ">=" => PackageVersionRange.GreaterThanOrEqual,
                            "" => PackageVersionRange.None,
                            _ => throw new InvalidOperationException($"Invalid range {range} in dependency line part `{part}` in `{line}` from package {currentPackage}")
                        };


                        // We don't handle dbus dependencies
                        if (kind == PackageProvideKind.Dbus)
                        {
                            continue;
                        }

                        if (IsPackageSupported(name))
                        {
                            var package = packages.GetOrCreatePackage(currentPackage!);
                            if (line.StartsWith("D:", StringComparison.Ordinal))
                            {
                                package.Dependencies.Add(new PackageDep(name, kind, pkgRange, version));
                            }
                            else
                            {
                                package.ContentDependencies.Add(new PackageContent(name, kind, pkgRange, version));

                                // Register package configs
                                if (kind == PackageProvideKind.PackageConfig)
                                {
                                    packages.PackageConfigs[name] = package;
                                }
                            }
                        }
                        
                    }
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    currentPackage = null;
                }
            }

            break;
        }
    }

    private bool IsPackageSupported(string packageName)
    {
        return packageName != "pkgconfig" && !packageName.Contains('/');
    }


    private void ValidateArch(string arch)
    {
        if (!Architectures.Contains(arch))
        {
            throw new InvalidOperationException($"Invalid arch {arch}. Supported archs: {string.Join(", ", Architectures)}");
        }
    }
    
    public async Task EnsureManPages()
    {
        foreach (var arch in Architectures)
        {
            var packageName = "man-pages";
            try
            {
                var packages = GetPackages(arch);

                if (!packages.TryGetValue(packageName, out var info))
                {
                    throw new InvalidOperationException($"Package {packageName} not found");
                }

                var packageFile = $"{packageName}-{info.Version}.apk";
                var localPath = GetLocalPackagePath(arch, info.Repository, packageFile);
                if (!File.Exists(localPath))
                {
                    var apkUrl = GetApkUrl(arch, info.Repository, packageFile);
                    DownloadFile(apkUrl, localPath).Wait();
                }

                var manDirectory = GetManDirectory(arch, info.Repository);
                await ExtractFiles("usr/share/man/", localPath, manDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to ensure includes for {packageName}: {ex.Message}");
                throw;
            }
        }
    }

    public async Task ExtractFolders(string packageName, string cacheFolder, string packageFolderPath)
    {
        if (!packageFolderPath.EndsWith('/')) throw new ArgumentException("Expecting a folder path ending with /", nameof(packageFolderPath));

        foreach (var arch in Architectures)
        {
            try
            {
                var packages = GetPackages(arch);

                if (!packages.TryGetValue(packageName, out var info))
                {
                    throw new InvalidOperationException($"Package {packageName} not found");
                }

                var packageFile = $"{packageName}-{info.Version}.apk";
                var localPath = GetLocalPackagePath(arch, info.Repository, packageFile);
                if (!File.Exists(localPath))
                {
                    var apkUrl = GetApkUrl(arch, info.Repository, packageFile);
                    DownloadFile(apkUrl, localPath).Wait();
                }

                var subCacheDirectory = GetSubCacheDirectory(cacheFolder, arch, info.Repository);
                await ExtractFiles(packageFolderPath, localPath, subCacheDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to ensure includes for {packageName}: {ex.Message}");
                throw;
            }
        }
    }

    public async Task EnsureIncludes(string packageName)
    {
        var visited = new HashSet<string>();
        await EnsureIncludes(packageName, visited);
    }
    
    private async Task EnsureIncludes(string packageName, HashSet<string> visited)
    {
        if (!visited.Add(packageName)) return;

        foreach (var arch in Architectures)
        {
            try
            {
                var packages = GetPackages(arch);

                if (!packages.TryGetValue(packageName, out var info))
                {
                    throw new InvalidOperationException($"Package {packageName} not found");
                }

                foreach (var dep in info.Dependencies)
                {
                    if (dep.Kind == PackageProvideKind.PackageConfig)
                    {
                        if (packages.PackageConfigs.TryGetValue(dep.Name, out var linkedPackage))
                        {
                            await EnsureIncludes(linkedPackage.Name, visited);
                        }
                    }
                }

                var packageFile = $"{packageName}-{info.Version}.apk";
                var localPackagePath = GetLocalPackagePath(arch, info.Repository, packageFile);
                if (!File.Exists(localPackagePath))
                {
                    var apkUrl = GetApkUrl(arch, info.Repository, packageFile);
                    DownloadFile(apkUrl, localPackagePath).Wait();
                }

                var includeDirectory = packageName == "musl-dev" ? GetSysIncludeDirectory(arch, info.Repository) : GetIncludeDirectory(arch, info.Repository);
                await ExtractFiles("usr/include/", localPackagePath, includeDirectory);

                includeDirectory = GetPackageIncludeDirectory(packageName, arch);
                await ExtractFiles("usr/include/", localPackagePath, includeDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to ensure includes for {packageName}: {ex.Message}");
                throw;
            }
        }
    }

    [GeneratedRegex("^struct timespec.*$", RegexOptions.Multiline)]
    private static partial Regex MatchTimespec();


    [GeneratedRegex(@"^#define\s+_Reg", RegexOptions.Multiline)]
    private static partial Regex MatchDefineReg();

    private async Task ExtractFiles(string includePrefix, string apkIndexPath, string includeFolder)
    {
        using var fileStream = new FileStream(apkIndexPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress);
        using var reader = new TarReader(gzipStream, true);

        while (true)
        {
            var tarEntry = await reader.GetNextEntryAsync();
            if (tarEntry == null)
            {
                break;
            }

            if (!tarEntry.Name.StartsWith(includePrefix) || tarEntry.EntryType != TarEntryType.RegularFile)
            {
                continue;
            }

            var fileName = tarEntry.Name.Substring(includePrefix.Length);
            if (string.IsNullOrEmpty(fileName) || fileName.EndsWith('/'))
            {
                continue;
            }

            var includeFinalPath = Path.Combine(includeFolder, tarEntry.Name.Substring(includePrefix.Length).Replace('/', Path.DirectorySeparatorChar));
            var fileInfo = new FileInfo(includeFinalPath);
            var isAllTypes = Path.GetFileName(fileInfo.Name) == "alltypes.h";
            if (!fileInfo.Exists || fileInfo.Length != tarEntry.Length || isAllTypes)
            {
                if (!isAllTypes || !fileInfo.Exists)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(includeFinalPath)!);
                    await using var outputFile = new FileStream(includeFinalPath, FileMode.Create, FileAccess.Write, FileShare.None);
                    // DataStream might be null when the file is empty
                    if (tarEntry.DataStream is not null)
                    {
                        await tarEntry.DataStream.CopyToAsync(outputFile);
                    }
                }

                // We need to patch alltypes.h to add XENO_ATOM_INTEROP
                // for properly handling _Addr, _Int64, _Reg which are defined per platform but can be simplified across the Linux arch we will support
                // _Addr => C# nint
                // _Int64 => C# long
                // _Reg => C# nint
                if (isAllTypes)
                {
                    var allTypesContent = File.ReadAllText(includeFinalPath);
                    string newContent = allTypesContent;
                    const string xenoAtomInteropOverride = "#ifdef XENO_ATOM_INTEROP\n#undef _Addr\n#define _Addr long\n#undef _Int64\n#define _Int64 long long\n#undef _Reg\n#define _Reg long\n#endif\n";
                    const string overrideTimeSpec = "struct timespec { time_t tv_sec; long tv_nsec; };";
                    if (!allTypesContent.Contains(xenoAtomInteropOverride) || !allTypesContent.Contains(overrideTimeSpec))
                    {
                        // We reread the original content (in case we have already patched it)
                        if (tarEntry.DataStream!.Position == 0)
                        {
                            var streamReader = new StreamReader(tarEntry.DataStream!);
                            allTypesContent = await streamReader.ReadToEndAsync();
                        }

                        var matchDefineRegResult = MatchDefineReg().Match(allTypesContent);
                        if (!matchDefineRegResult.Success)
                        {
                            throw new InvalidOperationException("Invalid alltypes.h file. Expecting #define _Reg");
                        }
                        var index = matchDefineRegResult.Index;
                        index = allTypesContent.IndexOf('\n', index) + 1;
                        newContent = allTypesContent.Insert(index, xenoAtomInteropOverride);

                        newContent = MatchTimespec().Replace(newContent, overrideTimeSpec);
                    }

                    if (newContent != allTypesContent)
                        File.WriteAllText(includeFinalPath, newContent);
                }
            }
        }
    }
    
    private async Task DownloadFile(string url, string localPath)
    {
        using var client = new System.Net.Http.HttpClient();
        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Failed to download {url}. Status code: {response.StatusCode}");
        }
        await using var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write, FileShare.None);
        await response.Content.CopyToAsync(fileStream);
    }

    private async Task<DateTimeOffset?> GetLastModifiedForUrl(string url)
    {
        using var client = new System.Net.Http.HttpClient();
        using HttpRequestMessage request = new(HttpMethod.Head, url);

        using HttpResponseMessage response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Failed to check {url}. Status code: {response.StatusCode}");
        }

        return response.Content.Headers.LastModified;
    }

    private string GetCacheDirectory(string arch, string repository)
    {
        return Path.Combine(CacheRootName, Version, repository, arch);
    }

    private string GetPackagesIncludeFolderForArch(string arch)
    {
        return Path.Combine(CacheRootName, Version, PackagesIncludeFolder, arch);
    }

    private string GetPackageDirectory(string arch, string repository)
    {
        return GetSubCacheDirectory(PackageFolder, arch, repository);
    }

    public string GetSubCacheDirectory(string name, string repository)
    {
        return GetSubCacheDirectory(name, DefaultArch, repository);
    }

    public string GetSubCacheDirectory(string name, string arch, string repository)
    {
        return Path.Combine(GetCacheDirectory(arch, repository), name);
    }

    public string GetPackageIncludeDirectory(string packageName, string? arch = null)
    {
        arch ??= DefaultArch;
        var info = GetPackages(arch);
        if (!info.ContainsKey(packageName))
        {
            throw new InvalidOperationException($"Package {packageName} not found");
        }

        var includesFolder = GetPackagesIncludeFolderForArch(arch);
        var fullPath  = Path.GetFullPath(Path.Combine(includesFolder, packageName));
        if (!Directory.Exists(fullPath))
        {
            Directory.CreateDirectory(fullPath);
        }
        return fullPath;
    }
    
    private bool IsDevPackage(string packageName)
    {
        return packageName.EndsWith("-dev") || packageName.EndsWith("-headers");
    }

    public List<string> GetPackageIncludeDirectoryAndDependencies(string packageName, out string packageIncludeFolder, string? arch = null)
    {
        var visited = new HashSet<string>();
        var directoryIncludes = new List<string>();

        arch ??= DefaultArch;
        FindPackageIncludeDependencies(packageName, arch ?? DefaultArch, directoryIncludes, visited);

        packageIncludeFolder = directoryIncludes[^1];
        directoryIncludes.RemoveAt(directoryIncludes.Count - 1);
        
        return directoryIncludes;
    }
    
    private void FindPackageIncludeDependencies(string packageName, string arch, List<string> directoryIncludes, HashSet<string> visited)
    {
        if (!visited.Add(packageName)) return;
        
        var packages = GetPackages(arch);
        if (!packages.TryGetValue(packageName, out var packageInfo))
        {
            throw new InvalidOperationException($"Package {packageName} not found");
        }

        foreach (var dep in packageInfo.Dependencies)
        {
            if (dep.Kind == PackageProvideKind.PackageConfig)
            {
                if (packages.PackageConfigs.TryGetValue(dep.Name, out var linkedPackage))
                {
                    FindPackageIncludeDependencies(linkedPackage.Name, arch, directoryIncludes, visited);
                }
            }
        }

        directoryIncludes.Add(GetPackageIncludeDirectory(packageName, arch));
    }

    public string GetIncludeDirectory(string repository)
    {
        return GetIncludeDirectory(DefaultArch, repository);
    }

    public string GetIncludeDirectory(string arch, string repository)
    {
        return GetSubCacheDirectory(IncludeFolder, arch, repository);
    }

    public string GetManDirectory(string arch, string repository)
    {
        return GetSubCacheDirectory(ManFolder, arch, repository);
    }

    public string GetSysIncludeDirectory(string repository)
    {
        return GetSysIncludeDirectory(DefaultArch, repository);
    }

    public string GetSysIncludeDirectory(string arch, string repository)
    {
        return GetSubCacheDirectory(SysIncludeFolder, arch, repository);
    }

    public string GetPackageDescriptionUrl(PackageInfo packageInfo)
    {
        // https://pkgs.alpinelinux.org/package/v3.19/main/x86_64/musl-dev
        return $"https://pkgs.alpinelinux.org/package/{Version}/{packageInfo.Repository}/{DefaultArch}/{packageInfo.Name}";
    }

    private string GetLocalPackagePath(string arch, string repository, string filename)
    {
        return Path.Combine(GetPackageDirectory(arch, repository), filename);
    }
    
    private string GetApkUrl(string arch, string repository, string filename)
    {
        return $"https://dl-cdn.alpinelinux.org/alpine/{Version}/{repository}/{arch}/{filename}";
    }
}


public record PackageDep(string Name, PackageProvideKind Kind, PackageVersionRange VersionRange = PackageVersionRange.None, string? Version = null);

public record PackageContent(string Name, PackageProvideKind Kind, PackageVersionRange VersionRange = PackageVersionRange.None, string? Version = null);

public class PackageMap : Dictionary<string, PackageInfo>
{
    public Dictionary<string, PackageInfo> PackageConfigs { get; } = new();
    
    internal PackageInfo GetOrCreatePackage(string packageName)
    {
        if (!TryGetValue(packageName, out var info))
        {
            info = new PackageInfo()
            {
                Name = packageName
            };
            Add(packageName, info);
        }

        return info;
    }

    internal void CreatePackage(string packageName, string repository)
    {
        if (!TryGetValue(packageName, out var info))
        {
            info = new PackageInfo()
            {
                Name = packageName
            };
            Add(packageName, info);
        }

        info.Repository = repository;
    }

    internal void SetPackageVersion(string packageName, string version)
    {
        if (!TryGetValue(packageName, out var info))
        {
            info = new PackageInfo()
            {
                Name = packageName
            };
            Add(packageName, info);
        }

        info.Version = version;
    }
}


[DebuggerDisplay("{Name} {Version}")]
public class PackageInfo
{
    public PackageInfo()
    {
        Version = string.Empty;
        Repository = string.Empty;
    }

    public required string Name { get; init; }

    public string Version { get; set; }

    public string Repository { get; set; }

    public List<PackageDep> Dependencies { get; } = new();

    public List<PackageContent> ContentDependencies { get; } = new();
}

public enum PackageVersionRange
{
    None,
    Equal,
    GreaterThan,
    GreaterThanOrEqual,
}


public enum PackageProvideKind
{
    Package,
    PackageConfig,
    Command,
    SharedObject,
    Dbus,
}
