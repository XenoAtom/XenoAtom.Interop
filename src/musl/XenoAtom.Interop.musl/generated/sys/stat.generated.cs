//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;

namespace XenoAtom.Interop
{
    using System.Runtime.InteropServices;
    
    using System.Runtime.CompilerServices;
    
    public static unsafe partial class musl
    {
        public const int S_IFMT = 61440;
        
        public const int S_IFDIR = 16384;
        
        public const int S_IFCHR = 8192;
        
        public const int S_IFBLK = 24576;
        
        public const int S_IFREG = 32768;
        
        public const int S_IFIFO = 4096;
        
        public const int S_IFLNK = 40960;
        
        public const int S_IFSOCK = 49152;
        
        public const int UTIME_NOW = 1073741823;
        
        public const int UTIME_OMIT = 1073741822;
        
        public const int S_IREAD = 256;
        
        public const int S_IWRITE = 128;
        
        public const int S_IEXEC = 64;
        
        /// <summary>
        /// Change permissions of a file
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "chmod")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int chmod(byte* pathname, musl.mode_t mode);
        
        /// <summary>
        /// Change permissions of a file
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "chmod")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int chmod([global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> pathname, musl.mode_t mode);
        
        /// <summary>
        /// Change permissions of a file
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "fchmod")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int fchmod(int fd, musl.mode_t mode);
        
        /// <summary>
        /// Change permissions of a file
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "fchmodat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int fchmodat(int dirfd, byte* pathname, musl.mode_t mode, int flags);
        
        /// <summary>
        /// Change permissions of a file
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "fchmodat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int fchmodat(int dirfd, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> pathname, musl.mode_t mode, int flags);
        
        /// <summary>
        /// Set file mode creation mask
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "umask")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial musl.mode_t umask(musl.mode_t mask);
        
        /// <summary>
        /// Create a directory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mkdir")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mkdir(byte* pathname, musl.mode_t mode);
        
        /// <summary>
        /// Create a directory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mkdir")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mkdir([global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> pathname, musl.mode_t mode);
        
        /// <summary>
        /// Make a FIFO special file (a named pipe)
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mkfifo")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mkfifo(byte* pathname, musl.mode_t mode);
        
        /// <summary>
        /// Make a FIFO special file (a named pipe)
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mkfifo")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mkfifo([global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> pathname, musl.mode_t mode);
        
        /// <summary>
        /// Create a directory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mkdirat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mkdirat(int dirfd, byte* pathname, musl.mode_t mode);
        
        /// <summary>
        /// Create a directory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mkdirat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mkdirat(int dirfd, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> pathname, musl.mode_t mode);
        
        /// <summary>
        /// Make a FIFO special file (a named pipe)
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mkfifoat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mkfifoat(int dirfd, byte* pathname, musl.mode_t mode);
        
        /// <summary>
        /// Make a FIFO special file (a named pipe)
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mkfifoat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mkfifoat(int dirfd, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> pathname, musl.mode_t mode);
        
        /// <summary>
        /// Create a special or ordinary file
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mknod")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mknod(byte* pathname, musl.mode_t mode, musl.dev_t dev);
        
        /// <summary>
        /// Create a special or ordinary file
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mknod")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mknod([global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> pathname, musl.mode_t mode, musl.dev_t dev);
        
        /// <summary>
        /// Create a special or ordinary file
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mknodat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mknodat(int dirfd, byte* pathname, musl.mode_t mode, musl.dev_t dev);
        
        /// <summary>
        /// Create a special or ordinary file
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mknodat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mknodat(int dirfd, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> pathname, musl.mode_t mode, musl.dev_t dev);
        
        /// <summary>
        /// Change file timestamps with nanosecond precision
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "futimens")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int futimens(int fd, FixedArray2<musl.timespec> times);
        
        /// <summary>
        /// Change file timestamps with nanosecond precision
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "utimensat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int utimensat(int arg0, byte* arg1, FixedArray2<musl.timespec> arg2, int arg3);
        
        /// <summary>
        /// Change file timestamps with nanosecond precision
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "utimensat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int utimensat(int arg0, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> arg1, FixedArray2<musl.timespec> arg2, int arg3);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "lchmod")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int lchmod(byte* arg0, musl.mode_t arg1);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "lchmod")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int lchmod([global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> arg0, musl.mode_t arg1);
    }
}
