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
/// This class is a C# representation of the libdrm library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
unsafe partial class libdrm
{
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "drmGetFormatModifierName")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(Utf8CustomMarshaller))]
    public static partial string drmGetFormatModifierName(ulong modifier);

    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "drmGetFormatName")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(Utf8CustomMarshaller))]
    public static partial string drmGetFormatName(uint format);

    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "drmGetDeviceNameFromFd")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(Utf8CustomMarshaller))]
    public static partial string drmGetDeviceNameFromFd(int fd);

    /// <summary>
    /// Improved version of drmGetDeviceNameFromFd which attributes for any type of
    /// device/node - card or renderD.
    /// </summary>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "drmGetDeviceNameFromFd2")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(Utf8CustomMarshaller))]
    public static partial string drmGetDeviceNameFromFd2(int fd);

    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "drmGetPrimaryDeviceNameFromFd")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(Utf8CustomMarshaller))]
    public static partial string drmGetPrimaryDeviceNameFromFd(int fd);

    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "drmGetRenderDeviceNameFromFd")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(Utf8CustomMarshaller))]
    public static partial string drmGetRenderDeviceNameFromFd(int fd);

    public partial struct drmServerInfo
    {
        public delegate* unmanaged[Cdecl]<byte*, nint> debug_print; // void (*debug_print)(const char *format, ...);

        public delegate* unmanaged[Cdecl]<byte*, int> load_module;

        public delegate* unmanaged[Cdecl]<uint*, uint*, void> get_perms;
    }

    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "drmSetServerInfo")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void drmSetServerInfo(drmServerInfo* info);


    partial struct drmVersion
    {
        public string Name
        {
            get
            {
                fixed (drmVersion* pThis = &this)
                {
                    return Utf8CustomMarshaller.ConvertToManaged(pThis->name, name_len)!;
                }
            }
        }

        public string Date
        {
            get
            {
                fixed (drmVersion* pThis = &this)
                {
                    return Utf8CustomMarshaller.ConvertToManaged(pThis->date, date_len)!;
                }
            }
        }

        public string Desc
        {
            get
            {
                fixed (drmVersion* pThis = &this)
                {
                    return Utf8CustomMarshaller.ConvertToManaged(pThis->desc, desc_len)!;
                }
            }
        }
    }
}