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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XenoAtom.Interop.CodeGen.libshaderc
{
    /// <summary>
    /// Generator for libshaderc API.
    /// </summary>
    internal partial class LibshadercGenerator(LibDescriptor descriptor) : GeneratorBase(descriptor)
    {
        protected override async Task<CSharpCompilation?> Generate()
        {
            //var sysIncludes = Apk.GetSysIncludeDirectory("main");
            //var mainInclude = Apk.GetIncludeDirectory("main");

            var sysFolders = Apk.GetPackageIncludeDirectoryAndDependencies(Descriptor.ApkDeps[0], out var libshadercIncludeFolder);

            var sysDirectory = Apk.GetPackageIncludeDirectory("musl-dev");
            sysFolders.Insert(0, sysDirectory);

            var csOptions = new CSharpConverterOptions()
            {
                DefaultClassLib = "libshaderc",
                DefaultNamespace = "XenoAtom.Interop",
                DefaultOutputFilePath = "/libshaderc_library.generated.cs",
                DefaultDllImportNameAndArguments = "LibraryName",
                TargetVendor = "linux",
                TargetSystem = "gnu",
                DefaultCallingConvention = CallingConvention.StdCall,
                Defines =
                {
                },
                AdditionalArguments =
                {
                },
                SystemIncludeFolders =
                {
                },
                IncludeFolders =
                {
                    libshadercIncludeFolder,
                },

                PreHeaderText = @"",

                DispatchOutputPerInclude = true,
                DisableRuntimeMarshalling = true,
                AllowMarshalForString = false,
                EnableAutoByRef = false,
                MapCLongToIntPtr = true,

                MappingRules =
                {
                    e => e.Map<CppParameter>("(shaderc_get_spv_version|shaderc_parse_version_profile)::(version|revision|profile)").ByRef(CSharpRefKind.Out),
                }
            };

            foreach (var folder in sysFolders)
            {
                csOptions.SystemIncludeFolders.Add(folder);
            }

            csOptions.IncludeFolders.Add(Path.Combine(libshadercIncludeFolder, "shaderc-dev"));

            var files = new List<string>()
            {
                Path.Combine(libshadercIncludeFolder, "shaderc/shaderc.h"),
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
                ProcessFunction(csFunction);
            }
            
            return csCompilation;
        }


        private void ProcessFunction(CSharpMethod csMethod)
        {
            for (var i = 0; i < csMethod.Parameters.Count; i++)
            {
                var param = csMethod.Parameters[i];
                if (param.ParameterType is CSharpPrimitiveType csPrimitiveType && csPrimitiveType.Kind == CSharpPrimitiveKind.Bool)
                {
                    param.ParameterType = new CSharpTypeWithAttributes(param.ParameterType)
                    {
                        Attributes = { new CSharpMarshalAsAttribute(UnmanagedType.U1) }
                    };
                }
            }

            if (csMethod.ReturnType is CSharpPrimitiveType csPrimitiveType2 && csPrimitiveType2.Kind == CSharpPrimitiveKind.Bool)
            {
                csMethod.ReturnType = new CSharpTypeWithAttributes(csMethod.ReturnType)
                {
                    Attributes =
                    {
                        new CSharpMarshalAsAttribute(UnmanagedType.U1)
                        {
                            Scope = CSharpAttributeScope.Return
                        }
                    }
                };
            }


            ProcessStringArgs(csMethod);
        }

        private void ProcessStringArgs(CSharpMethod csMethod)
        {
            bool hasSpanMarhsalling = false;
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
                        // Don't convert arguments that are taking a size, we will add a manual override
                        if (i + 1 < csMethod.Parameters.Count)
                        {
                            var nextParam = csMethod.Parameters[i + 1];
                            if (nextParam.Name.EndsWith("_size") || nextParam.Name.EndsWith("_length"))
                            {
                                hasSpanMarhsalling = true;
                                continue;
                            }
                        }

                        newManagedMethod ??= csMethod.Clone();
                        newManagedMethod.Parameters[i].ParameterType = new CSharpTypeWithAttributes(new CSharpFreeType("ReadOnlySpan<char>"))
                        {
                            Attributes = { new CSharpMarshalUsingAttribute("typeof(Utf8CustomMarshaller)") }
                        };
                    }
                }
            }

            if (csMethod.Name != "shaderc_result_get_bytes")
            {
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
                }
            }

            if (newManagedMethod != null)
            {
                var parent = ((ICSharpContainer)csMethod.Parent!);
                var indexOf = parent.Members.IndexOf(csMethod);
                parent.Members.Insert(indexOf + 1, newManagedMethod);
            }

            // Create Span version
            if (hasSpanMarhsalling)
            {
                var previousMethod = newManagedMethod ?? csMethod;
                var newManagedMethodWithSpan = previousMethod.Clone();
                // Remove all attributes
                newManagedMethodWithSpan.Attributes.Clear();
                foreach (var param in newManagedMethodWithSpan.Parameters)
                {
                    param.Attributes.Clear();
                }

                newManagedMethodWithSpan.Modifiers &= ~CSharpModifiers.Partial;


                for (var i = newManagedMethodWithSpan.Parameters.Count - 1; i >= 1; i--)
                {
                    var param = newManagedMethodWithSpan.Parameters[i];

                    if (!param.Name.EndsWith("_size") && !param.Name.EndsWith("_length"))
                    {
                        continue;
                    }

                    var previousParameter = newManagedMethodWithSpan.Parameters[i - 1];
                    var cppType = (CppType)previousParameter.ParameterType!.CppElement!;
                    if (cppType.TryGetElementTypeFromPointerToConst(out var elementType) && elementType is CppPrimitiveType { Kind: CppPrimitiveKind.Char })
                    {
                        newManagedMethodWithSpan.Parameters.RemoveAt(i);
                        previousParameter.ParameterType = new CSharpGenericTypeReference("ReadOnlySpan", [CSharpPrimitiveType.Char()]);
                    }
                }

                newManagedMethodWithSpan.Body = (writer, element) =>
                {
                    foreach (var param in newManagedMethodWithSpan.Parameters)
                    {
                        if (param.ParameterType is not CSharpGenericTypeReference)
                        {
                            continue;
                        }
                        
                        writer.WriteLine($"byte* {param.Name}__ = default;");
                        writer.WriteLine($"global::XenoAtom.Interop.Utf8CustomMarshaller.ManagedToUnmanagedIn {param.Name}__marshaller = new();");
                    }

                    writer.WriteLine("try");
                    writer.OpenBraceBlock();


                    foreach (var param in newManagedMethodWithSpan.Parameters)
                    {
                        if (param.ParameterType is not CSharpGenericTypeReference)
                        {
                            continue;
                        }

                        writer.WriteLine($"{param.Name}__marshaller.FromManaged({param.Name}, stackalloc byte[global::XenoAtom.Interop.Utf8CustomMarshaller.ManagedToUnmanagedIn.BufferSize]);");
                        writer.WriteLine($"{param.Name}__ = {param.Name}__marshaller.ToUnmanaged();");
                    }


                    if (newManagedMethodWithSpan.ReturnType is not CSharpPrimitiveType primitiveType || primitiveType.Kind != CSharpPrimitiveKind.Void)
                    {
                        writer.Write("return ");
                    }
                    
                    writer.Write(newManagedMethodWithSpan.Name);
                    writer.Write("(");
                    for (var i = 0; i < newManagedMethodWithSpan.Parameters.Count; i++)
                    {
                        var param = newManagedMethodWithSpan.Parameters[i];
                        if (i > 0)
                        {
                            writer.Write(", ");
                        }


                        if (param.ParameterType is CSharpRefType refType)
                        {
                            writer.Write(refType.Kind == CSharpRefKind.Ref ? "ref " : "out ");
                        }

                        if (param.ParameterType is CSharpGenericTypeReference)
                        {
                            writer.Write($"{param.Name}__");
                            writer.Write(", ");
                            writer.Write($"(nuint){param.Name}__marshaller.Length");
                        }
                        else
                        {
                            writer.Write(param.Name);
                        }
                    }
                    writer.WriteLine(");");

                    writer.CloseBraceBlock();
                    writer.WriteLine("finally");
                    writer.OpenBraceBlock();


                    foreach (var param in newManagedMethodWithSpan.Parameters)
                    {
                        if (param.ParameterType is not CSharpGenericTypeReference)
                        {
                            continue;
                        }

                        writer.WriteLine($"{param.Name}__marshaller.Free();");
                    }

                    writer.CloseBraceBlock();
                };

                var parent = ((ICSharpContainer)csMethod.Parent!);
                var indexOf = parent.Members.IndexOf(previousMethod);
                parent.Members.Insert(indexOf + 1, newManagedMethodWithSpan);
            }
        }
    }
}