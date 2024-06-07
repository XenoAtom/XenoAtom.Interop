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
    /// <summary>
    /// Get a string describing a connector type.
    /// </summary>
    /// <remarks>
    /// NULL is returned if the connector type is unsupported. Callers should handle
    /// this gracefully, e.g. by falling back to "Unknown" or printing the raw value.
    /// </remarks>
    [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "drmModeGetConnectorTypeName")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(Utf8CustomMarshaller))]
    public static partial string drmModeGetConnectorTypeName(uint connector_type);


    partial struct drmModeModeInfo
    {
        public string Name
        {
            get
            {
                fixed (drmModeModeInfo* pThis = &this)
                {
                    return Utf8CustomMarshaller.ConvertToManaged(pThis->name, 32)!;
                }
            }
        }
    }
    partial struct drmModePropertyRes
    {
        public string Name
        {
            get
            {
                fixed (drmModePropertyRes* pThis = &this)
                {
                    return Utf8CustomMarshaller.ConvertToManaged(pThis->name, 32)!;
                }
            }
        }
    }
}