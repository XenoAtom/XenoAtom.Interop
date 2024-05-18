// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.IO;
using System.Threading.Tasks;
using CppAst.CodeGen.Common;
using CppAst.CodeGen.CSharp;
using Zio.FileSystems;

namespace XenoAtom.Interop.CodeGen;

public abstract class GeneratorBase
{
    private ApkManager? _apkManager;

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

    public string LibName => Descriptor.Name;

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

        GenerateGitHubWorkflows();

        GenerateLibraryProjectStructure();
    }

    public async Task Run()
    {
        var csCompilation = await Generate();
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
        if (!Directory.Exists(LibraryFolder))
        {
            Directory.CreateDirectory(LibraryFolder);
        }

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
        
        if (Descriptor.HasGeneratedFolder)
        {
            if (!Directory.Exists(GeneratedFolder))
            {
                Directory.CreateDirectory(GeneratedFolder);
            }
        }
    }

    private string PatchFileName(string text)
    {
        return PatchTemplate(text);
    }

    private string PatchTemplate(string text)
    {
        return text.Replace("LIBNAME", LibName, StringComparison.Ordinal);
    }

    private string ReadTemplateFileAndPatchTemplate(string templateFile)
    {
        return PatchTemplate(File.ReadAllText(templateFile));
    }
}