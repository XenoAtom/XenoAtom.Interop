// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using CppAst;
using CppAst.CodeGen.CSharp;

namespace XenoAtom.Interop.CodeGen.vulkan;

/// <summary>
/// Generator for Vulkan API.
/// </summary>
internal partial class VulkanGenerator(LibDescriptor descriptor) : GeneratorBase(descriptor)
{
    private const string CommonVkExt = "(AMD|AMDX|ARM|EXT|GOOGLE|HUAWEI|IMG|INTEL|KHR|LUNARG|NV|NVX|QCOM|SEC|VALVE)";
    private const string CommonVkUint = "(MAX|QUEUE|REMAINING|SHADER|VERSION_1|TRUE|FALSE|UUID|ATTACHMENT|LUID|SUBPASS)";
    private readonly List<CppFunction> _extensionFunctions = new();
    private readonly Dictionary<string, VulkanCommand> _functionRegistry = new();
    private readonly Dictionary<VulkanDocTypeKind, VulkanDocDefinitions> _docDefinitions = new();
    private readonly Dictionary<string, CSharpStruct> _structFunctionPointers = new();
    private readonly Dictionary<string, CSharpStruct> _structAsEnumFlags = new();
    private readonly Dictionary<string, VulkanElementInfo> _vulkanElementInfos = new();
    private readonly List<int> _tempOptionalParameterIndexList = new();
    private readonly Dictionary<string, Dictionary<string, string>> _mapStructToFieldsWithDefaultValue = new();

    private readonly HashSet<string> _structsAsRecord = new()
    {
        "VkOffset2D",
        "VkOffset3D",
        "VkExtent2D",
        "VkExtent3D",
        "VkRect2D",
    };

    public override async Task Initialize(ApkManager apkHelper)
    {
        await base.Initialize(apkHelper);

        await Apk.ExtractFolders("vulkan-headers", "vulkan-registry", "usr/share/vulkan/registry/");

        var vkXml = Path.Combine(AppContext.BaseDirectory, Apk.GetSubCacheDirectory("vulkan-registry", "main"), "vk.xml");
        if (!File.Exists(vkXml))
        {
            throw new FileNotFoundException($"Cannot find Vulkan registry: {vkXml}");
        }

        LoadVulkanRegistry(vkXml);

        await DownloadAndProcessManPages();
    }

    protected override async Task<CSharpCompilation?> Generate()
    {
        var sysIncludes = Apk.GetSysIncludeDirectory("main");
        var vulkanSysIncludes = Path.Combine(AppContext.BaseDirectory, "vulkan_sys_includes");

        var vulkanInclude = Apk.GetPackageIncludeDirectory("vulkan-headers");

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
                vulkanInclude
            },
            PreHeaderText = @"
#define LUID unsigned long long
",

            DispatchOutputPerInclude = true,
            DisableRuntimeMarshalling = true,
            AllowMarshalForString = false,
            EnableAutoByRef = false,
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
            Path.Combine(vulkanInclude, "vulkan/vulkan.h"),
            Path.Combine(vulkanInclude, "vulkan/vk_icd.h"),
            Path.Combine(vulkanInclude, "vulkan/vk_layer.h"),
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

        foreach (var csEnum in csCompilation.AllEnums)
        {
            ApplyDocumentation(csEnum);
        }
        
        foreach (var csStruct in csCompilation.AllStructs)
        {
            ApplyDocumentation(csStruct);
            AddVulkanVersionAndExtensionInfoToCSharpElement(csStruct);
            ProcessStruct(csStruct);

            // Associate Enum XXXFlagBits with Struct XXXFlags
            if (csStruct.Name.Contains("Flags", StringComparison.Ordinal))
            {
                _structAsEnumFlags.Add(csStruct.Name, csStruct);
            }

            // Collect PFN function pointers
            if (csStruct.Name.Contains("PFN_vk", StringComparison.Ordinal))
            {
                _structFunctionPointers.Add(csStruct.Name["PFN_".Length..], csStruct);
            }
        }
        
        foreach (var csFunction in csCompilation.AllFunctions)
        {
            ProcessVulkanFunction(csFunction);
        }

        foreach (var csEnum in csCompilation.AllEnums)
        {
            ProcessVulkanEnum(csEnum);
        }

        // Transform const string literal into ReadOnlyMemoryUtf8
        foreach (var csField in csCompilation.AllFields)
        {
            if (csField.FieldType is CSharpPrimitiveType primitiveType && primitiveType.Kind == CSharpPrimitiveKind.String)
            {
                var csProperty = new CSharpProperty(csField.Name)
                {
                    ReturnType = new CSharpFreeType("ReadOnlyMemoryUtf8"),
                    GetBodyInlined = csField.InitValue + "u8",
                    Visibility = CSharpVisibility.Public,
                    Modifiers = CSharpModifiers.Static,
                };
                var parent = (ICSharpContainer)csField.Parent!;
                parent.Members.Insert(parent.Members.IndexOf(csField) + 1, csProperty);
                parent.Members.Remove(csField);
            }
        }

        foreach (var csClass in csCompilation.AllClasses)
        {
            foreach (var csField in csClass.Members.OfType<CSharpField>())
            {
                if (csField.FieldType is CSharpEnum csEnum)
                {
                    // Copy the comment from the enum to the field
                    var csEnumItem = (CSharpEnumItem?)csEnum.Members.FirstOrDefault(x => x is CSharpEnumItem csEnumItem && csEnumItem.Name == csField.Name);
                    if (csEnumItem is not null && csField.Comment is null)
                    {
                        csField.Comment = csEnumItem.Comment;
                    }
                }
            }
        }
 
