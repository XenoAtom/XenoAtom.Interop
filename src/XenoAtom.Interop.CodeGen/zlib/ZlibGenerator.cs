using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CppAst;
using CppAst.CodeGen.Common;
using CppAst.CodeGen.CSharp;
using Zio.FileSystems;

namespace XenoAtom.Interop.CodeGen.zlib;

internal partial class ZlibGenerator
{
    private readonly ApkIncludeHelper _apkIncludeHelper;


    public ZlibGenerator(ApkIncludeHelper apkIncludeHelper)
    {
        _apkIncludeHelper = apkIncludeHelper;
    }

    public async Task Run()
    {
        // Make sure that we download libgit2-dev includes
        await _apkIncludeHelper.EnsureIncludes("zlib-dev");

        var sysIncludes = _apkIncludeHelper.GetSysIncludeDirectory("main");

        var mainInclude = _apkIncludeHelper.GetIncludeDirectory("main");
        List<string> srcFolders =
        [
            mainInclude,
        ];

        var destFolder = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory,
            @"..\..\..\..\..\zlib\XenoAtom.Interop.zlib\generated"));

        if (!Directory.Exists(destFolder))
        {
            throw new DirectoryNotFoundException($"The destination folder `{destFolder}` doesn't exist");
        }

        var csOptions = new CSharpConverterOptions()
        {
            DefaultClassLib = "zlib",
            DefaultNamespace = "XenoAtom.Interop",
            DefaultOutputFilePath = "/zlib_library.generated.cs",
            DefaultDllImportNameAndArguments = "LibraryName",
            TargetVendor = "linux",
            TargetSystem = "musl",
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
                e => e.MapMacroToConst("Z_NO_FLUSH", "int"),
                e => e.MapMacroToConst("Z_PARTIAL_FLUSH", "int"),
                e => e.MapMacroToConst("Z_SYNC_FLUSH", "int"),
                e => e.MapMacroToConst("Z_FULL_FLUSH", "int"),
                e => e.MapMacroToConst("Z_FINISH", "int"),
                e => e.MapMacroToConst("Z_BLOCK", "int"),
                e => e.MapMacroToConst("Z_TREES", "int"),
                e => e.MapMacroToConst("Z_OK", "int"),
                e => e.MapMacroToConst("Z_STREAM_END", "int"),
                e => e.MapMacroToConst("Z_NEED_DICT", "int"),
                e => e.MapMacroToConst("Z_ERRNO", "int"),
                e => e.MapMacroToConst("Z_STREAM_ERROR", "int"),
                e => e.MapMacroToConst("Z_DATA_ERROR", "int"),
                e => e.MapMacroToConst("Z_MEM_ERROR", "int"),
                e => e.MapMacroToConst("Z_BUF_ERROR", "int"),
                e => e.MapMacroToConst("Z_VERSION_ERROR", "int"),
                e => e.MapMacroToConst("Z_NO_COMPRESSION", "int"),
                e => e.MapMacroToConst("Z_BEST_SPEED", "int"),
                e => e.MapMacroToConst("Z_BEST_COMPRESSION", "int"),
                e => e.MapMacroToConst("Z_DEFAULT_COMPRESSION", "int"),
                e => e.MapMacroToConst("Z_FILTERED", "int"),
                e => e.MapMacroToConst("Z_HUFFMAN_ONLY", "int"),
                e => e.MapMacroToConst("Z_RLE", "int"),
                e => e.MapMacroToConst("Z_FIXED", "int"),
                e => e.MapMacroToConst("Z_DEFAULT_STRATEGY", "int"),
                e => e.MapMacroToConst("Z_BINARY", "int"),
                e => e.MapMacroToConst("Z_TEXT", "int"),
                e => e.MapMacroToConst("Z_ASCII", "int"),
                e => e.MapMacroToConst("Z_UNKNOWN", "int"),
                e => e.MapMacroToConst("Z_DEFLATED", "int"),
                e => e.MapMacroToConst("Z_NULL", "int"),
                e => e.MapMacroToConst("ZLIB_VERSION", "char*"),
                e => e.MapMacroToConst("ZLIB_VER[N_].*", "int"),
                e => e.Map<CppClass>("z_stream_s").Name("z_stream"),
                e => e.Map<CppClass>("gz_header_s").Name("gz_header"),
                e => e.Map<CppFunction>("^gz.*").Discard(),
                e => e.Map<CppClass>("gzFile_s").Discard(),
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
            foreach (var param in function.Parameters)
            {
                if (param.ParameterType is CSharpPointerType pointerType &&
                    pointerType.ElementType is CSharpStruct csStruct &&
                    (csStruct.Name == "z_stream_s" || csStruct.Name == "gzFile_s" || csStruct.Name == "gz_header_s"))
                {
                    param.ParameterType = new CSharpRefType(CSharpRefKind.Ref, csStruct);
                }
            }

            ProcessStringArgs(function);
        }

        var fs = new PhysicalFileSystem();

        {
            var subfs = new SubFileSystem(fs, fs.ConvertPathFromInternal(destFolder));
            var codeWriter = new CodeWriter(new CodeWriterOptions(subfs));
            csCompilation.DumpTo(codeWriter);
        }
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