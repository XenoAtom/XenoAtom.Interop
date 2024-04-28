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
    /// Get the branch name
    /// </summary>
    /// <param name="out">The abbreviated reference name.</param>
    /// <param name="ref">A reference object, ideally pointing to a branch</param>
    /// <returns>@return 0 on success; GIT_EINVALID if the reference isn't either a local or
    /// remote branch, otherwise an error code.</returns>
    /// <remarks>
    /// Given a reference object, this will check that it really is a branch (ie.
    /// it lives under "refs/heads/" or "refs/remotes/"), and return the branch part
    /// of it.
    /// </remarks>
    public static libgit2.git_result git_branch_name(out string? @out, libgit2.git_reference @ref)
    {
        byte* @ref_name;
        var result = git_branch_name(out @ref_name, @ref);
        @out = result == 0 ? GetStringFromUTF8(@ref_name) : null;
        return result;
    }
}