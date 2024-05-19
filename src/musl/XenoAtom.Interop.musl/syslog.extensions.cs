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
    /// Send messages to the system logger
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syslog")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void syslog(int priority, byte* format, nint arg1);

    /// <summary>
    /// Send messages to the system logger
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syslog")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void syslog(int priority, byte* format, nint arg1, nint arg2);

    /// <summary>
    /// Send messages to the system logger
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syslog")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void syslog(int priority, byte* format, nint arg1, nint arg2, nint arg3);

    /// <summary>
    /// Send messages to the system logger
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syslog")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void syslog(int priority, byte* format, nint arg1, nint arg2, nint arg3, nint arg4);

    /// <summary>
    /// Send messages to the system logger
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syslog")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void syslog(int priority, byte* format, nint arg1, nint arg2, nint arg3, nint arg4, nint arg5);

    /// <summary>
    /// Send messages to the system logger
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syslog")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void syslog(int priority, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> format, nint arg1);

    /// <summary>
    /// Send messages to the system logger
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syslog")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void syslog(int priority, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> format, nint arg1, nint arg2);

    /// <summary>
    /// Send messages to the system logger
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syslog")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void syslog(int priority, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> format, nint arg1, nint arg2, nint arg3);

    /// <summary>
    /// Send messages to the system logger
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syslog")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void syslog(int priority, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> format, nint arg1, nint arg2, nint arg3, nint arg4);

    /// <summary>
    /// Send messages to the system logger
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "syslog")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void syslog(int priority, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> format, nint arg1, nint arg2, nint arg3, nint arg4, nint arg5);
}