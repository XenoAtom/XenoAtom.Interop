using System;
using System.Threading.Tasks;
using XenoAtom.Interop.CodeGen.libgit2;
using XenoAtom.Interop.CodeGen.musl;
using XenoAtom.Interop.CodeGen.sqlite;
using XenoAtom.Interop.CodeGen.zlib;

namespace XenoAtom.Interop.CodeGen;

public static class Program
{
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
}