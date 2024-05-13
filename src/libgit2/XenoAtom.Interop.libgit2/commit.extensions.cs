// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    /// <summary>
    /// Create new commit in the repository from a list of `git_object` pointers
    /// </summary>
    /// <param name="id">Pointer in which to store the OID of the newly created commit</param>
    /// <param name="repo">Repository where to store the commit</param>
    /// <param name="update_ref">If not NULL, name of the reference that
    /// will be updated to point to this commit. If the reference
    /// is not direct, it will be resolved to a direct reference.
    /// Use "HEAD" to update the HEAD of the current branch and
    /// make it point to this commit. If the reference doesn't
    /// exist yet, it will be created. If it does exist, the first
    /// parent must be the tip of this branch.</param>
    /// <param name="author">Signature with author and author time of commit</param>
    /// <param name="committer">Signature with committer and * commit time of commit</param>
    /// <param name="message_encoding">The encoding for the message in the
    /// commit, represented with a standard encoding name.
    /// E.g. "UTF-8". If NULL, no encoding header is written and
    /// UTF-8 is assumed.</param>
    /// <param name="message">Full message for this commit</param>
    /// <param name="tree">An instance of a `git_tree` object that will
    /// be used as the tree for the commit. This tree object must
    /// also be owned by the given `repo`.</param>
    /// <param name="parents">Array of `parent_count` pointers to `git_commit`
    /// objects that will be used as the parents for this commit. This
    /// array may be NULL if `parent_count` is 0 (root commit). All the
    /// given commits must be owned by the `repo`.</param>
    /// <returns>@return 0 or an error code
    /// The created commit will be written to the Object Database and
    /// the given reference will be updated to point to it</returns>
    /// <remarks>
    /// The message will **not** be cleaned up automatically. You can do that
    /// with the `git_message_prettify()` function.
    /// </remarks>
    public static libgit2.git_result git_commit_create(out libgit2.git_oid id, libgit2.git_repository repo, byte* update_ref, in libgit2.git_signature author, in libgit2.git_signature committer, byte* message_encoding, byte* message,
        libgit2.git_tree tree, ReadOnlySpan<libgit2.git_commit> parents)
    {
        fixed (libgit2.git_commit* parentsPtr = parents)
        {
            return git_commit_create(out id, repo, update_ref, author, committer, message_encoding, message, tree, (nuint)parents.Length, parentsPtr);
        }
    }

    /// <summary>
    /// Create new commit in the repository from a list of `git_object` pointers
    /// </summary>
    /// <param name="id">Pointer in which to store the OID of the newly created commit</param>
    /// <param name="repo">Repository where to store the commit</param>
    /// <param name="update_ref">If not NULL, name of the reference that
    /// will be updated to point to this commit. If the reference
    /// is not direct, it will be resolved to a direct reference.
    /// Use "HEAD" to update the HEAD of the current branch and
    /// make it point to this commit. If the reference doesn't
    /// exist yet, it will be created. If it does exist, the first
    /// parent must be the tip of this branch.</param>
    /// <param name="author">Signature with author and author time of commit</param>
    /// <param name="committer">Signature with committer and * commit time of commit</param>
    /// <param name="message_encoding">The encoding for the message in the
    /// commit, represented with a standard encoding name.
    /// E.g. "UTF-8". If NULL, no encoding header is written and
    /// UTF-8 is assumed.</param>
    /// <param name="message">Full message for this commit</param>
    /// <param name="tree">An instance of a `git_tree` object that will
    /// be used as the tree for the commit. This tree object must
    /// also be owned by the given `repo`.</param>
    /// <param name="parents">Array of `parent_count` pointers to `git_commit`
    /// objects that will be used as the parents for this commit. This
    /// array may be NULL if `parent_count` is 0 (root commit). All the
    /// given commits must be owned by the `repo`.</param>
    /// <returns>@return 0 or an error code
    /// The created commit will be written to the Object Database and
    /// the given reference will be updated to point to it</returns>
    /// <remarks>
    /// The message will **not** be cleaned up automatically. You can do that
    /// with the `git_message_prettify()` function.
    /// </remarks>
    public static libgit2.git_result git_commit_create(out libgit2.git_oid id, libgit2.git_repository repo, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] string update_ref,
        in libgit2.git_signature author, in libgit2.git_signature committer, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] string message_encoding,
        [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] string message, libgit2.git_tree tree, ReadOnlySpan<libgit2.git_commit> parents)
    {
        fixed (libgit2.git_commit* parentsPtr = parents)
        {
            return git_commit_create(out id, repo, update_ref, author, committer, message_encoding, message, tree, (nuint)parents.Length, parentsPtr);
        }
    }

    /// <summary>
    /// Create a commit and write it into a buffer
    /// </summary>
    /// <param name="out">the buffer into which to write the commit object content</param>
    /// <param name="repo">Repository where the referenced tree and parents live</param>
    /// <param name="author">Signature with author and author time of commit</param>
    /// <param name="committer">Signature with committer and * commit time of commit</param>
    /// <param name="message_encoding">The encoding for the message in the
    /// commit, represented with a standard encoding name.
    /// E.g. "UTF-8". If NULL, no encoding header is written and
    /// UTF-8 is assumed.</param>
    /// <param name="message">Full message for this commit</param>
    /// <param name="tree">An instance of a `git_tree` object that will
    /// be used as the tree for the commit. This tree object must
    /// also be owned by the given `repo`.</param>
    /// <param name="parents">Array of `parent_count` pointers to `git_commit`
    /// objects that will be used as the parents for this commit. This
    /// array may be NULL if `parent_count` is 0 (root commit). All the
    /// given commits must be owned by the `repo`.</param>
    /// <returns>@return 0 or an error code</returns>
    /// <remarks>
    /// Create a commit as with `git_commit_create()` but instead of
    /// writing it to the objectdb, write the contents of the object into a
    /// buffer.
    /// </remarks>
    public static libgit2.git_result git_commit_create_buffer(out libgit2.git_buf @out, libgit2.git_repository repo, in libgit2.git_signature author, in libgit2.git_signature committer, byte* message_encoding, byte* message,
        libgit2.git_tree tree, ReadOnlySpan<libgit2.git_commit> parents)
    {
        fixed (libgit2.git_commit* parentsPtr = parents)
        {
            return git_commit_create_buffer(out @out, repo, author, committer, message_encoding, message, tree, (nuint)parents.Length, parentsPtr);
        }
    }

    /// <summary>
    /// Create a commit and write it into a buffer
    /// </summary>
    /// <param name="out">the buffer into which to write the commit object content</param>
    /// <param name="repo">Repository where the referenced tree and parents live</param>
    /// <param name="author">Signature with author and author time of commit</param>
    /// <param name="committer">Signature with committer and * commit time of commit</param>
    /// <param name="message_encoding">The encoding for the message in the
    /// commit, represented with a standard encoding name.
    /// E.g. "UTF-8". If NULL, no encoding header is written and
    /// UTF-8 is assumed.</param>
    /// <param name="message">Full message for this commit</param>
    /// <param name="tree">An instance of a `git_tree` object that will
    /// be used as the tree for the commit. This tree object must
    /// also be owned by the given `repo`.</param>
    /// <param name="parents">Array of `parent_count` pointers to `git_commit`
    /// objects that will be used as the parents for this commit. This
    /// array may be NULL if `parent_count` is 0 (root commit). All the
    /// given commits must be owned by the `repo`.</param>
    /// <returns>@return 0 or an error code</returns>
    /// <remarks>
    /// Create a commit as with `git_commit_create()` but instead of
    /// writing it to the objectdb, write the contents of the object into a
    /// buffer.
    /// </remarks>
    public static libgit2.git_result git_commit_create_buffer(out libgit2.git_buf @out, libgit2.git_repository repo, in libgit2.git_signature author, in libgit2.git_signature committer,
        [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] string message_encoding,
        [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] string message, libgit2.git_tree tree, ReadOnlySpan<libgit2.git_commit> parents)
    {
        fixed (libgit2.git_commit* parentsPtr = parents)
        {
            return git_commit_create_buffer(out @out, repo, author, committer, message_encoding, message, tree, (nuint)parents.Length, parentsPtr);
        }
    }

}