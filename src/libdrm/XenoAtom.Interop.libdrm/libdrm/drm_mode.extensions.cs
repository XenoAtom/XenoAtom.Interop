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
    partial struct drm_mode_get_property
    {
        public string Name
        {
            get
            {
                fixed (drm_mode_get_property* pThis = &this)
                {
                    return Utf8CustomMarshaller.ConvertToManaged(pThis->name, 32)!;
                }
            }
        }
    }

    partial struct drm_mode_modeinfo
    {
        public string Name
        {
            get
            {
                fixed (drm_mode_modeinfo* pThis = &this)
                {
                    return Utf8CustomMarshaller.ConvertToManaged(pThis->name, 32)!;
                }
            }
        }
    }

    partial struct drm_mode_property_enum
    {
        public string Name
        {
            get
            {
                fixed (drm_mode_property_enum* pThis = &this)
                {
                    return Utf8CustomMarshaller.ConvertToManaged(pThis->name, 32)!;
                }
            }
        }
    }
}