        return csCompilation;
    }

    private void ProcessStruct(CSharpStruct csStruct)
    {
        var cppName = ((ICppMember)csStruct.CppElement!).Name;

        if (_structsAsRecord.Contains(cppName))
        {
            csStruct.IsRecord = true;
        }
        
        if (!_mapStructToFieldsWithDefaultValue.TryGetValue(cppName, out var fieldsWithDefaultValue))
        {
            return;
        }

        // Apply default value
        foreach (var csField in csStruct.Members.OfType<CSharpField>())
        {
            if (fieldsWithDefaultValue.TryGetValue(csField.Name, out var defaultValue))
            {
                csField.InitValue = defaultValue;
                csStruct.ForcePrimaryConstructorParameters = true;
            }
        }
    }

    private void ProcessVulkanEnum(CSharpEnum csEnum)
    {
        // We only need to modify flags in this method
        if (!csEnum.Name.Contains("FlagBits", StringComparison.Ordinal))
        {
            return;
        }

        csEnum.Attributes.Add(new CSharpFreeAttribute("Flags"));
                
        var structName = csEnum.Name.Replace("FlagBits", "Flags", StringComparison.Ordinal);
        if (_structAsEnumFlags.TryGetValue(structName, out var csStruct))
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

    [GeneratedRegex($"{CommonVkExt}")]
    private static partial Regex RegexCommandExt();

    private void ProcessVulkanFunction(CSharpMethod csFunction)
    {
        // Apply doc to the function
        ApplyDocumentation(csFunction);
        AddVulkanVersionAndExtensionInfoToCSharpElement(csFunction);

        if (!_structFunctionPointers.TryGetValue(csFunction.Name, out var pfn))
        {
            Console.WriteLine($"Warning, cannot find PFN for function {csFunction.Name}");
            return;
        }

        var cppFunction = (CppFunction)csFunction.CppElement!;

        pfn.BaseTypes.Add(new CSharpGenericTypeReference("IvkFunctionPointer", [pfn]));

        var csProperty = new CSharpProperty("Name")
        {
            ReturnType = new CSharpFreeType($"ReadOnlyMemoryUtf8"),
            GetBodyInlined = $"\"{csFunction.Name}\"u8",
            Visibility = CSharpVisibility.Public,
            Modifiers = CSharpModifiers.Static,
        };

        csProperty.Comment = new CSharpFullComment()
        {
            Children =
            {
                new CSharpXmlComment("summary")
                {
                    Children =
                    {
                        new CSharpTextComment($"Gets the prototype of the function `{csFunction.Name}`.")
                    }
                }
            }
        };
        var parent = (ICSharpContainer)csFunction.Parent!;
        pfn.Members.Add(csProperty);

        // Extension functions are not part of the core API, so we should remove LibraryImport for them
        var isExtensionFunction = IsFunctionPointerStruct(cppFunction);
        if (isExtensionFunction)
        {
            parent.Members.Remove(csFunction);

            // Record the extension function to still expose it in the supported API list of functions
            _extensionFunctions.Add(cppFunction);
        }

        // Add an invoke method to the function pointer to allow calling the function pointers with proper arguments names and ref/in types
        var csMethod = new CSharpMethod("Invoke")
        {
            Comment = csFunction.Comment,
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

        CreateNewFunctionOverloads(csFunction);
    }

    private static bool IsFunctionPointerStruct(CppFunction cppFunction)
    {
        return RegexCommandExt().IsMatch(cppFunction.Name);
    }
    
    private void CreateNewFunctionOverloads(CSharpMethod csFunction)
    {
        var cppFunction = (CppFunction)csFunction.CppElement!;
        var pfn = _structFunctionPointers[cppFunction.Name];
        bool isExtensionFunction = IsFunctionPointerStruct(cppFunction);

        if (!TryGetVulkanCommand(cppFunction.Name, out var command))
        {
            Console.WriteLine($"Warning, cannot find Vulkan command for function {cppFunction.Name}");
            return;
        }

        if (command.Parameters.Count != csFunction.Parameters.Count)
        {
            Console.WriteLine($"Warning, Vulkan command {cppFunction.Name} from registry has different number of parameters {command.Parameters.Count} than the C function {csFunction.Name} {cppFunction.Parameters.Count}");
            return;
        }

        _tempOptionalParameterIndexList.Clear();
        var parameterListToProcess = GetParamToProcessArray(csFunction, command, cppFunction, _tempOptionalParameterIndexList);

        // Process optional parameters
        int lastIndex = cppFunction.Parameters.Count - 1;
        for (var i = _tempOptionalParameterIndexList.Count - 1; i >= 0 ; i--)
        {
            var optionalIndex = _tempOptionalParameterIndexList[i];
            if (optionalIndex != lastIndex)
            {
                break;
            }
            lastIndex--;

            csFunction.Parameters[optionalIndex].DefaultValue = "default";
        }

        // If there are no parameters to marshal, we don't need to create a new overload
        if (parameterListToProcess.All(x => x is null))
        {
            return;
        }
        
        CreateNewFunctionOverload(csFunction, command, parameterListToProcess, false);


        if (parameterListToProcess.Any(x => x is not null && x.VkParameter.Kind == VulkanCommandParameterKind.Length && x.RefKind == CSharpRefKind.Ref))
        {
            CreateNewFunctionOverload(csFunction, command, parameterListToProcess, true);
        }

    }

    private void CreateNewFunctionOverload(CSharpMethod csFunction, VulkanCommand command, ParamToProcess?[] paramsToProcess, bool makeLengthOut)
    {
        var cppFunction = (CppFunction)csFunction.CppElement!;
        var pfn = _structFunctionPointers[cppFunction.Name];
        bool isExtensionFunction = IsFunctionPointerStruct(cppFunction);
        var parent = (ICSharpContainer)csFunction.Parent!;

        // Create the new method
        var newMethod = csFunction.Clone();
        if (isExtensionFunction)
        {
            newMethod.Name = "Invoke";
            newMethod.Modifiers &= ~CSharpModifiers.Static;
        }

        newMethod.Parent = null;

        // Remove the partial attribute as we are giving it a body
        newMethod.Modifiers &= ~CSharpModifiers.Partial;
        // We remove all attributes as we are calling to call the interop method from the body
        newMethod.Attributes.Clear();

        // We go down from the last parameter to the first one to replace the array parameters with Span
        for (var index = paramsToProcess.Length - 1; index >= 0; index--)
        {
            var paramToProcess = paramsToProcess[index];
            if (paramToProcess == null)
            {
                continue;
            }

            // command.Parameters[index]

            var vkParameter = command.Parameters[index];
            if (vkParameter.Kind == VulkanCommandParameterKind.Array)
            {
                // The array is optional, so we need to remove the array from the parameters
                if (makeLengthOut && command.Parameters[vkParameter.LengthParameterIndex].Optional != VulkanCommandOptional.None && paramsToProcess[vkParameter.LengthParameterIndex]!.RefKind == CSharpRefKind.Ref)
                {
                    newMethod.Parameters.RemoveAt(index);
                }
                else
                {
                    // Convert this array parameter to Span
                    var arrayElementType = paramToProcess.ElementType;
                    var spanParameterType = paramToProcess.IsConst ? new CSharpGenericTypeReference("ReadOnlySpan", [arrayElementType]) : new CSharpGenericTypeReference("Span", [arrayElementType]);
                    newMethod.Parameters[index].ParameterType = spanParameterType;
                }
            }
            else if (vkParameter.Kind == VulkanCommandParameterKind.Length)
            {
                if (makeLengthOut && vkParameter.Optional != VulkanCommandOptional.None && paramToProcess.RefKind == CSharpRefKind.Ref)
                {
                    // Convert this parameter to out
                    var refType = new CSharpRefType(CSharpRefKind.Out, paramToProcess.ElementType);
                    newMethod.Parameters[index].ParameterType = refType;
                }
                else if (paramToProcess.SupportedArrayParameters != null)
                {
                    // Length parameters are removed if they are used by an array parameter that will be converted to Span
                    newMethod.Parameters.RemoveAt(index);
                }
            }
            else if (vkParameter.Kind == VulkanCommandParameterKind.NullTerminated)
            {
                // Convert this parameter to string
                newMethod.Parameters[index].ParameterType = new CSharpGenericTypeReference("ReadOnlySpan", [CSharpPrimitiveType.Char()]);
            }
            else if (paramToProcess.RefKind != CSharpRefKind.None)
            {
                // Convert this parameter to ref/in
                var refType = new CSharpRefType(paramToProcess.RefKind, paramToProcess.ElementType);
                newMethod.Parameters[index].ParameterType = refType;
            }
            else
            {
                // TODO? Check what is left here?
            }
        }

        newMethod.Body = (writer, _) =>
        {
            bool hasReturnType = newMethod.ReturnType is not CSharpPrimitiveType primitiveType || primitiveType.Kind != CSharpPrimitiveKind.Void;
            bool hasStrings = false;

            // Write local variables for each parameter that is required locally (string, length by ref...)
            for (var paramIndex = 0; paramIndex < paramsToProcess.Length; paramIndex++)
            {
                var paramToProcess = paramsToProcess[paramIndex];
                if (paramToProcess == null)
                {
                    continue;
                }

                var csParameter = csFunction.Parameters[paramIndex];
                var cppParameter = cppFunction.Parameters[paramIndex];
                var vkParameter = command.Parameters[paramIndex];

                if (vkParameter.Kind == VulkanCommandParameterKind.NullTerminated)
                {
                    writer.WriteLine($"byte* {paramToProcess.LocalVariableName!} = default;");
                    writer.WriteLine($"global::XenoAtom.Interop.Utf8CustomMarshaller.ManagedToUnmanagedIn {paramToProcess.LocalVariableName!}__marshaller = new();");
                    hasStrings = true;
                }
                else if (vkParameter.Kind == VulkanCommandParameterKind.Length)
                {
                    if (makeLengthOut && vkParameter.Optional != VulkanCommandOptional.None && paramToProcess.RefKind == CSharpRefKind.Ref)
                    {
                        writer.WriteLine($"{csParameter.Name} = default;");
                    }
                    else
                    {
                        var firstArrayParameterIndex = paramToProcess.SupportedArrayParameters![0];
                        var csArrayParameter = csFunction.Parameters[firstArrayParameterIndex];

                        paramToProcess.ElementType.DumpReferenceTo(writer);
                        writer.Write($" {paramToProcess.LocalVariableName} = checked((");
                        paramToProcess.ElementType.DumpReferenceTo(writer);
                        writer.WriteLine($"){csArrayParameter.Name}.Length);");
                    }
                }
            }

            // Write all fixed statements required by spans and by ref
            for (var paramIndex = 0; paramIndex < paramsToProcess.Length; paramIndex++)
            {
                var paramToProcess = paramsToProcess[paramIndex];
                if (paramToProcess == null)
                {
                    continue;
                }

                var csParameter = csFunction.Parameters[paramIndex];
                var cppParameter = cppFunction.Parameters[paramIndex];
                var vkParameter = command.Parameters[paramIndex];

                if (vkParameter.Kind == VulkanCommandParameterKind.Array)
                {
                    if (!makeLengthOut || !paramToProcess.IsArrayOptional)
                    {
                        writer.Write("fixed (");
                        paramToProcess.ElementType.DumpReferenceTo(writer);
                        writer.WriteLine($"* {paramToProcess.LocalVariableName} = {csParameter.Name})");
                    }
                }
                else if (vkParameter.Kind == VulkanCommandParameterKind.Standard && paramToProcess.RefKind != CSharpRefKind.None)
                {
                    writer.Write("fixed (");
                    paramToProcess.ElementType.DumpReferenceTo(writer);
                    writer.WriteLine($"* {paramToProcess.LocalVariableName} = &{csParameter.Name})");
                }
                else if (makeLengthOut && vkParameter.Kind == VulkanCommandParameterKind.Length && paramToProcess.RefKind == CSharpRefKind.Ref)
                {
                    writer.Write("fixed (");
                    paramToProcess.ElementType.DumpReferenceTo(writer);
                    writer.WriteLine($"* {paramToProcess.LocalVariableName} = &{csParameter.Name})");
                }
            }

            if (hasStrings)
            {
                writer.WriteLine("try");
                writer.OpenBraceBlock();

                // Write local variables for each parameter that is required locally (string, length by ref...)
                for (var paramIndex = 0; paramIndex < paramsToProcess.Length; paramIndex++)
                {
                    var paramToProcess = paramsToProcess[paramIndex];
                    if (paramToProcess == null)
                    {
                        continue;
                    }

                    var csParameter = csFunction.Parameters[paramIndex];
                    var vkParameter = command.Parameters[paramIndex];

                    if (vkParameter.Kind == VulkanCommandParameterKind.NullTerminated)
                    {
                       writer.WriteLine($"{paramToProcess.LocalVariableName}__marshaller.FromManaged({csParameter.Name}, stackalloc byte[global::XenoAtom.Interop.Utf8CustomMarshaller.ManagedToUnmanagedIn.BufferSize]);");
                       writer.WriteLine($"{paramToProcess.LocalVariableName} = {paramToProcess.LocalVariableName}__marshaller.ToUnmanaged();");
                    }
                }
            }

            if (hasReturnType)
            {
                writer.Write("return ");
            }

            writer.Write(isExtensionFunction ? "this.Invoke" : csFunction.Name);
            writer.Write("(");
            for (var i = 0; i < csFunction.Parameters.Count; i++)
            {
                if (i > 0)
                {
                    writer.Write(", ");
                }

                var paramToProcess = paramsToProcess[i];
                var csParameter = csFunction.Parameters[i];
                var vkParameter = command.Parameters[i];

                if (paramToProcess == null || paramToProcess.LocalVariableName is null)
                {
                    writer.Write(csParameter.Name);
                }
                else if (paramToProcess.LocalVariableName != null)
                {
                    // TODO: handle address to local length variable
                    if (vkParameter.Kind == VulkanCommandParameterKind.Length && paramToProcess.RefKind == CSharpRefKind.Ref)
                    {
                        writer.Write(makeLengthOut ? $"{paramToProcess.LocalVariableName}" : $"&{paramToProcess.LocalVariableName}");
                    }
                    else
                    {
                        if (makeLengthOut && paramToProcess.IsArrayOptional)
                        {
                            writer.Write("default");
                        }
                        else
                        {
                            writer.Write(paramToProcess.LocalVariableName);
                        }
                    }
                }
            }
            writer.WriteLine(");");

            if (hasStrings)
            {
                writer.CloseBraceBlock();
                writer.WriteLine("finally");
                writer.OpenBraceBlock();

                // Write local variables for each parameter that is required locally (string, length by ref...)
                for (var paramIndex = 0; paramIndex < paramsToProcess.Length; paramIndex++)
                {
                    var paramToProcess = paramsToProcess[paramIndex];
                    if (paramToProcess == null)
                    {
                        continue;
                    }

                    var vkParameter = command.Parameters[paramIndex];

                    if (vkParameter.Kind == VulkanCommandParameterKind.NullTerminated)
                    {
                        writer.WriteLine($"{paramToProcess.LocalVariableName}__marshaller.Free();");
                    }
                }

                writer.CloseBraceBlock();
            }
        };

        if (isExtensionFunction)
        {
            // If it is an extension function, add it to the extension function pointer
            pfn.Members.Add(newMethod);
        }
        else
        {
            // Insert the overload after the original function
            parent.Members.Insert(parent.Members.IndexOf(csFunction) + 1, newMethod);
        }
    }

    private static string GetLocalVariableFromParameter(string parameterName) => $"__{parameterName}_local";
    
    /// <summary>
    /// This class is used to identify parameters that we want to process to create new overloads:
    /// - Ref parameters
    /// - Length/Array parameters to convert to Span
    /// - Null terminated strings
    /// </summary>
    /// <param name="Index"></param>
    private class ParamToProcess(int Index)
    {
        public required VulkanCommandParameter VkParameter { get; init; }

        public CSharpRefKind RefKind { get; init; }

        public bool IsConst { get; init; }

        public bool IsArrayOptional { get; set; }

        public required CSharpType ElementType { get; init; }

        public List<int>? SupportedArrayParameters { get; set; }

        public required string? LocalVariableName { get; init; }
    }

    private ParamToProcess?[] GetParamToProcessArray(CSharpMethod csFunction, VulkanCommand command, CppFunction cppFunction, List<int> optionalParameters)
    {
        var paramsToProcess = new ParamToProcess?[cppFunction.Parameters.Count];
        for (var i = 0; i < cppFunction.Parameters.Count; i++)
        {
            var cppParameter = cppFunction.Parameters[i];
            var csParameter = csFunction.Parameters[i];
            var vkParameter = command.Parameters[i];

            CSharpType elementType = csParameter.ParameterType!;

            bool isPointerType = false;
            if (elementType is CSharpPointerType lengthPointerType)
            {
                isPointerType = true;
                elementType = lengthPointerType.ElementType;
            }

            cppParameter.Type.TryGetElementTypeFromPointer(out var isConst, out var _);

            ParamToProcess? paramToProcess = null;
            if (vkParameter.Kind == VulkanCommandParameterKind.Length)
            {
                var refKind = isPointerType ? CSharpRefKind.Ref : CSharpRefKind.None;
                paramToProcess = new ParamToProcess(i)
                {
                    VkParameter = vkParameter,
                    RefKind = refKind,
                    ElementType = elementType,
                    LocalVariableName = GetLocalVariableFromParameter(cppParameter.Name),
                };
            }
            else if (vkParameter.Kind == VulkanCommandParameterKind.NullTerminated)
            {
                if (cppParameter.Type.TryGetElementTypeFromPointer(out var isConstString, out var _) && isConstString)
                {
                    paramToProcess = new ParamToProcess(i)
                    {
                        VkParameter = vkParameter,
                        ElementType = elementType, // ElementType is not used for strings
                        LocalVariableName = GetLocalVariableFromParameter(cppParameter.Name),
                    };
                }
            }
            else if (vkParameter.Kind == VulkanCommandParameterKind.Array)
            {
                if (IsValidPointerTypeToProcess(cppParameter.Type) && IsValidPointerTypeForSpan(cppParameter.Type) && isPointerType && vkParameter.LengthParameterIndex >= 0)
                {
                    paramToProcess = new ParamToProcess(i)
                    {
                        VkParameter = vkParameter,
                        IsConst = isConst,
                        ElementType = elementType,
                        LocalVariableName = GetLocalVariableFromParameter(cppParameter.Name),
                        IsArrayOptional = command.Parameters[vkParameter.LengthParameterIndex].Optional != VulkanCommandOptional.None && paramsToProcess[vkParameter.LengthParameterIndex]!.RefKind == CSharpRefKind.Ref,
                    };
                }
            }
            else if (IsValidPointerTypeToProcess(cppParameter.Type, out var pointerElementType) && isPointerType)
            {
                if (vkParameter.Optional == VulkanCommandOptional.None)
                {
                    var refKind = isConst ? CSharpRefKind.In : CSharpRefKind.Out;
                    // Structures that have a sType field are always passed by ref as they need to be initialized before calling the function
                    if (!isConst && pointerElementType is CppClass cppClass && cppClass.Fields.Any(f => f.Name == "sType"))
                    {
                        refKind = CSharpRefKind.Ref;
                    }

                    paramToProcess = new ParamToProcess(i)
                    {
                        VkParameter = vkParameter,
                        RefKind = refKind,
                        ElementType = elementType,
                        LocalVariableName = GetLocalVariableFromParameter(cppParameter.Name),
                    };
                }
            }
            else if (vkParameter.Optional == VulkanCommandOptional.True)
            {
                optionalParameters.Add(i);
            }

            paramsToProcess[i] = paramToProcess;
        }

        for (var i = 0; i < command.Parameters.Count; i++)
        {
            var parameter = command.Parameters[i];
            if (parameter.Kind == VulkanCommandParameterKind.Array && paramsToProcess[i] != null)
            {
                var lengthParamToProcess = paramsToProcess[parameter.LengthParameterIndex]!;
                // Tag the length parameter as used by an array parameter that will be converted to Span
                lengthParamToProcess.SupportedArrayParameters ??= [];
                lengthParamToProcess.SupportedArrayParameters!.Add(i);
            }
        }

        for (var i = 0; i < paramsToProcess.Length; i++)
        {
            var paramToProcess = paramsToProcess[i];
            if (paramToProcess == null)
            {
                continue;
            }

            // If there is a length parameter, but it is not used, we don't process this parameter
            var parameter = command.Parameters[i];
            if (parameter.Kind == VulkanCommandParameterKind.Length && paramToProcess.SupportedArrayParameters is null)
            {
                paramsToProcess[i] = null;
            }
        }

        return paramsToProcess;
    }

    private void ApplyDocumentation(CSharpElement element)
    {
        var commentable = (ICSharpWithComment)element;
        
        List<CSharpComment>? additionalComments = null;
        CSharpComment? description = null;

        if (element is CSharpMethod csMethod && csMethod.CppElement is CppFunction cppFunction)
        {
            if (_docDefinitions.TryGetValue(VulkanDocTypeKind.Function, out var definitions) && definitions.TryGetValue(cppFunction.Name, out var definition))
            {
                description = definition.Description;

                // definition.Description;
                foreach (var docParameter in definition.Parameters)
                {
                    var xmlParamDoc = new CSharpParamComment(docParameter.Name)
                    {
                        Children =
                        {
                            docParameter.Description
                        }
                    };
                    additionalComments ??= [];
                    additionalComments.Add(xmlParamDoc);
                }

                if (definition.Return != null)
                {
                    additionalComments ??= [];
                    additionalComments.Add(definition.Return);
                }
            }
        }
        else if (element is CSharpEnum csEnum && element.CppElement is CppEnum cppEnum)
        {
            if (_docDefinitions.TryGetValue(VulkanDocTypeKind.Enum, out var definitions) && definitions.TryGetValue(cppEnum.Name, out var definition))
            {
                description = definition.Description;

                var enumItems = csEnum.Members.OfType<CSharpEnumItem>().ToList();
                foreach (var docParameter in definition.Parameters)
                {
                    var enumItem = enumItems.FirstOrDefault(f => f.Name == docParameter.Name);
                    if (enumItem != null)
                    {
                        enumItem.Comment = new CSharpFullComment()
                        {
                            Children =
                            {
                                new CSharpXmlComment("summary")
                                {
                                    Children =
                                    {
                                        docParameter.Description
                                    }
                                }
                            }
                        };
                    }
                }
            }
        }
        else if (element is CSharpStruct csStruct && element.CppElement is CppClass cppStruct)
        {
            if (_docDefinitions.TryGetValue(VulkanDocTypeKind.Struct, out var definitions) && definitions.TryGetValue(cppStruct.Name, out var definition))
            {
                description = definition.Description;

                var fields = csStruct.Members.OfType<CSharpField>().ToList();
                foreach (var docParameter in definition.Parameters)
                {
                    var field = fields.FirstOrDefault(f => f.Name == docParameter.Name);
                    if (field != null)
                    {
                        field.Comment = new CSharpFullComment()
                        {
                            Children =
                            {
                                new CSharpXmlComment("summary")
                                {
                                    Children =
                                    {
                                        docParameter.Description
                                    }
                                }
                            }
                        };
                    }
                }
            }
        }

        if (description != null)
        {
            var fullComment = new CSharpFullComment()
            {
                Children =
                {
                    new CSharpXmlComment("summary")
                    {
                        Children =
                        {
                            description
                        }
                    }
                }
            };

            if (additionalComments != null)
            {
                foreach (var additionalComment in additionalComments)
                {
                    fullComment.Children.Add(additionalComment);
                }
            }

            commentable.Comment = fullComment;
        }
    }

    private bool TryGetVulkanCommand(string name, [NotNullWhen(true)] out VulkanCommand? command)
    {
        if (_functionRegistry.TryGetValue(name, out command))
        {
            if (command.Alias != null)
            {
                command = _functionRegistry[command.Alias];
            }

            return true;
        }

        return false;
    }
  
    protected override IEnumerable<CppFunction> GetAdditionalExportedCppFunctions() => _extensionFunctions;

    private void LoadVulkanRegistry(string registryPath)
    {
        var doc = XDocument.Load(registryPath);
        LoadApiVersionsAndExtensions(doc);
        LoadVulkanParametersFromRegistry(doc);
    }

    private void AddVulkanVersionAndExtensionInfoToCSharpElement(CSharpElement element)
    {
        var cppElement = (ICppMember)element.CppElement!;
        var csElementComments = (ICSharpWithComment)element;

        if (_vulkanElementInfos.TryGetValue(cppElement.Name, out var info))
        {
            var fullComment = csElementComments.Comment as CSharpFullComment;
            if (fullComment == null)
            {
                fullComment = new CSharpFullComment();
                csElementComments.Comment = fullComment;
            }

            var remarks = (CSharpXmlComment?)fullComment.Children.FirstOrDefault(x => x is CSharpXmlComment xmlComment && xmlComment.TagName == "remarks");
            if (remarks == null)
            {
                remarks = new CSharpXmlComment("remarks");
                fullComment.Children.Add(remarks);
            }

            if (info.ApiVersion != null)
            {
                remarks.Children.Add(new CSharpXmlComment("para")
                {
                    IsInline = true,
                    Children =
                    {
                        new CSharpTextComment($"API Version: {info.ApiVersion}")
                    }
                });
            }

            if (info.Extension != null)
            {
                remarks.Children.Add(new CSharpXmlComment("para")
                {
                    IsInline = true,
                    Children =
                    {
                        new CSharpTextComment($"Extension: {info.Extension}")
                    }
                });
            }
        }
    }
    
    private void LoadApiVersionsAndExtensions(XDocument doc)
    {
        var features = doc.Descendants("feature");
        foreach (var feature in features)
        {
            var api = feature.Attribute("api")!.Value!;
            var version = feature.Attribute("number")!.Value!;
            if (!api.Split(",").Contains("vulkan"))
            {
                continue;
            }

            foreach (var type in feature.Descendants("type"))
            {
                var name = type.Attribute("name")!.Value!;
                var info = GetVulkanElementInfo(name);
                info.ApiVersion = version;
            }

            foreach (var command in feature.Descendants("command"))
            {
                var name = command.Attribute("name")!.Value!;
                var info = GetVulkanElementInfo(name);
                info.ApiVersion = version;
            }
        }

        var extensions = doc.Descendants("extensions").FirstOrDefault();
        if (extensions != null)
        {
            foreach (var extension in extensions.Elements("extension"))
            {
                var supportedApis = extension.Attribute("supported")!.Value!;
                if (!supportedApis.Split(",").Contains("vulkan"))
                {
                    continue;
                }

                var extensionName = extension.Attribute("name")!.Value!;

                foreach (var type in extension.Descendants("type"))
                {
                    var name = type.Attribute("name")!.Value!;
                    var info = GetVulkanElementInfo(name);
                    info.Extension = extensionName;
                }

                foreach (var command in extension.Descendants("command"))
                {
                    var name = command.Attribute("name")!.Value!;
                    var info = GetVulkanElementInfo(name);
                    info.Extension = extensionName;
                }
            }
        }
        
        VulkanElementInfo GetVulkanElementInfo(string name)
        {
            if (_vulkanElementInfos.TryGetValue(name, out var info))
            {
                return info;
            }
            info = new VulkanElementInfo();
            _vulkanElementInfos.Add(name, info);
            return info;
        }
    }
    
    private void LoadVulkanParametersFromRegistry(XDocument doc)
    {
        var commands = doc.Descendants("commands").First();

        int commandCount = 0;
        foreach (var xmlCommand in commands.Elements("command"))
        {
            string name;
            string? alias = xmlCommand.Attribute("alias")?.Value;
            if (alias != null)
            {
                name = xmlCommand.Attribute("name")!.Value;
                _functionRegistry.Add(name, new VulkanCommand(name) { Alias = alias });
                continue;
            }

            if (xmlCommand.Attribute("api") != null && xmlCommand.Attribute("api")!.Value != "vulkan")
            {
                // Process only vulkan api commands
                continue;
            }

            string? successcodes = xmlCommand.Attribute("successcodes")?.Value;
            string? errorcodes = xmlCommand.Attribute("errorcodes")?.Value;

            var proto = xmlCommand.Element("proto")!;
            name = proto.Element("name")!.Value;
            var parameters = xmlCommand.Elements("param").ToList();

            var vulcanCommand = new VulkanCommand(name)
            {
                ReturnSuccessCodes = successcodes?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
                ReturnErrorCodes = errorcodes?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
            };
            for (var parameterIndex = 0; parameterIndex < parameters.Count; parameterIndex++)
            {
                var parameter = parameters[parameterIndex];
                var paramName = parameter.Element("name")!.Value;
                var optionalAttr = parameter.Attribute("optional");
                var paramOptional = optionalAttr?.Value switch
                {
                    "true" => VulkanCommandOptional.True,
                    "false,true" => VulkanCommandOptional.Both,
                    _ => VulkanCommandOptional.None,
                };
                var lenAttr = parameter.Attribute("len");
                int lengthParameterIndex = -1;
                var parameterKind = VulkanCommandParameterKind.Standard;
                if (lenAttr != null)
                {
                    var lenValue = lenAttr.Value;
                    if (lenValue.StartsWith("null-"))
                    {
                        parameterKind = VulkanCommandParameterKind.NullTerminated;
                    }
                    else
                    {
                        parameterKind = VulkanCommandParameterKind.Array; // we still record this to record that a parameter is an array
                        lengthParameterIndex = parameters.FindIndex(element => string.Equals(element.Descendants("name").First().Value, lenValue, StringComparison.Ordinal));
                        if (lengthParameterIndex < 0)
                        {
                            Console.WriteLine($"Warning, special length parameter `{lenValue}` not handled for {name}");
                        }
                        else
                        {
                            var lengthParameter = vulcanCommand.Parameters[lengthParameterIndex];
                            lengthParameter.Kind = VulkanCommandParameterKind.Length;
                            lengthParameter.LinkedArrayParameters ??= [];
                            lengthParameter.LinkedArrayParameters.Add(parameterIndex);
                        }
                    }
                }

                vulcanCommand.Parameters.Add(new VulkanCommandParameter(parameterIndex, paramName, paramOptional, lengthParameterIndex)
                {
                    Kind = parameterKind
                });
            }

            _functionRegistry.Add(name, vulcanCommand);

            //Console.WriteLine($"Function {name} {parameters.Count}");
            commandCount++;
        }


        var types = doc.Descendants("types").FirstOrDefault();

        if (types != null)
        {
            // Collect default values for struct fields
            foreach (var type in types.Elements("type"))
            {
                if (type.Attribute("category")?.Value != "struct")
                {
                    continue;
                }

                var structName = type.Attribute("name")!.Value;

                foreach (var member in type.Elements("member"))
                {
                    var attr = member.Attribute("values");
                    if (attr != null && attr.Value.StartsWith("VK_"))
                    {
                        if (!_mapStructToFieldsWithDefaultValue.TryGetValue(structName, out var fields))
                        {
                            fields = new();
                            _mapStructToFieldsWithDefaultValue.Add(structName, fields);
                        }

                        var fieldName = member.Element("name")!.Value;
                        fields[fieldName] = attr.Value;
                    }
                }
            }
        }

        Console.WriteLine($"Total commands: {commandCount} processed from the registry");
    }

    [GeneratedRegex(@"^(\d+\.\d+\.\d+)")]
    private static partial Regex ParseVulkanVersion();

    private async Task DownloadAndProcessManPages()
    {
        var package = Apk.GetPackages(ApkManager.DefaultArch)["vulkan-headers"];
        var match = ParseVulkanVersion().Match(package.Version);
        if (!match.Success)
        {
            throw new InvalidOperationException($"Cannot parse Vulkan version from {package.Version}");
        }
        var version = match.Groups[1].Value;

        string vulkanZipName = $"vulkan-{version}.zip";
        var vulkanZipFilePath = Path.Combine(AppContext.BaseDirectory, vulkanZipName);
        if (!File.Exists(vulkanZipFilePath))
        {
            var url = $"https://github.com/KhronosGroup/Vulkan-Docs/archive/refs/tags/v{version}.zip";
            Console.WriteLine($"Downloading Vulkan-Docs v{version} from ");

            using var client = new HttpClient();
            using var response = await client.GetAsync(url);
            await using var fileStream = File.OpenWrite(vulkanZipFilePath);
            await response.Content.CopyToAsync(fileStream);
        }

        Console.WriteLine($"Loading Vulkan-Docs v{version}");
        ProcessVulkanDocsZip(vulkanZipFilePath);
    }

    private void ProcessVulkanDocsZip(string zipFilePath)
    {
        using var zipStream = new ZipArchive(File.OpenRead(zipFilePath));
        foreach (var entry in zipStream.Entries)
        {
            if (entry.FullName.Contains("/chapters/") && entry.FullName.EndsWith(".adoc", StringComparison.Ordinal))
            {
                using var stream = entry.Open();
                using var reader = new StreamReader(stream);
                ProcessVulkanDoc(reader);
            }
        }
    }

    [GeneratedRegex(@"^\s+\*\s+(pname|ename):(?<name>\w+)\s+(?<content>.+)")]
    private static partial Regex RegexParseParam();

    private void ProcessVulkanDoc(StreamReader reader)
    {
        string? line = null;

        var paramDocBuilder = new StringBuilder();

        while ((line = reader.ReadLine()) != null)
        {
            if (!line.StartsWith("[open,", StringComparison.Ordinal))
            {
                continue;
            }

            var desc = line;
            line = reader.ReadLine();
            if (line is null || !line.StartsWith("--", StringComparison.Ordinal))
            {
                continue;
            }

            var dict = ParseDesc(desc);
            if (dict.ContainsKey("refpage") && dict.ContainsKey("type"))
            {
                var name = dict["refpage"]!;
                var kind = ParseKindFromType(dict["type"]!);

                if (kind == VulkanDocTypeKind.Unknown)
                {
                    Console.WriteLine($"Doc (unknown kind): {desc}");
                    continue;
                }

                if (!_docDefinitions.TryGetValue(kind, out var definitions))
                {
                    definitions = new VulkanDocDefinitions();
                    _docDefinitions[kind] = definitions;
                }

                if (!definitions.TryGetValue(name, out var definition))
                {
                    definition = new VulkanDocDefinition(name);
                    definitions[name] = definition;
                }

                var isFunction = kind == VulkanDocTypeKind.Function;
                _functionRegistry.TryGetValue(name, out var command);
                definition.Description = TransformTextContentToCSharpComment(dict.TryGetValue("desc", out var value) ? value! : string.Empty, isFunction);

                // The following code will try to parse the parameters of the function, like this example:

                // include::{generated}/api/protos/vkCmdClearColorImage.adoc[]
                // 
                //   * pname:commandBuffer is the command buffer into which the command will be
                //     recorded.
                //   * pname:image is the image to be cleared.
                //   * pname:imageLayout specifies the current layout of the image subresource
                //     ranges to be cleared, and must: be
                // ifdef::VK_KHR_shared_presentable_image[]
                //     ename:VK_IMAGE_LAYOUT_SHARED_PRESENT_KHR,
                // endif::VK_KHR_shared_presentable_image[]
                //     ename:VK_IMAGE_LAYOUT_GENERAL or
                //     ename:VK_IMAGE_LAYOUT_TRANSFER_DST_OPTIMAL.
                //   * pname:pColor is a pointer to a slink:VkClearColorValue structure
                //     containing the values that the image subresource ranges will be cleared
                //     to (see <<clears-values>> below).
                //   * pname:rangeCount is the number of image subresource range structures in
                //     pname:pRanges.
                //   * pname:pRanges is a pointer to an array of slink:VkImageSubresourceRange
                //     structures describing a range of mipmap levels, array layers, and
                //     aspects to be cleared, as described in <<resources-image-views,Image
                //     Views>>.
                // 
                // Each specified range in pname:pRanges is cleared to the value specified by
                // pname:pColor.

                string? currentParameterName = null;
                while ((line = reader.ReadLine()) != null)
                {
                    // End of section
                    if (line.StartsWith("--", StringComparison.Ordinal))
                    {
                        break;
                    }

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        if (currentParameterName != null)
                        {
                            AddVkParameter(command, currentParameterName, definition, isFunction);
                        }

                        currentParameterName = null;
                        paramDocBuilder.Clear();
                        continue;
                    }

                    var match = RegexParseParam().Match(line);
                    if (!match.Success)
                    {
                        if (currentParameterName != null)
                        {
                            if (!line.StartsWith("ifdef::", StringComparison.Ordinal) && !line.StartsWith("endif::", StringComparison.Ordinal))
                            {
                                paramDocBuilder.Append(' ');
                                paramDocBuilder.Append(line.Trim());
                            }
                        }

                        continue;
                    }

                    if (currentParameterName != null)
                    {
                        AddVkParameter(command, currentParameterName, definition, isFunction);
                    }

                    currentParameterName = match.Groups["name"].Value;
                    paramDocBuilder.Clear();
                    paramDocBuilder.Append(match.Groups["content"].Value);
                }

                if (currentParameterName != null)
                {
                    AddVkParameter(command, currentParameterName, definition, isFunction);
                }

                if (isFunction && command != null)
                {
                    if (command.ReturnSuccessCodes != null || command.ReturnErrorCodes != null)
                    {
                        var remarks = new CSharpXmlComment("remarks");

                        AddReturnCodes(remarks, "On success, this command returns: ", command.ReturnSuccessCodes);
                        //AddReturnCodes(remarks, "", command.ReturnSuccessCodes);
                        AddReturnCodes(remarks, "On failure, this command returns: ", command.ReturnErrorCodes);
                        definition.Return = remarks;
                    }
                }
            }
            else
            {
                Console.WriteLine($"Doc (unknown): {desc}");
            }
        }

        void AddVkParameter(VulkanCommand? command, string currentParameterName, VulkanDocDefinition definition, bool isFunction)
        {
            var paramDesc = paramDocBuilder.ToString();

            if (command != null)
            {
                var parameterIndex = command.Parameters.FindIndex(p => p.Name == currentParameterName);

                if (parameterIndex >= 0)
                {
                    var vkParameter = command.Parameters[parameterIndex];
                    if (vkParameter.Optional == VulkanCommandOptional.True)
                    {
                        paramDesc += " This parameter is optional.";
                    }
                }
            }
            definition.Parameters.Add(new VulkanDocParameter(currentParameterName, TransformTextContentToCSharpComment(paramDesc, isFunction)));
        }

        void AddReturnCodes(CSharpXmlComment remarks, string context, string[]? codes)
        {
            if (codes is null) return;
            
            var listSuccessCodes = new CSharpXmlComment("list")
            {
                Attributes = { new CSharpXmlAttribute("type", "bullet") },
            };
            remarks.Children.Add(listSuccessCodes);
            
            listSuccessCodes.Children.Add(new CSharpXmlComment("listheader")
            {
                IsInline = true,
                Children =
                {
                    new CSharpXmlComment("description")
                    {
                        IsInline = true,
                        Children =
                        {
                            new CSharpTextComment(context)
                        }
                    }
                }
            });

            foreach (var code in codes)
            {
                listSuccessCodes.Children.Add(new CSharpXmlComment("item")
                {
                    IsInline = true,
                    Children =
                    {
                        new CSharpXmlComment("description")
                        {
                            IsInline = true,
                            Children =
                            {
                                new CSharpXmlComment("c")
                                {
                                    IsInline = true,
                                    Children = { new CSharpTextComment(code) }
                                }
                            }
                        }
                    }
                });
            }
        }
    }

    [GeneratedRegex(@"(?<key>\w+)(='(?<value>(?:\\'|[^'])*)')?")]
    private static partial Regex RegexParseDesc();

    private Dictionary<string, string?> ParseDesc(string desc)
    {
        // [open,refpage='vkGetPhysicalDeviceImageFormatProperties',desc='Lists physical device\'s image format capabilities',type='protos']

        desc = desc.Trim('[', ']');
        var dict = new Dictionary<string, string?>();

        int index = 0;
        while (true)
        {
            var match = RegexParseDesc().Match(desc, index);
            if (!match.Success)
            {
                break;
            }

            var key = match.Groups["key"].Value;
            string? value = null;
            var valueMatch = match.Groups["value"];
            if (valueMatch.Success)
            {
                value = valueMatch.Value;
                value = value.Replace("\\'", "'");
            }
            dict[key] = value;

            index += match.Length;
            if (index >= desc.Length)
            {
                break;
            }

            if (desc[index] == ',')
            {
                index++;
            }
        }

        return dict;
    }

    [GeneratedRegex(@"<<(?<id>[^,]+),(?<content>.*?)>>")]
    private static partial Regex ParseSpecialTextMarkers();
    
    [GeneratedRegex(@"\b(?<link>ename|sname|fname|elink|pname|slink|flink|code):(?<target>\w+)")]
    private static partial Regex ParseDocTextLinks();

    private static CSharpComment TransformTextContentToCSharpComment(string text, bool isInFunction)
    {
        // Enum => ename:VK_IMAGE_LAYOUT_SHARED_PRESENT_KHR
        // Struct => sname:VkDeviceMemory
        // Function => fname:vkCmdClearAttachments
        // Struct field => slink:VkImageSubresourceRange::pname:aspectMask
        // And similarly for elink, slink, flink

        // We substitute the special markers <<id,content>> with just the content
        text = ParseSpecialTextMarkers().Replace(text, m =>
        {
            var content = m.Groups["content"].Value;
            return content.Trim();
        });
        
        var regex = ParseDocTextLinks();

        int index = 0;

        var group = new CSharpGroupComment();

        while (true)
        {
            var match = regex.Match(text, index);
            if (!match.Success)
            {
                break;
            }

            var length = match.Index - index;
            if (length > 0)
            {
                group.Children.Add(new CSharpTextComment(text.Substring(index, length)));
            }
            index = match.Index + match.Length;

            var link = match.Groups["link"].Value;
            var target = match.Groups["target"].Value;
            string memberTarget = string.Empty;

            if (index + 2 <= text.Length && text[index] == ':' && text[index] == ':')
            {
                index += 2;
                match = regex.Match(text, index);
                if (!match.Success)
                {
                    throw new InvalidOperationException($"Invalid link format in {text}, expecting link after :: at index {index}");
                }

                memberTarget = $".{match.Groups["target"].Value}";
                index = match.Index + match.Length;
            }

            string? docTarget = null;
            switch (link)
            {
                case "code":
                    docTarget = target;
                    break;
                case "ename":
                case "sname":
                case "elink":
                case "slink":
                    docTarget = $"T:{target}{memberTarget}";
                    break;
                case "pname":
                    docTarget = isInFunction ? target : $"M:{target}";
                    break;
                case "fname":
                case "flink":
                    docTarget = $"M:{target}";
                    break;
            }

            if (docTarget != null)
            {
                if (link == "code")
                {
                    group.Children.Add(new CSharpXmlComment("c")
                    {
                        IsInline = true,
                        Children =
                        {
                            new CSharpTextComment(docTarget)
                        }
                    });
                }
                else if (link == "pname" && isInFunction)
                {
                    group.Children.Add(new CSharpXmlComment("paramref")
                    {
                        IsInline = true,
                        IsSelfClosing = true,
                        Attributes =
                        {
                            new CSharpXmlAttribute("name", docTarget)
                        }
                    });
                }
                else
                {
                    group.Children.Add(new CSharpXmlComment("see")
                    {
                        IsInline = true,
                        IsSelfClosing = true,
                        Attributes =
                        {
                            new CSharpXmlAttribute("cref", docTarget)
                        }
                    });
                }
            }
        }

        if (index < text.Length)
        {
            group.Children.Add(new CSharpTextComment(text.Substring(index)));
        }

        // Capitalize the first letter of the comment
        if (group.Children.FirstOrDefault() is CSharpTextComment csTextComment)
        {
            if (csTextComment.Text.StartsWith("is ", StringComparison.Ordinal))
            {
                if (csTextComment.Text.Length > 3)
                {
                    csTextComment.Text = char.ToUpper(csTextComment.Text[3]) + csTextComment.Text[4..];
                }
                else
                {
                    group.Children.RemoveAt(0);
                }
            }
            else if (csTextComment.Text.Length > 1)
            {
                csTextComment.Text = char.ToUpper(csTextComment.Text[0]) + csTextComment.Text[1..];
            }
        }

        return group;
    }

    private static bool IsValidPointerTypeForSpan(CppType type)
    {
        return type is CppPointerType { ElementType: not CppPointerType } pointerType;
    }

    private static bool IsValidPointerTypeToProcess(CppType type, [NotNullWhen(true)] out CppType? elementType)
    {
        elementType = null;
        if (type is CppPointerType pointerType)
        {
            elementType = pointerType.ElementType;

            // void* is not supported
            if (pointerType.ElementType is CppPrimitiveType primitiveType && primitiveType.Kind == CppPrimitiveKind.Void)
            {
                elementType = null;
                return false;
            }
            // const void* is not supported
            else if (pointerType.ElementType is CppQualifiedType qualifiedType)
            {
                elementType = qualifiedType.ElementType;
                if (((elementType is CppPrimitiveType anotherPrimitiveType && anotherPrimitiveType.Kind == CppPrimitiveKind.Void) || (elementType is CppPointerType)))
                {
                    elementType = null;
                    return false;
                }
            }

            // We keep VkAllocationCallbacks* as a pointer
            return true;
            //return elementType is not ICppMember member || member.Name != "VkAllocationCallbacks";
        }

        return false;
    }

    private static bool IsValidPointerTypeToProcess(CppType type) => IsValidPointerTypeToProcess(type, out _);
    
    private class VulkanDocDefinitions() : Dictionary<string, VulkanDocDefinition>(StringComparer.Ordinal);

    private record VulkanDocDefinition(string Name)
    {
        public CSharpComment? Description { get; set; }

        public VulkanDocTypeKind Kind { get; set; }

        public List<VulkanDocParameter> Parameters { get; } = new();

        public CSharpComment? Return { get; set; }
    }

    private record VulkanDocParameter(string Name, CSharpComment Description);

    private enum VulkanDocTypeKind
    {
        Unknown = 0,
        Function,
        Enum,
        Flags,
        Struct,
        Handle,
        FunctionPointer,
    }

    private static VulkanDocTypeKind ParseKindFromType(string type)
    {
        return type switch
        {
            "protos" => VulkanDocTypeKind.Function,
            "enums" => VulkanDocTypeKind.Enum,
            "flags" => VulkanDocTypeKind.Flags,
            "structs" => VulkanDocTypeKind.Struct,
            "handles" => VulkanDocTypeKind.Handle,
            "funcpointers" => VulkanDocTypeKind.FunctionPointer,
            _ => VulkanDocTypeKind.Unknown,
        };
    }

    private record VulkanCommand(string Name)
    {
        public string? Alias { get; init; }

        public List<VulkanCommandParameter> Parameters { get; } = [];

        public string[]? ReturnSuccessCodes { get; init; }

        public string[]? ReturnErrorCodes { get; init; }
    }

    private record VulkanCommandParameter(int Index, string Name, VulkanCommandOptional Optional, int LengthParameterIndex)
    {
        /// <summary>
        /// Gets or sets the kind of this parameter
        /// </summary>
        public VulkanCommandParameterKind Kind { get; set; }

        /// <summary>
        /// If <see cref="M:Kind"/> is <see cref="VulkanCommandParameterKind.Length"/>, this is the list of linked array parameters
        /// </summary>
        public List<int>? LinkedArrayParameters { get; set; }
    }

    private enum VulkanCommandParameterKind
    {
        Standard,
        Length,
        Array,
        NullTerminated,
    }

    private enum VulkanCommandOptional
    {
        None,
        True,
        Both,
    }

    private record VulkanElementInfo
    {
        public string? ApiVersion { get; set; }

        public string? Extension { get; set; }
    }
}