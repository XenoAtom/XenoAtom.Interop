// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_email_create_options : IDisposable
    {
        /// <summary>
        /// The subject prefix, by default "PATCH".  If set to an empty
        /// string ("") then only the patch numbers will be shown in the
        /// prefix.  If the subject_prefix is empty and patch numbers
        /// are not being shown, the prefix will be omitted entirely.
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? subject_prefix_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(subject_prefix);
            set => subject_prefix = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        public void Dispose()
        {
            LibGit2Helper.FreeUnmanagedUtf8String(subject_prefix);
            subject_prefix = null;
        }
    }
}