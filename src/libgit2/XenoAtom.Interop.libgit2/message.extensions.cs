// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_message_trailer
    {
        public string? key_string => LibGit2Helper.UnmanagedUtf8StringToString(key);

        public string? value_string => LibGit2Helper.UnmanagedUtf8StringToString(value);
    }

    partial struct git_message_trailer_array
    {
        public ReadOnlySpan<libgit2.git_message_trailer> AsSpan() => trailers == null ? new() : new(trailers, (int)count);
    }
}