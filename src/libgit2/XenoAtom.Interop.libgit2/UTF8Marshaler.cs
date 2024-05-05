// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static partial class libgit2
{
    /// <summary>
    /// Custom marshaller used for marshalling a string as a UTF-8 string.
    /// </summary>
    [CustomMarshaller(typeof(string), MarshalMode.Default, typeof(Utf8CustomMarshaller))]
    [CustomMarshaller(typeof(ReadOnlySpan<char>), MarshalMode.ManagedToUnmanagedIn, typeof(Utf8CustomMarshaller.ManagedToUnmanagedIn))]
    private static unsafe class Utf8CustomMarshaller
    {
        /// <summary>Converts a string to an unmanaged version.</summary>
        /// <param name="managed">The managed string to convert.</param>
        /// <returns>An unmanaged string.</returns>
        public static byte* ConvertToUnmanaged(string? managed)
        {
            if (managed == null)
                return (byte*)null;
            int lengthPlus1 = UTF8EncodingRelaxed.Default.GetByteCount(managed) + 1;
            byte* pointer = (byte*)NativeMemory.Alloc((nuint)lengthPlus1);
            var span = new Span<byte>(pointer, lengthPlus1);
            int length = UTF8EncodingRelaxed.Default.GetBytes((ReadOnlySpan<char>)managed, span);
            span[length] = 0;
            return pointer;
        }

        /// <summary>Converts an unmanaged string to a managed version.</summary>
        /// <param name="unmanaged">The unmanaged string to convert.</param>
        /// <returns>A managed string.</returns>
        public static string? ConvertToManaged(byte* unmanaged)
        {
            if (unmanaged == null)
                return null;

            return UTF8EncodingRelaxed.Default.GetString(unmanaged, new ReadOnlySpan<byte>(unmanaged, int.MaxValue).IndexOf((byte)0));
        }

        /// <param name="unmanaged">The memory allocated for the unmanaged string.</param>
        public static void UserFree(byte* unmanaged) => NativeMemory.Free(unmanaged);

        /// <summary>Free the memory for a specified unmanaged string.</summary>
        /// <param name="unmanaged">The memory allocated for the unmanaged string.</param>
        public static void Free(byte* unmanaged)
        {
            // We don't free memory allocated by the marshaller
        }
        
        /// <summary>Custom marshaller to marshal a managed string as a UTF-8 unmanaged string.</summary>
        public ref struct ManagedToUnmanagedIn
        {
            private unsafe byte* _unmanagedValue;
            private bool _allocated;

            /// <summary>Gets the requested buffer size for optimized marshalling.</summary>
            public static int BufferSize => 256;

            /// <summary>Initializes the marshaller with a managed string and requested buffer.</summary>
            /// <param name="managed">The managed string with which to initialize the marshaller.</param>
            /// <param name="buffer">The request buffer whose size is at least <see cref="P:System.Runtime.InteropServices.Marshalling.Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize" />.</param>
            public void FromManaged(ReadOnlySpan<char> managed, Span<byte> buffer)
            {
                this._allocated = false;
                if (managed == null)
                {
                    this._unmanagedValue = (byte*)null;
                }
                else
                {
                    if (3L * (long)managed.Length >= (long)buffer.Length)
                    {
                        int num = checked(UTF8EncodingRelaxed.Default.GetByteCount(managed) + 1);
                        if (num > buffer.Length)
                        {
                            buffer = new Span<byte>(NativeMemory.Alloc((UIntPtr)num), num);
                            this._allocated = true;
                        }
                    }
                    this._unmanagedValue = (byte*)Unsafe.AsPointer<byte>(ref MemoryMarshal.GetReference<byte>(buffer));
                    int bytes = UTF8EncodingRelaxed.Default.GetBytes(managed, buffer);
                    buffer[bytes] = (byte)0;
                }
            }

            /// <summary>Converts the current managed string to an unmanaged string.</summary>
            /// <returns>An unmanaged string.</returns>
            public byte* ToUnmanaged() => this._unmanagedValue;

            /// <summary>Frees any allocated unmanaged memory.</summary>
            public void Free()
            {
                if (!this._allocated)
                    return;
                NativeMemory.Free((void*)this._unmanagedValue);
            }
        }
    }
    
    private sealed class UTF8EncodingRelaxed : UTF8Encoding
    {
        public new static readonly UTF8EncodingRelaxed Default = new UTF8EncodingRelaxed();

        private UTF8EncodingRelaxed() : base(false, false)
        {
        }
    }
}