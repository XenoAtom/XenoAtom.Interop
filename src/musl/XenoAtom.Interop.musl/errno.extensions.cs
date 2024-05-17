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
    /// Gets the result of the last error.
    /// </summary>
    public static int errno
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => *__errno_location();
    }

    /// <summary>
    /// Return string describing error number
    /// </summary>
    [LibraryImport(LibraryName, EntryPoint = "strerror")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial byte* strerror_(int errno);

    /// <summary>
    /// Return string describing error number
    /// </summary>
    [LibraryImport(LibraryName, EntryPoint = "strerror")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))]
    public static partial string strerror(int errno);
}