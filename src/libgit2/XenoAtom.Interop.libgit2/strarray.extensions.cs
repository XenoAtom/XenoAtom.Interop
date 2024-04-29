// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static unsafe partial class libgit2
{

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
        public string? this[int index]
        {
            get
            {
                if (index < 0 || index > count) throw new ArgumentOutOfRangeException();
                return UTF8CustomMarshaller.ConvertToManaged(strings[index]);
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
        public static git_strarray Allocate(string[]? array)
        {
            if (array is null || array.Length == 0) return new git_strarray();

            var nativeArray = new git_strarray
            {
                strings = (byte**)NativeMemory.Alloc((nuint)(array.Length * nint.Size)), 
                count = array.Length
            };
            for (int i = 0; i < array.Length; i++)
            {
                nativeArray.strings[i] = UTF8CustomMarshaller.ConvertToUnmanaged(array[i]);
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
                UTF8CustomMarshaller.Free(strings[i]);
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
}