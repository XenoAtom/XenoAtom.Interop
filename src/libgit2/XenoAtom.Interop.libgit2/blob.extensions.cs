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
    /// Write an in-memory buffer to the ODB as a blob
    /// </summary>
    /// <param name="id">return the id of the written blob</param>
    /// <param name="repo">repository where the blob will be written</param>
    /// <param name="buffer">data to be written into the blob</param>
    /// <returns>@return 0 or an error code</returns>
    public static libgit2.git_result git_blob_create_from_buffer(out libgit2.git_oid id, libgit2.git_repository repo, ReadOnlySpan<byte> buffer)
    {
        fixed (byte* pBuffer = buffer)
        {
            return git_blob_create_from_buffer(out id, repo, pBuffer, buffer.Length);
        }
    }

    /// <summary>
    /// Determine if the given content is most certainly binary or not;
    /// this is the same mechanism used by `git_blob_is_binary` but only
    /// looking at raw data.
    /// </summary>
    /// <param name="data">The blob data which content should be analyzed</param>
    /// <returns>@return 1 if the content of the blob is detected
    /// as binary; 0 otherwise.</returns>
    public static int git_blob_data_is_binary(ReadOnlySpan<byte> data)
    {
        fixed (byte* pData = data)
        {
            return git_blob_data_is_binary(pData, data.Length);
        }
    }
}