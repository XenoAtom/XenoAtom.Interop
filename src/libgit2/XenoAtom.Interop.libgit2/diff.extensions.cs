// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_diff_binary_file
    {
        /// <summary>
        /// The binary data, deflated.
        /// </summary>
        public ReadOnlySpan<byte> AsSpan() => data == null ? default : new((void*)data, (int)datalen);
    }

    partial struct git_diff_file
    {
        /// <summary>
        /// The NUL-terminated path to the entry relative to the working
        /// directory of the repository.
        /// </summary>
        public string? path_string => LibGit2Helper.UnmanagedUtf8StringToString(path);
    }

    partial struct git_diff_line
    {
        /// <summary>
        /// Diff text.
        /// </summary>
        public string? content_string => LibGit2Helper.UnmanagedUtf8StringToString(content, (int)content_len);
    }

    /// <summary>
    /// Read the contents of a git patch file into a `git_diff` object.
    /// </summary>
    /// <param name="out">A pointer to a git_diff pointer that will be allocated.</param>
    /// <param name="content">The contents of a patch file</param>
    /// <returns>@return 0 or an error code</returns>
    /// <remarks>
    /// The diff object produced is similar to the one that would be
    /// produced if you actually produced it computationally by comparing
    /// two trees, however there may be subtle differences.  For example,
    /// a patch file likely contains abbreviated object IDs, so the
    /// object IDs in a `git_diff_delta` produced by this function will
    /// also be abbreviated.This function will only read patch files created by a git
    /// implementation, it will not read unified diffs produced by
    /// the `diff` program, nor any other types of patch files.
    /// </remarks>
    public static libgit2.git_result git_diff_from_buffer(out libgit2.git_diff @out, ReadOnlySpan<byte> content)
    {
        fixed (byte* pContent = content)
        {
            return git_diff_from_buffer(out @out, pContent, content.Length);
        }
    }
}