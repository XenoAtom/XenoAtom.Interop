// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace XenoAtom.Interop;

/// <summary>
/// This class is a C# representation of the vulkan library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
unsafe partial class vulkan
{
    public const ulong VK_WHOLE_SIZE = ulong.MaxValue;

    /// <summary>
    /// Base interfaces for all function pointers.
    /// </summary>
    public interface IvkFunctionPointer
    {
        /// <summary>
        /// Gets the pointer to the function.
        /// </summary>
        nint Pointer { get; }
    }

    /// <summary>
    /// Represents a prototype of a function pointer.
    /// </summary>
    /// <typeparam name="TPFN">The type of the function pointer that inherits from <see cref="IvkFunctionPointer"/>.</typeparam>
    /// <param name="name">The name of the exported function.</param>
    public readonly ref struct vkFunctionPointerPrototype<TPFN>(ReadOnlySpanUtf8 name) where TPFN: unmanaged, IvkFunctionPointer
    {
        public ReadOnlySpanUtf8 Name { get; } = name;
    }

    /// <summary>
    /// Gets the address of the specified exported function from the Vulkan library.
    /// </summary>
    /// <typeparam name="TPFN">The type of the function pointer that inherits from <see cref="IvkFunctionPointer"/>.</typeparam>
    public static TPFN vkGetInstanceProcAddr<TPFN>(global::XenoAtom.Interop.vulkan.VkInstance instance, vkFunctionPointerPrototype<TPFN> pPrototype) where TPFN: unmanaged, IvkFunctionPointer
    {
        fixed (byte* pName = pPrototype.Name.Bytes)
        {
            return Unsafe.BitCast<PFN_vkVoidFunction, TPFN>(vkGetInstanceProcAddr(instance, pName));
        }
    }

    /// <summary>
    /// Gets the address of the specified exported function from the Vulkan library.
    /// </summary>
    /// <typeparam name="TPFN">The type of the function pointer that inherits from <see cref="IvkFunctionPointer"/>.</typeparam>
    public static TPFN vkGetDeviceProcAddr<TPFN>(global::XenoAtom.Interop.vulkan.VkDevice device, vkFunctionPointerPrototype<TPFN> pPrototype) where TPFN: unmanaged, IvkFunctionPointer
    {
        fixed (byte* pName = pPrototype.Name.Bytes)
        {
            return Unsafe.BitCast<PFN_vkVoidFunction, TPFN>(vkGetDeviceProcAddr(device, pName));
        }
    }
    
    partial struct VkGeometryInstanceFlagsKHR
    {
        public static implicit operator uint(vulkan.VkGeometryInstanceFlagsKHR from) => from.Value;

        public static implicit operator vulkan.VkGeometryInstanceFlagsKHR(uint from) => new(from);
    }

    partial struct VkBool32
    {
        public static implicit operator bool(vulkan.VkBool32 from) => from.Value != 0;

        public static implicit operator vulkan.VkBool32(bool from) => new(from ? 1U : 0U);
    }
}