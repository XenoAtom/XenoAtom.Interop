// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

/// <summary>
/// This class is a C# representation of the libgbm library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static unsafe partial class libgbm
{
    private const DllImportSearchPath DefaultDllImportSearchPath = DllImportSearchPath.ApplicationDirectory | DllImportSearchPath.UserDirectories | DllImportSearchPath.UseDllDirectoryForDependencies;

    /// <summary>
    /// Set the resolver for the libgbm native library.
    /// </summary>
    static libgbm()
    {
        NativeLibrary.SetDllImportResolver(typeof(libgbm).Assembly, (libraryName, methodName, searchPath) =>
        {
            if (libraryName == LibraryName)
            {
                var ptr = IntPtr.Zero;
                var resolver = LibdrmDllImporterResolver;
                if (resolver != null)
                {
                    ptr = resolver(libraryName, methodName, searchPath);
                }

                if (ptr != IntPtr.Zero)
                {
                    return ptr;
                }

                {
                    if (NativeLibrary.TryLoad(LibraryNameUnix, typeof(libgbm).Assembly, DefaultDllImportSearchPath, out ptr))
                    {
                        return ptr;
                    }
                }
            }
            return IntPtr.Zero;
        });
    }

    public static System.Runtime.InteropServices.DllImportResolver? LibdrmDllImporterResolver { get; set; }


    private const string LibraryName = "libgbm";
    private const string LibraryNameUnix = "libgbm.so.1";
}