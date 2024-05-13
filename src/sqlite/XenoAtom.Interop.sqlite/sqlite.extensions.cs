// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

/// <summary>
/// This class is a C# representation of the sqlite library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static unsafe partial class sqlite
{
    private const DllImportSearchPath DefaultDllImportSearchPath = DllImportSearchPath.ApplicationDirectory | DllImportSearchPath.UserDirectories | DllImportSearchPath.UseDllDirectoryForDependencies;

    /// <summary>
    /// Set the resolver for the sqlite native library. 
    /// </summary>
    static sqlite()
    {
        NativeLibrary.SetDllImportResolver(typeof(sqlite).Assembly, (libraryName, methodName, searchPath) =>
        {
            if (libraryName == LibraryName)
            {
                var ptr = IntPtr.Zero;
                var resolver = SqliteDllImporterResolver;
                if (resolver != null)
                {
                    ptr = resolver(libraryName, methodName, searchPath);
                }

                if (ptr != IntPtr.Zero)
                {
                    return ptr;
                }

                if (NativeLibrary.TryLoad(LibraryName, typeof(sqlite).Assembly, DefaultDllImportSearchPath, out ptr)
                    || NativeLibrary.TryLoad(AlternativeLibraryName, typeof(sqlite).Assembly, DefaultDllImportSearchPath, out ptr))
                {
                    return ptr;
                }
            }
            return IntPtr.Zero;
        });
    }

    public static System.Runtime.InteropServices.DllImportResolver? SqliteDllImporterResolver { get; set; }

    private const string LibraryName = "sqlite";
    private const string AlternativeLibraryName = "e_sqlite3";

}