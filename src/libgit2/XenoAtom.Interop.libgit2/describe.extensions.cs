// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_describe_format_options : IDisposable
    {
        /// <summary>
        /// If the workdir is dirty and this is set, this string will
        /// be appended to the description string.
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? dirty_suffix_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(dirty_suffix);
            set => dirty_suffix = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            LibGit2Helper.FreeUnmanagedUtf8String(dirty_suffix);
            dirty_suffix = null;
        }
    }

    partial struct git_describe_options : IDisposable
    {
        public string? pattern_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(pattern);
            set => pattern = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            LibGit2Helper.FreeUnmanagedUtf8String(pattern);
            pattern = null;
        }
    }

}