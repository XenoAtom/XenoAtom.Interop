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
        [global::System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
        public partial struct epoll_data
        {
            [FieldOffset(0)]
            public void* ptr;
            
            [FieldOffset(0)]
            public int fd;
            
            [FieldOffset(0)]
            public uint u32;
            
            [FieldOffset(0)]
            public ulong u64;
        }
        
        public partial struct epoll_event
        {
            public uint events;
            
            public musl.epoll_data_t data;
        }
        
        public readonly partial struct epoll_data_t : IEquatable<musl.epoll_data_t>
        {
            public epoll_data_t(musl.epoll_data value) => this.Value = value;
            
            public musl.epoll_data Value { get; }
            
            public override bool Equals(object obj) => obj is epoll_data_t other && Equals(other);
            
            public bool Equals(epoll_data_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator musl.epoll_data (musl.epoll_data_t from) => from.Value;
            
            public static implicit operator musl.epoll_data_t (musl.epoll_data from) => new musl.epoll_data_t(from);
            
            public static bool operator ==(epoll_data_t left, epoll_data_t right) => left.Equals(right);
            
            public static bool operator !=(epoll_data_t left, epoll_data_t right) => !left.Equals(right);
        }
        
        /// <summary>
        /// The associated file is available for
        /// read (2)
        /// operations.
        /// </summary>
        public const int EPOLLIN = 1;
        
        /// <summary>
        /// There is an exceptional condition on the file descriptor.
        /// See the discussion of
        /// POLLPRI
        /// in
        /// poll (2).
        /// </summary>
        public const int EPOLLPRI = 2;
        
        /// <summary>
        /// The associated file is available for
        /// write (2)
        /// operations.
        /// </summary>
        public const int EPOLLOUT = 4;
        
        public const int EPOLLRDNORM = 64;
        
        public const int EPOLLNVAL = 32;
        
        public const int EPOLLRDBAND = 128;
        
        public const int EPOLLWRNORM = 256;
        
        public const int EPOLLWRBAND = 512;
        
        public const int EPOLLMSG = 1024;
        
        /// <summary>
        /// Error condition happened on the associated file descriptor.
        /// This event is also reported for the write end of a pipe when the read end
        /// has been closed.
        /// 
        /// epoll_wait (2)
        /// will always report for this event; it is not necessary to set it in
        /// events
        /// when calling
        /// epoll_ctl ().
        /// </summary>
        public const int EPOLLERR = 8;
        
        /// <summary>
        /// Hang up happened on the associated file descriptor.
        /// 
        /// epoll_wait (2)
        /// will always wait for this event; it is not necessary to set it in
        /// events
        /// when calling
        /// epoll_ctl ().
        /// 
        /// Note that when reading from a channel such as a pipe or a stream socket,
        /// this event merely indicates that the peer closed its end of the channel.
        /// Subsequent reads from the channel will return 0 (end of file)
        /// only after all outstanding data in the channel has been consumed.
        /// 
        /// And the available input flags are:
        /// </summary>
        public const int EPOLLHUP = 16;
        
        public const int EPOLLRDHUP = 8192;
        
        public const int EPOLLEXCLUSIVE = 268435456;
        
        public const int EPOLLWAKEUP = 536870912;
        
        public const int EPOLLONESHOT = 1073741824;
        
        /// <summary>
        /// Requests edge-triggered notification for the associated file descriptor.
        /// The default behavior for
        /// epoll
        /// is level-triggered.
        /// See
        /// epoll (7)
        /// for more detailed information about edge-triggered and
        /// level-triggered notification.
        /// </summary>
        public const int EPOLLET = -2147483648;
        
        /// <summary>
        /// Open an epoll file descriptor
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "epoll_create")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int epoll_create(int size);
        
        /// <summary>
        /// Open an epoll file descriptor
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "epoll_create1")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int epoll_create1(int flags);
        
        /// <summary>
        /// Control interface for an epoll file descriptor
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "epoll_ctl")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int epoll_ctl(int epfd, int op, int fd, ref musl.epoll_event @event);
        
        /// <summary>
        /// epoll_wait, epoll_pwait, epoll_pwait2 \-
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "epoll_wait")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int epoll_wait(int epfd, ref musl.epoll_event events, int maxevents, int timeout);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "epoll_pwait")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int epoll_pwait(int epfd, ref musl.epoll_event events, int maxevents, int timeout, ref musl.sigset_t sigmask);
    }
}
