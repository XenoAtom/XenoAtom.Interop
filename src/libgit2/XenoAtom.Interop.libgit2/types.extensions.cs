// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_signature : IDisposable
    {
        /// <summary>
        /// full name of the author
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? name_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(name);
            set => name = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <summary>
        /// email of the author
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? email_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(email);
            set => email = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            LibGit2Helper.FreeUnmanagedUtf8String(name);
            name = null;
            LibGit2Helper.FreeUnmanagedUtf8String(email);
            email = null;
        }
    }
}