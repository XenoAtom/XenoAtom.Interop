// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CppAst;
using CppAst.CodeGen.CSharp;
using XenoAtom.Interop.CodeGen.musl;

namespace XenoAtom.Interop.CodeGen.libdrm;

/// <summary>
/// Generator for libdrm API.
/// </summary>
internal partial class LibdrmGenerator(LibDescriptor descriptor) : GeneratorBase(descriptor)
{
    protected override async Task<CSharpCompilation?> Generate()
    {
        //var sysIncludes = Apk.GetSysIncludeDirectory("main");
        //var mainInclude = Apk.GetIncludeDirectory("main");

        var sysFolders = Apk.GetPackageIncludeDirectoryAndDependencies(Descriptor.ApkDeps[0], out var libdrmIncludeFolder);

        var sysDirectory = Apk.GetPackageIncludeDirectory("musl-dev");
        sysFolders.Insert(0, sysDirectory);
        var linuxHeaders = Apk.GetPackageIncludeDirectory("linux-headers");
        sysFolders.Insert(1, linuxHeaders);

        var csOptions = new CSharpConverterOptions()
        {
            DefaultClassLib = "libdrm",
            DefaultNamespace = "XenoAtom.Interop",
            DefaultOutputFilePath = "/libdrm_library.generated.cs",
            DefaultDllImportNameAndArguments = "LibraryName",
            TargetVendor = "linux",
            TargetSystem = "gnu",
            DefaultCallingConvention = CallingConvention.Cdecl,
            Defines =
            {
                "__linux__",
                // Defines for musl to override _Addr, _Int64, _Reg in bits/alltypes.h
                // See ApkIncludeHelper.ExtractFiles
                "XENO_ATOM_INTEROP",
            },
            AdditionalArguments =
            {
                //"-nostdinc",
                //"-std=c99"
            },
            SystemIncludeFolders =
            {
            },
            IncludeFolders =
            {
                libdrmIncludeFolder,
            },

            PreHeaderText = @"
#include <fcntl.h>
",

            DispatchOutputPerInclude = true,
            DisableRuntimeMarshalling = true,
            AllowMarshalForString = false,
            EnableAutoByRef = false,
            MapCLongToIntPtr = true,

            MappingRules =
            {
                e => e.MapMacroToConst("DRM_DIR_NAME", "char*"),
                e => e.MapMacroToConst("DRM_PRIMARY_MINOR_NAME", "char*"),
                e => e.MapMacroToConst("DRM_DEV_DIRMODE", "int"),
                e => e.MapMacroToConst("DRM_ERR_.*", "int"),
                e => e.MapMacroToConst("DRM_LOCK_.*", "unsigned int"),
                e => e.MapMacroToConst("DRM_EVENT_CONTEXT_VERSION", "int"),
                e => e.MapMacroToConst("DRM_BUS_.*", "int"),
                e => e.MapMacroToConst("DRM_DEVICE_GET_PCI_REVISION", "int"),
                e => e.MapMacroToConst("DRM_PLANE_TYPE_.*", "int"),
                e => e.MapMacroToConst("DRM_NODE_(?!NAME_MAX).*", "int"),
                e => e.MapMacroToConst("DRM_CAP_.*", "int"),
                e => e.MapMacroToConst("DRM_CLIENT_CAP_.*", "int"),

                e => e.MapMacroToConst("DRM_IOCTL_.*", "uintptr_t"),

                e => e.MapMacroToConst("DRM_FORMAT_(?!MOD_|RESERVED).*", "unsigned int"),
                e => e.MapMacroToConst("(I915|DRM|AFBC|AFRC)_FORMAT_MOD_.*", "unsigned long long"),
                e => e.MapMacroToConst("AMLOGIC_FBC_.*", "int"),
                e => e.MapMacroToConst("VIVANTE_MOD_.*", "unsigned long long"),

                e => e.MapMacroToConst("DRM_MODE_FEATURE_.*", "int"),

                e => e.MapMacroToEnum("DRM_MODE_TYPE_.*", "drm_mode_type", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_FLAG_.*", "drm_mode_flag", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_PICTURE_ASPECT_.*", "drm_mode_picture_aspect", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_CONTENT_TYPE_.*", "drm_mode_content_type", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_DPMS_.*", "drm_mode_dpms", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_SCALE_.*", "drm_mode_scale", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_DITHERING_.*", "drm_mode_dithering", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_DIRTY_.*", "drm_mode_dirty", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_ROTATE_.*", "drm_mode_rotate", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_REFLECT_.*", "drm_mode_reflect", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_CONTENT_PROTECTION_.*", "drm_mode_content_protection", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_PRESENT_.*", "drm_mode_present", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_ENCODER_.*", "drm_mode_encoder", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_CONNECTOR_.*", "drm_mode_connector", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_PROP_.*", "drm_mode_prop", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_OBJECT_.*", "drm_mode_object", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_FB_(?!DIRTY_).*", "drm_mode_fb", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_FB_DIRTY_.*", "drm_mode_fb_dirty", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_CURSOR_.*", "drm_mode_cursor_flags", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_PAGE_FLIP_.*", "drm_mode_page_flip", integerType: "unsigned int"),
                e => e.MapMacroToEnum("DRM_MODE_ATOMIC_.*", "drm_mode_atomic_flags", integerType: "unsigned int"),

                e => e.Map<CppField>("drm_mode_modeinfo::flags").Type("drm_mode_flag"),
                e => e.Map<CppField>("drm_mode_modeinfo::type").Type("drm_mode_type"),

                e => e.Map<CppField>("_drmModeModeInfo::flags").Type("drm_mode_flag"),
                e => e.Map<CppField>("_drmModeModeInfo::type").Type("drm_mode_type"),
                e => e.Map<CppField>("_drmModePropertyRes::flags").Type("drm_mode_prop"),
                e => e.Map<CppField>("_drmModeEncoder::encoder_type").Type("drm_mode_encoder"),
                e => e.Map<CppField>("_drmModeConnector::connector_type").Type("drm_mode_connector"),
                
                e => e.MapMacroToConst("AMD_FMT_MOD_.*", "int"),
                e => e.Map<CppFunction>("drmModeFormatModifierBlobIterNext").Type("unsigned char"),
                e => e.Map<CppTypedef>("drmSizePtr").Discard(),
                // We handle this manually
                e => e.Map<CppTypedef>("drmServerInfo.*").Discard(),
                e => e.Map<CppClass>("_drmServerInfo").Discard(),
                e => e.Map<CppFunction>("drmSetServerInfo").Discard(),
            }
        };

        //  Add default C types from musl and kernel
        var typeDefConverter = MuslGenerator.AddDefaultMuslAndKernelCTypes(csOptions);
        typeDefConverter.StandardCTypes.Add("__kernel_ulong_t", () => CSharpPrimitiveType.UIntPtr());
        typeDefConverter.StandardCTypes.Add("__kernel_size_t", () => CSharpPrimitiveType.UIntPtr());
        typeDefConverter.StandardCTypes.Add("gid_t", () => CSharpPrimitiveType.UInt());
        typeDefConverter.StandardCTypes.Add("mode_t", () => CSharpPrimitiveType.UInt());
        typeDefConverter.StandardCTypes.Add("dev_t", () => CSharpPrimitiveType.UIntPtr());

        csOptions.Plugins.Add(new CustomTypedefConverterPlugin());

        foreach (var folder in sysFolders)
        {
            csOptions.SystemIncludeFolders.Add(folder);
        }

        csOptions.IncludeFolders.Add(Path.Combine(libdrmIncludeFolder, "libdrm"));

        var files = new List<string>()
        {
            Path.Combine(libdrmIncludeFolder, "libdrm/drm.h"),
            Path.Combine(libdrmIncludeFolder, "libdrm/drm_mode.h"),
            Path.Combine(libdrmIncludeFolder, "libdrm/drm_fourcc.h"),
            Path.Combine(libdrmIncludeFolder, "libdrm/drm_sarea.h"),
            Path.Combine(libdrmIncludeFolder, "xf86drm.h"),
            Path.Combine(libdrmIncludeFolder, "xf86drmMode.h"),
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

        foreach (var csFunction in csCompilation.AllFunctions)
        {
            ProcessFunctionStrings(csFunction);
        }

        return csCompilation;
    }

    private void ProcessFunctionStrings(CSharpMethod csFunction)
    {
        // We will provide manual overloads
        if (csFunction.Name.StartsWith("drm") && csFunction.Name.Contains("Name"))
        {
            csFunction.Name = $"{csFunction.Name}_";
        }
    }


    private class CustomTypedefConverterPlugin : ICSharpConverterPlugin
    {
        public void Register(CSharpConverter converter, CSharpConverterPipeline pipeline)
        {
            pipeline.TypedefConverters.Add(ProcessTypeDef);
        }

        private static CSharpElement? ProcessTypeDef(CSharpConverter converter, CppTypedef cpptypedef, CSharpElement context)
        {
            var elementType = cpptypedef.ElementType;
            var cSharpType = converter.GetCSharpType(elementType, context, false);
            if (cpptypedef.Name.EndsWith("_t") && (cSharpType is CSharpStruct || cSharpType is CSharpEnum))
            {
                return cSharpType;
            }
            else if (cSharpType is CSharpPointerType csPointerType && csPointerType.ElementType is not CSharpPrimitiveType)
            {
                return cSharpType;
            }
            else if (cSharpType is CSharpStruct csStruct && csStruct.Name.StartsWith('_')  && !cpptypedef.Name.StartsWith('_'))
            {
                var typedefName = converter.GetCSharpName(cpptypedef, context);
                csStruct.Name = typedefName;
                return cSharpType;
            }
            return null;
        }
    }
}