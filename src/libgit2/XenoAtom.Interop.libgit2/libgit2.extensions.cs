// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace XenoAtom.Interop
{
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

        private static string GetExecutingAssemblyDirectory()
        {
            var path = Assembly.GetExecutingAssembly().CodeBase;
            if (!File.Exists(path))
            {
                path = Assembly.GetExecutingAssembly().Location;
            }
            else if (path.StartsWith("file:///"))
            {
                path = path.Substring(8).Replace('/', '\\');
            }
            else if (path.StartsWith("file://"))
            {
                path = "\\\\" + path.Substring(7).Replace('/', '\\');
            }
            return Path.GetDirectoryName(path);
        }

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

        //public readonly partial struct git_result_bool
        //{
        //    public bool Success => Value >= 0;

        //    public bool Failure => Value < 0;

        //    public git_error_code ErrorCode => (git_error_code) Value;

        //    public git_result_bool Check()
        //    {
        //        if (Failure)
        //        {
        //            var errorMessage = git_error_last()->ToString();
        //            throw new GitException(ErrorCode, errorMessage);
        //        }
        //        return this;
        //    }

        //    public static implicit operator bool(git_result_bool result)
        //    {
        //        return result.Value > 0;
        //    }
        //}

        public partial struct size_t
        {
            public static implicit operator long(size_t from) => from.Value.ToInt64();

            public static implicit operator size_t(long from) => new size_t(new IntPtr(from));

            public static implicit operator int(size_t from) => from.Value.ToInt32();

            public static implicit operator size_t(int from) => new size_t(new IntPtr(from));
        }

        public partial struct git_oid : IEquatable<git_oid>
        {
            const int Size = 20; 

            public override string ToString()
            {
                unsafe
                {
                    var builder = new StringBuilder(Size*2);
                    for (int i = 0; i < Size; i++)
                    {
                        var b = id[i];
                        builder.Append(b.ToString("x2"));
                    }
                    return builder.ToString();
                }
            }

            public bool Equals(git_oid other)
            {
                unsafe
                {
                    for (int i = 0; i < Size; i++)
                    {
                        if (id[i] != other.id[i]) return false;
                    }
                    return true;
                }
            }

            public override bool Equals(object obj)
            {
                return obj is git_oid other && Equals(other);
            }

            public override int GetHashCode()
            {
                unsafe
                {
                    fixed (byte* pInt = id)
                    {
                        return *(int*) pInt;
                    }
                }
            }

            public static bool operator ==(git_oid left, git_oid right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(git_oid left, git_oid right)
            {
                return !left.Equals(right);
            }
        }
    }
}