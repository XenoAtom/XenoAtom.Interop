// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using CppAst;
using CppAst.CodeGen.CSharp;

namespace XenoAtom.Interop.CodeGen.vulkan;

internal partial class VulkanGenerator(LibDescriptor descriptor) : GeneratorBase(descriptor)
{
    private readonly List<CppFunction> _extensionFunctions = new();
    private readonly Dictionary<string, VulkanCommand> _registry = new();
    private readonly Dictionary<VulkanDocTypeKind, VulkanDocDefinitions> _docDefinitions = new();

    public override async Task Initialize(ApkManager apkHelper)
    {
        await base.Initialize(apkHelper);

        await Apk.ExtractFolders("vulkan-headers", "vulkan-registry", "usr/share/vulkan/registry/");

        var vkXml = Path.Combine(AppContext.BaseDirectory, Apk.GetSubCacheDirectory("vulkan-registry", "main"), "vk.xml");
        if (!File.Exists(vkXml))
        {
            throw new FileNotFoundException($"Cannot find Vulkan registry: {vkXml}");
        }

        LoadVulkanParametersFromRegistry(vkXml);

        await DownloadAndProcessManPages();
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
                e => e.MapAll<CppParameter>().CppAction(ProcessParameters)
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

        foreach (var csEnum in csCompilation.AllEnums)
        {
            ApplyDocumentation(csEnum);
        }


        foreach (var csStruct in csCompilation.AllStructs)
        {
            ApplyDocumentation(csStruct);

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

            // Apply doc to the function
            ApplyDocumentation(csFunction);
            
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
                    additionalComments??= new List<CSharpComment>();
                    additionalComments.Add(xmlParamDoc);
                }
            }
        }
        else if (element is CSharpEnum && element.CppElement is CppEnum cppEnum)
        {
            if (_docDefinitions.TryGetValue(VulkanDocTypeKind.Enum, out var definitions) && definitions.TryGetValue(cppEnum.Name, out var definition))
            {
                description = definition.Description;
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


    private void ProcessParameters(CSharpConverter converter, CppElement element)
    {
        var cppParameter = (CppParameter)element;
        var function = cppParameter.Parent as CppFunction;
        if (function == null)
        {
            // function type
            return;
        }
        var parameterIndex = function.Parameters.IndexOf(cppParameter);
        if (!_registry.TryGetValue(function.Name, out var command))
        {
            Console.WriteLine($"Warning: Function {function.Name} not found in the registry");
            return;
        }

        if (command.Alias != null)
        {
            command = _registry[command.Alias];
        }
        
        if (command.Parameters.Count != function.Parameters.Count)
        {
            Console.WriteLine($"Warning: Function {function.Name} has different number of parameters in the registry");
            return;
        }

        var vulkanParameter = command.Parameters[parameterIndex];

        if (vulkanParameter.Kind == VulkanCommandParameterKind.Array)
        {
            converter.CurrentParameterRefKind = CSharpRefKind.None;
        }
        else if (cppParameter.Type is CppPointerType pointerType)
        {
            if (pointerType.ElementType is CppQualifiedType qualified && qualified.Qualifier == CppTypeQualifier.Const)
            {
                if (qualified.ElementType is CppPrimitiveType primitiveType && (primitiveType.Kind == CppPrimitiveKind.Void || primitiveType.Kind == CppPrimitiveKind.Char || primitiveType.Kind == CppPrimitiveKind.UnsignedChar))
                {
                    converter.CurrentParameterRefKind = CSharpRefKind.None;
                }
                else
                {
                    converter.CurrentParameterRefKind = CSharpRefKind.In;
                }
            }
            else
            {
                if (pointerType.ElementType is CppPrimitiveType primitiveType && (primitiveType.Kind == CppPrimitiveKind.Void || primitiveType.Kind == CppPrimitiveKind.Char || primitiveType.Kind == CppPrimitiveKind.UnsignedChar))
                {
                    converter.CurrentParameterRefKind = CSharpRefKind.None;
                }
                else
                {
                    // length parameters by pointers should be ref
                    converter.CurrentParameterRefKind = vulkanParameter.Kind == VulkanCommandParameterKind.Length ? CSharpRefKind.Ref : CSharpRefKind.Out;
                }
            }
        }
    }

    protected override IEnumerable<CppFunction> GetAdditionalExportedCppFunctions() => _extensionFunctions;

    private void LoadVulkanParametersFromRegistry(string registryPath)
    {
        var doc = XDocument.Load(registryPath);

        var commands = doc.Descendants("commands").First();

        int commandCount = 0;
        foreach (var xmlCommand in commands.Elements("command"))
        {
            string name;
            string? alias = xmlCommand.Attribute("alias")?.Value;
            if (alias != null)
            {
                name = xmlCommand.Attribute("name")!.Value;
                _registry.Add(name, new VulkanCommand(name) { Alias = alias });
                continue;
            }

            if (xmlCommand.Attribute("api") != null && xmlCommand.Attribute("api")!.Value != "vulkan")
            {
                // Process only vulkan api commands
                continue;
            }

            var proto = xmlCommand.Element("proto")!;
            name = proto.Element("name")!.Value;
            var parameters = xmlCommand.Elements("param").ToList();

            var vulcanCommand = new VulkanCommand(name);
            foreach (var parameter in parameters)
            {
                var paramName = parameter.Element("name")!.Value;
                var optionalAttr = parameter.Attribute("optional");
                var paramOptional = optionalAttr?.Value switch
                {
                    "true" => VulkanCommandOptional.True,
                    "false,true" => VulkanCommandOptional.Both,
                    _ => VulkanCommandOptional.None,
                };
                var lenAttr = parameter.Attribute("len");
                int parameterIndex = -1;
                var parameterKind = VulkanCommandParameterKind.Standard;
                if (lenAttr != null)
                {
                    var lenValue = lenAttr.Value;
                    if (!lenValue.StartsWith("null-"))
                    {
                        parameterKind = VulkanCommandParameterKind.Array; // we still record this to record that a parameter is an array
                        parameterIndex = parameters.FindIndex(element => string.Equals(element.Descendants("name").First().Value, lenValue, StringComparison.Ordinal));
                        if (parameterIndex < 0)
                        {
                            Console.WriteLine($"Warning, special length parameter `{lenValue}` not handled for {name}");
                        }
                        else
                        {
                            vulcanCommand.Parameters[parameterIndex].Kind = VulkanCommandParameterKind.Length;
                        }
                    }
                }

                vulcanCommand.Parameters.Add(new VulkanCommandParameter(paramName, paramOptional, parameterIndex)
                {
                    Kind = parameterKind
                });
            }

            _registry.Add(name, vulcanCommand);

            //Console.WriteLine($"Function {name} {parameters.Count}");
            commandCount++;
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

    [GeneratedRegex(@"^\s+\*\s+pname:(?<pname>\w+)\s+(?<content>.+)")]
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

                while ((line = reader.ReadLine()) != null)
                {
                    // End of section
                    if (line.StartsWith("--", StringComparison.Ordinal))
                    {
                        break;
                    }

                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    line = reader.ReadLine()!;
                    var match = RegexParseParam().Match(line);
                    if (!match.Success)
                    {
                        continue;
                    }

                    string? currentParameterName = null;
                    while (!string.IsNullOrWhiteSpace(line))
                    {
                        match = RegexParseParam().Match(line);
                        if (match.Success)
                        {
                            if (currentParameterName != null)
                            {
                                var paramDesc = paramDocBuilder.ToString();
                                definition.Parameters.Add(new VulkanDocParameter(currentParameterName, TransformTextContentToCSharpComment(paramDesc, isFunction)));
                            }

                            currentParameterName = match.Groups["pname"].Value;
                            paramDocBuilder.Clear();
                            paramDocBuilder.Append(match.Groups["content"].Value);
                        }
                        else if (!line.StartsWith("ifdef::", StringComparison.Ordinal) && !line.StartsWith("endif::", StringComparison.Ordinal))
                        {
                            paramDocBuilder.Append(' ');
                            paramDocBuilder.Append(line.Trim());
                        }

                        line = reader.ReadLine()!;
                    }

                    if (currentParameterName != null)
                    {
                        var paramDesc = paramDocBuilder.ToString();
                        definition.Parameters.Add(new VulkanDocParameter(currentParameterName, TransformTextContentToCSharpComment(paramDesc, isFunction)));
                    }
                }
            }
            else
            {
                Console.WriteLine($"Doc (unknown): {desc}");
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

    [GeneratedRegex(@"\b(?<link>ename|sname|fname|elink|pname|slink|flink):(?<target>\w+)")]
    private static partial Regex ParseDocTextLinks();

    private static CSharpComment TransformTextContentToCSharpComment(string text, bool isInFunction)
    {
        // Enum => ename:VK_IMAGE_LAYOUT_SHARED_PRESENT_KHR
        // Struct => sname:VkDeviceMemory
        // Function => fname:vkCmdClearAttachments
        // Struct field => slink:VkImageSubresourceRange::pname:aspectMask
        // And simiar with elink, slink, flink

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
                if (!match.Success) throw new InvalidOperationException($"Invalid link format in {text}, expecting link after :: at index {index}");

                memberTarget = $".{match.Groups["target"].Value}";
                index = match.Index + match.Length;
            }

            string? docTarget = null;
            switch (link)
            {
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
                if (link == "pname" && isInFunction)
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

    private class VulkanDocDefinitions() : Dictionary<string, VulkanDocDefinition>(StringComparer.Ordinal);

    private record VulkanDocDefinition(string Name)
    {
        public CSharpComment? Description { get; set; }

        public VulkanDocTypeKind Kind { get; set; }

        public List<VulkanDocParameter> Parameters { get; } = new();
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
        public string? Alias { get; set; }

        public List<VulkanCommandParameter> Parameters { get; } = new();
    }

    private record VulkanCommandParameter(string Name, VulkanCommandOptional Optional, int LengthParameterIndex)
    {
        public VulkanCommandParameterKind Kind { get; set; }
    }

    private enum VulkanCommandParameterKind
    {
        Standard,
        Length,
        Array,
    }

    private enum VulkanCommandOptional
    {
        None,
        True,
        Both,
    }
}