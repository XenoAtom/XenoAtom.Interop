// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_merge_file_input
    {
        /// <summary>
        /// File name of the conflicted file, or `NULL` to not merge the path.
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? path_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(path);
            set => path = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            LibGit2Helper.FreeUnmanagedUtf8String(path);
            path = null;
        }
    }

    partial struct git_merge_file_options : IDisposable
    {
        /// <summary>
        /// Label for the ancestor file side of the conflict which will be prepended
        /// to labels in diff3-format merge files.
        /// </summary>
        public string? ancestor_label_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(ancestor_label);
            set => ancestor_label = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <summary>
        /// Label for our file side of the conflict which will be prepended
        /// to labels in merge files.
        /// </summary>
        public string? our_label_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(our_label);
            set => our_label = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <summary>
        /// Label for their file side of the conflict which will be prepended
        /// to labels in merge files.
        /// </summary>
        public string? their_label_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(their_label);
            set => their_label = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            LibGit2Helper.FreeUnmanagedUtf8String(ancestor_label);
            ancestor_label = null;
            LibGit2Helper.FreeUnmanagedUtf8String(our_label);
            our_label = null;
            LibGit2Helper.FreeUnmanagedUtf8String(their_label);
            their_label = null;
        }
    }

    partial struct git_merge_file_result
    {
        /// <summary>
        /// The path that the resultant merge file should use, or NULL if a
        /// filename conflict would occur.
        /// </summary>
        public string? path_string => LibGit2Helper.UnmanagedUtf8StringToString(path);

        /// <summary>
        /// The contents of the merge.
        /// </summary>
        public ReadOnlySpan<byte> AsSpan() => ptr == null ? default : new((void*)ptr, (int)len);
    }

    /// <summary>
    /// Analyzes the given branch(es) and determines the opportunities for
    /// merging them into the HEAD of the repository.
    /// </summary>
    /// <param name="analysis_out">analysis enumeration that the result is written into</param>
    /// <param name="preference_out">One of the `git_merge_preference_t` flag.</param>
    /// <param name="repo">the repository to merge</param>
    /// <param name="their_heads">the heads to merge into</param>
    /// <returns>@return 0 on success or error code</returns>
    public static libgit2.git_result git_merge_analysis(out libgit2.git_merge_analysis_t analysis_out, out libgit2.git_merge_preference_t preference_out, libgit2.git_repository repo, ReadOnlySpan<libgit2.git_annotated_commit> their_heads)
    {
        fixed (libgit2.git_annotated_commit* their_headsPtr = their_heads)
        {
            return git_merge_analysis(out analysis_out, out preference_out, repo, their_headsPtr, (nuint)their_heads.Length);
        }
    }

    /// <summary>
    /// Analyzes the given branch(es) and determines the opportunities for
    /// merging them into a reference.
    /// </summary>
    /// <param name="analysis_out">analysis enumeration that the result is written into</param>
    /// <param name="preference_out">One of the `git_merge_preference_t` flag.</param>
    /// <param name="repo">the repository to merge</param>
    /// <param name="our_ref">the reference to perform the analysis from</param>
    /// <param name="their_heads">the heads to merge into</param>
    /// <returns>@return 0 on success or error code</returns>
    public static libgit2.git_result git_merge_analysis_for_ref(out libgit2.git_merge_analysis_t analysis_out, out libgit2.git_merge_preference_t preference_out, libgit2.git_repository repo, libgit2.git_reference our_ref,
        ReadOnlySpan<libgit2.git_annotated_commit> their_heads)
    {
        fixed (libgit2.git_annotated_commit* their_headsPtr = their_heads)
        {
            return git_merge_analysis_for_ref(out analysis_out, out preference_out, repo, our_ref, their_headsPtr, (nuint)their_heads.Length);
        }
    }


    /// <summary>
    /// Find a merge base given a list of commits
    /// </summary>
    /// <param name="out">the OID of a merge base considering all the commits</param>
    /// <param name="repo">the repository where the commits exist</param>
    /// <param name="input_array">oids of the commits</param>
    /// <returns>@return Zero on success; GIT_ENOTFOUND or -1 on failure.</returns>
    public static libgit2.git_result git_merge_base_many(out libgit2.git_oid @out, libgit2.git_repository repo, ReadOnlySpan<libgit2.git_oid> input_array)
    {
        fixed (libgit2.git_oid* input_arrayPtr = input_array)
        {
            return git_merge_base_many(out @out, repo, (nuint)input_array.Length, input_arrayPtr);
        }
    }

    /// <summary>
    /// Find all merge bases given a list of commits
    /// </summary>
    /// <param name="out">array in which to store the resulting ids</param>
    /// <param name="repo">the repository where the commits exist</param>
    /// <param name="input_array">oids of the commits</param>
    /// <returns>@return Zero on success; GIT_ENOTFOUND or -1 on failure.</returns>
    public static libgit2.git_result git_merge_bases_many(out libgit2.git_oidarray @out, libgit2.git_repository repo, ReadOnlySpan<libgit2.git_oid> input_array)
    {
        fixed (libgit2.git_oid* input_arrayPtr = input_array)
        {
            return git_merge_bases_many(out @out, repo, (nuint)input_array.Length, input_arrayPtr);
        }
    }

    /// <summary>
    /// Find a merge base in preparation for an octopus merge
    /// </summary>
    /// <param name="out">the OID of a merge base considering all the commits</param>
    /// <param name="repo">the repository where the commits exist</param>
    /// <param name="input_array">oids of the commits</param>
    /// <returns>@return Zero on success; GIT_ENOTFOUND or -1 on failure.</returns>
    public static libgit2.git_result git_merge_base_octopus(out libgit2.git_oid @out, libgit2.git_repository repo, ReadOnlySpan<libgit2.git_oid> input_array)
    {
        fixed (libgit2.git_oid* input_arrayPtr = input_array)
        {
            return git_merge_base_octopus(out @out, repo, (nuint)input_array.Length, input_arrayPtr);
        }
    }

    /// <summary>
    /// Merges the given commit(s) into HEAD, writing the results into the working
    /// directory.  Any changes are staged for commit and any conflicts are written
    /// to the index.  Callers should inspect the repository's index after this
    /// completes, resolve any conflicts and prepare a commit.
    /// </summary>
    /// <param name="repo">the repository to merge</param>
    /// <param name="their_heads">the heads to merge into</param>
    /// <param name="merge_opts">merge options</param>
    /// <param name="checkout_opts">checkout options</param>
    /// <returns>@return 0 on success or error code</returns>
    /// <remarks>
    /// For compatibility with git, the repository is put into a merging
    /// state. Once the commit is done (or if the user wishes to abort),
    /// you should clear this state by calling
    /// `git_repository_state_cleanup()`.
    /// </remarks>
    public static libgit2.git_result git_merge(libgit2.git_repository repo, ReadOnlySpan<libgit2.git_annotated_commit> their_heads, in libgit2.git_merge_options merge_opts, in libgit2.git_checkout_options checkout_opts)
    {
        fixed (libgit2.git_annotated_commit* their_headsPtr = their_heads)
        {
            return git_merge(repo, their_headsPtr, (nuint)their_heads.Length, in merge_opts, in checkout_opts);
        }
    }
}