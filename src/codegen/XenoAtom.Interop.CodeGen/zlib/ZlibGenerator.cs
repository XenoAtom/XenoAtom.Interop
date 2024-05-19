// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CppAst;
using CppAst.CodeGen.Common;
using CppAst.CodeGen.CSharp;
using Zio.FileSystems;

namespace XenoAtom.Interop.CodeGen.zlib;

internal partial class ZlibGenerator(LibDescriptor descriptor) : GeneratorBase(descriptor)
{
    protected override async Task<CSharpCompilation?> Generate()
    {
        var sysIncludes = Apk.GetSysIncludeDirectory("main");
        var mainInclude = Apk.GetIncludeDirectory("main");
        List<string> srcFolders =
        [
            mainInclude,
        ];

        var csOptions = new CSharpConverterOptions()
        {
            DefaultClassLib = "zlib",
            DefaultNamespace = "XenoAtom.Interop",
            DefaultOutputFilePath = "/zlib_library.generated.cs",
            DefaultDllImportNameAndArguments = "LibraryName",
            TargetVendor = "linux",
            TargetSystem = "gnu",
            Defines =
            {

            },
            PreHeaderText = @"",
            TypedefCodeGenKind = CppTypedefCodeGenKind.NoWrap,

            DispatchOutputPerInclude = true,
            DisableRuntimeMarshalling = true,
            AllowMarshalForString = false,

            MappingRules =
            {
                e => e.MapMacroToConst("MAX_WBITS", "int"),

                e => e.MapMacroToEnum("(Z_NO_FLUSH|Z_PARTIAL_FLUSH|Z_SYNC_FLUSH|Z_FULL_FLUSH|Z_FINISH|Z_BLOCK|Z_TREES)", "z_flush_t"),
                e => e.MapMacroToEnum("(Z_OK|Z_STREAM_END|Z_NEED_DICT|Z_ERRNO|Z_STREAM_ERROR|Z_DATA_ERROR|Z_MEM_ERROR|Z_BUF_ERROR|Z_VERSION_ERROR)", "z_result_t"),
                e => e.MapMacroToEnum("(Z_FILTERED|Z_HUFFMAN_ONLY|Z_RLE|Z_FIXED|Z_DEFAULT_STRATEGY)", "z_strategy_t"),
                e => e.MapMacroToEnum("(Z_BINARY|Z_TEXT|Z_ASCII|Z_UNKNOWN|Z_DEFLATED)", "z_datatype_t"),

                e => e.MapMacroToConst("Z_NO_COMPRESSION", "int"),
                e => e.MapMacroToConst("Z_BEST_SPEED", "int"),
                e => e.MapMacroToConst("Z_BEST_COMPRESSION", "int"),
                e => e.MapMacroToConst("Z_DEFAULT_COMPRESSION", "int"),
                
                e => e.MapMacroToConst("Z_NULL", "ssize_t"),
                e => e.MapMacroToConst("ZLIB_VERSION", "char*"),
                e => e.MapMacroToConst("ZLIB_VER[N_].*", "int"),

                e => e.Map<CppParameter>("(inflate|deflate)::flush").Type("z_flush_t"),

                e => e.Map<CppFunction>("(deflate|deflateEnd|inflate|inflateEnd|deflateSetDictionary|deflateGetDictionary|deflateCopy|deflateReset|deflateParams|deflateTune|deflatePending|deflatePrime|deflateSetHeader|inflateSetDictionary|inflateGetDictionary|inflateSync|inflateCopy|inflateReset|inflateReset2|inflatePrime|inflateGetHeader|inflateBack|inflateBackEnd|compress|compress2|uncompress|uncompress2|deflateInit_|inflateInit_|deflateInit2_|inflateInit2_|inflateBackInit_)").Type("z_result_t"),

                e => e.Map<CppParameter>("(adler32|adler32_z)::adler").Type("unsigned int"),
                e => e.Map<CppParameter>("(crc32|crc32_z)::crc").Type("unsigned int"),
                e => e.Map<CppFunction>("(adler32|adler32_z|crc32|crc32_z|crc32_combine_op|zlibCompileFlags|compressBound|adler32_combine|crc32_combine|crc32_combine_gen)").Type("unsigned int"),
                e => e.Map<CppParameter>("(compressBound|crc32_combine_op)::.*").Type("unsigned int"),
                e => e.Map<CppParameter>("(adler32_combine|crc32_combine)::arg[01]").Type("unsigned int"),
                e => e.Map<CppParameter>("(adler32_combine|crc32_combine)::arg2").Type("int"),
                e => e.Map<CppParameter>("crc32_combine_gen::arg0").Type("int"),

                e => e.Map<CppClass>("z_stream_s").Name("z_stream"),
                e => e.Map<CppClass>("gz_header_s").Name("gz_header"),

                e => e.Map<CppField>("z_stream::data_type").Type("z_datatype_t"),

                e => e.Map<CppFunction>("^gz.*").Discard(),
                e => e.Map<CppClass>("gzFile_s").Discard(),
                e => e.Map<CppFunction>("(zError|inflateSyncPoint|get_crc_table|inflateUndermine|inflateValidate|inflateCodesUsed|inflateResetKeep|deflateResetKeep)").Discard(),
            },
        };

        foreach (var srcFolder in srcFolders)
        {
            csOptions.IncludeFolders.Add(srcFolder);
        }

        csOptions.SystemIncludeFolders.Add(sysIncludes);
        csOptions.AdditionalArguments.Add("-nostdinc");

        var files = new List<string>()
        {
            Path.Combine(mainInclude, "zlib.h"),
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

        // Transform all pointers to z_stream_s and gzFile_s to ref
        foreach (var function in csCompilation.AllFunctions)
        {
            // Remove comments as they are not properly setup in zlib (they are declared after the function and sometimes commented)
            function.Comment = null;

            foreach (var param in function.Parameters)
            {
                if (param.ParameterType is CSharpPointerType pointerType &&
                    pointerType.ElementType is CSharpStruct csStruct &&
                    (csStruct.Name == "z_stream" || csStruct.Name == "gz_header"))
                {
                    param.ParameterType = new CSharpRefType(CSharpRefKind.Ref, csStruct);
                }
            }

            ProcessStringArgs(function);
        }

        return csCompilation;
    }

    private void ProcessStringArgs(CSharpMethod csMethod)
    {
        // If we have a potential marshalling for return/parameter string, we will duplicate the method with string marshalling
        CSharpMethod? newManagedMethod = null;
        for (var i = 0; i < csMethod.Parameters.Count; i++)
        {
            var param = csMethod.Parameters[i];
            var cppType = (CppType)param.ParameterType!.CppElement!;
            if (cppType.TryGetElementTypeFromPointerToConst(out var elementType))
            {
                if (elementType is CppPrimitiveType { Kind: CppPrimitiveKind.Char })
                {
                    newManagedMethod ??= csMethod.Clone();
                    newManagedMethod.Parameters[i].ParameterType = new CSharpTypeWithAttributes(new CSharpFreeType("ReadOnlySpan<char>"))
                    {
                        Attributes = { new CSharpMarshalUsingAttribute("typeof(Utf8CustomMarshaller)") }
                    };
                }
            }
        }

        if (((CppType)csMethod.ReturnType!.CppElement!).TryGetElementTypeFromPointerToConst(out var returnElementType))
        {
            if (returnElementType is CppPrimitiveType { Kind: CppPrimitiveKind.Char })
            {
                newManagedMethod ??= csMethod.Clone();
                newManagedMethod.Name = $"{newManagedMethod.Name}_string";
                newManagedMethod.ReturnType = new CSharpTypeWithAttributes(CSharpPrimitiveType.String())
                {
                    Attributes = { new CSharpMarshalUsingAttribute("typeof(Utf8CustomMarshaller)") { Scope = CSharpAttributeScope.Return } }
                };
            }
        }

        if (newManagedMethod != null)
        {
            var parent = ((ICSharpContainer) csMethod.Parent!);
            var indexOf = parent.Members.IndexOf(csMethod);
            parent.Members.Insert(indexOf + 1, newManagedMethod);
        }
    }
}