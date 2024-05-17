// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XenoAtom.Interop.CodeGen;

public partial class ApkIncludeHelper
{
    private const string ApkIndex = "APKINDEX.tar.gz";
    private const string PackageFolder = "packages";
    private const string IncludeFolder = "include";
    private const string ManFolder = "man";
    private const string SysIncludeFolder = "system_include";
    private Dictionary<string, PackageMap> _archToPackages = new();

    public const string DefaultArch = "x86_64";

    public ApkIncludeHelper()
    {
        Version = "v3.19";
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
                if (!File.Exists(apkIndex))
                {
                    var apkUrl = GetApkUrl(arch, repository, ApkIndex);
                    await DownloadFile(apkUrl, apkIndex);
                }

                await ReadIndex(arch, repository, apkIndex);
            }
        }
    }

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
                else if (currentPackage != null && line.StartsWith("D:", StringComparison.Ordinal))
                {
                    // D:pc:skalibs pkgconfig s6-dns=2.3.7.0-r0
                    // D:so:libc.musl-x86_64.so.1 so:libskarnet.so.2.14
                    var parts = line.Substring(2).Split(' ');
                    foreach (var part in parts)
                    {
                        var indexColon  = part.IndexOf(':', StringComparison.Ordinal);
                        if (indexColon > 0)
                        {
                            var name = part.Substring(indexColon + 1);
                            var prefix = part.Substring(0, indexColon);
                            var kind = prefix switch
                            {
                                "pc" => PackageProvideKind.PackageConfig,
                                "so" => PackageProvideKind.SharedObject,
                                "cmd" => PackageProvideKind.Command,
                                "dbus" => PackageProvideKind.Dbus,
                                _ => throw new InvalidOperationException($"Invalid prefix {prefix}")
                            };

                            // We don't handle dbus dependencies
                            if (kind == PackageProvideKind.Dbus)
                            {
                                continue;
                            }

                            string pkgName = GetPackageAndVersion(name, out var pkgVersion, out var range);
                            if (IsPackageSupported(pkgName))
                            {
                                packages.AddPackageDep(currentPackage!, new PackageDep(pkgName, kind, range, pkgVersion));
                            }
                        }
                        else 
                        {
                            var pkgName = GetPackageAndVersion(part, out var pkgVersion, out var range);
                            if (IsPackageSupported(pkgName))
                            {
                                packages.AddPackageDep(currentPackage!, new PackageDep(pkgName, PackageProvideKind.Package, range, pkgVersion));
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

    public async Task EnsureIncludes(string packageName)
    {
        foreach (var arch in Architectures)
        {
            try
            {
                var packages = GetPackages(arch);

                if (!packages.TryGetValue(packageName, out var info))
                {
                    throw new InvalidOperationException($"Package {packageName} not found");
                }

                foreach (var dep in info)
                {
                    if (dep.Kind == PackageProvideKind.Package)
                    {
                        if (dep.Name.EndsWith("-dev"))
                        {
                            await EnsureIncludes(dep.Name);
                        }
                    }
                }

                var packageFile = $"{packageName}-{info.Version}.apk";
                var localPath = GetLocalPackagePath(arch, info.Repository, packageFile);
                if (!File.Exists(localPath))
                {
                    var apkUrl = GetApkUrl(arch, info.Repository, packageFile);
                    DownloadFile(apkUrl, localPath).Wait();
                }

                var includeDirectory = packageName == "musl-dev" ? GetSysIncludeDirectory(arch, info.Repository) : GetIncludeDirectory(arch, info.Repository);
                await ExtractFiles("usr/include/", localPath, includeDirectory);
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

    private string GetPackageAndVersion(string part, out string? packageVersion, out PackageVersionRange range)
    {
        var indexEqual = part.IndexOf('=');
        if (indexEqual > 0)
        {
            packageVersion = part.Substring(indexEqual + 1);
            part = part.Substring(0, indexEqual);
            range = PackageVersionRange.Equal;
            return part;
        }
        var indexSup = part.IndexOf('>');
        if (indexSup > 0)
        {
            packageVersion = part.Substring(indexEqual + 1);
            part = part.Substring(0, indexSup);
            range = PackageVersionRange.GreaterThan;
            return part;
        }

        packageVersion = null;
        range = PackageVersionRange.None;
        return part;
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
    
    private string GetCacheDirectory(string arch, string repository)
    {
        return Path.Combine(CacheRootName, Version, repository, arch);
    }

    private string GetPackageDirectory(string arch, string repository)
    {
        return Path.Combine(GetCacheDirectory(arch, repository), PackageFolder);
    }

    public string GetIncludeDirectory(string repository)
    {
        return GetIncludeDirectory(DefaultArch, repository);
    }

    public string GetIncludeDirectory(string arch, string repository)
    {
        return Path.Combine(GetCacheDirectory(arch, repository), IncludeFolder);
    }

    public string GetManDirectory(string arch, string repository)
    {
        return Path.Combine(GetCacheDirectory(arch, repository), ManFolder);
    }

    public string GetSysIncludeDirectory(string repository)
    {
        return GetSysIncludeDirectory(DefaultArch, repository);
    }

    public string GetSysIncludeDirectory(string arch, string repository)
    {
        return Path.Combine(GetCacheDirectory(arch, repository), SysIncludeFolder);
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

public class PackageMap : Dictionary<string, PackageInfo>
{
    internal void AddPackageDep(string packageName, PackageDep dep)
    {
        if (!TryGetValue(packageName, out var info))
        {
            info = new PackageInfo();
            Add(packageName, info);
        }

        info.Add(dep);
    }

    internal void CreatePackage(string packageName, string repository)
    {
        if (!TryGetValue(packageName, out var info))
        {
            info = new PackageInfo();
            Add(packageName, info);
        }

        info.Repository = repository;
    }

    internal void SetPackageVersion(string packageName, string version)
    {
        if (!TryGetValue(packageName, out var info))
        {
            info = new PackageInfo();
            Add(packageName, info);
        }

        info.Version = version;
    }
}


public class PackageInfo : List<PackageDep>
{
    public PackageInfo()
    {
        Version = string.Empty;
        Repository = string.Empty;
    }

    public string Version { get; set; }

    public string Repository { get; set; }
}

public enum PackageVersionRange
{
    None,
    Equal,
    GreaterThan,
}


public enum PackageProvideKind
{
    Package,
    PackageConfig,
    Command,
    SharedObject,
    Dbus,
}
