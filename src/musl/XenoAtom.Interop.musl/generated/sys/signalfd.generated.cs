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
        public unsafe partial struct signalfd_siginfo
        {
            public uint ssi_signo;
            
            public int ssi_errno;
            
            public int ssi_code;
            
            public uint ssi_pid;
            
            public uint ssi_uid;
            
            public int ssi_fd;
            
            public uint ssi_tid;
            
            public uint ssi_band;
            
            public uint ssi_overrun;
            
            public uint ssi_trapno;
            
            public int ssi_status;
            
            public int ssi_int;
            
            public ulong ssi_ptr;
            
            public ulong ssi_utime;
            
            public ulong ssi_stime;
            
            public ulong ssi_addr;
            
            public ushort ssi_addr_lsb;
            
            public ushort __pad2;
            
            public int ssi_syscall;
            
            public ulong ssi_call_addr;
            
            public uint ssi_arch;
            
            public fixed byte __pad[28];
        }
        
        /// <summary>
        /// Set the close-on-exec
        /// ( FD_CLOEXEC )
        /// flag on the new file descriptor.
        /// See the description of the
        /// O_CLOEXEC
        /// flag in
        /// open (2)
        /// for reasons why this may be useful.
        /// 
        /// Up to Linux 2.6.26, the
        /// flags
        /// argument is unused, and must be specified as zero.
        /// 
        /// signalfd ()
        /// returns a file descriptor that supports the following operations:
        /// </summary>
        public const int SFD_CLOEXEC = 524288;
        
        /// <summary>
        /// Set the
        /// O_NONBLOCK
        /// file status flag on the open file description (see
        /// open (2))
        /// referred to by the new file descriptor.
        /// Using this flag saves extra calls to
        /// fcntl (2)
        /// to achieve the same result.
        /// </summary>
        public const int SFD_NONBLOCK = 2048;
        
        /// <summary>
        /// Create a file descriptor for accepting signals
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "signalfd")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int signalfd(int fd, ref musl.sigset_t mask, int flags);
    }
}