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
        public partial struct elf_siginfo
        {
            public int si_signo;
            
            public int si_code;
            
            public int si_errno;
        }
        
        public unsafe partial struct elf_prpsinfo
        {
            public byte pr_state;
            
            public byte pr_sname;
            
            public byte pr_zomb;
            
            public byte pr_nice;
            
            public nuint pr_flag;
            
            public uint pr_uid;
            
            public uint pr_gid;
            
            public int pr_pid;
            
            public int pr_ppid;
            
            public int pr_pgrp;
            
            public int pr_sid;
            
            public fixed byte pr_fname[16];
            
            public fixed byte pr_psargs[80];
        }
        
        public readonly partial struct psaddr_t : IEquatable<psaddr_t>
        {
            public psaddr_t(void* value) => this.Value = value;
            
            public void* Value { get; }
            
            public override bool Equals(object obj) => obj is psaddr_t other && Equals(other);
            
            public bool Equals(psaddr_t other) => Value == other.Value;
            
            public override int GetHashCode() => ((nint)(void*)Value).GetHashCode();
            
            public override string ToString() => ((nint)(void*)Value).ToString();
            
            public static implicit operator void* (musl.psaddr_t from) => from.Value;
            
            public static implicit operator musl.psaddr_t (void* from) => new musl.psaddr_t(from);
            
            public static bool operator ==(psaddr_t left, psaddr_t right) => left.Equals(right);
            
            public static bool operator !=(psaddr_t left, psaddr_t right) => !left.Equals(right);
        }
        
        public readonly partial struct lwpid_t : IEquatable<lwpid_t>
        {
            public lwpid_t(musl.pid_t value) => this.Value = value;
            
            public musl.pid_t Value { get; }
            
            public override bool Equals(object obj) => obj is lwpid_t other && Equals(other);
            
            public bool Equals(lwpid_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator musl.pid_t (musl.lwpid_t from) => from.Value;
            
            public static implicit operator musl.lwpid_t (musl.pid_t from) => new musl.lwpid_t(from);
            
            public static bool operator ==(lwpid_t left, lwpid_t right) => left.Equals(right);
            
            public static bool operator !=(lwpid_t left, lwpid_t right) => !left.Equals(right);
        }
        
        public readonly partial struct prpsinfo_t : IEquatable<prpsinfo_t>
        {
            public prpsinfo_t(musl.elf_prpsinfo value) => this.Value = value;
            
            public musl.elf_prpsinfo Value { get; }
            
            public override bool Equals(object obj) => obj is prpsinfo_t other && Equals(other);
            
            public bool Equals(prpsinfo_t other) => Value.Equals(other.Value);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator musl.elf_prpsinfo (musl.prpsinfo_t from) => from.Value;
            
            public static implicit operator musl.prpsinfo_t (musl.elf_prpsinfo from) => new musl.prpsinfo_t(from);
            
            public static bool operator ==(prpsinfo_t left, prpsinfo_t right) => left.Equals(right);
            
            public static bool operator !=(prpsinfo_t left, prpsinfo_t right) => !left.Equals(right);
        }
        
        public const int ELF_PRARGSZ = 80;
    }
}