// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static unsafe partial class libgit2
{
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

        public override bool Equals(object? obj)
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