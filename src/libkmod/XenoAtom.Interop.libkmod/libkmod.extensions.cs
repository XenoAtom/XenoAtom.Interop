// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

/// <summary>
/// This class is a C# representation of the libkmod library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static unsafe partial class libkmod
{
    private const DllImportSearchPath DefaultDllImportSearchPath = DllImportSearchPath.ApplicationDirectory | DllImportSearchPath.UserDirectories | DllImportSearchPath.UseDllDirectoryForDependencies;

    /// <summary>
    /// Set the resolver for the libkmod native library.
    /// </summary>
    static libkmod()
    {
        NativeLibrary.SetDllImportResolver(typeof(libkmod).Assembly, (libraryName, methodName, searchPath) =>
        {
            if (libraryName == LibraryName)
            {
                var ptr = IntPtr.Zero;
                var resolver = LibkmodDllImporterResolver;
                if (resolver != null)
                {
                    ptr = resolver(libraryName, methodName, searchPath);
                }

                if (ptr != IntPtr.Zero)
                {
                    return ptr;
                }

                {
                    if (NativeLibrary.TryLoad(LibraryNameUnix, typeof(libkmod).Assembly, DefaultDllImportSearchPath, out ptr))

                    {
                        return ptr;
                    }
                }
            }
            return IntPtr.Zero;
        });
    }

    public static System.Runtime.InteropServices.DllImportResolver? LibkmodDllImporterResolver { get; set; }

    private const string LibraryName = "libkmod";
    private const string LibraryNameUnix = "libkmod.so.2";
}