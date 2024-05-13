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
    /// Apply filter list to a data buffer.
    /// </summary>
    /// <param name="out">Buffer to store the result of the filtering</param>
    /// <param name="filters">A loaded git_filter_list (or NULL)</param>
    /// <param name="in">Buffer containing the data to filter</param>
    /// <returns>@return 0 on success, an error code otherwise</returns>
    public static libgit2.git_result git_filter_list_apply_to_buffer(out libgit2.git_buf @out, libgit2.git_filter_list filters, ReadOnlySpan<byte> @in)
    {
        fixed (byte* pIn = @in)
        {
            return git_filter_list_apply_to_buffer(out @out, filters, pIn, (nuint)@in.Length);
        }
    }

    /// <summary>
    /// Apply a filter list to an arbitrary buffer as a stream
    /// </summary>
    /// <param name="filters">the list of filters to apply</param>
    /// <param name="buffer">the buffer to filter</param>
    /// <param name="target">the stream into which the data will be written</param>
    /// <returns>@return 0 or an error code.</returns>
    public static libgit2.git_result git_filter_list_stream_buffer(libgit2.git_filter_list filters, ReadOnlySpan<byte> buffer, ref libgit2.git_writestream target)
    {
        fixed (byte* pBuffer = buffer)
        {
            return git_filter_list_stream_buffer(filters, pBuffer, (nuint)buffer.Length, ref target);
        }
    }
}