// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CppAst;
using CppAst.CodeGen.Common;
using CppAst.CodeGen.CSharp;
using Zio.FileSystems;

namespace XenoAtom.Interop.CodeGen;

/// <summary>
/// Base class for a code generator using the CppAst and CppAst.CodeGen library.
/// </summary>
public abstract class GeneratorBase
{
    private ApkManager? _apkManager;
    private string? _packageDescriptionMarkdown;

    protected GeneratorBase(LibDescriptor descriptor)
    {
        Descriptor = descriptor;
        RepositoryRootFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $@"..", "..", "..", "..", "..", "..", ".."));
        // This is a protection to make sure that we are in the right folder
        var srcFolder = Path.Combine(RepositoryRootFolder, "src");
        if (!Directory.Exists(srcFolder) || !File.Exists(Path.Combine(srcFolder, "XenoAtom.Interop.sln")))
        {
            throw new DirectoryNotFoundException($"The source folder `{srcFolder}` doesn't exist");
        }
        GitHubWorkflowsFolder = Path.Combine(RepositoryRootFolder, ".github", "workflows");
        if (!Directory.Exists(GitHubWorkflowsFolder))
        {
            throw new DirectoryNotFoundException($"The GitHub workflows folder `{GitHubWorkflowsFolder}` doesn't exist");
        }

        TemplateFolder = Path.Combine(srcFolder, "codegen", "XenoAtom.Interop.CodeGen", "Templates");
        if (!Directory.Exists(TemplateFolder))
        {
            throw new DirectoryNotFoundException($"The template folder `{TemplateFolder}` doesn't exist");
        }
        LibraryFolder = Path.Combine(srcFolder, LibName);
        GeneratedFolder = Path.Combine(LibraryFolder, $"XenoAtom.Interop.{LibName}", "generated");
    }

    public LibDescriptor Descriptor { get; }

    public string? NativeVersion { get; private set; }

    public string LibName => Descriptor.Name;

    public bool IsCommonLib => LibName == "common";

    public string ManagedPackageName => IsCommonLib ? "XenoAtom.Interop" : $"XenoAtom.Interop.{LibName}";

    public string RepositoryRootFolder { get; }

    public string GitHubWorkflowsFolder { get; }

    public string TemplateFolder { get; }

    public string LibraryFolder { get; }

    public string GeneratedFolder { get; }
    
    [System.Diagnostics.DebuggerHidden]
    public ApkManager Apk => _apkManager ?? throw new InvalidOperationException("ApkManager is not set. Run through generate");

    public async Task Initialize(ApkManager apkHelper)
    {
        _apkManager = apkHelper;

        await DownloadApkDependencies();

        if (!IsCommonLib && Descriptor.ApkDeps.Length > 0)
        {
            var packageInfo = Apk.GetPackages(ApkManager.DefaultArch)[Descriptor.ApkDeps[0]];
            var version = packageInfo.Version;
            version = Regex.Replace(version, "-r.*$", string.Empty);
            NativeVersion = version;
        }

        GenerateGitHubWorkflows();

        if (!Directory.Exists(LibraryFolder))
        {
            Directory.CreateDirectory(LibraryFolder);
        }

        if (Descriptor.HasGeneratedFolder)
        {
            if (!Directory.Exists(GeneratedFolder))
            {
                Directory.CreateDirectory(GeneratedFolder);
            }
        }
    }

    public async Task Run()
    {
        var csCompilation = await Generate();

        GenerateMarkdownDescription(csCompilation);

        GenerateLibraryProjectStructure();
        
        if (csCompilation == null)
        {
            return;
        }

        var fs = new PhysicalFileSystem();
        {
            var subfs = new SubFileSystem(fs, fs.ConvertPathFromInternal(GeneratedFolder));
            var codeWriter = new CodeWriter(new CodeWriterOptions(subfs));
            csCompilation.DumpTo(codeWriter);
        }
    }

    protected virtual string? GetUrlDocumentationForCppFunction(CppFunction cppFunction)
    {
        return null;
    }

    protected abstract Task<CSharpCompilation?> Generate();

    private void GenerateGitHubWorkflows()
    {
        // Generate GitHub workflows
        foreach (var file in Directory.EnumerateFiles(TemplateFolder, "ci_*.yml"))
        {
            var workflow = ReadTemplateFileAndPatchTemplate(file);
            var outputFileName = PatchFileName(Path.GetFileName(file));
            var outputFile = Path.Combine(GitHubWorkflowsFolder, outputFileName);

            var fileExisting = File.Exists(outputFile);
            if (fileExisting)
            {
                var existingWorkflow = File.ReadAllText(outputFile);
                if (existingWorkflow == workflow)
                {
                    continue;
                }
            }
            
            Console.WriteLine($"{(fileExisting ? "Updating" : "Generating")} GitHub workflow `{Path.GetRelativePath(RepositoryRootFolder, outputFile)}`");
            File.WriteAllText(outputFile, workflow);
        }
    }

    private void GenerateLibraryProjectStructure()
    {
        var libNameTemplateFolder = Path.Combine(TemplateFolder, "LIBNAME");

        foreach (var path in Directory.EnumerateFileSystemEntries(libNameTemplateFolder, "*.*", SearchOption.AllDirectories))
        {
            var directoryExists = Directory.Exists(path);
            if (directoryExists)
            {
                var relativePath = PatchFileName(Path.GetRelativePath(libNameTemplateFolder, path));
                var outputPath = Path.Combine(LibraryFolder, relativePath);
                if (!Directory.Exists(outputPath))
                {
                    Console.WriteLine($"Creating project directory :`{Path.GetRelativePath(RepositoryRootFolder, outputPath)}`");
                    Directory.CreateDirectory(outputPath);
                }
                continue;
            }

            var file = path;
            var content = ReadTemplateFileAndPatchTemplate(file);
            var outputFileName = PatchFileName(Path.GetRelativePath(libNameTemplateFolder, file));
            var outputFile = Path.Combine(LibraryFolder, outputFileName);

            var fileExisting = File.Exists(outputFile);
            if (fileExisting)
            {
                // We don't override existing csproj or cs files in projects
                if (outputFile.EndsWith(".csproj") || outputFile.EndsWith(".cs"))
                {
                    continue;
                }

                var existingContent = File.ReadAllText(outputFile);
                if (existingContent == content)
                {
                    continue;
                }
            }

            Console.WriteLine($"{(fileExisting?"Updating":"Generating")} project file:`{Path.GetRelativePath(RepositoryRootFolder, outputFile)}`");
            File.WriteAllText(outputFile, content);
        }
        
    }

    private async Task DownloadApkDependencies()
    {
        foreach (var apkDep in Descriptor.ApkDeps)
        {
            await Apk.EnsureIncludes(apkDep);
        }
    }

    private void GenerateMarkdownDescription(CSharpCompilation? csCompilation)
    {
        var builder = new StringBuilder();
        if (Descriptor.CppDescription != null)
        {
            builder.Append(Descriptor.CppDescription);
        }

        if (!IsCommonLib)
        {
            builder.Append($" For more information, see [{Descriptor.Name}]({Descriptor.Url}) website.");
        }

        builder.AppendLine();
        builder.AppendLine("## 💻 Usage");
        builder.AppendLine();
        if (!IsCommonLib)
        {
            builder.AppendLine($"After installing the package, you can access the library through the static class `{ManagedPackageName}`.");
            builder.AppendLine();
            if (Descriptor.UrlDocumentation != null)
            {
                builder.AppendLine($"For more information, see the official documentation at {Descriptor.UrlDocumentation}.");
                builder.AppendLine();
            }
        }

        if (Descriptor.UsageInCSharp != null)
        {
            builder.AppendLine(Descriptor.UsageInCSharp);
        }

        if (csCompilation != null)
        {
            // Append the version
            var packageInfo = Apk.GetPackages(ApkManager.DefaultArch)[Descriptor.ApkDeps[0]];

            builder.AppendLine("## 📦 Compatible Native Binaries");
            builder.AppendLine();
            builder.AppendLine($"This library does not provide C native binaries but only P/Invoke .NET bindings to `{LibName}` `{packageInfo.Version}`.");
            builder.AppendLine();
            builder.AppendLine("If the native library is already installed on your system, check the version installed. If you are using this library on Alpine Linux, see the compatible version in the [Supported API](#supported-api) section below.");
            builder.AppendLine("Other OS might require a different setup.");
            builder.AppendLine();

            if (Descriptor.NativeNuGets != null)
            {
                builder.AppendLine($"For convenience, you can install an existing NuGet package (outside of XenoAtom.Interop project) that is providing native binaries.");
                builder.AppendLine($"The following packages were tested and are compatible with **{ManagedPackageName}**:");
                builder.AppendLine();
                builder.AppendLine("| NuGet Package with Native Binaries | Version |");
                builder.AppendLine("|------------------------------------|---------|");
                foreach (var nativeNuGet in Descriptor.NativeNuGets)
                {
                    builder.AppendLine($"| [{nativeNuGet.Name}](https://www.nuget.org/packages/{nativeNuGet.Name}) | `{nativeNuGet.Version}`");
                }
                builder.AppendLine();
            }

            builder.AppendLine();
            builder.AppendLine("## 📚 Supported API");
            builder.AppendLine();
            builder.AppendLine("> This package is based on the following header version:");
            builder.AppendLine("> ");
            builder.AppendLine($"> - {LibName} C include headers: [`{Descriptor.ApkDeps[0]}`]({Apk.GetPackageDescriptionUrl(packageInfo)})");
            builder.AppendLine($"> - Version: `{packageInfo.Version}`");
            builder.AppendLine($"> - Distribution: AlpineLinux `{Apk.Version}`");
            builder.AppendLine();
            builder.AppendLine("The following API were automatically generated from the C/C++ code:");
            builder.AppendLine();

            var map = CollectAllCppFunctionAndInclude(csCompilation);

            foreach (var pair in map.OrderBy(x => x.Key, StringComparer.Ordinal))
            {
                builder.Append($"- {pair.Key}: ");
                pair.Value.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));
                for (var i = 0; i < pair.Value.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(", ");
                    }
                    var cppFunction = pair.Value[i];
                    var url = GetUrlDocumentationForCppFunction(cppFunction);
                    if (url != null)
                    {
                        builder.Append($"[`{cppFunction.Name}`]({url})");
                    }
                    else
                    {
                        builder.Append($"`{cppFunction.Name}`");
                    }
                }

                builder.AppendLine();
            }
        }

        _packageDescriptionMarkdown = builder.ToString();
    }

    private string PatchFileName(string text)
    {
        return PatchTemplate(text);
    }

    private string PatchTemplate(string text)
    {
        var newText = text.Replace("LIBNAME", LibName, StringComparison.Ordinal);
        newText = newText.Replace("LIBSUMMARY", Descriptor.Summary, StringComparison.Ordinal);
        newText = newText.Replace("LIBDESCRIPTION", _packageDescriptionMarkdown ?? string.Empty, StringComparison.Ordinal);
        return newText;
    }

    private string ReadTemplateFileAndPatchTemplate(string templateFile)
    {
        var text = PatchTemplate(File.ReadAllText(templateFile));

        if (IsCommonLib && Path.GetFileName(templateFile) == "readme.md")
        {
            text = text.Replace("XenoAtom.Interop.common", "XenoAtom.Interop");
        }
        return text;
    }

    private Dictionary<string, List<CppFunction>> CollectAllCppFunctionAndInclude(CSharpCompilation csCompilation)
    {
        var mapIncludeNameToCppFunction = new Dictionary<string, List<CppFunction>>();
        foreach (var csLibClass in csCompilation.Members.OfType<CSharpGeneratedFile>().Select(x => x.GetLibClassFromGeneratedFile()))
        {
            foreach (var csMethod in CollectAllFunctions(csLibClass))
            {
                var cppFunction = (CppFunction)csMethod.CppElement!;

                var includeHeaderFileName = Path.GetFileName(cppFunction.SourceFile);
                
                if (!mapIncludeNameToCppFunction.TryGetValue(includeHeaderFileName, out var list))
                {
                    list = new List<CppFunction>();
                    mapIncludeNameToCppFunction[includeHeaderFileName] = list;
                }

                list.Add(cppFunction);
            }
        }

        return mapIncludeNameToCppFunction;
    }

    private static IEnumerable<CSharpMethod> CollectAllFunctions(CSharpClass csClass)
    {
        foreach (var csElement in csClass.Members)
        {
            // .OfType<CSharpMethod>().Where(x => !x.IsManaged && x.CppElement is CppFunction)
            if (csElement is CSharpMethod csMethod && !csMethod.IsManaged && csMethod.CppElement is CppFunction)
            {
                yield return csMethod;
            }
            else
            {
                if (csElement is CSharpClass csInnerClass)
                {
                    foreach (var method in CollectAllFunctions(csInnerClass))
                    {
                        yield return method;
                    }
                }
            }
        }
    }
}