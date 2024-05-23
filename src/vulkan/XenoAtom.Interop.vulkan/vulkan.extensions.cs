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
public static unsafe partial class vulkan
{
    private const DllImportSearchPath DefaultDllImportSearchPath = DllImportSearchPath.ApplicationDirectory | DllImportSearchPath.UserDirectories | DllImportSearchPath.UseDllDirectoryForDependencies;

    /// <summary>
    /// Set the resolver for the vulkan native library. 
    /// </summary>
    static vulkan()
    {
        NativeLibrary.SetDllImportResolver(typeof(vulkan).Assembly, (libraryName, methodName, searchPath) =>
        {
            if (libraryName == LibraryName)
            {
                var ptr = IntPtr.Zero;
                var resolver = VulkanDllImporterResolver;
                if (resolver != null)
                {
                    ptr = resolver(libraryName, methodName, searchPath);
                }

                if (ptr != IntPtr.Zero)
                {
                    return ptr;
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    if (NativeLibrary.TryLoad(LibraryNameWindows, typeof(vulkan).Assembly, DefaultDllImportSearchPath, out ptr))
                    {
                        return ptr;
                    }
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    if (NativeLibrary.TryLoad(LibraryNameMacOS, typeof(vulkan).Assembly, DefaultDllImportSearchPath, out ptr) ||
                        NativeLibrary.TryLoad(LibraryNameMacOSAlternative, typeof(vulkan).Assembly, DefaultDllImportSearchPath, out ptr))

                    {
                        return ptr;
                    }
                }
                else
                {
                    if (NativeLibrary.TryLoad(LibraryNameUnix, typeof(vulkan).Assembly, DefaultDllImportSearchPath, out ptr) ||
                        NativeLibrary.TryLoad(LibraryNameUnixAlternative, typeof(vulkan).Assembly, DefaultDllImportSearchPath, out ptr))

                    {
                        return ptr;
                    }
                }
            }
            return IntPtr.Zero;
        });
    }

    public static System.Runtime.InteropServices.DllImportResolver? VulkanDllImporterResolver { get; set; }

    private const string LibraryName = "vulkan";
    private const string LibraryNameWindows = "vulkan-1";
    private const string LibraryNameUnix = "libvulkan.so.1";
    private const string LibraryNameUnixAlternative = "libvulkan.so";
    private const string LibraryNameMacOS = "libvulkan.dylib.1";
    private const string LibraryNameMacOSAlternative = "libvulkan.dylib";

    /// <summary>
    /// Checks the result of a Vulkan function.
    /// </summary>
    /// <param name="result">The result of a command.</param>
    /// <param name="context">An optional message to explain the context in which this error occured.</param>
    /// <returns>The result if no exceptions was thrown.</returns>
    /// <exception cref="VulkanException">If the result is an error.</exception>
    public static VkResult VkCheck(this VkResult result, string? context = null)
    {
        if ((int)result < 0)
        {
            throw new VulkanException(result, context);
        }
        return result;
    }
    
    /// <summary>
    /// Exception thrown when a Vulkan error occurs.
    /// </summary>
    public class VulkanException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="VulkanException"/>.
        /// </summary>
        public VulkanException(VkResult result, string? message = null) : base(FormatError(result, message))
        {
            Result = result;
        }
        
        /// <summary>
        /// Gets the result of the Vulkan error.
        /// </summary>
        public VkResult Result { get; }

        private static string FormatError(VkResult result, string? message)
        {
            message ??= "An error occurred";
            return $"{message} ({ConvertResultToText(result)})";
        }

