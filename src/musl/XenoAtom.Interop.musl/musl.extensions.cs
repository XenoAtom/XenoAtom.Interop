// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

/// <summary>
/// This class is a C# representation of the musl library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
static unsafe partial class musl
{
    private const DllImportSearchPath DefaultDllImportSearchPath = DllImportSearchPath.ApplicationDirectory | DllImportSearchPath.UserDirectories | DllImportSearchPath.UseDllDirectoryForDependencies;

    /// <summary>
    /// Set the resolver for the musl native library.
    /// </summary>
    static musl()
    {
        NativeLibrary.SetDllImportResolver(typeof(musl).Assembly, (libraryName, methodName, searchPath) =>
        {
            if (libraryName == LibraryName)
            {
                var ptr = IntPtr.Zero;
                var resolver = MuslDllImportResolver;
                if (resolver != null)
                {
                    ptr = resolver(libraryName, methodName, searchPath);
                }

                if (ptr != IntPtr.Zero)
                {
                    return ptr;
                }

                if (RuntimeInformation.OSArchitecture == Architecture.X64)
                {
                    if (NativeLibrary.TryLoad($"ld-musl-x86_64.so.1", typeof(musl).Assembly, DefaultDllImportSearchPath, out ptr))
                    {
                        return ptr;
                    }
                }
                else if (RuntimeInformation.OSArchitecture == Architecture.Arm64)
                {
                    if (NativeLibrary.TryLoad($"ld-musl-aarch64.so.1", typeof(musl).Assembly, DefaultDllImportSearchPath, out ptr))
                    {
                        return ptr;
                    }
                }
            }
            return IntPtr.Zero;
        });
    }

    private static void Initialize()
    {
        // Force initialization of the static constructor
    }

    partial class x86_64
    {
        static x86_64() => Initialize();
    }

    partial class aarch64
    {
        static aarch64() => Initialize();
    }
    
    public static System.Runtime.InteropServices.DllImportResolver? MuslDllImportResolver { get; set; }

    private const string LibraryName = "musl";

    public static dev_t makedev(ulong x, ulong y)
    {
        return (((x & 0xfffff000L) << 32) | (((x) & 0x00000fffL) << 8) | (((y) & 0xffffff00L) << 12) | (((y) & 0x000000ffL)));
    }

    public static dev_t makedev(long x, long y)
    {
        return (ulong)(((x & 0xfffff000L) << 32) | (((x) & 0x00000fffL) << 8) | (((y) & 0xffffff00L) << 12) | (((y) & 0x000000ffL)));
    }
}