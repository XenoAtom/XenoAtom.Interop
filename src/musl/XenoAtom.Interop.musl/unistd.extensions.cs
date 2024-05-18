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
    /// Indirect system call
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syscall")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial nint syscall(nint number, nint arg0);

    /// <summary>
    /// Indirect system call
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syscall")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial nint syscall(nint number, nint arg0, nint arg1);

    /// <summary>
    /// Indirect system call
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syscall")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial nint syscall(nint number, nint arg0, nint arg1, nint arg2);

    /// <summary>
    /// Indirect system call
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syscall")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial nint syscall(nint number, nint arg0, nint arg1, nint arg2, nint arg3);

    /// <summary>
    /// Indirect system call
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syscall")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial nint syscall(nint number, nint arg0, nint arg1, nint arg2, nint arg3, nint arg4);

    /// <summary>
    /// Indirect system call
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syscall")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial nint syscall(nint number, nint arg0, nint arg1, nint arg2, nint arg3, nint arg4, nint arg5);

    /// <summary>
    /// Indirect system call
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syscall")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial nint syscall(nint number, nint arg0, nint arg1, nint arg2, nint arg3, nint arg4, nint arg5, nint arg6);
}