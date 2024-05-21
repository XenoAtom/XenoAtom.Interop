// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CppAst;
using CppAst.CodeGen.Common;
using CppAst.CodeGen.CSharp;
using Zio.FileSystems;

namespace XenoAtom.Interop.CodeGen.vulkan;

internal partial class VulkanGenerator(LibDescriptor descriptor) : GeneratorBase(descriptor)
{
    private readonly List<CppFunction> _extensionFunctions = new();

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
                "VK_ENABLE_BETA_EXTENSIONS",
                "VK_USE_PLATFORM_FUCHSIA"
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
            PreHeaderText = @"
#define LUID unsigned long long
",

            DispatchOutputPerInclude = true,
            DisableRuntimeMarshalling = true,
            AllowMarshalForString = false,
            MapCLongToIntPtr = true,

            MappingRules =
            {
                e => e.Map<CppEnum>("VkLoaderFeastureFlagBits").Name("VkLoaderFeatureFlagBits"), // 🧐
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
            Path.Combine(mainInclude, "vulkan/vk_icd.h"),
            Path.Combine(mainInclude, "vulkan/vk_layer.h"),
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

        var csEnumFlags = new Dictionary<string, CSharpStruct>();
        var functionPFNs = new Dictionary<string, CSharpStruct>();

        foreach (var csStruct in csCompilation.AllStructs)
        {
            // Associate Enum XXXFlagBits with Struct XXXFlags
            if (csStruct.Name.Contains("Flags", StringComparison.Ordinal))
            {
                csEnumFlags.Add(csStruct.Name, csStruct);
            }

            // Collect PFN function pointers
            if (csStruct.Name.Contains("PFN_vk", StringComparison.Ordinal))
            {
                functionPFNs.Add(csStruct.Name["PFN_".Length..], csStruct);
            }
        }


        var commandExtRegex = new Regex($"{CommonVkExt}$");
        foreach (var csFunction in csCompilation.AllFunctions)
        {
            var isExtensionFunction = commandExtRegex.IsMatch(csFunction.Name);

            if (functionPFNs.TryGetValue(csFunction.Name, out var pfn))
            {
                pfn.BaseTypes.Add(new CSharpFreeType("IvkFunctionPointer"));

                var csProperty = new CSharpProperty(csFunction.Name + "_")
                {
                    ReturnType = new CSharpGenericTypeReference($"vkFunctionPointerPrototype", [pfn]),
                    GetBodyInlined = $"new(\"{csFunction.Name}\"u8)",
                    Visibility = CSharpVisibility.Public,
                    Modifiers = CSharpModifiers.Static,
                };
                var parent = (ICSharpContainer)csFunction.Parent!;
                parent.Members.Insert(parent.Members.IndexOf(csFunction) + 1, csProperty);

                // Extension functions are not part of the core API, so we should remove LibraryImport for them
                if (isExtensionFunction)
                {
                    parent.Members.Remove(csFunction);

                    // Record the extension function to still expose it in the supported API list of functions
                    _extensionFunctions.Add((CppFunction)csFunction.CppElement!);
                }

                // Add an invoke method to the function pointer to allow calling the function pointers with proper arguments names and ref/in types
                var csMethod = new CSharpMethod("Invoke")
                {
                    ReturnType = csFunction.ReturnType,
                    Visibility = CSharpVisibility.Public,
                    Body = (writer, _) =>
                    {
                        var isReturnVoid = csFunction.ReturnType is CSharpPrimitiveType primitiveType && primitiveType.Kind == CSharpPrimitiveKind.Void;

                        foreach (var csParameter in csFunction.Parameters)
                        {
                            if (csParameter.ParameterType is CSharpRefType refType)
                            {
                                writer.Write("fixed (");
                                refType.ElementType.DumpReferenceTo(writer);
                                writer.WriteLine($"* __{csParameter.Name} = &{csParameter.Name})");
                            }
                        }

                        if (!isReturnVoid)
                        {
                            writer.Write("return ");
                        }
                        writer.Write("Value(");
                        for (var i = 0; i < csFunction.Parameters.Count; i++)
                        {
                            var csParameter = csFunction.Parameters[i];
                            if (i > 0)
                            {
                                writer.Write(", ");
                            }
                            writer.Write(csParameter.ParameterType is CSharpRefType ? $"__{csParameter.Name}" : csParameter.Name);
                        }

                        writer.WriteLine(");");
                    }
                };
                var csPointerProperty = new CSharpProperty("Pointer")
                {
                    ReturnType = CSharpPrimitiveType.IntPtr(),
                    GetBodyInlined = "(nint)Value",
                    Visibility = CSharpVisibility.Public,
                };

                var csIsNullProperty = new CSharpProperty("IsNull")
                {
                    ReturnType = CSharpPrimitiveType.Bool(),
                    GetBodyInlined = "(nint)Value == 0",
                    Visibility = CSharpVisibility.Public,
                };

                pfn.Members.Add(csMethod);
                pfn.Members.Add(csPointerProperty);
                pfn.Members.Add(csIsNullProperty);

                // Replicate parameters
                foreach (var csParameter in csFunction.Parameters)
                {
                    csMethod.Parameters.Add(csParameter);
                }
            }
            else
            {
               throw new InvalidOperationException($"Cannot find PFN for function {csFunction.Name}");
            }
        }

        foreach (var csEnum in csCompilation.AllEnums)
        {
            if (csEnum.Name.Contains("FlagBits", StringComparison.Ordinal))
            {
                csEnum.Attributes.Add(new CSharpFreeAttribute("Flags"));
                
                var structName = csEnum.Name.Replace("FlagBits", "Flags", StringComparison.Ordinal);
                if (csEnumFlags.TryGetValue(structName, out var csStruct))
                {
                    // Add implicit operators between XXXFlagBits and Struct XXXFlags
                    csStruct.Members.Add(new CSharpMethod(string.Empty)
                    {
                        Kind = CSharpMethodKind.Operator,
                        ReturnType = csEnum,
                        Modifiers = CSharpModifiers.Static | CSharpModifiers.Implicit,
                        Parameters =
                        {
                            new CSharpParameter("from") {ParameterType = csStruct},
                        },
                        BodyInline = ((writer, _) =>
                        {
                            writer.Write("(");
                            csEnum.DumpReferenceTo(writer);
                            writer.Write(")(uint)from.Value");
                        }),
                        Visibility = CSharpVisibility.Public
                    });
                    csStruct.Members.Add(new CSharpMethod(string.Empty)
                    {
                        Kind = CSharpMethodKind.Operator,
                        ReturnType = csStruct,
                        Modifiers = CSharpModifiers.Static | CSharpModifiers.Implicit,
                        Parameters =
                        {
                            new CSharpParameter("from") {ParameterType = csEnum},
                        },
                        BodyInline = (writer, element) =>
                        {
                            writer.Write("new ");
                            csStruct.DumpReferenceTo(writer);
                            writer.Write("((uint)from)");
                        },
                        Visibility = CSharpVisibility.Public
                    });
                }
                else
                {
                    Console.Error.WriteLine($"Cannot find struct {structName} for enum {csEnum.Name}");
                }
            }
        }

        // Transform const string literal into ReadOnlySpanUtf8
        foreach (var csField in csCompilation.AllFields)
        {
            if (csField.FieldType is CSharpPrimitiveType primitiveType && primitiveType.Kind == CSharpPrimitiveKind.String)
            {
                var csProperty = new CSharpProperty(csField.Name)
                {
                    ReturnType = new CSharpFreeType("ReadOnlySpanUtf8"),
                    GetBodyInlined = csField.InitValue + "u8",
                    Visibility = CSharpVisibility.Public,
                    Modifiers = CSharpModifiers.Static,
                };
                var parent = (ICSharpContainer)csField.Parent!;
                parent.Members.Insert(parent.Members.IndexOf(csField) + 1, csProperty);
                parent.Members.Remove(csField);
            }
        }
 
        return csCompilation;
    }

    protected override IEnumerable<CppFunction> GetAdditionalExportedCppFunctions() => _extensionFunctions;
}