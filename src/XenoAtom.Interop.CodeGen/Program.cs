using XenoAtom.Interop.CodeGen.libgit2;

namespace XenoAtom.Interop.CodeGen
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var program = new LibGit2Generator();
            program.Run();
        }
    }
}