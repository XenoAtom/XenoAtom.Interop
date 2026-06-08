/*
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

namespace XenoAtom.Interop.CodeGen.wlroots;

/// <summary>
/// Generator for wlroots API.
/// </summary>
internal partial class WlrootsGenerator(LibDescriptor descriptor) : GeneratorBase(descriptor)
{
    protected override async Task<CSharpCompilation?> Generate()
    {
        var sysIncludes = Apk.GetSysIncludeDirectory("main");
        var mainInclude = Apk.GetIncludeDirectory("main");
        var pixmanInclude = Path.Combine(mainInclude, "pixman-1");
        var communityInclude = Apk.GetIncludeDirectory("community");

        var csOptions = new CSharpConverterOptions()
        {
            DefaultClassLib = "wlroots",
            DefaultNamespace = "XenoAtom.Interop",
            DefaultOutputFilePath = "/wlroots_library.generated.cs",
            DefaultDllImportNameAndArguments = "LibraryName",
            TargetVendor = "linux",
            TargetSystem = "gnu",
            DefaultCallingConvention = CallingConvention.Cdecl,
            ParserKind = CppParserKind.C,
            Defines =
            {
                "WLR_USE_UNSTABLE"
            },
            AdditionalArguments =
            {
                //"-nostdinc",
                "-std=c99"
            },
            SystemIncludeFolders =
            {
                sysIncludes,
            },
            IncludeFolders =
            {
                mainInclude,
                pixmanInclude,
                communityInclude
            },
            PreHeaderText = @"",

            DispatchOutputPerInclude = true,
            DisableRuntimeMarshalling = true,
            AllowMarshalForString = false,
            EnableAutoByRef = false,
            MapCLongToIntPtr = true,

            MappingRules =
            {
            }
        };

        var files = new List<string>()
        {
            Path.Combine(mainInclude, "wayland-server-core.h"),
            Path.Combine(communityInclude, "wlr/backend.h"),
            Path.Combine(communityInclude, "wlr/render/allocator.h"),
            Path.Combine(communityInclude, "wlr/render/wlr_renderer.h"),
            Path.Combine(communityInclude, "wlr/types/wlr_compositor.h"),
            Path.Combine(communityInclude, "wlr/types/wlr_output.h"),
            Path.Combine(communityInclude, "wlr/types/wlr_xdg_shell.h"),
            Path.Combine(communityInclude, "wlr/util/log.h"),
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
*/