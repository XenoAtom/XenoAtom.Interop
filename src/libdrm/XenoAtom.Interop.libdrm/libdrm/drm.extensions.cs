// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

/// <summary>
/// This class is a C# representation of the libdrm library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
unsafe partial class libdrm
{
    partial struct drm_version
    {
        public string Name
        {
            get
            {
                fixed (drm_version* pThis = &this)
                {
                    return Utf8CustomMarshaller.ConvertToManaged(pThis->name, (int)name_len)!;
                }
            }
        }
    }
}