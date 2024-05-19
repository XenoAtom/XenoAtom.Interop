using System.IO.Compression;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;

namespace XenoAtom.Interop.Tests;

using static XenoAtom.Interop.musl;

[TestClass]
public class BasicTests
{
    [TestMethod]
    public unsafe void TestInvalidInit()
    {
        //var ret = mkdir("test_directory", S_IRWXU | S_IRWXG | S_IRWXO);
        //if (ret == -1)
        //{
        //    var lerrno = errno;
        //    // ...
        //}
    }
}