// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static partial class libgit2
{
    /// <summary>
    /// Exception triggered by <see cref="git_result.Check"/> if <see cref="git_result.Failure"/> is <c>true</c>
    /// </summary>
    public class LibGit2Exception : Exception
    {
        /// <summary>
        /// Create a new instance of this exception.
        /// </summary>
        /// <param name="errorCode">The error code</param>
        /// <param name="message">The message</param>
        public LibGit2Exception(git_error_code errorCode, string? message) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Gets the error code associated with this exception.
        /// </summary>
        public git_error_code ErrorCode { get; }

        /// <summary>
        /// Gets the error class.
        /// </summary>
        public int ErrorClass { get; init; }

        /// <inheritdoc />
        public override string ToString() => $"Error {ErrorCode}/{ErrorClass}: {Message}";
    }
}