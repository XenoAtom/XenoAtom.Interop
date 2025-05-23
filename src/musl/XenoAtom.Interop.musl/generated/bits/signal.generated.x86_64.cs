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
        public static unsafe partial class x86_64
        {
            public unsafe partial struct _fpstate
            {
                public unsafe partial struct _fpstate__struct_0
                {
                    public fixed ushort significand[4];
                    
                    public ushort exponent;
                    
                    public fixed ushort padding[3];
                }
                
                public unsafe partial struct _fpstate__struct_1
                {
                    public fixed uint element[4];
                }
                
                public ushort cwd;
                
                public ushort swd;
                
                public ushort ftw;
                
                public ushort fop;
                
                public ulong rip;
                
                public ulong rdp;
                
                public uint mxcsr;
                
                public uint mxcr_mask;
                
                public FixedArray8<musl.x86_64._fpstate._fpstate__struct_0> _st;
                
                public FixedArray16<musl.x86_64._fpstate._fpstate__struct_1> _xmm;
                
                public fixed uint padding[24];
            }
            
            public readonly partial struct fpregset_t : IEquatable<musl.x86_64.fpregset_t>
            {
                public fpregset_t(musl.x86_64._fpstate* value) => this.Value = value;
                
                public musl.x86_64._fpstate* Value { get; }
                
                public override bool Equals(object obj) => obj is fpregset_t other && Equals(other);
                
                public bool Equals(fpregset_t other) => Value == other.Value;
                
                public override int GetHashCode() => ((nint)(void*)Value).GetHashCode();
                
                public override string ToString() => ((nint)(void*)Value).ToString();
                
                public static implicit operator musl.x86_64._fpstate* (musl.x86_64.fpregset_t from) => from.Value;
                
                public static implicit operator musl.x86_64.fpregset_t (musl.x86_64._fpstate* from) => new musl.x86_64.fpregset_t(from);
                
                public static bool operator ==(fpregset_t left, fpregset_t right) => left.Equals(right);
                
                public static bool operator !=(fpregset_t left, fpregset_t right) => !left.Equals(right);
            }
            
            public readonly partial struct greg_t : IEquatable<musl.x86_64.greg_t>
            {
                public greg_t(long value) => this.Value = value;
                
                public long Value { get; }
                
                public override bool Equals(object obj) => obj is greg_t other && Equals(other);
                
                public bool Equals(greg_t other) => Value.Equals(other.Value);
                
                public override int GetHashCode() => Value.GetHashCode();
                
                public override string ToString() => Value.ToString();
                
                public static implicit operator long (musl.x86_64.greg_t from) => from.Value;
                
                public static implicit operator musl.x86_64.greg_t (long from) => new musl.x86_64.greg_t(from);
                
                public static bool operator ==(greg_t left, greg_t right) => left.Equals(right);
                
                public static bool operator !=(greg_t left, greg_t right) => !left.Equals(right);
            }
            
            public readonly partial struct gregset_t : IEquatable<musl.x86_64.gregset_t>
            {
                public gregset_t(FixedArray23<long> value) => this.Value = value;
                
                public FixedArray23<long> Value { get; }
                
                public override bool Equals(object obj) => obj is gregset_t other && Equals(other);
                
                public bool Equals(gregset_t other) => Value.Equals(other.Value);
                
                public override int GetHashCode() => Value.GetHashCode();
                
                public override string ToString() => Value.ToString();
                
                public static implicit operator FixedArray23<long> (musl.x86_64.gregset_t from) => from.Value;
                
                public static implicit operator musl.x86_64.gregset_t (FixedArray23<long> from) => new musl.x86_64.gregset_t(from);
                
                public static bool operator ==(gregset_t left, gregset_t right) => left.Equals(right);
                
                public static bool operator !=(gregset_t left, gregset_t right) => !left.Equals(right);
            }
            
            public unsafe partial struct mcontext_t
            {
                public musl.x86_64.gregset_t gregs;
                
                public musl.x86_64.fpregset_t fpregs;
                
                public fixed ulong __reserved1[8];
            }
            
            public partial struct sigcontext
            {
                public nuint r8;
                
                public nuint r9;
                
                public nuint r10;
                
                public nuint r11;
                
                public nuint r12;
                
                public nuint r13;
                
                public nuint r14;
                
                public nuint r15;
                
                public nuint rdi;
                
                public nuint rsi;
                
                public nuint rbp;
                
                public nuint rbx;
                
                public nuint rdx;
                
                public nuint rax;
                
                public nuint rcx;
                
                public nuint rsp;
                
                public nuint rip;
                
                public nuint eflags;
                
                public ushort cs;
                
                public ushort gs;
                
                public ushort fs;
                
                public ushort __pad0;
                
                public nuint err;
                
                public nuint trapno;
                
                public nuint oldmask;
                
                public nuint cr2;
                
                public musl.x86_64._fpstate* fpstate;
                
                public FixedArray8<nuint> __reserved1;
            }
            
            /// <summary>
            /// This is a pointer to a
            /// ucontext_t
            /// structure, cast to \fIvoid\ *\fP.
            /// The structure pointed to by this field contains
            /// signal context information that was saved
            /// on the user-space stack by the kernel; for details, see
            /// sigreturn (2).
            /// Further information about the
            /// ucontext_t
            /// structure can be found in
            /// getcontext (3)
            /// and
            /// signal (7).
            /// Commonly, the handler function doesn't make any use of the third argument.
            /// 
            /// The
            /// siginfo_t
            /// data type is a structure with the following fields:
            /// 
            /// +4n
            /// 
            /// siginfo_t {
            ///     int      si_signo;     /* Signal number */
            ///     int      si_errno;     /* An errno value */
            ///     int      si_code;      /* Signal code */
            ///     int      si_trapno;    /* Trap number that caused
            ///                               hardware\-generated signal
            ///                               (unused on most architectures) */
            /// FIXME
            /// The siginfo_t 'si_trapno' field seems to be used
            /// only on SPARC and Alpha; this page could use
            /// a little more detail on its purpose there.
            ///     pid_t    si_pid;       /* Sending process ID */
            ///     uid_t    si_uid;       /* Real user ID of sending process */
            ///     int      si_status;    /* Exit value or signal */
            ///     clock_t  si_utime;     /* User time consumed */
            ///     clock_t  si_stime;     /* System time consumed */
            ///     union sigval si_value; /* Signal value */
            ///     int      si_int;       /* POSIX.1b signal */
            ///     void    *si_ptr;       /* POSIX.1b signal */
            ///     int      si_overrun;   /* Timer overrun count;
            ///                               POSIX.1b timers */
            ///     int      si_timerid;   /* Timer ID; POSIX.1b timers */
            /// In the kernel: si_tid
            ///     void    *si_addr;      /* Memory location which caused fault */
            ///     long     si_band;      /* Band event (was \fIint\fP in
            ///                               glibc 2.3.2 and earlier) */
            ///     int      si_fd;        /* File descriptor */
            ///     short    si_addr_lsb;  /* Least significant bit of address
            ///                               (since Linux 2.6.32) */
            ///     void    *si_lower;     /* Lower bound when address violation
            ///                               occurred (since Linux 3.19) */
            ///     void    *si_upper;     /* Upper bound when address violation
            ///                               occurred (since Linux 3.19) */
            ///     int      si_pkey;      /* Protection key on PTE that caused
            ///                               fault (since Linux 4.6) */
            ///     void    *si_call_addr; /* Address of system call instruction
            ///                               (since Linux 3.5) */
            ///     int      si_syscall;   /* Number of attempted system call
            ///                               (since Linux 3.5) */
            ///     unsigned int si_arch;  /* Architecture of attempted system call
            ///                               (since Linux 3.5) */
            /// }
            /// 
            /// 
            /// 
            /// si_signo ", " si_errno " and " si_code
            /// are defined for all signals.
            /// ( si_errno
            /// is generally unused on Linux.)
            /// The rest of the struct may be a union, so that one should
            /// read only the fields that are meaningful for the given signal:
            /// \[bu] 3
            /// Signals sent with
            /// kill (2)
            /// and
            /// sigqueue (3)
            /// fill in
            /// si_pid " and " si_uid .
            /// In addition, signals sent with
            /// sigqueue (3)
            /// fill in
            /// si_int " and " si_ptr
            /// with the values specified by the sender of the signal;
            /// see
            /// sigqueue (3)
            /// for more details.
            /// \[bu]
            /// Signals sent by POSIX.1b timers (since Linux 2.6) fill in
            /// si_overrun
            /// and
            /// si_timerid .
            /// The
            /// si_timerid
            /// field is an internal ID used by the kernel to identify
            /// the timer; it is not the same as the timer ID returned by
            /// timer_create (2).
            /// The
            /// si_overrun
            /// field is the timer overrun count;
            /// this is the same information as is obtained by a call to
            /// timer_getoverrun (2).
            /// These fields are nonstandard Linux extensions.
            /// \[bu]
            /// Signals sent for message queue notification (see the description of
            /// SIGEV_SIGNAL
            /// in
            /// mq_notify (3))
            /// fill in
            /// si_int / si_ptr ,
            /// with the
            /// sigev_value
            /// supplied to
            /// mq_notify (3);
            /// si_pid ,
            /// with the process ID of the message sender; and
            /// si_uid ,
            /// with the real user ID of the message sender.
            /// \[bu]
            /// SIGCHLD
            /// fills in
            /// si_pid ", " si_uid ", " si_status ", " si_utime ", and " si_stime ,
            /// providing information about the child.
            /// The
            /// si_pid
            /// field is the process ID of the child;
            /// si_uid
            /// is the child's real user ID.
            /// The
            /// si_status
            /// field contains the exit status of the child (if
            /// si_code
            /// is
            /// CLD_EXITED ),
            /// or the signal number that caused the process to change state.
            /// The
            /// si_utime
            /// and
            /// si_stime
            /// contain the user and system CPU time used by the child process;
            /// these fields do not include the times used by waited-for children (unlike
            /// getrusage (2)
            /// and
            /// times (2)).
            /// Up to Linux 2.6, and since Linux 2.6.27, these fields report
            /// CPU time in units of
            /// sysconf(_SC_CLK_TCK) .
            /// In Linux 2.6 kernels before Linux 2.6.27,
            /// a bug meant that these fields reported time in units
            /// of the (configurable) system jiffy (see
            /// time (7)).
            /// FIXME .
            /// When si_utime and si_stime where originally implemented, the
            /// measurement unit was HZ, which was the same as clock ticks
            /// (sysconf(_SC_CLK_TCK)).  In Linux 2.6, HZ became configurable, and
            /// was *still* used as the unit to return the info these fields,
            /// with the result that the field values depended on the
            /// configured HZ.  Of course, the should have been measured in
            /// USER_HZ instead, so that sysconf(_SC_CLK_TCK) could be used to
            /// convert to seconds.  I have a queued patch to fix this:
            /// http://thread.gmane.org/gmane.linux.kernel/698061/ .
            /// This patch made it into Linux 2.6.27.
            /// But note that these fields still don't return the times of
            /// waited-for children (as is done by getrusage() and times()
            /// and wait4()).  Solaris 8 does include child times.
            /// \[bu]
            /// SIGILL ,
            /// SIGFPE ,
            /// SIGSEGV ,
            /// SIGBUS ,
            /// and
            /// SIGTRAP
            /// fill in
            /// si_addr
            /// with the address of the fault.
            /// On some architectures,
            /// these signals also fill in the
            /// si_trapno
            /// field.
            /// 
            /// Some suberrors of
            /// SIGBUS ,
            /// in particular
            /// BUS_MCEERR_AO
            /// and
            /// BUS_MCEERR_AR ,
            /// also fill in
            /// si_addr_lsb .
            /// This field indicates the least significant bit of the reported address
            /// and therefore the extent of the corruption.
            /// For example, if a full page was corrupted,
            /// si_addr_lsb
            /// contains
            /// log2(sysconf(_SC_PAGESIZE)) .
            /// When
            /// SIGTRAP
            /// is delivered in response to a
            /// ptrace (2)
            /// event (PTRACE_EVENT_foo),
            /// si_addr
            /// is not populated, but
            /// si_pid
            /// and
            /// si_uid
            /// are populated with the respective process ID and user ID responsible for
            /// delivering the trap.
            /// In the case of
            /// seccomp (2),
            /// the tracee will be shown as delivering the event.
            /// BUS_MCEERR_*
            /// and
            /// si_addr_lsb
            /// are Linux-specific extensions.
            /// 
            /// The
            /// SEGV_BNDERR
            /// suberror of
            /// SIGSEGV
            /// populates
            /// si_lower
            /// and
            /// si_upper .
            /// 
            /// The
            /// SEGV_PKUERR
            /// suberror of
            /// SIGSEGV
            /// populates
            /// si_pkey .
            /// \[bu]
            /// SIGIO / SIGPOLL
            /// (the two names are synonyms on Linux)
            /// fills in
            /// si_band
            /// and
            /// si_fd .
            /// The
            /// si_band
            /// event is a bit mask containing the same values as are filled in the
            /// revents
            /// field by
            /// poll (2).
            /// The
            /// si_fd
            /// field indicates the file descriptor for which the I/O event occurred;
            /// for further details, see the description of
            /// F_SETSIG
            /// in
            /// fcntl (2).
            /// \[bu]
            /// SIGSYS ,
            /// generated (since Linux 3.5)
            /// commit a0727e8ce513fe6890416da960181ceb10fbfae6
            /// when a seccomp filter returns
            /// SECCOMP_RET_TRAP ,
            /// fills in
            /// si_call_addr ,
            /// si_syscall ,
            /// si_arch ,
            /// si_errno ,
            /// and other fields as described in
            /// seccomp (2).
            /// </summary>
            public partial struct ucontext
            {
                public nuint uc_flags;
                
                public musl.x86_64.ucontext* uc_link;
                
                public musl.stack_t uc_stack;
                
                public musl.x86_64.mcontext_t uc_mcontext;
                
                public musl.sigset_t uc_sigmask;
                
                public FixedArray64<nuint> __fpregs_mem;
            }
            
            public readonly partial struct ucontext_t : IEquatable<musl.x86_64.ucontext_t>
            {
                public ucontext_t(musl.x86_64.ucontext value) => this.Value = value;
                
                public musl.x86_64.ucontext Value { get; }
                
                public override bool Equals(object obj) => obj is ucontext_t other && Equals(other);
                
                public bool Equals(ucontext_t other) => Value.Equals(other.Value);
                
                public override int GetHashCode() => Value.GetHashCode();
                
                public override string ToString() => Value.ToString();
                
                public static implicit operator musl.x86_64.ucontext (musl.x86_64.ucontext_t from) => from.Value;
                
                public static implicit operator musl.x86_64.ucontext_t (musl.x86_64.ucontext from) => new musl.x86_64.ucontext_t(from);
                
                public static bool operator ==(ucontext_t left, ucontext_t right) => left.Equals(right);
                
                public static bool operator !=(ucontext_t left, ucontext_t right) => !left.Equals(right);
            }
        }
    }
}
