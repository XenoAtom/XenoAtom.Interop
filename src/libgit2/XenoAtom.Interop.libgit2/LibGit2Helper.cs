// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

namespace XenoAtom.Interop;

static partial class libgit2
{
    public static class LibGit2Helper
    {
        public static unsafe string? UnmanagedUtf8StringToString(byte* pNativeData) => Utf8CustomMarshaller.ConvertToManaged(pNativeData);

        public static unsafe string? UnmanagedUtf8StringToString(byte* pNativeData, int size) => pNativeData == null ? null : new((sbyte*)pNativeData, 0, size, UTF8EncodingRelaxed.Default);

        public static unsafe byte* StringToUnmanagedUtf8String(string? managed) => Utf8CustomMarshaller.ConvertToUnmanaged(managed);

        public static unsafe void FreeUnmanagedUtf8String(byte* utf8String) => Utf8CustomMarshaller.Free(utf8String);
    }
}