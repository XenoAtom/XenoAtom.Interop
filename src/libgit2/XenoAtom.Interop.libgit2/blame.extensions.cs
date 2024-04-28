// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_blame_hunk
    {
        /// <summary>
        /// The path to the file where this hunk originated, as of the commit
        /// specified by `orig_commit_id`.
        /// </summary>
        public string orig_path_managed => GetStringFromUTF8(orig_path)!;
    }
}