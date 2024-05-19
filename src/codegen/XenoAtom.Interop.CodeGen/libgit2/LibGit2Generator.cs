// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CppAst;
using CppAst.CodeGen.Common;
using CppAst.CodeGen.CSharp;
using Zio.FileSystems;

namespace XenoAtom.Interop.CodeGen.libgit2;

internal partial class LibGit2Generator : GeneratorBase
{
    private CppTypedef _gitResultType;
    private readonly Dictionary<string, List<string>> _gitResultFunctionsDetectedButNotRegistered;
    private readonly List<CSharpStruct> _structs;
    private readonly Dictionary<CSharpStruct, HashSet<CSharpRefKind>> _structRefUsages;

    /// <summary>
    /// List of functions that should not use string marshalling for byte* buffers
    /// </summary>
    private static readonly HashSet<string> FunctionsWithNoStringMarshalling =
    [
        "git_blob_data_is_binary",
        "git_diff_from_buffer",
        "git_filter_list_apply_to_buffer",
        "git_filter_list_stream_buffer",
        "git_mailmap_from_buffer",
        "git_object_rawcontent_is_valid",
        "git_odb_stream_write",
    ];

    public LibGit2Generator(LibDescriptor descriptor) : base(descriptor)
    {
        _structs = new List<CSharpStruct>();
        _structRefUsages = new Dictionary<CSharpStruct, HashSet<CSharpRefKind>>();
        _gitResultFunctionsDetectedButNotRegistered = new Dictionary<string, List<string>>();
    }

