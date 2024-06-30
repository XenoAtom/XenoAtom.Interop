// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
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
    /// The VK_LAYER_KHRONOS_validation extension name.
    /// </summary>
    public static ReadOnlyMemoryUtf8 VK_LAYER_KHRONOS_VALIDATION_EXTENSION_NAME => "VK_LAYER_KHRONOS_validation"u8;
    
    /// <summary>
    /// Base interfaces for all function pointers.
    /// </summary>
    public interface IvkFunctionPointer
    {
        /// <summary>
        /// Gets the pointer to the function.
        /// </summary>
        nint Pointer { get; }

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        static abstract ReadOnlyMemoryUtf8 Name { get; }
    }

    /// <summary>
    /// Base interfaces for non-Global/Device/Instance function pointers.
    /// </summary>
    public interface IvkFunctionPointer<TPFN> : IvkFunctionPointer
        where TPFN : unmanaged, IvkFunctionPointer<TPFN>
    {
    }

    /// <summary>
    /// Base interfaces for all Global function pointers.
    /// </summary>
    /// <remarks>
    /// The global commands are: <see cref="vkEnumerateInstanceVersion"/>, <see cref="vkEnumerateInstanceExtensionProperties"/>, <see cref="vkEnumerateInstanceLayerProperties"/>, and <see cref="vkCreateInstance"/>. 
    /// </remarks>
    public interface IvkGlobalFunctionPointer<TPFN> : IvkFunctionPointer
        where TPFN : unmanaged, IvkGlobalFunctionPointer<TPFN>
    {
    }

    /// <summary>
    /// Base interfaces for all <see cref="VkInstance"/> related function pointers.
    /// </summary>
    public interface IvkInstanceFunctionPointer<TPFN> : IvkFunctionPointer
        where TPFN : unmanaged, IvkInstanceFunctionPointer<TPFN>
    {
    }

    /// <summary>
    /// Base interfaces for all <see cref="VkDevice"/> related function pointers.
    /// </summary>
    public interface IvkDeviceFunctionPointer<TPFN> : IvkFunctionPointer
        where TPFN : unmanaged, IvkDeviceFunctionPointer<TPFN>
    {
    }

    /// <summary>
    /// Gets the address of the specified exported Global function from the Vulkan library.
    /// </summary>
    /// <typeparam name="TPFN">The type of the function pointer that inherits from <see cref="IvkGlobalFunctionPointer{TPFN}"/>.</typeparam>
    /// <remarks>
    /// The global commands are: <see cref="vkEnumerateInstanceVersion"/>, <see cref="vkEnumerateInstanceExtensionProperties"/>, <see cref="vkEnumerateInstanceLayerProperties"/>, and <see cref="vkCreateInstance"/>. 
    /// </remarks>
    [VkVersion(VK_API_VERSION_1_0)]
    public static TPFN vkGetGlobalProcAddr<TPFN>() where TPFN : unmanaged, IvkGlobalFunctionPointer<TPFN>
    {
        fixed (byte* pName = TPFN.Name.Bytes)
        {
            return Unsafe.BitCast<PFN_vkVoidFunction, TPFN>(vkGetInstanceProcAddr(default, pName));
        }
    }

    /// <summary>
    /// Gets the address of the specified exported function from the Vulkan library.
    /// </summary>
    /// <typeparam name="TPFN">The type of the function pointer that inherits from <see cref="IvkInstanceFunctionPointer{TPFN}"/>.</typeparam>
    [VkVersion(VK_API_VERSION_1_0)]
    public static TPFN vkGetInstanceProcAddr<TPFN>(global::XenoAtom.Interop.vulkan.VkInstance instance) where TPFN: unmanaged, IvkInstanceFunctionPointer<TPFN>
    {
        fixed (byte* pName = TPFN.Name.Bytes)
        {
            return Unsafe.BitCast<PFN_vkVoidFunction, TPFN>(vkGetInstanceProcAddr(instance, pName));
        }
    }

    /// <summary>
    /// Gets the address of the specified exported function from the Vulkan library.
    /// </summary>
    /// <typeparam name="TPFN">The type of the function pointer that inherits from <see cref="IvkDeviceFunctionPointer{TPFN}"/>.</typeparam>
    [VkVersion(VK_API_VERSION_1_0)]
    public static TPFN vkGetDeviceProcAddr<TPFN>(global::XenoAtom.Interop.vulkan.VkDevice device) where TPFN : unmanaged, IvkDeviceFunctionPointer<TPFN>
    {
        fixed (byte* pName = TPFN.Name.Bytes)
        {
            return Unsafe.BitCast<PFN_vkVoidFunction, TPFN>(vkGetDeviceProcAddr(device, pName));
        }
    }

    partial struct VkBool32
    {
        public static implicit operator bool(vulkan.VkBool32 from) => from.Value != 0;

        public static implicit operator vulkan.VkBool32(bool from) => new(from ? 1U : 0U);
    }

    partial struct VkClearColorValue
    {
        public VkClearColorValue(float r, float g, float b, float a)
        {
            float32[0] = r;
            float32[1] = g;
            float32[2] = b;
            float32[3] = a;
        }
    }

    partial record struct VkOffset2D
    {
        public VkOffset2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    partial record struct VkOffset3D
    {
        public VkOffset3D(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    partial record struct VkExtent2D
    {
        public VkExtent2D(uint width, uint height)
        {
            this.width = width;
            this.height = height;
        }
    }

    partial record struct VkExtent3D
    {
        public VkExtent3D(uint width, uint height, uint depth)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;
        }
    }
    
    partial record struct VkRect2D
    {
        public VkRect2D(int x, int y, uint width, uint height)
        {
            this.offset = new(x, y);
            this.extent = new(width, height);
        }

        public VkRect2D(VkOffset2D offset, VkExtent2D extent)
        {
            this.offset = offset;
            this.extent = extent;
        }
    }

    partial struct VkClearDepthStencilValue
    {
        public VkClearDepthStencilValue(float depth, uint stencil)
        {
            this.depth = depth;
            this.stencil = stencil;
        }
    }
}