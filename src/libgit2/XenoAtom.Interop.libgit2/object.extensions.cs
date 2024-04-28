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
    /// Analyzes a buffer of raw object content and determines its validity.
    /// Tree, commit, and tag objects will be parsed and ensured that they
    /// are valid, parseable content.  (Blobs are always valid by definition.)
    /// An error message will be set with an informative message if the object
    /// is not valid.
    /// </summary>
    /// <param name="valid">Output pointer to set with validity of the object content</param>
    /// <param name="buf">The contents to validate</param>
    /// <param name="len">The length of the buffer</param>
    /// <param name="object_type">The type of the object in the buffer</param>
    /// <returns>@return 0 on success or an error code</returns>
    /// <warning>
    /// @warning This function is experimental and its signature may change in
    /// the future.
    /// </warning>
    public static libgit2.git_result git_object_rawcontent_is_valid(out int valid, ReadOnlySpan<byte> buf, libgit2.git_object_t object_type)
    {
        fixed (byte* bufPtr = buf)
        {
            return git_object_rawcontent_is_valid(out valid, bufPtr, (UIntPtr)buf.Length, object_type);
        }
    }
}