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
            public partial struct elf_prstatus
            {
                public partial struct elf_prstatus_pr_cstime
                {
                    public nint tv_sec;
                    
                    public nint tv_usec;
                }
                
                public musl.elf_siginfo pr_info;
                
                public short pr_cursig;
                
                public nuint pr_sigpend;
                
                public nuint pr_sighold;
                
                public musl.pid_t pr_pid;
                
                public musl.pid_t pr_ppid;
                
                public musl.pid_t pr_pgrp;
                
                public musl.pid_t pr_sid;
                
                public musl.aarch64.elf_prstatus.elf_prstatus_pr_cstime pr_cstime;
                
                public musl.aarch64.elf_gregset_t pr_reg;
                
                public int pr_fpvalid;
            }
            
            public readonly partial struct prfpregset_t : IEquatable<prfpregset_t>
            {
                public prfpregset_t(musl.aarch64.elf_fpregset_t value) => this.Value = value;
                
                public musl.aarch64.elf_fpregset_t Value { get; }
                
                public override bool Equals(object obj) => obj is prfpregset_t other && Equals(other);
                
                public bool Equals(prfpregset_t other) => Value.Equals(other.Value);
                
                public override int GetHashCode() => Value.GetHashCode();
                
                public override string ToString() => Value.ToString();
                
                public static implicit operator musl.aarch64.elf_fpregset_t (musl.aarch64.prfpregset_t from) => from.Value;
                
                public static implicit operator musl.aarch64.prfpregset_t (musl.aarch64.elf_fpregset_t from) => new musl.aarch64.prfpregset_t(from);
                
                public static bool operator ==(prfpregset_t left, prfpregset_t right) => left.Equals(right);
                
                public static bool operator !=(prfpregset_t left, prfpregset_t right) => !left.Equals(right);
            }
            
            public readonly partial struct prgregset_t : IEquatable<prgregset_t>
            {
                public prgregset_t(musl.aarch64.elf_gregset_t value) => this.Value = value;
                
                public musl.aarch64.elf_gregset_t Value { get; }
                
                public override bool Equals(object obj) => obj is prgregset_t other && Equals(other);
                
                public bool Equals(prgregset_t other) => Value.Equals(other.Value);
                
                public override int GetHashCode() => Value.GetHashCode();
                
                public override string ToString() => Value.ToString();
                
                public static implicit operator musl.aarch64.elf_gregset_t (musl.aarch64.prgregset_t from) => from.Value;
                
                public static implicit operator musl.aarch64.prgregset_t (musl.aarch64.elf_gregset_t from) => new musl.aarch64.prgregset_t(from);
                
                public static bool operator ==(prgregset_t left, prgregset_t right) => left.Equals(right);
                
                public static bool operator !=(prgregset_t left, prgregset_t right) => !left.Equals(right);
            }
            
            public readonly partial struct prstatus_t : IEquatable<prstatus_t>
            {
                public prstatus_t(musl.aarch64.elf_prstatus value) => this.Value = value;
                
                public musl.aarch64.elf_prstatus Value { get; }
                
                public override bool Equals(object obj) => obj is prstatus_t other && Equals(other);
                
                public bool Equals(prstatus_t other) => Value.Equals(other.Value);
                
                public override int GetHashCode() => Value.GetHashCode();
                
                public override string ToString() => Value.ToString();
                
                public static implicit operator musl.aarch64.elf_prstatus (musl.aarch64.prstatus_t from) => from.Value;
                
                public static implicit operator musl.aarch64.prstatus_t (musl.aarch64.elf_prstatus from) => new musl.aarch64.prstatus_t(from);
                
                public static bool operator ==(prstatus_t left, prstatus_t right) => left.Equals(right);
                
                public static bool operator !=(prstatus_t left, prstatus_t right) => !left.Equals(right);
            }
        }
    }
}