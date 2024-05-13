// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

/// <summary>
/// This class is a C# representation of the zlib library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class zlib
{
    private const DllImportSearchPath DefaultDllImportSearchPath = DllImportSearchPath.ApplicationDirectory | DllImportSearchPath.UserDirectories | DllImportSearchPath.UseDllDirectoryForDependencies;

    /// <summary>
    /// Set the resolver for the zlib native library.
    /// </summary>
    static zlib()
    {
        NativeLibrary.SetDllImportResolver(typeof(zlib).Assembly, (libraryName, methodName, searchPath) =>
        {
            if (libraryName == LibraryName)
            {
                var ptr = IntPtr.Zero;
                var resolver = ZlibDllImportResolver;
                if (resolver != null)
                {
                    ptr = resolver(libraryName, methodName, searchPath);
                }

                if (ptr != IntPtr.Zero)
                {
                    return ptr;
                }

                if (NativeLibrary.TryLoad(LibraryName, typeof(zlib).Assembly, DefaultDllImportSearchPath, out ptr)
                    || NativeLibrary.TryLoad(AlternativeLibraryName1, typeof(zlib).Assembly, DefaultDllImportSearchPath, out ptr)
                    || NativeLibrary.TryLoad(AlternativeLibraryName2, typeof(zlib).Assembly, DefaultDllImportSearchPath, out ptr))
                {
                    return ptr;
                }
            }
            return IntPtr.Zero;
        });
    }

    public static System.Runtime.InteropServices.DllImportResolver? ZlibDllImportResolver { get; set; }

    private const string LibraryName = "z";
    private const string AlternativeLibraryName1 = "zlib";
    private const string AlternativeLibraryName2 = "zlibwapi";
    
    public static z_result_t deflateInit(ref z_stream strm, int level)
        => deflateInit_(ref strm, level, ZLIB_VERSION, sizeof(global::XenoAtom.Interop.zlib.z_stream));

    public static z_result_t inflateInit(ref z_stream strm)
        => inflateInit_(ref strm, ZLIB_VERSION, sizeof(global::XenoAtom.Interop.zlib.z_stream));

    public static z_result_t deflateInit2(ref z_stream strm, int level, int method, int windowBits, int memLevel, int strategy)
        => deflateInit2_(ref strm, level, method, windowBits, memLevel, strategy, ZLIB_VERSION, sizeof(global::XenoAtom.Interop.zlib.z_stream));

    public static z_result_t inflateInit2(ref z_stream strm, int windowBits)
        => inflateInit2_(ref strm, windowBits, ZLIB_VERSION, sizeof(global::XenoAtom.Interop.zlib.z_stream));

    public static z_result_t inflateBackInit(ref z_stream strm, int windowBits, byte* window)
        => inflateBackInit_(ref strm, windowBits, window, ZLIB_VERSION, sizeof(global::XenoAtom.Interop.zlib.z_stream));
    
    public static zlib.z_result_t compress(Span<byte> dest, out uint destCompressedLength, ReadOnlySpan<byte> source)
    {
        var localDestCompressedLength = new CULong((uint)dest.Length);
        fixed (byte* destPtr = dest)
        fixed (byte* sourcePtr = source)
        {
            var result = compress(destPtr, &localDestCompressedLength, sourcePtr, new CULong((uint)source.Length));
            destCompressedLength =  (uint)localDestCompressedLength.Value;
            return result;
        }
    }

    public static zlib.z_result_t compress2(Span<byte> dest, out uint destCompressedLength, ReadOnlySpan<byte> source, int level)
    {
        var localDestCompressedLength = new CULong((uint)dest.Length);
        fixed (byte* destPtr = dest)
        fixed (byte* sourcePtr = source)
        {
            var result = compress2(destPtr, &localDestCompressedLength, sourcePtr, new CULong((uint)source.Length), level);
            destCompressedLength =  (uint)localDestCompressedLength.Value;
            return result;
        }
    }

    public static zlib.z_result_t uncompress(Span<byte> dest, out uint destDecompressedLength, ReadOnlySpan<byte> source)
    {
        var localDestDecompressedLength = new CULong((uint)dest.Length);
        fixed (byte* destPtr = dest)
        fixed (byte* sourcePtr = source)
        {
            var result = uncompress(destPtr, &localDestDecompressedLength, sourcePtr, new CULong((uint)source.Length));
            destDecompressedLength =  (uint)localDestDecompressedLength.Value;
            return result;
        }
    }

    public static zlib.z_result_t uncompress2(Span<byte> dest, out uint destDecompressedLength, ReadOnlySpan<byte> source, out uint sourceBytesConsumed)
    {
        var localDestDecompressedLength = new CULong((uint)dest.Length);
        var localSourceBytesConsumed = new CULong((uint)source.Length);
        fixed (byte* destPtr = dest)
        fixed (byte* sourcePtr = source)
        {
            var result = uncompress2(destPtr, &localDestDecompressedLength, sourcePtr, &localSourceBytesConsumed);
            destDecompressedLength =  (uint)localDestDecompressedLength.Value;
            sourceBytesConsumed =  (uint)localSourceBytesConsumed.Value;
            return result;
        }
    }
    
    partial struct z_stream
    {
        /// <summary>
        /// Gets the message as a string.
        /// </summary>
        public string? msg_string => Utf8CustomMarshaller.ConvertToManaged(msg);
    }

    partial struct gz_header
    {
        /// <summary>
        /// Gets the name as a string.
        /// </summary>
        public string? name_string => Utf8CustomMarshaller.ConvertToManaged(name);

        /// <summary>
        /// Gets the comment as a string.
        /// </summary>
        public string? comment_string => Utf8CustomMarshaller.ConvertToManaged(comment);
    }
}