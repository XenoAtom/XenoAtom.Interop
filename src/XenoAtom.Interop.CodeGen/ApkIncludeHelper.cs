// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace XenoAtom.Interop.CodeGen;

public class ApkIncludeHelper
{
    private const string ApkIndex = "APKINDEX.tar.gz";
    private const string PackageFolder = "packages";
    private const string IncludeFolder = "include";
    private const string SysIncludeFolder = "system_include";
    private Dictionary<string, PackageInfo> _packages = new();

    public ApkIncludeHelper()
    {
        Version = "v3.19";
        Repositories= ["main", "community"];
        Arch = "x86_64";
        CacheRootName = "ApkCache";
    }

    public string Version { get; set; }
    
    public List<string> Repositories { get; }

    public string Arch { get; set; }

    public string CacheRootName { get; set; }

    public Dictionary<string, PackageInfo> Packages => _packages;

    public async Task Initialize()
    {
        foreach (var repository in Repositories)
        {
            var cacheDirectory = GetCacheDirectory(repository);
            if (!Directory.Exists(cacheDirectory))
            {
                Directory.CreateDirectory(cacheDirectory);
            }
            var packageDirectory = GetPackageDirectory(repository);
            if (!Directory.Exists(packageDirectory))
            {
                Directory.CreateDirectory(packageDirectory);
            }
            var includeDirectory = GetIncludeDirectory(repository);
            if (!Directory.Exists(includeDirectory))
            {
                Directory.CreateDirectory(includeDirectory);
            }
            var sysIncludeDirectory = GetSysIncludeDirectory(repository);
            if (!Directory.Exists(sysIncludeDirectory))
            {
                Directory.CreateDirectory(sysIncludeDirectory);
            }

            var apkIndex = GetLocalPackagePath(repository, ApkIndex);
            if (!File.Exists(apkIndex))
            {
                var apkUrl = GetApkUrl(repository, ApkIndex);
                await DownloadFile(apkUrl, apkIndex);
            }

            await ReadIndex(repository, apkIndex);
        }
    }

    private async Task ReadIndex(string repository, string apkIndexPath)
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
                    CreatePackage(currentPackage, repository);
                }
                else if (line.StartsWith("V:", StringComparison.Ordinal))
                {
                    var currentPackageVersion = line.Substring(2);
                    SetPackageVersion(currentPackage ?? throw new ArgumentNullException("Version found before package name"), currentPackageVersion);
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
                                AddPackageDep(currentPackage!, new PackageDep(pkgName, kind, range, pkgVersion));
                            }
                        }
                        else 
                        {
                            var pkgName = GetPackageAndVersion(part, out var pkgVersion, out var range);
                            if (IsPackageSupported(pkgName))
                            {
                                AddPackageDep(currentPackage!, new PackageDep(pkgName, PackageProvideKind.Package, range, pkgVersion));
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


    public async Task EnsureIncludes(string packageName)
    {
        if (!packageName.EndsWith("-dev")) throw new InvalidOperationException($"Package {packageName} is not a dev package");

        try
        {
            if (!_packages.TryGetValue(packageName, out var info))
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
            var localPath = GetLocalPackagePath(info.Repository, packageFile);
            if (!File.Exists(localPath))
            {
                var apkUrl = GetApkUrl(info.Repository, packageFile);
                DownloadFile(apkUrl, localPath).Wait();
            }

            var includeDirectory = packageName == "musl-dev" ? GetSysIncludeDirectory(info.Repository) : GetIncludeDirectory(info.Repository);
            await ExtractIncludes(localPath, includeDirectory);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to ensure includes for {packageName}: {ex.Message}");
            throw;
        }
    }

    private async Task ExtractIncludes(string apkIndexPath, string includeFolder)
    {
        using var fileStream = new FileStream(apkIndexPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress);
        using var reader = new TarReader(gzipStream, true);
        const string includePrefix = "usr/include/";

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
            if (!fileInfo.Exists || fileInfo.Length != tarEntry.Length)
            {
                //if (Path.GetFileName(fileInfo.Name) == "alltypes.h")
                //{
                //    Console.WriteLine($"Skipping {includeFinalPath}");
                //    continue;
                //}

                Directory.CreateDirectory(Path.GetDirectoryName(includeFinalPath)!);
                await using var outputFile = new FileStream(includeFinalPath, FileMode.Create, FileAccess.Write, FileShare.None);
                // DataStream might be null when the file is empty
                if (tarEntry.DataStream is not null)
                {
                    await tarEntry.DataStream.CopyToAsync(outputFile);
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

    private void AddPackageDep(string packageName, PackageDep dep)
    {
        if (!_packages.TryGetValue(packageName, out var info))
        {
            info = new PackageInfo();
            _packages.Add(packageName, info);
        }

        info.Add(dep);
    }

    private void CreatePackage(string packageName, string repository)
    {
        if (!_packages.TryGetValue(packageName, out var info))
        {
            info = new PackageInfo();
            _packages.Add(packageName, info);
        }

        info.Repository = repository;
    }

    private void SetPackageVersion(string packageName, string version)
    {
        if (!_packages.TryGetValue(packageName, out var info))
        {
            info = new PackageInfo();
            _packages.Add(packageName, info);
        }

        info.Version = version;
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
    
    private string GetCacheDirectory(string repository)
    {
        return Path.Combine(CacheRootName, Version, repository, Arch);
    }

    private string GetPackageDirectory(string repository)
    {
        return Path.Combine(GetCacheDirectory(repository), PackageFolder);
    }

    public string GetIncludeDirectory(string repository)
    {
        return Path.Combine(GetCacheDirectory(repository), IncludeFolder);
    }

    public string GetSysIncludeDirectory(string repository)
    {
        return Path.Combine(GetCacheDirectory(repository), SysIncludeFolder);
    }

    private string GetLocalPackagePath(string repository, string filename)
    {
        return Path.Combine(GetPackageDirectory(repository), filename);
    }
    
    private string GetApkUrl(string repository, string filename)
    {
        return $"https://dl-cdn.alpinelinux.org/alpine/{Version}/{repository}/{Arch}/{filename}";
    }
}


public record PackageDep(string Name, PackageProvideKind Kind, PackageVersionRange VersionRange = PackageVersionRange.None, string? Version = null);

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
