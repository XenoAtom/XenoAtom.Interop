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

        [DebuggerDisplay("Count = {count}")]
        public partial struct git_strarray : IEnumerable<string>
        {
            /// <summary>
            /// gets the number of strings in this array.
            /// </summary>
            public int Length => count;
            
            /// <summary>
            /// Gets the string from the specified array index.
            /// </summary>
            /// <param name="index">An index into the array</param>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public string this[int index]
            {
                get
                {
                    if (index < 0 || index > count) throw new ArgumentOutOfRangeException();
                    unsafe
                    {
                        return UTF8MarshalerRelaxed.FromNative(((IntPtr*)strings)[index]);
                    }
                }
            }

            /// <summary>
            /// Creates a managed array of string from this git native array of string.
            /// </summary>
            /// <returns>A managed array of string from this git native array of string.</returns>
            public string[] ToArray()
            {
                var array = new string[count];
                for(int i = 0; i < count; i++)
                {
                    array[i] = this[i];
                }

                return array;
            }

            /// <summary>
            /// Allocate a new instance of git native array of string from the specified array of string.
            /// The returned instance must be free by calling later <see cref="Free"/>
            /// </summary>
            /// <param name="array">A managed array of string</param>
            /// <returns>A git native array of string</returns>
            public static unsafe git_strarray Allocate(string[] array)
            {
                if (array == null || array.Length == 0) return new git_strarray();

                var nativeArray = new git_strarray
                {
                    strings = (byte**)NativeMemory.Alloc((nuint)(array.Length * nint.Size)), 
                    count = array.Length
                };
                for (int i = 0; i < array.Length; i++)
                {
                    ((IntPtr*) nativeArray.strings)[i] = UTF8MarshalerRelaxed.ToNative(array[i]);
                }

                return nativeArray;
            }

            /// <summary>
            /// Frees this instance. Must have been created by <see cref="Allocate"/>. This method should not be call
            /// on <see cref="git_strarray"/> returned by git methods. 
            /// </summary>
            public unsafe void Free()
            {
                for (int i = 0; i < count; i++)
                {
                    UTF8MarshalerRelaxed.FreeNative(((IntPtr*) strings)[i]);
                }

                count = 0;
                NativeMemory.Free(strings);
                strings = null;
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(this);
            }

            IEnumerator<string> IEnumerable<string>.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public struct Enumerator : IEnumerator<string>
            {
                private git_strarray _array;
                private int index;

                public Enumerator(git_strarray array) : this()
                {
                    _array = array;
                    index = -1;
                }

                public void Dispose()
                {
                }

                public bool MoveNext()
                {
                    if ((index + 1) < _array.count)
                    {
                        index++;
                        return true;
                    }
                    return false;
                }

                public void Reset()
                {
                    index = -1;
                }

                public string Current
                {
                    get
                    {
                        if (index < 0) throw new InvalidOperationException("Must call MoveNext() before Current");
                        
                        if (index >= _array.count) throw new InvalidOperationException("Cannot call Current after last element");
                        
                        return _array[index];
                    }
                }

                object IEnumerator.Current => Current;
            }
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