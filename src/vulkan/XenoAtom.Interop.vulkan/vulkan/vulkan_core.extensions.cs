// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

/// <summary>
/// This class is a C# representation of the vulkan library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
partial class vulkan
{
    public const ulong VK_WHOLE_SIZE = ulong.MaxValue;

    partial struct VkGeometryInstanceFlagsKHR
    {
        public static implicit operator uint(vulkan.VkGeometryInstanceFlagsKHR from) => from.Value;

        public static implicit operator vulkan.VkGeometryInstanceFlagsKHR(uint from) => new(from);
    }
}