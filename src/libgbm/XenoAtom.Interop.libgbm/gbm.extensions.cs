// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace XenoAtom.Interop;

/// <summary>
/// This class is a C# representation of the libgbm library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static unsafe partial class libgbm
{
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "gbm_device_get_backend_name")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(Utf8CustomMarshaller))]
    public static partial string gbm_device_get_backend_name(libgbm.gbm_device gbm);

    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "gbm_format_get_name")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(Utf8CustomMarshaller))]
    public static partial string gbm_format_get_name(uint gbm_format, libgbm.gbm_format_name_desc* desc);
}