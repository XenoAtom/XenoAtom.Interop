// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_rebase_operation
    {
        /// <summary>
        /// The executable the user has requested be run.  This will only
        /// be populated for operations of type `GIT_REBASE_OPERATION_EXEC`.
        /// </summary>
        public string? exec_string => LibGit2Helper.UnmanagedUtf8StringToString(exec);
    }

    partial struct git_rebase_options : IDisposable
    {
        /// <summary>
        /// Used by `git_rebase_finish`, this is the name of the notes reference
        /// used to rewrite notes for rebased commits when finishing the rebase;
        /// if NULL, the contents of the configuration option `notes.rewriteRef`
        /// is examined, unless the configuration option `notes.rewrite.rebase`
        /// is set to false.  If `notes.rewriteRef` is also NULL, notes will
        /// not be rewritten.
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? rewrite_notes_ref_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(rewrite_notes_ref);
            set => rewrite_notes_ref = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        public void Dispose()
        {
            LibGit2Helper.FreeUnmanagedUtf8String(rewrite_notes_ref);
            rewrite_notes_ref = null;
        }
    }
}