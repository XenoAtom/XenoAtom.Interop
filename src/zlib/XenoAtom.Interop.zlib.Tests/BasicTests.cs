using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

namespace XenoAtom.Interop.Tests;

using static XenoAtom.Interop.zlib;

[TestClass]
public class BasicTests
{
    [TestMethod]
    public unsafe void TestRoundTrip()
    {
        var data = Encoding.UTF8.GetBytes("Hello, World!");
        var compressedMem = new MemoryStream();
        var zStream = new GZipStream(compressedMem, CompressionLevel.Optimal, true);
        zStream.Write(data, 0, data.Length);
        zStream.Close();
        var compressedData = compressedMem.ToArray();

        var output = new byte[32];
        int decompressedSize = 0;
        fixed (byte* decompressed_data = output)
        fixed (byte* compressed_data = compressedData)
        {
            z_stream strm = new z_stream();

            var ret = inflateInit2(ref strm, 16 + MAX_WBITS);
            Assert.AreEqual(Z_OK, ret);

            strm.avail_in = (uint)compressedMem.Length;
            strm.next_in = compressed_data;

            /* Set output buffer parameters */
            strm.avail_out = (uint)output.Length ;
            strm.next_out = decompressed_data;

            /* Inflate the data */
            ret = inflate(&strm, Z_FINISH);
            Assert.AreEqual(Z_STREAM_END, ret);

            decompressedSize = output.Length - (int)strm.avail_out;

            inflateEnd(&strm);
        }

        Assert.AreEqual(data.Length, decompressedSize);
        Assert.AreEqual("Hello, World!", Encoding.UTF8.GetString(output, 0, decompressedSize));
    }

}