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
}