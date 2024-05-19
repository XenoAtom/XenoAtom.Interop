// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class musl
{
    /// <summary>
    /// Operations on a process or thread
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "prctl")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial int prctl(int option, nuint arg2);

    /// <summary>
    /// Operations on a process or thread
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "prctl")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial int prctl(int option, nuint arg2, nuint arg3);

    /// <summary>
    /// Operations on a process or thread
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "prctl")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial int prctl(int option, nuint arg2, nuint arg3, nuint arg4);

    /// <summary>
    /// Operations on a process or thread
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "prctl")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial int prctl(int option, nuint arg2, nuint arg3, nuint arg4, nuint arg5);
}