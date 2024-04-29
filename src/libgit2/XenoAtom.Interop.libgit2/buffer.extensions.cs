// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_buf
    {
        /// <summary>
        /// Gets the span associated to this buffer.
        /// </summary>
        /// <returns>The span associated to this buffer</returns>
        public Span<byte> AsSpan() => ptr != null ? new Span<byte>(ptr, (int)size) : Span<byte>.Empty;

        /// <summary>
        /// Gets the string associated to this buffer using UTF8 encoding.
        /// </summary>
        /// <returns>The span associated to this buffer</returns>
        public string? AsString() => LibGit2Helper.UnmanagedUtf8StringToString(ptr, size);
    }
}