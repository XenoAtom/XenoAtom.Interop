using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoAtom.Interop.CodeGen.libgit2;
using XenoAtom.Interop.CodeGen.sqlite;
using XenoAtom.Interop.CodeGen.zlib;

namespace XenoAtom.Interop.CodeGen;

public static class Program
{
    static async Task Main(string[] args)
    {

        var apkHelper = new ApkIncludeHelper();
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

    }
}