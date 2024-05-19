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

namespace XenoAtom.Interop.CodeGen.sqlite;

internal partial class SqliteGenerator(LibDescriptor descriptor) : GeneratorBase(descriptor)
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
            DefaultClassLib = "sqlite",
            DefaultNamespace = "XenoAtom.Interop",
            DefaultOutputFilePath = "/sqlite_library.generated.cs",
            DefaultDllImportNameAndArguments = "LibraryName",
            TargetVendor = "linux",
            TargetSystem = "gnu",
            Defines =
            {

            },
            PreHeaderText = @"",

            DispatchOutputPerInclude = true,
            DisableRuntimeMarshalling = true,
            AllowMarshalForString = false,

            MappingRules =
            {
                e => e.MapMacroToConst("SQLITE_(?!EXTERN|API|CDECL|APICALL|STDCALL|CALLBACK|SYSAPI|DEPRECATED|EXPERIMENTAL|VERSION|VERSION_NUMBER|SOURCE_ID|STATIC|TRANSIENT)\\w+", "int"),
                e => e.MapMacroToConst("SQLITE_VERSION", "char*"),
                e => e.MapMacroToConst("SQLITE_SOURCE_ID", "char*"),
                e => e.MapMacroToConst("SQLITE_VERSION_NUMBER", "int"),
                e => e.Map<CppParameter>(".*::ppStmt").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_prepare.*::pzTail").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_open.*::ppDb").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_exec::errmsg").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_get_table::pazResult").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_get_table::pnRow").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_get_table::pnColumn").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_get_table::pzErrmsg").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_free_table::result").NoByRef(),
                e => e.Map<CppParameter>("sqlite3_create_filename::azParam").NoByRef(),
                e => e.Map<CppParameter>("sqlite3_table_column_metadata::pzDataType").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_table_column_metadata::pzCollSeq").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_table_column_metadata::pNotNull").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_table_column_metadata::pPrimaryKey").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_table_column_metadata::pAutoinc").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_load_extension::pzErrMsg").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_drop_modules::azKeep").NoByRef(),
                e => e.Map<CppParameter>("sqlite3_blob_open::ppBlob").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_vfs_register::.*").NoByRef(),
                e => e.Map<CppParameter>("sqlite3_vfs_unregister::.*").NoByRef(),
                e => e.Map<CppParameter>("sqlite3_keyword_name::arg1").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_keyword_name::arg2").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_status::pCurrent").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_status::pHighwater").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_status64::pCurrent").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_status64::pHighwater").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_db_status::pCur").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_db_status::pHiwtr").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_wal_checkpoint_v2::pnLog").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_wal_checkpoint_v2::pnCkpt").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_vtab_in_first::ppOut").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_vtab_in_next::ppOut").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_vtab_rhs_value::ppVal").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_snapshot_get::ppSnapshot").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("sqlite3_snapshot_open::pSnapshot").NoByRef(),
                e => e.Map<CppParameter>("sqlite3_snapshot_free::arg0").NoByRef(),
                e => e.Map<CppParameter>("sqlite3_snapshot_cmp::.*").NoByRef(),
                e => e.Map<CppParameter>("sqlite3_serialize::piSize").ByRef(CSharpRefKind.Out),
                e => e.Map<CppFunction>("sqlite3_.*printf").Discard(),
                e => e.Map<CppFunction>("sqlite3_str_.*appendf").Discard(),
            },
        };
        csOptions.Plugins.Insert(0, new CustomTypeConverter());
        
        foreach (var srcFolder in srcFolders)
        {
            csOptions.IncludeFolders.Add(srcFolder);
        }

        csOptions.SystemIncludeFolders.Add(sysIncludes);
        csOptions.AdditionalArguments.Add("-nostdinc");

        var files = new List<string>()
        {
            Path.Combine(mainInclude, "sqlite3.h"),
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
            //foreach (var param in function.Parameters)
            //{
            //    if (param.ParameterType is CSharpPointerType pointerType &&
            //        pointerType.ElementType is CSharpStruct csStruct &&
            //        (csStruct.Name == "z_stream_s" || csStruct.Name == "gzFile_s" || csStruct.Name == "gz_header_s"))
            //    {
            //        param.ParameterType = new CSharpRefType(CSharpRefKind.Ref, csStruct);
            //    }
            //}

            ProcessStringArgs(function);
        }

        return csCompilation;
    }

    protected override string? GetUrlDocumentationForCppFunction(CppFunction cppFunction)
    {
        // sqlite3_column_bytes16  => https://www.sqlite.org/c3ref/column_blob.html
        if (MapFunctionToUrlPart.TryGetValue(cppFunction.Name, out var urlPart))
        {
            return $"{Descriptor.Url}{urlPart}";
        }

        return null;
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
                else if (csMethod.Name.Contains("16") && elementType is CppPrimitiveType { Kind: CppPrimitiveKind.Void })
                {
                    newManagedMethod ??= csMethod.Clone();
                    newManagedMethod.Parameters[i].ParameterType = new CSharpTypeWithAttributes(CSharpPrimitiveType.String())
                    {
                        Attributes = { new CSharpMarshalAsAttribute(UnmanagedType.LPWStr) }
                    };
                }
            }
        }

        if (((CppType)csMethod.ReturnType!.CppElement!).TryGetElementTypeFromPointerToConst(out var returnElementType))
        {
            if (returnElementType is CppPrimitiveType { Kind: CppPrimitiveKind.Char })
            {
                newManagedMethod ??= csMethod.Clone();
                csMethod.Name = $"{csMethod.Name}_";
                newManagedMethod.ReturnType = new CSharpTypeWithAttributes(CSharpPrimitiveType.String())
                {
                    Attributes = { new CSharpMarshalUsingAttribute("typeof(Utf8CustomMarshaller)") { Scope = CSharpAttributeScope.Return } }
                };
            }
            else if (csMethod.Name.Contains("16") && returnElementType is CppPrimitiveType { Kind: CppPrimitiveKind.Void })
            {
                newManagedMethod ??= csMethod.Clone();
                csMethod.Name = $"{csMethod.Name}_";
                newManagedMethod.ReturnType = new CSharpTypeWithAttributes(CSharpPrimitiveType.String())
                {
                    Attributes = { new CSharpMarshalAsAttribute(UnmanagedType.LPWStr)
                    {
                        Scope = CSharpAttributeScope.Return
                    }},
                };
            }
        }

        if (newManagedMethod != null)
        {
            var parent = ((ICSharpContainer)csMethod.Parent!);
            var indexOf = parent.Members.IndexOf(csMethod);
            parent.Members.Insert(indexOf + 1, newManagedMethod);
        }
    }

    public class CustomTypeConverter : ICSharpConverterPlugin
    {
        private ConvertTypedefDelegate _defaultTypeDefConverter;
        
        /// <inheritdoc />
        public void Register(CSharpConverter converter, CSharpConverterPipeline pipeline)
        {
            pipeline.GetCSharpTypeResolvers.Add(GetType);
            _defaultTypeDefConverter = pipeline.TypedefConverters[0];
            pipeline.TypedefConverters.Clear();
            pipeline.TypedefConverters.Add(GetTypeDef);
        }

        private CSharpElement? GetTypeDef(CSharpConverter converter, CppTypedef cpptypedef, CSharpElement context)
        {
            if (cpptypedef.Name == "sqlite3_int64"
                || cpptypedef.Name == "sqlite3_uint64"
                || cpptypedef.Name == "sqlite_int64"
                || cpptypedef.Name == "sqlite_uint64")
            {
                return null;
            }
            return _defaultTypeDefConverter(converter, cpptypedef, context);
        }

        public static CSharpType? GetType(CSharpConverter converter, CppType cppType, CSharpElement context, bool nested)
        {
            if (cppType is CppTypedef typedef)
            {
                if (typedef.Name == "sqlite3_int64") return CSharpPrimitiveType.Long();
                if (typedef.Name == "sqlite3_uint64") return CSharpPrimitiveType.ULong();
                if (typedef.Name == "sqlite_int64") return CSharpPrimitiveType.Long();
                if (typedef.Name == "sqlite_uint64") return CSharpPrimitiveType.ULong();
            }

            return DefaultTypeConverter.GetCSharpType(converter, cppType, context, nested);
        }
    }
}