    protected override async Task<CSharpCompilation?> Generate()
    {
        var sysIncludes = Apk.GetSysIncludeDirectory("main");
        var communityFolder = Apk.GetIncludeDirectory("community");
        List<string> srcFolders =
        [
            Apk.GetIncludeDirectory("main"),
            communityFolder,
        ];

        var csOptions = new CSharpConverterOptions()
        {
            DefaultClassLib = "libgit2",
            DefaultNamespace = "XenoAtom.Interop",
            DefaultOutputFilePath = "/libgit2_library.generated.cs",
            DefaultDllImportNameAndArguments = "LibraryName",
            TargetCpu = CppTargetCpu.X86_64,
            TargetVendor = "linux",
            TargetSystem = "gnu",
            Defines =
            {
                "GIT_DEPRECATE_HARD"
            },
            PreHeaderText = @"
// A result integer from a git function. 0 if successful, < 0 if an error.
typedef int git_result;
",

            DispatchOutputPerInclude = true,
            DisableRuntimeMarshalling = true,
            AllowMarshalForString = false,

            MappingRules =
            {
                // Remove this function as it is not supported (varargs)
                e => e.Map<CppFunction>("git_libgit2_opts").Discard(),
                    
                // Mappings for repository.h
                e => e.MapMacroToConst("LIBGIT2_VERSION", "char*"),
                e => e.MapMacroToConst("LIBGIT2_SOVERSION", "char*"),
                e => e.MapMacroToConst("LIBGIT2_VER_MAJOR", "int"),
                e => e.MapMacroToConst("LIBGIT2_VER_MINOR", "int"),
                e => e.MapMacroToConst("LIBGIT2_VER_REVISION", "int"),
                e => e.MapMacroToConst("LIBGIT2_VER_PATCH", "int"),
                e => e.MapMacroToConst("GIT_.*_VERSION", "unsigned int"),

                e => e.Map<CppParameter>(".*::out").ByRef(CSharpRefKind.Out),

                e => e.Map<CppField>("git_apply_options::flags").Type("git_apply_flags_t"),
                e => e.Map<CppField>("git_blame_options::flags").Type("git_blame_flag_t"),
                e => e.Map<CppField>("git_blob_filter_options::flags").Type("git_blob_filter_flag_t"),
                e => e.Map<CppParameter>("git_blob_lookup::blob").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_blob_lookup_prefix::blob").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_blob_create_from_workdir::id").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_blob_create_from_disk::id").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_blob_create_from_buffer::id").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_branch_next::out_type").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_branch_name_is_valid::valid").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_buf_dispose::buffer"),
                e => e.Map<CppField>("git_checkout_options::notify_flags").Type("git_checkout_notify_t"),
                e => e.Map<CppParameter>("git_checkout_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_cherrypick_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_clone_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_commit_lookup::commit").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_commit_lookup_prefix::commit").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_commit_tree::tree_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_commit_extract_signature::signed_data").NoByRef(),
                e => e.Map<CppParameter>("git_commit_extract_signature::commit_id").ByRef(CSharpRefKind.In),
                e => e.Map<CppParameter>("git_commit_create::id").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_commit_create_v::id").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_commit_amend::id").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_libgit2_version::.*").ByRef(CSharpRefKind.Out),
                e => e.Map<CppFunction>("git_libgit2_features").Type("git_feature_t"),
                e => e.Map<CppParameter>("git_config_entry_free::entry").NoByRef(),
                e => e.Map<CppParameter>("git_config_next::entry").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_config_get_mapped::maps").NoByRef(),
                e => e.Map<CppParameter>("git_config_lookup_map_value::maps").NoByRef(),
                e => e.Map<CppParameter>("git_config_lock::tx").ByRef(CSharpRefKind.Out),
                e => e.Map<CppField>("git_describe_options::describe_strategy").Type("git_describe_strategy_t"),
                e => e.Map<CppParameter>("git_describe_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_describe_format_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_describe_commit::result").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_describe_commit::opts").ByRef(CSharpRefKind.In),
                e => e.Map<CppParameter>("git_describe_workdir::opts").ByRef(CSharpRefKind.In),
                e => e.Map<CppField>("git_diff_file::flags").Type("git_diff_flag_t"),
                e => e.Map<CppField>("git_diff_file::mode").Type("git_filemode_t"),
                e => e.Map<CppField>("git_diff_delta::flags").Type("git_diff_flag_t"),
                e => e.Map<CppField>("git_diff_options::flags").Type("git_diff_option_t"),
                e => e.Map<CppField>("git_diff_find_options::flags").Type("git_diff_find_t"),
                e => e.Map<CppParameter>("git_diff_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_diff_find_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_diff_tree_to_tree::diff").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_diff_tree_to_index::diff").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_diff_index_to_workdir::diff").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_diff_tree_to_workdir::diff").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_diff_tree_to_workdir_with_index::diff").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_diff_index_to_index::diff").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_diff_patchid::opts").ByRef(CSharpRefKind.In),
                e => e.Map<CppField>("git_email_create_options::flags").Type("git_email_create_flags_t"),
                e => e.Map<CppParameter>("git_error_set::error_class").Type("git_error_t"),
                e => e.Map<CppParameter>("git_error_set_str::error_class").Type("git_error_t"),
                e => e.Map<CppField>("git_filter_options::flags").Type("git_filter_flag_t"),
                e => e.Map<CppParameter>("git_filter_list_load::filters").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_filter_list_load::flags").Type("git_filter_flag_t"),
                e => e.Map<CppParameter>("git_filter_list_load_ext::filters").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_graph_ahead_behind::ahead").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_graph_ahead_behind::behind").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_ignore_path_is_ignored::ignored").ByRef(CSharpRefKind.Out),
                e => e.Map<CppFunction>("git_index_caps").Type("git_index_capability_t"),
                e => e.Map<CppParameter>("git_index_set_caps::caps").Type("git_index_capability_t"),
                e => e.Map<CppParameter>("git_index_iterator_new::iterator_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_index_add_all::flags").Type("git_index_add_option_t"),
                e => e.Map<CppParameter>("git_index_find::at_pos").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_index_find_prefix::at_pos").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_index_conflict_get::ancestor_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_index_conflict_iterator_new::iterator_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_index_conflict_next::.*_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_indexer_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_indexer_new::opts").ByRef(CSharpRefKind.In),
                e => e.Map<CppParameter>("git_indexer_append::stats").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_indexer_commit::stats").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_mailmap_resolve::real_.*").ByRef(CSharpRefKind.Out),
                e => e.Map<CppField>("git_merge_file_options::flags").Type("git_merge_file_flag_t"),
                e => e.Map<CppField>("git_merge_options::flags").Type("git_merge_flag_t"),
                e => e.Map<CppField>("git_merge_options::file_flags").Type("git_merge_file_flag_t"),
                e => e.Map<CppParameter>("git_merge_file_input_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_merge_file_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_merge_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_merge_analysis.*::.*_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_merge_analysis.*::their_heads").NoByRef(),
                e => e.Map<CppParameter>("git_merge::their_heads").NoByRef(),
                e => e.Map<CppParameter>("git_message_trailers::arr").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_note_next::.*_id").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_note_commit_create::.*_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_note_commit_remove::notes_commit_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_object_lookup::object").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_object_lookup_prefix::object_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_object_peel::peeled").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_object_dup::dest").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_object_rawcontent_is_valid::valid").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_odb_read_header::.*_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_odb_exists_ext::flags").Type("git_odb_lookup_flags_t"),
                e => e.Map<CppParameter>("git_odb_expand_ids::ids").NoByRef(),
                e => e.Map<CppParameter>("git_odb_stream_write::buffer").NoByRef(),
                e => e.Map<CppParameter>("git_odb_stream_finalize_write::stream").NoByRef(),
                e => e.Map<CppParameter>("git_odb_stream_read::stream").NoByRef(),
                e => e.Map<CppParameter>("git_odb_stream_free::stream").NoByRef(),
                e => e.Map<CppParameter>("git_odb_open_rstream::len").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_odb_open_rstream::type").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_odb_object_dup::dest").ByRef(CSharpRefKind.Out),
                e => e.Map<CppField>("git_odb_backend_loose_options::flags").Type("git_odb_backend_loose_flag_t"),
                e => e.Map<CppParameter>("git_oid_fmt::out").NoByRef(),
                e => e.Map<CppParameter>("git_oid_nfmt::out").NoByRef(),
                e => e.Map<CppParameter>("git_oid_pathfmt::out").NoByRef(),
                e => e.Map<CppParameter>("git_oid_tostr::out").NoByRef(),
                e => e.Map<CppParameter>("git_oidarray_dispose::array"),
                e => e.Map<CppParameter>("git_patch_line_stats::total_context").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_patch_line_stats::total_additions").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_patch_line_stats::total_deletions").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_patch_get_hunk::lines_in_hunk").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_pathspec_matches_path::flags").Type("git_pathspec_flag_t"),
                e => e.Map<CppParameter>("git_pathspec_match_workdir::flags").Type("git_pathspec_flag_t"),
                e => e.Map<CppParameter>("git_pathspec_match_index::flags").Type("git_pathspec_flag_t"),
                e => e.Map<CppParameter>("git_pathspec_match_tree::flags").Type("git_pathspec_flag_t"),
                e => e.Map<CppParameter>("git_pathspec_match_diff::flags").Type("git_pathspec_flag_t"),
                e => e.Map<CppParameter>("git_proxy_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_rebase_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_rebase_commit::id").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_rebase_next::operation").ByRef(CSharpRefKind.Out),
                e => e.Map<CppField>("git_remote_create_options::flags").Type("git_remote_create_flags"),
                e => e.Map<CppParameter>("git_remote_create_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_remote_dup::dest").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_remote_get_push_refspecs::array").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_remote_ls::size").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_remote_init_callbacks::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_revwalk_sorting::sort_mode").Type("git_sort_t"),
                e => e.Map<CppParameter>("git_fetch_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_push_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_remote_connect_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppField>("git_repository_init_options::flags").Type("git_repository_init_flag_t"),
                e => e.Map<CppParameter>("git_repository_init_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_repository_init_ext::opts").ByRef(CSharpRefKind.In),
                e => e.Map<CppParameter>("git_repository_open_ext::flags").Type("git_repository_open_flag_t"),
                e => e.Map<CppParameter>("git_repository_ident::name").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_repository_ident::email").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_revert_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppField>("git_revspec::flags").Type("git_revspec_t"),
                e => e.Map<CppParameter>("git_revparse_ext::.*_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_revparse::revspec").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_signature_free::sig").NoByRef(),
                e => e.Map<CppField>("git_stash_save_options::flags").Type("git_stash_flags"),
                e => e.Map<CppField>("git_stash_apply_options::flags").Type("git_stash_apply_flags"),
                e => e.Map<CppParameter>("git_stash_save::flags").Type("git_stash_flags"),
                e => e.Map<CppParameter>("git_stash_save_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_stash_apply_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppField>("git_status_options::flags").Type("git_status_opt_t"),
                e => e.Map<CppParameter>("git_status_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_status_file::status_flags").ByRef(CSharpRefKind.Out).Type("git_status_t*"),
                e => e.Map<CppParameter>("git_status_should_ignore::ignored").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_submodule_update_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_submodule_update::options").ByRef(CSharpRefKind.In),
                e => e.Map<CppParameter>("git_submodule_open::repo").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_submodule_status::status").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_submodule_location::location_status").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_tag_target::target_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_tag_create::oid").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_tag_annotation_create::oid").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_tag_create_from_buffer::oid").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_tag_create_lightweight::oid").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_tag_list::tag_names").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_tag_list_match::tag_names").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_tag_peel::tag_target_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_tag_name_is_valid::valid").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_tree_entry_dup::dest").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_tree_entry_to_object::object_out").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_tree_create_updated::updates").NoByRef(),
                e => e.Map<CppParameter>("git_treebuilder_write::id").ByRef(CSharpRefKind.Out),
                e => e.Map<CppField>("git_worktree_prune_options::flags").Type("git_worktree_prune_t"),
                e => e.Map<CppParameter>("git_worktree_add_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_worktree_prune_options_init::opts").ByRef(CSharpRefKind.Out),
                e => e.Map<CppParameter>("git_worktree_is_prunable::opts").ByRef(CSharpRefKind.In),
                e => e.Map<CppParameter>("git_worktree_prune::opts").ByRef(CSharpRefKind.In),
                
                e => e.MapAll<CppFunction>().CppAction(ProcessCppFunctions).CSharpAction(ProcessCSharpMethods)
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
            Path.Combine(communityFolder, "git2.h"),
            Path.Combine(communityFolder, "git2", "trace.h")
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

        var structsWithStringMarshalling = new List<CSharpStruct>();

        foreach (var csStruct in csCompilation.AllStructs)
        {
            var cppStruct = csStruct.CppElement as CppClass;
            if (cppStruct == null) continue;
            foreach (var cppField in cppStruct.Fields)
            {
                if (cppField.Type is CppPointerType cppPointerType)
                {
                    var elementType = cppPointerType.ElementType;
                    if (elementType is CppQualifiedType qualifiedType)
                    {
                        elementType = qualifiedType.ElementType;
                    }

                    if (elementType is CppPrimitiveType cppPrimitiveType && cppPrimitiveType.Kind == CppPrimitiveKind.Char)
                    {
                        structsWithStringMarshalling.Add(csStruct);
                    }
                    break;
                }
            }
        }
        
        foreach (var csStruct in structsWithStringMarshalling)
        {
            Console.WriteLine($"{csStruct.Name} -> {csStruct.MarshallingUsage}");
        }
        

        ReportFunctionWithPossibleGitResultNotRegistered();

        return csCompilation;
    }

    protected override string? GetUrlDocumentationForCppFunction(CppFunction cppFunction)
    {
        // https://libgit2.org/libgit2/#v1.8.1/group/blob/git_blob_lookup
        var includeName = Path.GetFileNameWithoutExtension(cppFunction.SourceFile);
        return $"https://libgit2.org/libgit2/#v{NativeVersion}/group/{includeName}/{cppFunction.Name}";
    }

    private static string ToPascalCase(string name)
    {
        var builder = new StringBuilder();
        builder.Append(char.ToUpper(name[0]));
        int start = 1;
        int nextUnderscore;
        while ((nextUnderscore = name.IndexOf('_', start)) >= 0)
        {
            builder.Append(name, start, nextUnderscore - start);
            start = nextUnderscore + 1;
            builder.Append(char.ToUpper(name[start]));
            start++;
        }

        if (start < name.Length)
        {
            builder.Append(name.Substring(start));
        }

        return builder.ToString();
    }

    private void ReportFunctionWithPossibleGitResultNotRegistered()
    {
        if (_gitResultFunctionsDetectedButNotRegistered.Count == 0) return;
            
        Console.WriteLine($"// The following functions are possible returning a git_result but are not registered a such");
            
        foreach (var keyPair in _gitResultFunctionsDetectedButNotRegistered.OrderBy(x => x.Key))
        {
            Console.WriteLine($"// {keyPair.Key}");
            foreach (var functionName in keyPair.Value.OrderBy(x => x))
            {
                Console.WriteLine($"\"{functionName}\",");
            }
        }
    }

    private void ProcessCppFunctions(CSharpConverter converter, CppElement cppFunctionElement)
    {
        var cppFunction = (CppFunction) cppFunctionElement;

        // We are only looking for functions returning an int
        if (!cppFunction.ReturnType.Equals(CppPrimitiveType.Int)) return;
            
        // If the function is not registered as a git_result, we will try to detect if it is a potential one
        if (!GitResultFunctions.Contains(cppFunction.Name))
        {
            if (cppFunction.Comment == null) return;

            // Start from the bottom of the comments to catch "return" more quickly
            for (int i = cppFunction.Comment.Children.Count - 1; i >= 0; i--)
            {
                var csComment = cppFunction.Comment.Children[i];
                if (csComment is CppCommentBlockCommand blockCommand && blockCommand.CommandName == "return")
                {
                    var returnText = blockCommand.ChildrenToString().Trim().Replace("\r\n", " ")
                        .Replace("\n", " ");
                    if ((returnText.StartsWith("Zero", StringComparison.OrdinalIgnoreCase) || returnText.StartsWith("0")) &&
                        (returnText.Contains("failure") || returnText.Contains("error")))
                    {
                        var includeName = Path.GetFileName(cppFunction.Span.Start.File);

                        if (!_gitResultFunctionsDetectedButNotRegistered.TryGetValue(includeName,
                                out var functionList))
                        {
                            functionList = new List<string>();
                            _gitResultFunctionsDetectedButNotRegistered.Add(includeName, functionList);
                        }

                        functionList.Add(cppFunction.Name);
                    }
                }
            }
        }
        else
        {
            if (_gitResultType == null)
            {
                _gitResultType = (CppTypedef) converter.CurrentCppCompilation.FindByName("git_result");
                Debug.Assert(_gitResultType != null);
            }

            cppFunction.ReturnType = _gitResultType;
        }
    }
        
    private void ProcessCSharpStruct(CSharpConverter converter, CSharpElement csElement)
    {
        if (!(csElement is CSharpStruct csStruct))
        {
            return;
        }
        _structs.Add(csStruct);
    }


    private void ProcessStringMarshallingForStructs()
    {
        foreach (var csStruct in _structs)
        {
            if (!_structRefUsages.TryGetValue(csStruct, out var refKinds))
            {
                continue;
            }

            bool isIn = false;
            bool isOut = false;
            foreach (var refKind in refKinds)
            {
                switch (refKind)
                {
                    case CSharpRefKind.None:
                    case CSharpRefKind.In:
                        isIn = true;
                        break;
                    case CSharpRefKind.Out:
                        isOut = true;
                        break;
                    case CSharpRefKind.Ref:
                    case CSharpRefKind.RefReadOnly:
                        isIn = true;
                        isOut = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (var csField in csStruct.Members.OfType<CSharpField>())
            {
                if (csField.FieldType is CSharpTypeWithAttributes csTypeWithAttributes && csTypeWithAttributes.ElementType is CSharpPrimitiveType csPrimitiveType &&  csPrimitiveType.Kind == CSharpPrimitiveKind.String)
                {
                    if (isIn && !isOut)
                    {
                        csTypeWithAttributes = new CSharpTypeWithAttributes(csTypeWithAttributes.ElementType);
                        csTypeWithAttributes.Attributes.Add(new CSharpMarshalAsAttribute(UnmanagedType.CustomMarshaler) {MarshalTypeRef = "typeof(UTF8MarshalerStrict)"});
                        csField.FieldType = csTypeWithAttributes;
                    }
                }
            }
        }
    }

    private void ProcessCSharpMethods(CSharpConverter converter, CSharpElement element)
    {
        if (element is CSharpMethod csMethod)
        {
            if (FunctionsWithNoStringMarshalling.Contains(csMethod.Name))
            {
                return;
            }

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
                ((ICSharpContainer)csMethod.Parent!).Members.Add(newManagedMethod);
            }
        }
    }
}