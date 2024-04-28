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
    /// Determine if one or more objects can be found in the object database
    /// by their abbreviated object ID and type.
    /// </summary>
    /// <param name="db">The database to be searched for the given objects.</param>
    /// <param name="ids">An array of short object IDs to search for</param>
    /// <param name="count">The length of the `ids` array</param>
    /// <returns>@return 0 on success or an error code on failure</returns>
    /// <remarks>
    /// The given array will be updated in place: for each abbreviated ID that is
    /// unique in the database, and of the given type (if specified),
    /// the full object ID, object ID length (`GIT_OID_SHA1_HEXSIZE`) and type will be
    /// written back to the array. For IDs that are not found (or are ambiguous),
    /// the array entry will be zeroed.Note that since this function operates on multiple objects, the
    /// underlying database will not be asked to be reloaded if an object is
    /// not found (which is unlike other object database operations.)
    /// </remarks>
    public static libgit2.git_result git_odb_expand_ids(libgit2.git_odb db, ReadOnlySpan<libgit2.git_odb_expand_id> ids)
    {
        fixed (libgit2.git_odb_expand_id* pids = ids)
        {
            return git_odb_expand_ids(db, pids, ids.Length);
        }
    }

    /// <summary>
    /// Write an object directly into the ODB
    /// </summary>
    /// <param name="out">pointer to store the OID result of the write</param>
    /// <param name="odb">object database where to store the object</param>
    /// <param name="data">buffer with the data to store</param>
    /// <param name="type">type of the data to store</param>
    /// <returns>@return 0 or an error code</returns>
    /// <remarks>
    /// This method writes a full object straight into the ODB.
    /// For most cases, it is preferred to write objects through a write
    /// stream, which is both faster and less memory intensive, specially
    /// for big objects.This method is provided for compatibility with custom backends
    /// which are not able to support streaming writes
    /// </remarks>
    public static libgit2.git_result git_odb_write(out libgit2.git_oid @out, libgit2.git_odb odb, ReadOnlySpan<byte> data, libgit2.git_object_t type)
    {
        fixed (byte* pdata = data)
        {
            return git_odb_write(out @out, odb, pdata, data.Length, type);
        }
    }

    /// <summary>
    /// Write to an odb stream
    /// </summary>
    /// <param name="stream">the stream</param>
    /// <param name="buffer">the data to write</param>
    /// <returns>@return 0 if the write succeeded, error code otherwise</returns>
    /// <remarks>
    /// This method will fail if the total number of received bytes exceeds the
    /// size declared with `git_odb_open_wstream()`
    /// </remarks>
    public static libgit2.git_result git_odb_stream_write(ref libgit2.git_odb_stream stream, ReadOnlySpan<byte> buffer)
    {
        fixed (byte* pbuffer = buffer)
        {
            return git_odb_stream_write(ref stream, pbuffer, buffer.Length);
        }
    }

    /// <summary>
    /// Read from an odb stream
    /// </summary>
    /// <param name="stream">the stream</param>
    /// <param name="buffer">a user-allocated buffer to store the data in.</param>
    /// <returns>@return 0 if the read succeeded, error code otherwise</returns>
    /// <remarks>
    /// Most backends don't implement streaming reads
    /// </remarks>
    public static libgit2.git_result git_odb_stream_read(libgit2.git_odb_stream* stream, Span<byte> buffer)
    {
        fixed (byte* pbuffer = buffer)
        {
            return git_odb_stream_read(stream, pbuffer, buffer.Length);
        }
    }

    public static libgit2.git_result git_odb_hash(out libgit2.git_oid @out, ReadOnlySpan<byte> data, libgit2.git_object_t type)
    {
        fixed (byte* pdata = data)
        {
            return git_odb_hash(out @out, pdata, data.Length, type);
        }
    }
}