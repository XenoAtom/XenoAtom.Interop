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
    static partial class x86_64
    {
        /// <summary>
        /// Structure used by semctl.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct semun
        {
            [FieldOffset(0)]
            public int val;    /* Value for SETVAL */

            [FieldOffset(0)]
            public semid_ds* buf;    /* Buffer for IPC_STAT, IPC_SET */

            [FieldOffset(0)]
            public ushort* array;  /* Array for GETALL, SETALL */

            [FieldOffset(0)]
            public seminfo  *__buf;  /* Buffer for IPC_INFO (Linux-specific) */
        };

        /// <summary>
        /// System V semaphore control operations
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "semctl")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int semctl(int semid, int semnum, int cmd, semun value);
    }

    static partial class aarch64
    {
        /// <summary>
        /// Structure used by semctl.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct semun
        {
            [FieldOffset(0)]
            public int val;    /* Value for SETVAL */

            [FieldOffset(0)]
            public semid_ds* buf;    /* Buffer for IPC_STAT, IPC_SET */

            [FieldOffset(0)]
            public ushort* array;  /* Array for GETALL, SETALL */

            [FieldOffset(0)]
            public seminfo* __buf;  /* Buffer for IPC_INFO (Linux-specific) */
        };

        /// <summary>
        /// System V semaphore control operations
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "semctl")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int semctl(int semid, int semnum, int cmd, semun value);
    }
}