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

                e => e.MapMacroToConst("DRM_FORMAT_(?!MOD_|RESERVED).*", "unsigned int"),
                e => e.MapMacroToConst("(I915|DRM|AFBC)_FORMAT_MOD_.*", "unsigned long long"),
                e => e.MapMacroToConst("VIVANTE_MOD_.*", "unsigned long long"),
                e => e.MapMacroToConst("DRM_MODE_.*", "int"),
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