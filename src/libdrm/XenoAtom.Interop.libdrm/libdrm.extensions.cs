// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

/// <summary>
/// This class is a C# representation of the libdrm library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static unsafe partial class libdrm
{
    private const DllImportSearchPath DefaultDllImportSearchPath = DllImportSearchPath.ApplicationDirectory | DllImportSearchPath.UserDirectories | DllImportSearchPath.UseDllDirectoryForDependencies;

    /// <summary>
    /// Set the resolver for the libdrm native library. 
    /// </summary>
    static libdrm()
    {
        NativeLibrary.SetDllImportResolver(typeof(libdrm).Assembly, (libraryName, methodName, searchPath) =>
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

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    if (NativeLibrary.TryLoad(LibraryNameWindows, typeof(libdrm).Assembly, DefaultDllImportSearchPath, out ptr))
                    {
                        return ptr;
                    }
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    if (NativeLibrary.TryLoad(LibraryNameMacOS, typeof(libdrm).Assembly, DefaultDllImportSearchPath, out ptr) ||
                        NativeLibrary.TryLoad(LibraryNameMacOSAlternative, typeof(libdrm).Assembly, DefaultDllImportSearchPath, out ptr))

                    {
                        return ptr;
                    }
                }
                else
                {
                    if (NativeLibrary.TryLoad(LibraryNameUnix, typeof(libdrm).Assembly, DefaultDllImportSearchPath, out ptr) ||
                        NativeLibrary.TryLoad(LibraryNameUnixAlternative, typeof(libdrm).Assembly, DefaultDllImportSearchPath, out ptr))

                    {
                        return ptr;
                    }
                }
            }
            return IntPtr.Zero;
        });
    }

    public static System.Runtime.InteropServices.DllImportResolver? LibdrmDllImporterResolver { get; set; }


    private const string LibraryName = "libdrm";
    private const string LibraryNameWindows = "libdrm-1";
    private const string LibraryNameUnix = "liblibdrm.so.1";
    private const string LibraryNameUnixAlternative = "liblibdrm.so";
    private const string LibraryNameMacOS = "liblibdrm.dylib.1";
    private const string LibraryNameMacOSAlternative = "liblibdrm.dylib";
}