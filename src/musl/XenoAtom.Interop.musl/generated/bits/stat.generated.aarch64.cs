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
    public static unsafe partial class musl
    {
        public static unsafe partial class aarch64
        {
            public unsafe partial struct stat_t
            {
                public musl.dev_t st_dev;
                
                public musl.ino_t st_ino;
                
                public musl.mode_t st_mode;
                
                public musl.aarch64.nlink_t st_nlink;
                
                public musl.uid_t st_uid;
                
                public musl.gid_t st_gid;
                
                public musl.dev_t st_rdev;
                
                public nuint __pad;
                
                public musl.off_t st_size;
                
                public musl.aarch64.blksize_t st_blksize;
                
                public int __pad2;
                
                public musl.blkcnt_t st_blocks;
                
                public musl.timespec st_atim;
                
                public musl.timespec st_mtim;
                
                public musl.timespec st_ctim;
                
                public fixed uint __unused[2];
            }
        }
    }
}