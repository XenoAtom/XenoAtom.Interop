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
            public const int ELF_NGREG = 27;
            
            public const int NBPG = 4096;
            
            public const nint PAGE_MASK = -4096;
            
            public const int UPAGES = 1;
            
            public readonly partial struct elf_fpregset_t : IEquatable<musl.x86_64.elf_fpregset_t>
            {
                public elf_fpregset_t(musl.x86_64.user_fpregs_struct value) => this.Value = value;
                
                public musl.x86_64.user_fpregs_struct Value { get; }
                
                public override bool Equals(object obj) => obj is elf_fpregset_t other && Equals(other);
                
                public bool Equals(elf_fpregset_t other) => Value.Equals(other.Value);
                
                public override int GetHashCode() => Value.GetHashCode();
                
                public override string ToString() => Value.ToString();
                
                public static implicit operator musl.x86_64.user_fpregs_struct (musl.x86_64.elf_fpregset_t from) => from.Value;
                
                public static implicit operator musl.x86_64.elf_fpregset_t (musl.x86_64.user_fpregs_struct from) => new musl.x86_64.elf_fpregset_t(from);
                
                public static bool operator ==(elf_fpregset_t left, elf_fpregset_t right) => left.Equals(right);
                
                public static bool operator !=(elf_fpregset_t left, elf_fpregset_t right) => !left.Equals(right);
            }
            
            public readonly partial struct elf_greg_t : IEquatable<musl.x86_64.elf_greg_t>
            {
                public elf_greg_t(ulong value) => this.Value = value;
                
                public ulong Value { get; }
                
                public override bool Equals(object obj) => obj is elf_greg_t other && Equals(other);
                
                public bool Equals(elf_greg_t other) => Value.Equals(other.Value);
                
                public override int GetHashCode() => Value.GetHashCode();
                
                public override string ToString() => Value.ToString();
                
                public static implicit operator ulong (musl.x86_64.elf_greg_t from) => from.Value;
                
                public static implicit operator musl.x86_64.elf_greg_t (ulong from) => new musl.x86_64.elf_greg_t(from);
                
                public static bool operator ==(elf_greg_t left, elf_greg_t right) => left.Equals(right);
                
                public static bool operator !=(elf_greg_t left, elf_greg_t right) => !left.Equals(right);
            }
            
            public readonly partial struct elf_gregset_t : IEquatable<musl.x86_64.elf_gregset_t>
            {
                public elf_gregset_t(FixedArray27<ulong> value) => this.Value = value;
                
                public FixedArray27<ulong> Value { get; }
                
                public override bool Equals(object obj) => obj is elf_gregset_t other && Equals(other);
                
                public bool Equals(elf_gregset_t other) => Value.Equals(other.Value);
                
                public override int GetHashCode() => Value.GetHashCode();
                
                public override string ToString() => Value.ToString();
                
                public static implicit operator FixedArray27<ulong> (musl.x86_64.elf_gregset_t from) => from.Value;
                
                public static implicit operator musl.x86_64.elf_gregset_t (FixedArray27<ulong> from) => new musl.x86_64.elf_gregset_t(from);
                
                public static bool operator ==(elf_gregset_t left, elf_gregset_t right) => left.Equals(right);
                
                public static bool operator !=(elf_gregset_t left, elf_gregset_t right) => !left.Equals(right);
            }
            
            public unsafe partial struct user
            {
                public musl.x86_64.user_regs_struct regs;
                
                public int u_fpvalid;
                
                public musl.x86_64.user_fpregs_struct i387;
                
                public nuint u_tsize;
                
                public nuint u_dsize;
                
                public nuint u_ssize;
                
                public nuint start_code;
                
                public nuint start_stack;
                
                public nint signal;
                
                public int reserved;
                
                public musl.x86_64.user_regs_struct* u_ar0;
                
                public musl.x86_64.user_fpregs_struct* u_fpstate;
                
                public nuint magic;
                
                public fixed byte u_comm[32];
                
                public FixedArray8<nuint> u_debugreg;
            }
            
            public unsafe partial struct user_fpregs_struct
            {
                public ushort cwd;
                
                public ushort swd;
                
                public ushort ftw;
                
                public ushort fop;
                
                public ulong rip;
                
                public ulong rdp;
                
                public uint mxcsr;
                
                public uint mxcr_mask;
                
                public fixed uint st_space[32];
                
                public fixed uint xmm_space[64];
                
                public fixed uint padding[24];
            }
            
            public partial struct user_regs_struct
            {
                public nuint r15;
                
                public nuint r14;
                
                public nuint r13;
                
                public nuint r12;
                
                public nuint rbp;
                
                public nuint rbx;
                
                public nuint r11;
                
                public nuint r10;
                
                public nuint r9;
                
                public nuint r8;
                
                public nuint rax;
                
                public nuint rcx;
                
                public nuint rdx;
                
                public nuint rsi;
                
                public nuint rdi;
                
                public nuint orig_rax;
                
                public nuint rip;
                
                public nuint cs;
                
                public nuint eflags;
                
                public nuint rsp;
                
                public nuint ss;
                
                public nuint fs_base;
                
                public nuint gs_base;
                
                public nuint ds;
                
                public nuint es;
                
                public nuint fs;
                
                public nuint gs;
            }
        }
    }
}
