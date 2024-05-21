using System.Text;

namespace XenoAtom.Interop.Tests;

[TestClass]
public class BasicTests
{
    [TestMethod]
    public unsafe void VerifyFixedArray()
    {
        Assert.AreEqual(100 * sizeof(int), sizeof(FixedArray100<int>));
    }

    [TestMethod]
    public void VerifyReadOnlySpanUtf8()
    {
        var span = new ReadOnlySpanUtf8(Encoding.UTF8.GetBytes("Hello World"));
        Assert.AreEqual("Hello World", span.ToString());

        var span2 = new ReadOnlySpanUtf8(Encoding.UTF8.GetBytes("Hello World"));
        Assert.IsTrue(span == span2);

        Assert.AreEqual(span.GetHashCode(), span2.GetHashCode());
    }
}