// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

/// <summary>
/// This class is a C# representation of the libgit2 library.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static unsafe partial class libgit2
{
    private const DllImportSearchPath DefaultDllImportSearchPath = DllImportSearchPath.ApplicationDirectory | DllImportSearchPath.UserDirectories | DllImportSearchPath.UseDllDirectoryForDependencies;

    /// <summary>
    /// Set the resolver for the libgit2 native library.
    /// </summary>
    static libgit2()
    {
        NativeLibrary.SetDllImportResolver(typeof(libgit2).Assembly, (libraryName, methodName, searchPath) =>
        {
            if (libraryName == LibraryName)
            {
                var ptr = IntPtr.Zero;
                var resolver = Libgit2DllImportResolver;
                if (resolver != null)
                {
                    ptr = resolver(libraryName, methodName, searchPath);
                }

                if (ptr != IntPtr.Zero)
                {
                    return ptr;
                }

                if (NativeLibrary.TryLoad(AlternativeLibraryName, typeof(libgit2).Assembly, DefaultDllImportSearchPath, out ptr)
                    || NativeLibrary.TryLoad(LibraryName, typeof(libgit2).Assembly, DefaultDllImportSearchPath, out ptr))
                {
                    return ptr;
                }
            }

            return IntPtr.Zero;
        });
    }

    public static System.Runtime.InteropServices.DllImportResolver? Libgit2DllImportResolver { get; set; }

    private const string LibraryName = "git2";

    /// <summary>
    /// Matching the version compatible from LibGit2Sharp.NativeBinaries
    /// </summary>
    private const string AlternativeLibraryName = "git2-a418d9d";

    /// <summary>
    /// Wrapper for the result of a libgit2 function.
    /// </summary>
    public readonly partial struct git_result
    {
        /// <summary>
        /// Gets a value indicating whether the result is a success.
        /// </summary>
        public bool Success => Value >= 0;

        /// <summary>
        /// Gets a value indicating whether the result is a failure.
        /// </summary>
        public bool Failure => Value < 0;

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public git_error_code ErrorCode => (git_error_code)Value;

        /// <summary>
        /// Check if there was a failure, retrieve the error code via <see cref="git_error_last"/> and throw a <see cref="LibGit2Exception"/>.
        /// </summary>
        /// <returns>The original git_result if no exceptions was thrown.</returns>
        /// <exception cref="LibGit2Exception">If this git_result has an error code that indicates a failure.</exception>
        public git_result Check()
        {
            if (Failure)
            {
                var errorLast = git_error_last();
                if (errorLast != null)
                {
                    var errorMessage = git_error_last()->ToString();
                    throw new LibGit2Exception(ErrorCode, errorMessage)
                    {
                        ErrorClass = errorLast->klass
                    };
                }
                else
                {
                    throw new LibGit2Exception(ErrorCode, "Unknown libgit2 error");
                }
            }
            return this;
        }
    }
}