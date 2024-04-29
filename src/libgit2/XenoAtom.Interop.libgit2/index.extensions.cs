// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    public partial struct git_index_entry : IDisposable
    {
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? path_string
        {
            get => path == null ? string.Empty : LibGit2Helper.UnmanagedUtf8StringToString(path);
            set => path = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            LibGit2Helper.FreeUnmanagedUtf8String(path);
            path = null;
        }
    }
}