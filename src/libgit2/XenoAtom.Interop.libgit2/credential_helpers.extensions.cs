// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_credential_userpass_payload : IDisposable
    {
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? username_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(username);
            set => username = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? password_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(password);
            set => password = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            LibGit2Helper.FreeUnmanagedUtf8String(username);
            username = null;
            LibGit2Helper.FreeUnmanagedUtf8String(password);
            password = null;
        }
    }
}