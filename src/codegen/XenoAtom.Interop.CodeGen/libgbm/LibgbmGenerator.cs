using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CppAst;
using CppAst.CodeGen.CSharp;
using XenoAtom.Interop.CodeGen.musl;

namespace XenoAtom.Interop.CodeGen.libgbm;

/// <summary>
/// Generator for Mesa libgbm API.
/// </summary>
internal partial class LibgbmGenerator(LibDescriptor descriptor) : GeneratorBase(descriptor)
{
    protected override async Task<CSharpCompilation?> Generate()
    {
        //var sysIncludes = Apk.GetSysIncludeDirectory("main");
        //var mainInclude = Apk.GetIncludeDirectory("main");

        var sysFolders = Apk.GetPackageIncludeDirectoryAndDependencies(Descriptor.ApkDeps[0], out var gbmIncludeFolder);

        var sysDirectory = Apk.GetPackageIncludeDirectory("musl-dev");
        sysFolders.Insert(0, sysDirectory);
        var linuxHeaders = Apk.GetPackageIncludeDirectory("linux-headers");
        sysFolders.Insert(1, linuxHeaders);

        var csOptions = new CSharpConverterOptions()
        {
            DefaultClassLib = "libgbm",
            DefaultNamespace = "XenoAtom.Interop",
            DefaultOutputFilePath = "/gbm_library.generated.cs",
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
                gbmIncludeFolder,
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
                e => e.MapMacroToConst("GBM_FORMAT_.*", "unsigned int"),
                e => e.MapMacroToConst("GBM_BO_.*", "unsigned int"),
                e => e.MapMacroToConst("GBM_MAX_PLANES", "int"),
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
        var files = new List<string>()
        {
            Path.Combine(gbmIncludeFolder, "gbm.h"),
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
        if (csFunction.Name.StartsWith("gbm") && csFunction.Name.Contains("name"))
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