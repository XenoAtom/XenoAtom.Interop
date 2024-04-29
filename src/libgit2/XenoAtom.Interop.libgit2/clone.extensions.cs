// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_clone_options : IDisposable
    {
        /// <summary>
        /// The name of the branch to checkout. NULL means use the
        /// remote's default branch.
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? checkout_branch_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(checkout_branch);
            set => checkout_branch = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            LibGit2Helper.FreeUnmanagedUtf8String(checkout_branch);
            checkout_branch = null;
        }
    }

    partial struct git_configmap : IDisposable
    {
        public string str_match_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(str_match)!;
            set => str_match = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        public void Dispose()
        {
            LibGit2Helper.FreeUnmanagedUtf8String(str_match);
            str_match = null;
        }
    }
}