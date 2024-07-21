// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ClangSharp;
using CppAst;
using CppAst.CodeGen.CSharp;

namespace XenoAtom.Interop.CodeGen.libkmod
{
    /// <summary>
    /// Generator for Mesa libkmod API.
    /// </summary>
    internal partial class LibkmodGenerator(LibDescriptor descriptor) : GeneratorBase(descriptor)
    {
        protected override async Task<CSharpCompilation?> Generate()
        {
            //var sysIncludes = Apk.GetSysIncludeDirectory("main");
            //var mainInclude = Apk.GetIncludeDirectory("main");

            var sysFolders = Apk.GetPackageIncludeDirectoryAndDependencies(Descriptor.ApkDeps[0], out var libkmodIncludeFolder);

            var sysDirectory = Apk.GetPackageIncludeDirectory("musl-dev");
            sysFolders.Insert(0, sysDirectory);
            //var linuxHeaders = Apk.GetPackageIncludeDirectory("linux-headers");
            //sysFolders.Insert(1, linuxHeaders);

            var csOptions = new CSharpConverterOptions()
            {
                DefaultClassLib = "libkmod",
                DefaultNamespace = "XenoAtom.Interop",
                DefaultOutputFilePath = "/libkmod_library.generated.cs",
                DefaultDllImportNameAndArguments = "LibraryName",
                TargetVendor = "linux",
                TargetSystem = "gnu",
                DefaultCallingConvention = CallingConvention.Cdecl,
                Defines =
                {
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
                    libkmodIncludeFolder,
                },

                PreHeaderText = @"",

                DispatchOutputPerInclude = true,
                DisableRuntimeMarshalling = true,
                AllowMarshalForString = false,
                EnableAutoByRef = false,
                MapCLongToIntPtr = true,

                MappingRules =
                {
                    e => e.Map<CppFunction>("kmod_validate_resources").Type("kmod_resources"),
                    e => e.Map<CppParameter>("kmod_module_remove_module::flags").Type("kmod_remove"),
                    e => e.Map<CppParameter>("kmod_module_insert_module::flags").Type("kmod_insert"),
                    e => e.Map<CppParameter>("kmod_module_probe_insert_module::flags").Type("kmod_probe"),
                    e => e.Map<CppEnum>("kmod_symbol_bind").Discard(), // Not used in public API but only internally
                    e => e.Map<CppEnumItem>("_KMOD_MODULE_PAD").Discard(),
                    e => e.Map<CppParameter>("kmod_set_log_fn::log_fn").Type("intptr_t"),
                    //e => e.Map<CppFunction>("kmod_set_log_fn").Discard(),
                    e => e.Map<CppFunction>("vsyslog").Discard(), // discard as it has a va_list
                    e => e.Map<CppTypedef>("va_list").Discard(), // discard va_list
                    e => e.Map<CppClass>("__va_list_tag").Discard(), // discard __va_list_tag
                }
            };

            foreach (var folder in sysFolders)
            {
                csOptions.SystemIncludeFolders.Add(folder);
            }
            var files = new List<string>()
            {
                Path.Combine(libkmodIncludeFolder, "libkmod.h"),
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

            // Fix enums to flags
            foreach (var csEnum in csCompilation.AllEnums)
            {
                switch (csEnum.Name)
                {
                    case "kmod_remove":
                    case "kmod_insert":
                    case "kmod_probe":
                    case "kmod_filter":
                        csEnum.IsFlags = true;
                        break;
                    case "kmod_module_initstate":
                        csEnum.IsFlags = false;
                        break;
                }
            }
            
            foreach(var csMethod in csCompilation.AllFunctions)
            {
                ProcessOutputParameter(csMethod);
                ProcessBoolArgumentsFunction(csMethod);
                ProcessConstStringArguments(csMethod);
            }

            return csCompilation;
        }

        private void ProcessOutputParameter(CSharpMethod csFunction)
        {
            if (csFunction.Name == "kmod_new") return;

            foreach (var csParameter in csFunction.Parameters)
            {
                if (csParameter.ParameterType is CSharpPointerType csPointerType)
                {
                    if (csPointerType.ElementType is CSharpStruct)
                    {
                        csParameter.ParameterType = new CSharpRefType(CSharpRefKind.Out, csPointerType.ElementType);
                    }
                }
            }
        }
    }
}