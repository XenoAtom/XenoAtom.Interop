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
    
    public static unsafe partial class libgit2
    {
        [Flags]
        public enum git_odb_backend_loose_flag_t : uint
        {
            GIT_ODB_BACKEND_LOOSE_FSYNC = unchecked((uint)(1<<0)),
        }
        
        public const libgit2.git_odb_backend_loose_flag_t GIT_ODB_BACKEND_LOOSE_FSYNC = git_odb_backend_loose_flag_t.GIT_ODB_BACKEND_LOOSE_FSYNC;
        
        /// <summary>
        /// Streaming mode
        /// </summary>
        [Flags]
        public enum git_odb_stream_t : uint
        {
            GIT_STREAM_RDONLY = unchecked((uint)(1<<1)),
            
            GIT_STREAM_WRONLY = unchecked((uint)(1<<2)),
            
            GIT_STREAM_RW = unchecked((uint)(GIT_STREAM_RDONLY|GIT_STREAM_WRONLY)),
        }
        
        public const libgit2.git_odb_stream_t GIT_STREAM_RDONLY = git_odb_stream_t.GIT_STREAM_RDONLY;
        
        public const libgit2.git_odb_stream_t GIT_STREAM_WRONLY = git_odb_stream_t.GIT_STREAM_WRONLY;
        
        public const libgit2.git_odb_stream_t GIT_STREAM_RW = git_odb_stream_t.GIT_STREAM_RW;
        
        /// <summary>
        /// Options for configuring a packfile object backend.
        /// </summary>
        public partial struct git_odb_backend_pack_options
        {
            /// <summary>
            /// version for the struct
            /// </summary>
            public uint version;
            
            /// <summary>
            /// Type of object IDs to use for this object database, or
            /// 0 for default (currently SHA1).
            /// </summary>
            public libgit2.git_oid_t oid_type;
        }
        
        /// <summary>
        /// Options for configuring a loose object backend.
        /// </summary>
        public partial struct git_odb_backend_loose_options
        {
            /// <summary>
            /// version for the struct
            /// </summary>
            public uint version;
            
            /// <summary>
            /// A combination of the `git_odb_backend_loose_flag_t` types.
            /// </summary>
            public libgit2.git_odb_backend_loose_flag_t flags;
            
            /// <summary>
            /// zlib compression level to use (0-9), where 1 is the fastest
            /// at the expense of larger files, and 9 produces the best
            /// compression at the expense of speed.  0 indicates that no
            /// compression should be performed.  -1 is the default (currently
            /// optimizing for speed).
            /// </summary>
            public int compression_level;
            
            /// <summary>
            /// Permissions to use creating a directory or 0 for defaults
            /// </summary>
            public uint dir_mode;
            
            /// <summary>
            /// Permissions to use creating a file or 0 for defaults
            /// </summary>
            public uint file_mode;
            
            /// <summary>
            /// Type of object IDs to use for this object database, or
            /// 0 for default (currently SHA1).
            /// </summary>
            public libgit2.git_oid_t oid_type;
        }
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_backend_pack")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_odb_backend_pack(out libgit2.git_odb_backend @out, byte* objects_dir);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_backend_pack")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_odb_backend_pack(out libgit2.git_odb_backend @out, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> objects_dir);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_backend_one_pack")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_odb_backend_one_pack(out libgit2.git_odb_backend @out, byte* index_file);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_backend_one_pack")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_odb_backend_one_pack(out libgit2.git_odb_backend @out, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> index_file);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_backend_loose")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_odb_backend_loose(out libgit2.git_odb_backend @out, byte* objects_dir, int compression_level, int do_fsync, uint dir_mode, uint file_mode);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_odb_backend_loose")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int git_odb_backend_loose(out libgit2.git_odb_backend @out, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> objects_dir, int compression_level, int do_fsync, uint dir_mode, uint file_mode);
    }
}