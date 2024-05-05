// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static unsafe partial class libgit2
{
    static libgit2()
    {
        NativeLibrary.SetDllImportResolver(typeof(XenoAtom.Interop.libgit2).Assembly, (libraryName, methodName, searchPath) =>
        {
            if (libraryName == LibraryName)
            {
                var basePath = AppContext.BaseDirectory;

                if (NativeLibrary.TryLoad(Path.Combine(basePath, "git2-a418d9d.dll"), out var ptr))
                {
                    return ptr;
                }
                else if (NativeLibrary.TryLoad(Path.Combine(basePath, "runtimes", RuntimeInformation.RuntimeIdentifier, "native", "git2-a418d9d.dll"), out ptr))
                {
                    return ptr;
                }
                else if (NativeLibrary.TryLoad(LibraryName, out ptr))
                {
                    return ptr;
                }
            }

            return FallbackResolver?.Invoke(libraryName, methodName, searchPath) ?? IntPtr.Zero;
        });
    }

    public static System.Runtime.InteropServices.DllImportResolver? FallbackResolver { get; set; }

    //private const string GitLibName = "git2-a418d9d";
    //NativeLibrary.SetDllImportResolver
    private const string LibraryName = "git2";

    public readonly partial struct git_result
    {
        public bool Success => Value >= 0;

        public bool Failure => Value < 0;

        public git_error_code ErrorCode => (git_error_code)Value;

        public git_result Check()
        {
            if (Failure)
            {
                var errorMessage = git_error_last()->ToString();
                throw new LibGit2Exception(ErrorCode, errorMessage);
            }
            return this;
        }
    }

    public partial struct size_t
    {
        public static implicit operator long(size_t from) => from.Value.ToInt64();

        public static implicit operator size_t(long from) => new size_t(new IntPtr(from));

        public static implicit operator int(size_t from) => from.Value.ToInt32();

        public static implicit operator size_t(int from) => new size_t(new IntPtr(from));
    }
}