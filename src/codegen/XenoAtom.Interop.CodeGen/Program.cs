using System;
using System.Threading.Tasks;
using XenoAtom.Interop.CodeGen.libgit2;
using XenoAtom.Interop.CodeGen.musl;
using XenoAtom.Interop.CodeGen.sqlite;
using XenoAtom.Interop.CodeGen.zlib;

namespace XenoAtom.Interop.CodeGen;

public static class Program
{
    private static readonly LibDescriptor[] LibDescriptors =
    [
        new()
        {
            Name = "musl",
            Description = "musl is an implementation of the C standard library built on top of the Linux system call API.",
            Url = "https://musl.libc.org/"
        },
        new()
        {
            Name = "libgit2",
            Description = "libgit2 is a pure C implementation of the Git core methods.",
            Url = "https://libgit2.org/",
            NativeNuGets = [new("LibGit2Sharp.NativeBinaries", "2.0.322")]
        },
        new()
        {
            Name = "sqlite",
            Description = "SQLite is a small and fast SQL database engine.",
            Url = "https://www.sqlite.org/",
            NativeNuGets = [new("SQLitePCLRaw.lib.e_sqlite3", "2.1.8")]
        },
        new()
        {
            Name = "zlib",
            Description = "Zlib library",
            Url = "https://zlib.net/",
            NativeNuGets =
            [
                new("elskom.zlib.redist.win", "1.2.13"),
                new("elskom.zlib.redist.linux", "1.2.13"),
                new("elskom.zlib.redist.osx", "1.2.13")
            ]
        }
    ];

    static async Task Main(string[] args)
    {
        var apkHelper = new ApkIncludeHelper();
        // Multi-arch is only used by musl, the other libraries are multi-arch by default using the same x86_64 default headers
        apkHelper.Architectures = [ApkIncludeHelper.DefaultArch, "aarch64"]; // "x86", "armv7" <- not supporting these for now
        await apkHelper.Initialize();

        // Always ensure that musl-dev is included
        await apkHelper.EnsureIncludes("musl-dev");

        {
            Console.WriteLine("=================================================");
            Console.WriteLine("Generating libgit2 bindings");
            Console.WriteLine("=================================================");
            var program = new LibGit2Generator(apkHelper);
            await program.Run();
        }
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("Generating sqlite bindings");
            Console.WriteLine("=================================================");
            var program = new SqliteGenerator(apkHelper);
            await program.Run();
        }
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("Generating zlib bindings");
            Console.WriteLine("=================================================");
            var program = new ZlibGenerator(apkHelper);
            await program.Run();
        }
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("Generating musl bindings");
            Console.WriteLine("=================================================");
            var program = new MuslGenerator(apkHelper);
            await program.Run();
        }
    }

    private record LibDescriptor
    {
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required string Url { get; init; }
        public CompatibleNativeNuGet[]? NativeNuGets { get; init; } = null;
    }

    private record CompatibleNativeNuGet(string Name, string Version);
}