using System;
using System.Linq;
using System.Threading.Tasks;
using XenoAtom.Interop.CodeGen.libgit2;

namespace XenoAtom.Interop.CodeGen;

public static class Program
{
    static async Task Main(string[] args)
    {

        var apkHelper = new ApkIncludeHelper();
        await apkHelper.Initialize();

        // Always ensure that musl-dev is included
        await apkHelper.EnsureIncludes("musl-dev");

        var program = new LibGit2Generator(apkHelper);
        await program.Run();
    }
}