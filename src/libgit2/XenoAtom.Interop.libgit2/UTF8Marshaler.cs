// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace XenoAtom.Interop;

static partial class libgit2
{
    private sealed class UTF8EncodingStrict : UTF8Encoding
    {
        public UTF8EncodingStrict() : base(false, true)
        {
        }
    }

    private sealed class UTF8EncodingRelaxed : UTF8Encoding
    {
        public new static readonly UTF8EncodingRelaxed Default = new UTF8EncodingRelaxed();  
            
        public UTF8EncodingRelaxed() : base(false, false)
        {
        }
    }

    private class UTF8MarshalerBase<T> : ICustomMarshaler where T : Encoding, new()
    {
        private static readonly Encoding EncodingUsed = new T();

        public static unsafe string FromNative(byte* pNativeData) => FromNative((nint)pNativeData);

        public static unsafe string FromNative(IntPtr pNativeData)
        {
            var pBuffer = (byte*)pNativeData;
            if (pBuffer == null) return string.Empty;
            return EncodingUsed.GetString((byte*)pNativeData, (new Span<byte>(pBuffer, int.MaxValue).IndexOf((byte)0)));
        }

        public static unsafe IntPtr ToNative(string text, bool strict = false)
        {
            if (text == null) return IntPtr.Zero;

            var length = EncodingUsed.GetByteCount(text);

            var pBuffer = (byte*)Marshal.AllocHGlobal(length + 1);
            if (pBuffer == null) return IntPtr.Zero;

            if (length > 0)
            {
                fixed (char* ptr = text)
                {
                    EncodingUsed.GetBytes(ptr, text.Length, pBuffer, length);
                }
            }

            pBuffer[length] = 0;

            return new IntPtr(pBuffer);
        }

        public static void FreeNative(IntPtr pNativeData)
        {
            if (pNativeData == IntPtr.Zero) return;
            Marshal.FreeHGlobal(pNativeData);
        }

        public virtual object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return FromNative(pNativeData);
        }

        public virtual IntPtr MarshalManagedToNative(object managedObj)
        {
            var text = (string) managedObj;
            return ToNative(text);
        }

        public virtual void CleanUpNativeData(IntPtr pNativeData)
        {
            FreeNative(pNativeData);
        }

        public virtual void CleanUpManagedData(object ManagedObj)
        {
        }

        public int GetNativeDataSize()
        {
            return -1;
        }
    }

    private sealed class UTF8MarshalerRelaxed : UTF8MarshalerBase<UTF8EncodingRelaxed>
    {
        private static readonly UTF8MarshalerRelaxed Instance = new UTF8MarshalerRelaxed();

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return Instance;
        }
    }


    private sealed class UTF8MarshalerStrict : UTF8MarshalerBase<UTF8EncodingStrict>
    {
        private static readonly UTF8MarshalerStrict Instance = new UTF8MarshalerStrict();

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return Instance;
        }

        public override IntPtr MarshalManagedToNative(object managedObj)
        {
            var text = (string)managedObj;
            return ToNative(text, true);
        }
    }

    internal static unsafe string? GetStringFromUTF8(byte* pNativeData) => UTF8MarshallerRelaxedNoCleanup.ConvertToManaged(pNativeData);

    internal static unsafe string? GetStringFromUTF8(byte* pNativeData, int size) => pNativeData == null ? null : new((sbyte*)pNativeData, 0, size, UTF8EncodingRelaxed.Default);


    [CustomMarshaller(typeof(string), MarshalMode.Default, typeof(UTF8MarshallerRelaxedNoCleanup))]
    [CustomMarshaller(typeof(ReadOnlySpan<char>), MarshalMode.ManagedToUnmanagedIn, typeof(UTF8MarshallerRelaxedNoCleanup.ManagedToUnmanagedIn))]
    private static unsafe class UTF8MarshallerRelaxedNoCleanup
    {
        /// <summary>Converts a string to an unmanaged version.</summary>
        /// <param name="managed">The managed string to convert.</param>
        /// <returns>An unmanaged string.</returns>
        public static unsafe byte* ConvertToUnmanaged(string? managed) => throw new NotSupportedException();

        /// <summary>Converts an unmanaged string to a managed version.</summary>
        /// <param name="unmanaged">The unmanaged string to convert.</param>
        /// <returns>A managed string.</returns>
        public static unsafe string? ConvertToManaged(byte* unmanaged)
        {
            return Marshal.PtrToStringUTF8((IntPtr)unmanaged);
        }

        /// <summary>Free the memory for a specified unmanaged string.</summary>
        /// <param name="unmanaged">The memory allocated for the unmanaged string.</param>
        public static unsafe void Free(byte* unmanaged)
        {
            // No cleanup
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
            public unsafe void FromManaged(ReadOnlySpan<char> managed, Span<byte> buffer)
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
            public unsafe byte* ToUnmanaged() => this._unmanagedValue;

            /// <summary>Frees any allocated unmanaged memory.</summary>
            public unsafe void Free()
            {
                if (!this._allocated)
                    return;
                NativeMemory.Free((void*)this._unmanagedValue);
            }
        }
    }

    private abstract class CustomMarshaler<TManaged, TMarshal> : ICustomMarshaler where TMarshal : unmanaged
    {
        public abstract void MarshalNativeToManaged(ref TMarshal marshal, out TManaged managed);

        public abstract void MarshalManagedToNative(ref TManaged managed, out TMarshal marshal);

        public abstract void CleanUpNativeData(ref TMarshal marshal);

        public abstract void CleanUpManagedData(ref TManaged managed);
            
        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            throw new NotImplementedException();
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            throw new NotImplementedException();
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            throw new NotImplementedException();
        }

        public void CleanUpManagedData(object ManagedObj)
        {
            throw new NotImplementedException();
        }

        public int GetNativeDataSize()
        {
            throw new NotImplementedException();
        }
    }

    private class BoolToIntMarshaler : CustomMarshaler<bool, int>
    {
        public static readonly BoolToIntMarshaler Instance = new BoolToIntMarshaler();

        public static ICustomMarshaler GetInstance(string cookie)
        {
            return Instance;
        }
            
        public override void MarshalNativeToManaged(ref int marshal, out bool managed)
        {
            managed = marshal != 0;
        }

        public override void MarshalManagedToNative(ref bool managed, out int marshal)
        {
            marshal = managed ? 1 : 0;
        }

        public override void CleanUpNativeData(ref int marshal)
        {
        }

        public override void CleanUpManagedData(ref bool managed)
        {
        }
    }
}