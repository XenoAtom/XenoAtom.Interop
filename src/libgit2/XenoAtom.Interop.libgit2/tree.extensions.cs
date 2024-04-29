// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    /// <summary>
    /// Create a tree based on another one with the specified modifications
    /// </summary>
    /// <param name="out">id of the new tree</param>
    /// <param name="repo">the repository in which to create the tree, must be the
    /// same as for `baseline`</param>
    /// <param name="baseline">the tree to base these changes on</param>
    /// <param name="updates">the list of updates to perform</param>
    /// <returns>@return 0 or an error code</returns>
    /// <remarks>
    /// Given the `baseline` perform the changes described in the list of
    /// `updates` and create a new tree.This function is optimized for common file/directory addition, removal and
    /// replacement in trees. It is much more efficient than reading the tree into a
    /// `git_index` and modifying that, but in exchange it is not as flexible.Deleting and adding the same entry is undefined behaviour, changing
    /// a tree to a blob or viceversa is not supported.
    /// </remarks>
    public static libgit2.git_result git_tree_create_updated(out libgit2.git_oid @out, libgit2.git_repository repo, libgit2.git_tree baseline, ReadOnlySpan<git_tree_update> updates)
    {
        fixed (git_tree_update* p_updates = updates)
        {
            return git_tree_create_updated(out @out, repo, baseline, updates.Length, p_updates);
        }
    }
}