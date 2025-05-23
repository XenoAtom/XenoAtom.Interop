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
        public partial struct pollfd
        {
            public int fd;
            
            public short events;
            
            public short revents;
        }
        
        public readonly partial struct nfds_t : IEquatable<musl.nfds_t>
        {
            public nfds_t(nuint value) => this.Value = value;
            
            public nuint Value { get; }
            
            public override bool Equals(object obj) => obj is nfds_t other && Equals(other);
            
            public bool Equals(nfds_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator nuint (musl.nfds_t from) => from.Value;
            
            public static implicit operator musl.nfds_t (nuint from) => new musl.nfds_t(from);
            
            public static bool operator ==(nfds_t left, nfds_t right) => left.Equals(right);
            
            public static bool operator !=(nfds_t left, nfds_t right) => !left.Equals(right);
        }
        
        /// <summary>
        /// There is data to read.
        /// </summary>
        public const int POLLIN = 1;
        
        /// <summary>
        /// There is some exceptional condition on the file descriptor.
        /// Possibilities include:
        /// \[bu] 3
        /// There is out-of-band data on a TCP socket (see
        /// tcp (7)).
        /// \[bu]
        /// A pseudoterminal master in packet mode has seen a state change on the slave
        /// (see
        /// ioctl_tty (2)).
        /// \[bu]
        /// A
        /// cgroup.events
        /// file has been modified (see
        /// cgroups (7)).
        /// </summary>
        public const int POLLPRI = 2;
        
        /// <summary>
        /// Writing is now possible, though a write larger than the available space
        /// in a socket or pipe will still block (unless
        /// O_NONBLOCK
        /// is set).
        /// </summary>
        public const int POLLOUT = 4;
        
        /// <summary>
        /// Error condition (only returned in
        /// revents ;
        /// ignored in
        /// events ).
        /// This bit is also set for a file descriptor referring
        /// to the write end of a pipe when the read end has been closed.
        /// </summary>
        public const int POLLERR = 8;
        
        /// <summary>
        /// Hang up (only returned in
        /// revents ;
        /// ignored in
        /// events ).
        /// Note that when reading from a channel such as a pipe or a stream socket,
        /// this event merely indicates that the peer closed its end of the channel.
        /// Subsequent reads from the channel will return 0 (end of file)
        /// only after all outstanding data in the channel has been consumed.
        /// </summary>
        public const int POLLHUP = 16;
        
        /// <summary>
        /// Invalid request:
        /// fd
        /// not open (only returned in
        /// revents ;
        /// ignored in
        /// events ).
        /// 
        /// When compiling with
        /// _XOPEN_SOURCE
        /// defined, one also has the following,
        /// which convey no further information beyond the bits listed above:
        /// </summary>
        public const int POLLNVAL = 32;
        
        /// <summary>
        /// Equivalent to
        /// POLLIN .
        /// </summary>
        public const int POLLRDNORM = 64;
        
        /// <summary>
        /// Priority band data can be read (generally unused on Linux).
        /// POLLRDBAND is used in the DECnet protocol.
        /// </summary>
        public const int POLLRDBAND = 128;
        
        /// <summary>
        /// Equivalent to
        /// POLLOUT .
        /// </summary>
        public const int POLLWRNORM = 256;
        
        /// <summary>
        /// Priority data may be written.
        /// 
        /// Linux also knows about, but does not use
        /// POLLMSG .
        /// </summary>
        public const int POLLWRBAND = 512;
        
        public const int POLLMSG = 1024;
        
        public const int POLLRDHUP = 8192;
        
        /// <summary>
        /// Wait for some event on a file descriptor
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "poll")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int poll(ref musl.pollfd fds, musl.nfds_t nfds, int timeout);
        
        /// <summary>
        /// Wait for some event on a file descriptor
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "ppoll")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int ppoll(ref musl.pollfd fds, musl.nfds_t nfds, in musl.timespec tmo_p, ref musl.sigset_t sigmask);
    }
}
