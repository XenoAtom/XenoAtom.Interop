// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    /// <summary>
    /// Retrieve the configured identity to use for reflogs
    /// </summary>
    /// <param name="name">where to store the pointer to the name</param>
    /// <param name="email">where to store the pointer to the email</param>
    /// <param name="repo">the repository</param>
    /// <returns>@return 0 or an error code</returns>
    /// <remarks>
    /// The memory is owned by the repository and must not be freed by the
    /// user.
    /// </remarks>
    public static libgit2.git_result git_repository_ident(out string? name, out string? email, libgit2.git_repository repo)
    {
        var ret = git_repository_ident(out byte* name_ptr, out var email_ptr, repo);
        name = ret == 0 ? GetStringFromUTF8(name_ptr) : null;
        email = ret == 0 ? GetStringFromUTF8(email_ptr) : null;
        return ret;
    }
}