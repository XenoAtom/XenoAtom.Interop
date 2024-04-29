// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_repository_init_options : IDisposable
    {
        /// <summary>
        /// The path to the working dir or NULL for default (i.e. repo_path parent
        /// on non-bare repos). IF THIS IS RELATIVE PATH, IT WILL BE EVALUATED
        /// RELATIVE TO THE REPO_PATH. If this is not the "natural" working
        /// directory, a .git gitlink file will be created here linking to the
        /// repo_path.
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? workdir_path_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(workdir_path);
            set => workdir_path = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <summary>
        /// If set, this will be used to initialize the "description" file in the
        /// repository, instead of using the template content.
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? description_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(description);
            set => description = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <summary>
        /// When GIT_REPOSITORY_INIT_EXTERNAL_TEMPLATE is set, this contains
        /// the path to use for the template directory. If this is NULL, the config
        /// or default directory options will be used instead.
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? template_path_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(template_path);
            set => template_path = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <summary>
        /// The name of the head to point HEAD at. If NULL, then this will be
        /// treated as "master" and the HEAD ref will be set to "refs/heads/master".
        /// If this begins with "refs/" it will be used verbatim;
        /// otherwise "refs/heads/" will be prefixed.
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? initial_head_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(initial_head);
            set => initial_head = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <summary>
        /// If this is non-NULL, then after the rest of the repository
        /// initialization is completed, an "origin" remote will be added
        /// pointing to this URL.
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? origin_url_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(origin_url);
            set => origin_url = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            LibGit2Helper.FreeUnmanagedUtf8String(workdir_path);
            workdir_path = null;
            LibGit2Helper.FreeUnmanagedUtf8String(description);
            description = null;
            LibGit2Helper.FreeUnmanagedUtf8String(template_path);
            template_path = null;
            LibGit2Helper.FreeUnmanagedUtf8String(initial_head);
            initial_head = null;
            LibGit2Helper.FreeUnmanagedUtf8String(origin_url);
            origin_url = null;
        }
    }

    /// <summary>
    /// Retrieve the configured identity to use for reflogs
    /// </summary>
    /// <param name="name">where to store the pointer to the name</param>
    /// <param name="email">where to store the pointer to the email</param>
    /// <param name="repo">the repository</param>
    /// <returns>@return 0 or an error code</returns>
    /// <remarks>
    /// The memory is owned by the repository and must not be freed by the
    /// user.
    /// </remarks>
    public static libgit2.git_result git_repository_ident(out string? name, out string? email, libgit2.git_repository repo)
    {
        var ret = git_repository_ident(out byte* name_ptr, out var email_ptr, repo);
        name = ret == 0 ? LibGit2Helper.UnmanagedUtf8StringToString(name_ptr) : null;
        email = ret == 0 ? LibGit2Helper.UnmanagedUtf8StringToString(email_ptr) : null;
        return ret;
    }
}