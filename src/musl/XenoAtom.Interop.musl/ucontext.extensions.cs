// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class musl
{
    partial class x86_64
    {
        /// <summary>
        /// Manipulate user context
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "makecontext")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void makecontext(ref musl.x86_64.ucontext ucp, delegate* unmanaged[Cdecl]<void> func, int argc, int arg1);

        /// <summary>
        /// Manipulate user context
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "makecontext")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void makecontext(ref musl.x86_64.ucontext ucp, delegate* unmanaged[Cdecl]<void> func, int argc, int arg1, int arg2);

        /// <summary>
        /// Manipulate user context
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "makecontext")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void makecontext(ref musl.x86_64.ucontext ucp, delegate* unmanaged[Cdecl]<void> func, int argc, int arg1, int arg2, int arg3);

        /// <summary>
        /// Manipulate user context
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "makecontext")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void makecontext(ref musl.x86_64.ucontext ucp, delegate* unmanaged[Cdecl]<void> func, int argc, int arg1, int arg2, int arg3, int arg4);

        /// <summary>
        /// Manipulate user context
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "makecontext")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void makecontext(ref musl.x86_64.ucontext ucp, delegate* unmanaged[Cdecl]<void> func, int argc, int arg1, int arg2, int arg3, int arg4, int arg5);

        /// <summary>
        /// Manipulate user context
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "makecontext")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void makecontext(ref musl.x86_64.ucontext ucp, delegate* unmanaged[Cdecl]<void> func, int argc, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6);
    }
    public static unsafe partial class aarch_64
    {
        /// <summary>
        /// Manipulate user context
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "makecontext")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void makecontext(ref musl.aarch64.ucontext ucp, delegate* unmanaged[Cdecl]<void> func, int argc, int arg1);

        /// <summary>
        /// Manipulate user context
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "makecontext")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void makecontext(ref musl.aarch64.ucontext ucp, delegate* unmanaged[Cdecl]<void> func, int argc, int arg1, int arg2);

        /// <summary>
        /// Manipulate user context
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "makecontext")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void makecontext(ref musl.aarch64.ucontext ucp, delegate* unmanaged[Cdecl]<void> func, int argc, int arg1, int arg2, int arg3);

        /// <summary>
        /// Manipulate user context
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "makecontext")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void makecontext(ref musl.aarch64.ucontext ucp, delegate* unmanaged[Cdecl]<void> func, int argc, int arg1, int arg2, int arg3, int arg4);

        /// <summary>
        /// Manipulate user context
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "makecontext")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void makecontext(ref musl.aarch64.ucontext ucp, delegate* unmanaged[Cdecl]<void> func, int argc, int arg1, int arg2, int arg3, int arg4, int arg5);

        /// <summary>
        /// Manipulate user context
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "makecontext")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void makecontext(ref musl.aarch64.ucontext ucp, delegate* unmanaged[Cdecl]<void> func, int argc, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6);
    }
}