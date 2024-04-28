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
    /// Add data to the indexer
    /// </summary>
    /// <param name="idx">the indexer</param>
    /// <param name="data">the data to add</param>
    /// <param name="stats">stat storage</param>
    /// <returns>@return 0 or an error code.</returns>
    public static libgit2.git_result git_indexer_append(libgit2.git_indexer idx, ReadOnlySpan<byte> data, out libgit2.git_indexer_progress stats)
    {
        fixed (byte* p_data = data)
        {
            return git_indexer_append(idx, p_data, data.Length, out stats);
        }
    }
}