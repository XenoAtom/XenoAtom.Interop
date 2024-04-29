// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_push_update
    {
        /// <summary>
        /// The source name of the reference
        /// </summary>
        public string? src_refname_string => LibGit2Helper.UnmanagedUtf8StringToString(src_refname);

        /// <summary>
        /// The name of the reference to update on the server
        /// </summary>
        public string? dst_refname_string => LibGit2Helper.UnmanagedUtf8StringToString(dst_refname);
    }

    partial struct git_remote_head
    {
        public string? name_string => LibGit2Helper.UnmanagedUtf8StringToString(name);

        /// <summary>
        /// If the server send a symref mapping for this ref, this will
        /// point to the target.
        /// </summary>
        public string? symref_target_string => LibGit2Helper.UnmanagedUtf8StringToString(symref_target);
    }
}