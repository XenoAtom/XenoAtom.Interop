// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CppAst;
using CppAst.CodeGen.Common;
using CppAst.CodeGen.CSharp;
using Zio.FileSystems;

namespace XenoAtom.Interop.CodeGen.vulkan;

internal partial class VulkanGenerator(LibDescriptor descriptor) : GeneratorBase(descriptor)
{
    public override async Task Initialize(ApkManager apkHelper)
    {
        await base.Initialize(apkHelper);

        //await Apk.ExtractFolders("vulkan-headers", "vulkan-registry", "usr/share/vulkan/registry/");
    }

    protected override async Task<CSharpCompilation?> Generate()
    {
        var sysIncludes = Apk.GetSysIncludeDirectory("main");
        var mainInclude = Apk.GetIncludeDirectory("main");
        var vulkanSysIncludes = Path.Combine(AppContext.BaseDirectory, "vulkan_sys_includes");

        const string CommonVkExt = "(AMD|AMDX|ARM|EXT|GOOGLE|HUAWEI|IMG|INTEL|KHR|LUNARG|NV|NVX|QCOM|SEC|VALVE)";
        const string CommonVkUint = "(MAX|QUEUE|REMAINING|SHADER|VERSION_1|TRUE|FALSE|UUID|ATTACHMENT|LUID)";

        var csOptions = new CSharpConverterOptions()
        {
            DefaultClassLib = "vulkan",
            DefaultNamespace = "XenoAtom.Interop",
            DefaultOutputFilePath = "/vulkan_library.generated.cs",
            DefaultDllImportNameAndArguments = "LibraryName",
            TargetVendor = "linux",
            TargetSystem = "gnu",
            DefaultCallingConvention = CallingConvention.StdCall, // For Vulkan, on Windows it's stdcall, otherwise it's cdecl
            Defines =
            {
                "VK_USE_PLATFORM_ANDROID_KHR",
                "VK_USE_PLATFORM_IOS_MVK",
                "VK_USE_PLATFORM_MACOS_MVK",
                "VK_USE_PLATFORM_METAL_EXT",
                "VK_USE_PLATFORM_VI_NN",
                "VK_USE_PLATFORM_WAYLAND_KHR",
                "VK_USE_PLATFORM_WIN32_KHR",
                "VK_USE_PLATFORM_XCB_KHR",
                "VK_USE_PLATFORM_XLIB_KHR",
                "VK_USE_PLATFORM_DIRECTFB_EXT",
                "VK_USE_PLATFORM_XLIB_XRANDR_EXT",
                "VK_USE_PLATFORM_GGP",
                "VK_USE_PLATFORM_SCREEN_QNX",
                "VK_ENABLE_BETA_EXTENSIONS"
            },
            AdditionalArguments =
            {
                "-nostdinc"
            },
            SystemIncludeFolders =
            {
                sysIncludes,
                vulkanSysIncludes
            },
            IncludeFolders =
            {
                mainInclude
            },
            PreHeaderText = @"",

            DispatchOutputPerInclude = true,
            DisableRuntimeMarshalling = true,
            AllowMarshalForString = false,
            MapCLongToIntPtr = true,

            MappingRules =
            {
                e => e.MapMacroToConst(@"VK_API_VERSION_1_\d", "unsigned int"),
                e => e.MapMacroToConst($@"VK_{CommonVkExt}_\w+(?<!_NAME)", "int"),
                e => e.MapMacroToConst($@"VK_{CommonVkExt}_\w+_NAME", "char*"),
                e => e.MapMacroToConst($@"VK_{CommonVkUint}\w*", "unsigned int"),
                e => e.MapMacroToConst($@"VK_LOD_CLAMP_NONE", "float"),
            },
        };

        var files = new List<string>()
        {
            Path.Combine(mainInclude, "vulkan/vulkan.h"),
        };

        var csCompilation = CSharpConverter.Convert(files, csOptions);

        {
            foreach (var message in csCompilation.Diagnostics.Messages)
            {
                Console.Error.WriteLine(message);
            }

            if (csCompilation.HasErrors)
            {
                Console.Error.WriteLine("Unexpected parsing errors");
                Environment.Exit(1);
            }
        }

 
        return csCompilation;
    }
}