// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

/// <summary>
/// This class is a C# representation of the libdrm library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
unsafe partial class libdrm
{
    public partial struct drmServerInfo
    {
        public delegate* unmanaged[Cdecl]<byte*, nint> debug_print; // void (*debug_print)(const char *format, ...);

        public delegate* unmanaged[Cdecl]<byte*, int> load_module;

        public delegate* unmanaged[Cdecl]<uint*, uint*, void> get_perms;
    }

    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "drmSetServerInfo")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void drmSetServerInfo(drmServerInfo* info);
}