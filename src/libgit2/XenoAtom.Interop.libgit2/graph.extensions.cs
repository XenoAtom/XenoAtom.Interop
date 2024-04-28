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
    /// Determine if a commit is reachable from any of a list of commits by
    /// following parent edges.
    /// </summary>
    /// <param name="repo">the repository where the commits exist</param>
    /// <param name="commit">a previously loaded commit</param>
    /// <param name="descendant_array">oids of the commits</param>
    /// <returns>@return 1 if the given commit is an ancestor of any of the given potential
    /// descendants, 0 if not, error code otherwise.</returns>
    public static libgit2.git_result git_graph_reachable_from_any(libgit2.git_repository repo, in libgit2.git_oid commit, ReadOnlySpan<libgit2.git_oid> descendant_array)
    {
        fixed (libgit2.git_oid* descendant_arrayPtr = descendant_array)
        {
            return git_graph_reachable_from_any(repo, commit, descendant_arrayPtr, (size_t)descendant_array.Length);
        }
    }
}