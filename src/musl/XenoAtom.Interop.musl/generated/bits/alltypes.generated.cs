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
    
    public static unsafe partial class musl
    {
        public partial struct timespec
        {
            public musl.time_t tv_sec;
            
            public nint tv_nsec;
        }
        
        public readonly partial struct time_t : IEquatable<musl.time_t>
        {
            public time_t(long value) => this.Value = value;
            
            public long Value { get; }
            
            public override bool Equals(object obj) => obj is time_t other && Equals(other);
            
            public bool Equals(time_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator long (musl.time_t from) => from.Value;
            
            public static implicit operator musl.time_t (long from) => new musl.time_t(from);
            
            public static bool operator ==(time_t left, time_t right) => left.Equals(right);
            
            public static bool operator !=(time_t left, time_t right) => !left.Equals(right);
        }
        
        public partial struct pthread_attr_t
        {
            [global::System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
            public unsafe partial struct pthread_attr_t__union_0
            {
                [FieldOffset(0)]
                public fixed int __i[14];
                
                [FieldOffset(0)]
                public FixedArray14<int> __vi;
                
                [FieldOffset(0)]
                public FixedArray7<nuint> __s;
            }
            
            public musl.pthread_attr_t.pthread_attr_t__union_0 __u;
        }
        
        public readonly partial struct pid_t : IEquatable<musl.pid_t>
        {
            public pid_t(int value) => this.Value = value;
            
            public int Value { get; }
            
            public override bool Equals(object obj) => obj is pid_t other && Equals(other);
            
            public bool Equals(pid_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator int (musl.pid_t from) => from.Value;
            
            public static implicit operator musl.pid_t (int from) => new musl.pid_t(from);
            
            public static bool operator ==(pid_t left, pid_t right) => left.Equals(right);
            
            public static bool operator !=(pid_t left, pid_t right) => !left.Equals(right);
        }
        
        public partial struct pthread_mutexattr_t
        {
            public uint __attr;
        }
        
        public partial struct pthread_condattr_t
        {
            public uint __attr;
        }
        
        public partial struct pthread_barrierattr_t
        {
            public uint __attr;
        }
        
        public unsafe partial struct pthread_rwlockattr_t
        {
            public fixed uint __attr[2];
        }
        
        public partial struct pthread_mutex_t
        {
            [global::System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
            public unsafe partial struct pthread_mutex_t__union_0
            {
                [FieldOffset(0)]
                public fixed int __i[10];
                
                [FieldOffset(0)]
                public FixedArray10<int> __vi;
                
                [FieldOffset(0)]
                public FixedArray5<nint> __p;
            }
            
            public musl.pthread_mutex_t.pthread_mutex_t__union_0 __u;
        }
        
        public partial struct pthread_cond_t
        {
            [global::System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
            public unsafe partial struct pthread_cond_t__union_0
            {
                [FieldOffset(0)]
                public fixed int __i[12];
                
                [FieldOffset(0)]
                public FixedArray12<int> __vi;
                
                [FieldOffset(0)]
                public FixedArray6<nint> __p;
            }
            
            public musl.pthread_cond_t.pthread_cond_t__union_0 __u;
        }
        
        public partial struct pthread_rwlock_t
        {
            [global::System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
            public unsafe partial struct pthread_rwlock_t__union_0
            {
                [FieldOffset(0)]
                public fixed int __i[14];
                
                [FieldOffset(0)]
                public FixedArray14<int> __vi;
                
                [FieldOffset(0)]
                public FixedArray7<nint> __p;
            }
            
            public musl.pthread_rwlock_t.pthread_rwlock_t__union_0 __u;
        }
        
        public partial struct pthread_barrier_t
        {
            [global::System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
            public unsafe partial struct pthread_barrier_t__union_0
            {
                [FieldOffset(0)]
                public fixed int __i[8];
                
                [FieldOffset(0)]
                public FixedArray8<int> __vi;
                
                [FieldOffset(0)]
                public FixedArray4<nint> __p;
            }
            
            public musl.pthread_barrier_t.pthread_barrier_t__union_0 __u;
        }
        
        public partial struct timeval
        {
            public musl.time_t tv_sec;
            
            public musl.suseconds_t tv_usec;
        }
        
        public readonly partial struct suseconds_t : IEquatable<musl.suseconds_t>
        {
            public suseconds_t(long value) => this.Value = value;
            
            public long Value { get; }
            
            public override bool Equals(object obj) => obj is suseconds_t other && Equals(other);
            
            public bool Equals(suseconds_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator long (musl.suseconds_t from) => from.Value;
            
            public static implicit operator musl.suseconds_t (long from) => new musl.suseconds_t(from);
            
            public static bool operator ==(suseconds_t left, suseconds_t right) => left.Equals(right);
            
            public static bool operator !=(suseconds_t left, suseconds_t right) => !left.Equals(right);
        }
        
        public partial struct __sigset_t
        {
            public FixedArray16<nuint> __bits;
        }
        
        public partial struct iovec
        {
            public void* iov_base;
            
            public nuint iov_len;
        }
        
        public readonly partial struct off_t : IEquatable<musl.off_t>
        {
            public off_t(long value) => this.Value = value;
            
            public long Value { get; }
            
            public override bool Equals(object obj) => obj is off_t other && Equals(other);
            
            public bool Equals(off_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator long (musl.off_t from) => from.Value;
            
            public static implicit operator musl.off_t (long from) => new musl.off_t(from);
            
            public static bool operator ==(off_t left, off_t right) => left.Equals(right);
            
            public static bool operator !=(off_t left, off_t right) => !left.Equals(right);
        }
        
        public readonly partial struct fsblkcnt_t : IEquatable<musl.fsblkcnt_t>
        {
            public fsblkcnt_t(ulong value) => this.Value = value;
            
            public ulong Value { get; }
            
            public override bool Equals(object obj) => obj is fsblkcnt_t other && Equals(other);
            
            public bool Equals(fsblkcnt_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator ulong (musl.fsblkcnt_t from) => from.Value;
            
            public static implicit operator musl.fsblkcnt_t (ulong from) => new musl.fsblkcnt_t(from);
            
            public static bool operator ==(fsblkcnt_t left, fsblkcnt_t right) => left.Equals(right);
            
            public static bool operator !=(fsblkcnt_t left, fsblkcnt_t right) => !left.Equals(right);
        }
        
        public readonly partial struct fsfilcnt_t : IEquatable<musl.fsfilcnt_t>
        {
            public fsfilcnt_t(ulong value) => this.Value = value;
            
            public ulong Value { get; }
            
            public override bool Equals(object obj) => obj is fsfilcnt_t other && Equals(other);
            
            public bool Equals(fsfilcnt_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator ulong (musl.fsfilcnt_t from) => from.Value;
            
            public static implicit operator musl.fsfilcnt_t (ulong from) => new musl.fsfilcnt_t(from);
            
            public static bool operator ==(fsfilcnt_t left, fsfilcnt_t right) => left.Equals(right);
            
            public static bool operator !=(fsfilcnt_t left, fsfilcnt_t right) => !left.Equals(right);
        }
        
        public partial struct winsize
        {
            public ushort ws_row;
            
            public ushort ws_col;
            
            public ushort ws_xpixel;
            
            public ushort ws_ypixel;
        }
        
        public readonly partial struct key_t : IEquatable<musl.key_t>
        {
            public key_t(int value) => this.Value = value;
            
            public int Value { get; }
            
            public override bool Equals(object obj) => obj is key_t other && Equals(other);
            
            public bool Equals(key_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator int (musl.key_t from) => from.Value;
            
            public static implicit operator musl.key_t (int from) => new musl.key_t(from);
            
            public static bool operator ==(key_t left, key_t right) => left.Equals(right);
            
            public static bool operator !=(key_t left, key_t right) => !left.Equals(right);
        }
        
        public readonly partial struct uid_t : IEquatable<musl.uid_t>
        {
            public uid_t(uint value) => this.Value = value;
            
            public uint Value { get; }
            
            public override bool Equals(object obj) => obj is uid_t other && Equals(other);
            
            public bool Equals(uid_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator uint (musl.uid_t from) => from.Value;
            
            public static implicit operator musl.uid_t (uint from) => new musl.uid_t(from);
            
            public static bool operator ==(uid_t left, uid_t right) => left.Equals(right);
            
            public static bool operator !=(uid_t left, uid_t right) => !left.Equals(right);
        }
        
        public readonly partial struct gid_t : IEquatable<musl.gid_t>
        {
            public gid_t(uint value) => this.Value = value;
            
            public uint Value { get; }
            
            public override bool Equals(object obj) => obj is gid_t other && Equals(other);
            
            public bool Equals(gid_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator uint (musl.gid_t from) => from.Value;
            
            public static implicit operator musl.gid_t (uint from) => new musl.gid_t(from);
            
            public static bool operator ==(gid_t left, gid_t right) => left.Equals(right);
            
            public static bool operator !=(gid_t left, gid_t right) => !left.Equals(right);
        }
        
        public readonly partial struct mode_t : IEquatable<musl.mode_t>
        {
            public mode_t(uint value) => this.Value = value;
            
            public uint Value { get; }
            
            public override bool Equals(object obj) => obj is mode_t other && Equals(other);
            
            public bool Equals(mode_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator uint (musl.mode_t from) => from.Value;
            
            public static implicit operator musl.mode_t (uint from) => new musl.mode_t(from);
            
            public static bool operator ==(mode_t left, mode_t right) => left.Equals(right);
            
            public static bool operator !=(mode_t left, mode_t right) => !left.Equals(right);
        }
        
        public readonly partial struct sigset_t : IEquatable<musl.sigset_t>
        {
            public sigset_t(musl.__sigset_t value) => this.Value = value;
            
            public musl.__sigset_t Value { get; }
            
            public override bool Equals(object obj) => obj is sigset_t other && Equals(other);
            
            public bool Equals(sigset_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator musl.__sigset_t (musl.sigset_t from) => from.Value;
            
            public static implicit operator musl.sigset_t (musl.__sigset_t from) => new musl.sigset_t(from);
            
            public static bool operator ==(sigset_t left, sigset_t right) => left.Equals(right);
            
            public static bool operator !=(sigset_t left, sigset_t right) => !left.Equals(right);
        }
        
        public readonly partial struct clock_t : IEquatable<musl.clock_t>
        {
            public clock_t(nint value) => this.Value = value;
            
            public nint Value { get; }
            
            public override bool Equals(object obj) => obj is clock_t other && Equals(other);
            
            public bool Equals(clock_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator nint (musl.clock_t from) => from.Value;
            
            public static implicit operator musl.clock_t (nint from) => new musl.clock_t(from);
            
            public static bool operator ==(clock_t left, clock_t right) => left.Equals(right);
            
            public static bool operator !=(clock_t left, clock_t right) => !left.Equals(right);
        }
        
        public readonly partial struct socklen_t : IEquatable<musl.socklen_t>
        {
            public socklen_t(uint value) => this.Value = value;
            
            public uint Value { get; }
            
            public override bool Equals(object obj) => obj is socklen_t other && Equals(other);
            
            public bool Equals(socklen_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator uint (musl.socklen_t from) => from.Value;
            
            public static implicit operator musl.socklen_t (uint from) => new musl.socklen_t(from);
            
            public static bool operator ==(socklen_t left, socklen_t right) => left.Equals(right);
            
            public static bool operator !=(socklen_t left, socklen_t right) => !left.Equals(right);
        }
        
        public readonly partial struct sa_family_t : IEquatable<musl.sa_family_t>
        {
            public sa_family_t(ushort value) => this.Value = value;
            
            public ushort Value { get; }
            
            public override bool Equals(object obj) => obj is sa_family_t other && Equals(other);
            
            public bool Equals(sa_family_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator ushort (musl.sa_family_t from) => from.Value;
            
            public static implicit operator musl.sa_family_t (ushort from) => new musl.sa_family_t(from);
            
            public static bool operator ==(sa_family_t left, sa_family_t right) => left.Equals(right);
            
            public static bool operator !=(sa_family_t left, sa_family_t right) => !left.Equals(right);
        }
        
        public readonly partial struct dev_t : IEquatable<musl.dev_t>
        {
            public dev_t(ulong value) => this.Value = value;
            
            public ulong Value { get; }
            
            public override bool Equals(object obj) => obj is dev_t other && Equals(other);
            
            public bool Equals(dev_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator ulong (musl.dev_t from) => from.Value;
            
            public static implicit operator musl.dev_t (ulong from) => new musl.dev_t(from);
            
            public static bool operator ==(dev_t left, dev_t right) => left.Equals(right);
            
            public static bool operator !=(dev_t left, dev_t right) => !left.Equals(right);
        }
        
        public readonly partial struct ino_t : IEquatable<musl.ino_t>
        {
            public ino_t(ulong value) => this.Value = value;
            
            public ulong Value { get; }
            
            public override bool Equals(object obj) => obj is ino_t other && Equals(other);
            
            public bool Equals(ino_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator ulong (musl.ino_t from) => from.Value;
            
            public static implicit operator musl.ino_t (ulong from) => new musl.ino_t(from);
            
            public static bool operator ==(ino_t left, ino_t right) => left.Equals(right);
            
            public static bool operator !=(ino_t left, ino_t right) => !left.Equals(right);
        }
        
        public readonly partial struct nlink_t : IEquatable<musl.nlink_t>
        {
            public nlink_t(nuint value) => this.Value = value;
            
            public nuint Value { get; }
            
            public override bool Equals(object obj) => obj is nlink_t other && Equals(other);
            
            public bool Equals(nlink_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator nuint (musl.nlink_t from) => from.Value;
            
            public static implicit operator musl.nlink_t (nuint from) => new musl.nlink_t(from);
            
            public static bool operator ==(nlink_t left, nlink_t right) => left.Equals(right);
            
            public static bool operator !=(nlink_t left, nlink_t right) => !left.Equals(right);
        }
        
        public readonly partial struct blksize_t : IEquatable<musl.blksize_t>
        {
            public blksize_t(nint value) => this.Value = value;
            
            public nint Value { get; }
            
            public override bool Equals(object obj) => obj is blksize_t other && Equals(other);
            
            public bool Equals(blksize_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator nint (musl.blksize_t from) => from.Value;
            
            public static implicit operator musl.blksize_t (nint from) => new musl.blksize_t(from);
            
            public static bool operator ==(blksize_t left, blksize_t right) => left.Equals(right);
            
            public static bool operator !=(blksize_t left, blksize_t right) => !left.Equals(right);
        }
        
        public readonly partial struct blkcnt_t : IEquatable<musl.blkcnt_t>
        {
            public blkcnt_t(long value) => this.Value = value;
            
            public long Value { get; }
            
            public override bool Equals(object obj) => obj is blkcnt_t other && Equals(other);
            
            public bool Equals(blkcnt_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator long (musl.blkcnt_t from) => from.Value;
            
            public static implicit operator musl.blkcnt_t (long from) => new musl.blkcnt_t(from);
            
            public static bool operator ==(blkcnt_t left, blkcnt_t right) => left.Equals(right);
            
            public static bool operator !=(blkcnt_t left, blkcnt_t right) => !left.Equals(right);
        }
        
        public readonly partial struct useconds_t : IEquatable<musl.useconds_t>
        {
            public useconds_t(uint value) => this.Value = value;
            
            public uint Value { get; }
            
            public override bool Equals(object obj) => obj is useconds_t other && Equals(other);
            
            public bool Equals(useconds_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator uint (musl.useconds_t from) => from.Value;
            
            public static implicit operator musl.useconds_t (uint from) => new musl.useconds_t(from);
            
            public static bool operator ==(useconds_t left, useconds_t right) => left.Equals(right);
            
            public static bool operator !=(useconds_t left, useconds_t right) => !left.Equals(right);
        }
        
        public readonly partial struct timer_t : IEquatable<musl.timer_t>
        {
            public timer_t(void* value) => this.Value = value;
            
            public void* Value { get; }
            
            public override bool Equals(object obj) => obj is timer_t other && Equals(other);
            
            public bool Equals(timer_t other) => Value == other.Value;
            
            public override int GetHashCode() => ((nint)(void*)Value).GetHashCode();
            
            public override string ToString() => ((nint)(void*)Value).ToString();
            
            public static implicit operator void* (musl.timer_t from) => from.Value;
            
            public static implicit operator musl.timer_t (void* from) => new musl.timer_t(from);
            
            public static bool operator ==(timer_t left, timer_t right) => left.Equals(right);
            
            public static bool operator !=(timer_t left, timer_t right) => !left.Equals(right);
        }
        
        public readonly partial struct clockid_t : IEquatable<musl.clockid_t>
        {
            public clockid_t(int value) => this.Value = value;
            
            public int Value { get; }
            
            public override bool Equals(object obj) => obj is clockid_t other && Equals(other);
            
            public bool Equals(clockid_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator int (musl.clockid_t from) => from.Value;
            
            public static implicit operator musl.clockid_t (int from) => new musl.clockid_t(from);
            
            public static bool operator ==(clockid_t left, clockid_t right) => left.Equals(right);
            
            public static bool operator !=(clockid_t left, clockid_t right) => !left.Equals(right);
        }
        
        public readonly partial struct locale_t : IEquatable<musl.locale_t>
        {
            public locale_t(musl.__locale_struct value) => this.Value = value;
            
            public musl.__locale_struct Value { get; }
            
            public override bool Equals(object obj) => obj is locale_t other && Equals(other);
            
            public bool Equals(locale_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator musl.__locale_struct (musl.locale_t from) => from.Value;
            
            public static implicit operator musl.locale_t (musl.__locale_struct from) => new musl.locale_t(from);
            
            public static bool operator ==(locale_t left, locale_t right) => left.Equals(right);
            
            public static bool operator !=(locale_t left, locale_t right) => !left.Equals(right);
        }
        
        public readonly partial struct intmax_t : IEquatable<musl.intmax_t>
        {
            public intmax_t(long value) => this.Value = value;
            
            public long Value { get; }
            
            public override bool Equals(object obj) => obj is intmax_t other && Equals(other);
            
            public bool Equals(intmax_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator long (musl.intmax_t from) => from.Value;
            
            public static implicit operator musl.intmax_t (long from) => new musl.intmax_t(from);
            
            public static bool operator ==(intmax_t left, intmax_t right) => left.Equals(right);
            
            public static bool operator !=(intmax_t left, intmax_t right) => !left.Equals(right);
        }
        
        public readonly partial struct uintmax_t : IEquatable<musl.uintmax_t>
        {
            public uintmax_t(ulong value) => this.Value = value;
            
            public ulong Value { get; }
            
            public override bool Equals(object obj) => obj is uintmax_t other && Equals(other);
            
            public bool Equals(uintmax_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator ulong (musl.uintmax_t from) => from.Value;
            
            public static implicit operator musl.uintmax_t (ulong from) => new musl.uintmax_t(from);
            
            public static bool operator ==(uintmax_t left, uintmax_t right) => left.Equals(right);
            
            public static bool operator !=(uintmax_t left, uintmax_t right) => !left.Equals(right);
        }
        
        public readonly partial struct register_t : IEquatable<musl.register_t>
        {
            public register_t(nint value) => this.Value = value;
            
            public nint Value { get; }
            
            public override bool Equals(object obj) => obj is register_t other && Equals(other);
            
            public bool Equals(register_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator nint (musl.register_t from) => from.Value;
            
            public static implicit operator musl.register_t (nint from) => new musl.register_t(from);
            
            public static bool operator ==(register_t left, register_t right) => left.Equals(right);
            
            public static bool operator !=(register_t left, register_t right) => !left.Equals(right);
        }
        
        public readonly partial struct u_int64_t : IEquatable<musl.u_int64_t>
        {
            public u_int64_t(ulong value) => this.Value = value;
            
            public ulong Value { get; }
            
            public override bool Equals(object obj) => obj is u_int64_t other && Equals(other);
            
            public bool Equals(u_int64_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator ulong (musl.u_int64_t from) => from.Value;
            
            public static implicit operator musl.u_int64_t (ulong from) => new musl.u_int64_t(from);
            
            public static bool operator ==(u_int64_t left, u_int64_t right) => left.Equals(right);
            
            public static bool operator !=(u_int64_t left, u_int64_t right) => !left.Equals(right);
        }
        
        public readonly partial struct id_t : IEquatable<musl.id_t>
        {
            public id_t(uint value) => this.Value = value;
            
            public uint Value { get; }
            
            public override bool Equals(object obj) => obj is id_t other && Equals(other);
            
            public bool Equals(id_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator uint (musl.id_t from) => from.Value;
            
            public static implicit operator musl.id_t (uint from) => new musl.id_t(from);
            
            public static bool operator ==(id_t left, id_t right) => left.Equals(right);
            
            public static bool operator !=(id_t left, id_t right) => !left.Equals(right);
        }
        
        public readonly partial struct pthread_t : IEquatable<musl.pthread_t>
        {
            public pthread_t(nuint value) => this.Value = value;
            
            public nuint Value { get; }
            
            public override bool Equals(object obj) => obj is pthread_t other && Equals(other);
            
            public bool Equals(pthread_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator nuint (musl.pthread_t from) => from.Value;
            
            public static implicit operator musl.pthread_t (nuint from) => new musl.pthread_t(from);
            
            public static bool operator ==(pthread_t left, pthread_t right) => left.Equals(right);
            
            public static bool operator !=(pthread_t left, pthread_t right) => !left.Equals(right);
        }
        
        public readonly partial struct pthread_once_t : IEquatable<musl.pthread_once_t>
        {
            public pthread_once_t(int value) => this.Value = value;
            
            public int Value { get; }
            
            public override bool Equals(object obj) => obj is pthread_once_t other && Equals(other);
            
            public bool Equals(pthread_once_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator int (musl.pthread_once_t from) => from.Value;
            
            public static implicit operator musl.pthread_once_t (int from) => new musl.pthread_once_t(from);
            
            public static bool operator ==(pthread_once_t left, pthread_once_t right) => left.Equals(right);
            
            public static bool operator !=(pthread_once_t left, pthread_once_t right) => !left.Equals(right);
        }
        
        public readonly partial struct pthread_key_t : IEquatable<musl.pthread_key_t>
        {
            public pthread_key_t(uint value) => this.Value = value;
            
            public uint Value { get; }
            
            public override bool Equals(object obj) => obj is pthread_key_t other && Equals(other);
            
            public bool Equals(pthread_key_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator uint (musl.pthread_key_t from) => from.Value;
            
            public static implicit operator musl.pthread_key_t (uint from) => new musl.pthread_key_t(from);
            
            public static bool operator ==(pthread_key_t left, pthread_key_t right) => left.Equals(right);
            
            public static bool operator !=(pthread_key_t left, pthread_key_t right) => !left.Equals(right);
        }
        
        public readonly partial struct pthread_spinlock_t : IEquatable<musl.pthread_spinlock_t>
        {
            public pthread_spinlock_t(int value) => this.Value = value;
            
            public int Value { get; }
            
            public override bool Equals(object obj) => obj is pthread_spinlock_t other && Equals(other);
            
            public bool Equals(pthread_spinlock_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator int (musl.pthread_spinlock_t from) => from.Value;
            
            public static implicit operator musl.pthread_spinlock_t (int from) => new musl.pthread_spinlock_t(from);
            
            public static bool operator ==(pthread_spinlock_t left, pthread_spinlock_t right) => left.Equals(right);
            
            public static bool operator !=(pthread_spinlock_t left, pthread_spinlock_t right) => !left.Equals(right);
        }
    }
}