        /// <summary>
        /// Converts a <see cref="VkResult"/> to a human-readable string.
        /// </summary>
        public static string ConvertResultToText(VkResult result)
        {
            return result switch
            {
                VK_SUCCESS => nameof(VK_SUCCESS),
                VK_NOT_READY => nameof(VK_NOT_READY),
                VK_TIMEOUT => nameof(VK_TIMEOUT),
                VK_EVENT_SET => nameof(VK_EVENT_SET),
                VK_EVENT_RESET => nameof(VK_EVENT_RESET),
                VK_INCOMPLETE => nameof(VK_INCOMPLETE),
                VK_ERROR_OUT_OF_HOST_MEMORY => nameof(VK_ERROR_OUT_OF_HOST_MEMORY),
                VK_ERROR_OUT_OF_DEVICE_MEMORY => nameof(VK_ERROR_OUT_OF_DEVICE_MEMORY),
                VK_ERROR_INITIALIZATION_FAILED => nameof(VK_ERROR_INITIALIZATION_FAILED),
                VK_ERROR_DEVICE_LOST => nameof(VK_ERROR_DEVICE_LOST),
                VK_ERROR_MEMORY_MAP_FAILED => nameof(VK_ERROR_MEMORY_MAP_FAILED),
                VK_ERROR_LAYER_NOT_PRESENT => nameof(VK_ERROR_LAYER_NOT_PRESENT),
                VK_ERROR_EXTENSION_NOT_PRESENT => nameof(VK_ERROR_EXTENSION_NOT_PRESENT),
                VK_ERROR_FEATURE_NOT_PRESENT => nameof(VK_ERROR_FEATURE_NOT_PRESENT),
                VK_ERROR_INCOMPATIBLE_DRIVER => nameof(VK_ERROR_INCOMPATIBLE_DRIVER),
                VK_ERROR_TOO_MANY_OBJECTS => nameof(VK_ERROR_TOO_MANY_OBJECTS),
                VK_ERROR_FORMAT_NOT_SUPPORTED => nameof(VK_ERROR_FORMAT_NOT_SUPPORTED),
                VK_ERROR_FRAGMENTED_POOL => nameof(VK_ERROR_FRAGMENTED_POOL),
                VK_ERROR_UNKNOWN => nameof(VK_ERROR_UNKNOWN),

                VK_ERROR_OUT_OF_POOL_MEMORY => nameof(VK_ERROR_OUT_OF_POOL_MEMORY),
                VK_ERROR_INVALID_EXTERNAL_HANDLE => nameof(VK_ERROR_INVALID_EXTERNAL_HANDLE),
                VK_ERROR_FRAGMENTATION => nameof(VK_ERROR_FRAGMENTATION),
                VK_ERROR_INVALID_OPAQUE_CAPTURE_ADDRESS => nameof(VK_ERROR_INVALID_OPAQUE_CAPTURE_ADDRESS),
                VK_PIPELINE_COMPILE_REQUIRED => nameof(VK_PIPELINE_COMPILE_REQUIRED),

                VK_ERROR_SURFACE_LOST_KHR => nameof(VK_ERROR_SURFACE_LOST_KHR),
                VK_ERROR_NATIVE_WINDOW_IN_USE_KHR => nameof(VK_ERROR_NATIVE_WINDOW_IN_USE_KHR),
                VK_SUBOPTIMAL_KHR => nameof(VK_SUBOPTIMAL_KHR),
                VK_ERROR_OUT_OF_DATE_KHR => nameof(VK_ERROR_OUT_OF_DATE_KHR),
                VK_ERROR_INCOMPATIBLE_DISPLAY_KHR => nameof(VK_ERROR_INCOMPATIBLE_DISPLAY_KHR),
                VK_ERROR_VALIDATION_FAILED_EXT => nameof(VK_ERROR_VALIDATION_FAILED_EXT),
                VK_ERROR_INVALID_SHADER_NV => nameof(VK_ERROR_INVALID_SHADER_NV),
                VK_ERROR_IMAGE_USAGE_NOT_SUPPORTED_KHR => nameof(VK_ERROR_IMAGE_USAGE_NOT_SUPPORTED_KHR),
                VK_ERROR_VIDEO_PICTURE_LAYOUT_NOT_SUPPORTED_KHR => nameof(VK_ERROR_VIDEO_PICTURE_LAYOUT_NOT_SUPPORTED_KHR),
                VK_ERROR_VIDEO_PROFILE_OPERATION_NOT_SUPPORTED_KHR => nameof(VK_ERROR_VIDEO_PROFILE_OPERATION_NOT_SUPPORTED_KHR),
                VK_ERROR_VIDEO_PROFILE_FORMAT_NOT_SUPPORTED_KHR => nameof(VK_ERROR_VIDEO_PROFILE_FORMAT_NOT_SUPPORTED_KHR),
                VK_ERROR_VIDEO_PROFILE_CODEC_NOT_SUPPORTED_KHR => nameof(VK_ERROR_VIDEO_PROFILE_CODEC_NOT_SUPPORTED_KHR),
                VK_ERROR_VIDEO_STD_VERSION_NOT_SUPPORTED_KHR => nameof(VK_ERROR_VIDEO_STD_VERSION_NOT_SUPPORTED_KHR),
                VK_ERROR_INVALID_DRM_FORMAT_MODIFIER_PLANE_LAYOUT_EXT => nameof(VK_ERROR_INVALID_DRM_FORMAT_MODIFIER_PLANE_LAYOUT_EXT),

                VK_ERROR_NOT_PERMITTED_KHR => nameof(VK_ERROR_NOT_PERMITTED_KHR),

                VK_ERROR_FULL_SCREEN_EXCLUSIVE_MODE_LOST_EXT => nameof(VK_ERROR_FULL_SCREEN_EXCLUSIVE_MODE_LOST_EXT),
                VK_THREAD_IDLE_KHR => nameof(VK_THREAD_IDLE_KHR),
                VK_THREAD_DONE_KHR => nameof(VK_THREAD_DONE_KHR),
                VK_OPERATION_DEFERRED_KHR => nameof(VK_OPERATION_DEFERRED_KHR),
                VK_OPERATION_NOT_DEFERRED_KHR => nameof(VK_OPERATION_NOT_DEFERRED_KHR),
                VK_ERROR_INVALID_VIDEO_STD_PARAMETERS_KHR => nameof(VK_ERROR_INVALID_VIDEO_STD_PARAMETERS_KHR),
                VK_ERROR_COMPRESSION_EXHAUSTED_EXT => nameof(VK_ERROR_COMPRESSION_EXHAUSTED_EXT),
                VK_ERROR_INCOMPATIBLE_SHADER_BINARY_EXT => nameof(VK_ERROR_INCOMPATIBLE_SHADER_BINARY_EXT),

                //VK_ERROR_OUT_OF_POOL_MEMORY_KHR => nameof(VK_ERROR_OUT_OF_POOL_MEMORY_KHR),
                //VK_ERROR_INVALID_EXTERNAL_HANDLE_KHR => nameof(VK_ERROR_INVALID_EXTERNAL_HANDLE_KHR),
                //VK_ERROR_FRAGMENTATION_EXT => nameof(VK_ERROR_FRAGMENTATION_EXT),
                //VK_ERROR_NOT_PERMITTED_EXT => nameof(VK_ERROR_NOT_PERMITTED_EXT),
                //VK_ERROR_INVALID_DEVICE_ADDRESS_EXT => nameof(VK_ERROR_INVALID_DEVICE_ADDRESS_EXT),
                //VK_ERROR_INVALID_OPAQUE_CAPTURE_ADDRESS_KHR => nameof(VK_ERROR_INVALID_OPAQUE_CAPTURE_ADDRESS_KHR),
                //VK_PIPELINE_COMPILE_REQUIRED_EXT => nameof(VK_PIPELINE_COMPILE_REQUIRED_EXT),
                //VK_ERROR_PIPELINE_COMPILE_REQUIRED_EXT => nameof(VK_ERROR_PIPELINE_COMPILE_REQUIRED_EXT),
                _ => $"0x{(uint)result:X8}",
            };
        }


    }


}