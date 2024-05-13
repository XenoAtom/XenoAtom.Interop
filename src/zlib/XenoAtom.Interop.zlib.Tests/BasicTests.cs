using System.IO.Compression;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;

namespace XenoAtom.Interop.Tests;

using static XenoAtom.Interop.zlib;

[TestClass]
public class BasicTests
{
    [TestMethod]
    public unsafe void TestInvalidInit()
    {
        z_stream strm = new z_stream();
        var ret = deflateInit_(ref strm, 1, "0.0.1", sizeof(z_stream));
        Assert.AreEqual(Z_VERSION_ERROR, ret);

        ret = deflateInit_(ref strm, 1, ZLIB_VERSION, 10);
        Assert.AreEqual(Z_VERSION_ERROR, ret);
    }

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
            ret = inflate(ref strm, Z_FINISH);
            Assert.AreEqual(Z_STREAM_END, ret);

            decompressedSize = output.Length - (int)strm.avail_out;

            inflateEnd(ref strm);
        }

        Assert.AreEqual(data.Length, decompressedSize);
        Assert.AreEqual("Hello, World!", Encoding.UTF8.GetString(output, 0, decompressedSize));
    }


    [TestMethod]
    public void TestCompressUncompress()
    {
        var data = Encoding.UTF8.GetBytes("Hello, World!");
        var compressedData = new byte[32];
        uint compressedSize;
        var ret = compress(compressedData, out compressedSize, data);
        Assert.AreEqual(Z_OK, ret);

        var decompressedData = new byte[32];
        uint decompressedSize;
        ret = uncompress(decompressedData, out decompressedSize, compressedData);
        Assert.AreEqual(Z_OK, ret);

        Assert.AreEqual((uint)data.Length, decompressedSize);
        Assert.AreEqual("Hello, World!", Encoding.UTF8.GetString(decompressedData, 0, (int)decompressedSize));
    }

    [TestMethod]
    public void TestCompress2Uncompress2()
    {
        var data = Encoding.UTF8.GetBytes("Hello, World!");
        var compressedData = new byte[32];
        uint compressedSize;
        var ret = compress2(compressedData, out compressedSize, data, Z_BEST_COMPRESSION);
        Assert.AreEqual(Z_OK, ret);

        var decompressedData = new byte[32];
        uint decompressedSize;
        uint sourceBytesConsumed;
        ret = uncompress2(decompressedData, out decompressedSize, compressedData, out sourceBytesConsumed);
        Assert.AreEqual(Z_OK, ret);

        Assert.AreEqual(sourceBytesConsumed, compressedSize);
        Assert.AreEqual((uint)data.Length, decompressedSize);
        Assert.AreEqual("Hello, World!", Encoding.UTF8.GetString(decompressedData, 0, (int)decompressedSize));
    }
}