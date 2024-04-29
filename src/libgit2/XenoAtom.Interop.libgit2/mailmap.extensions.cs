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
    /// Create a new mailmap instance containing a single mailmap file
    /// </summary>
    /// <param name="out">pointer to store the new mailmap</param>
    /// <param name="buf">buffer to parse the mailmap from</param>
    /// <returns>@return 0 on success, or an error code</returns>
    public static libgit2.git_result git_mailmap_from_buffer(out libgit2.git_mailmap @out, ReadOnlySpan<byte> buf)
    {
        fixed (byte* bufPtr = buf)
        {
            return git_mailmap_from_buffer(out @out, bufPtr, buf.Length);
        }
    }

    /// <summary>
    /// Resolve a name and email to the corresponding real name and email.
    /// </summary>
    /// <param name="real_name">pointer to store the real name</param>
    /// <param name="real_email">pointer to store the real email</param>
    /// <param name="mm">the mailmap to perform a lookup with (may be NULL)</param>
    /// <param name="name">the name to look up</param>
    /// <param name="email">the email to look up</param>
    /// <returns>@return 0 on success, or an error code</returns>
    /// <remarks>
    /// The lifetime of the strings are tied to `mm`, `name`, and `email` parameters.
    /// </remarks>
    public static libgit2.git_result git_mailmap_resolve(out string? real_name, out string? real_email, libgit2.git_mailmap mm, string name, string email)
    {
        var result = git_mailmap_resolve(out var real_namePtr, out byte* real_emailPtr, mm, name, email);
        real_name = result == 0 ? LibGit2Helper.UnmanagedUtf8StringToString(real_namePtr) : null;
        real_email = result == 0 ? LibGit2Helper.UnmanagedUtf8StringToString(real_emailPtr) : null;
        return result;
    }
}