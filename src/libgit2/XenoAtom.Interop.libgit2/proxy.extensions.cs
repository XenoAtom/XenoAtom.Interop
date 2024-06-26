// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_proxy_options
    {
        /// <summary>
        /// The URL of the proxy.
        /// </summary>
        public string? url_string => LibGit2Helper.UnmanagedUtf8StringToString(url);
    }
}