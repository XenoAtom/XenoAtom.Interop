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
public static unsafe partial class zlib
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
                    || NativeLibrary.TryLoad(AlternativeLibraryName, typeof(zlib).Assembly, DefaultDllImportSearchPath, out ptr))
                {
                    return ptr;
                }
            }
            return IntPtr.Zero;
        });
    }

    public static System.Runtime.InteropServices.DllImportResolver? ZlibDllImportResolver { get; set; }

    private const string LibraryName = "z";
    private const string AlternativeLibraryName = "zlibwapi";

    public static int deflateInit(ref global::XenoAtom.Interop.zlib.z_stream strm, int level)
    {
        fixed (global::XenoAtom.Interop.zlib.z_stream* pstrm = &strm)
        {
            return deflateInit_(pstrm, level, zlibVersion(), sizeof(global::XenoAtom.Interop.zlib.z_stream));
        }
    }

    public static int inflateInit(ref global::XenoAtom.Interop.zlib.z_stream strm)
    {
        fixed (global::XenoAtom.Interop.zlib.z_stream* pstrm = &strm)
        {
            return inflateInit_(pstrm, zlibVersion(), sizeof(global::XenoAtom.Interop.zlib.z_stream));
        }
    }

    public static int deflateInit2(ref global::XenoAtom.Interop.zlib.z_stream strm, int level, int method, int windowBits, int memLevel, int strategy)
    {
        fixed (global::XenoAtom.Interop.zlib.z_stream* pstrm = &strm)
        {
            return deflateInit2_(pstrm, level, method, windowBits, memLevel, strategy, zlibVersion(), sizeof(global::XenoAtom.Interop.zlib.z_stream));
        }
    }

    public static int inflateInit2(ref global::XenoAtom.Interop.zlib.z_stream strm, int windowBits)
    {
        fixed (global::XenoAtom.Interop.zlib.z_stream* pstrm = &strm)
        {
            return inflateInit2_(pstrm, windowBits, zlibVersion(), sizeof(global::XenoAtom.Interop.zlib.z_stream));
        }
    }

    public static int inflateBackInit(ref global::XenoAtom.Interop.zlib.z_stream strm, int windowBits, byte* window)
    {
        fixed (global::XenoAtom.Interop.zlib.z_stream* pstrm = &strm)
        {
            return inflateBackInit_(pstrm, windowBits, window, zlibVersion(), sizeof(global::XenoAtom.Interop.zlib.z_stream));
        }
    }
}