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
            public unsafe partial struct semid_ds
            {
                public musl.ipc_perm sem_perm;
                
                public musl.time_t sem_otime;
                
                public nint __unused1;
                
                public musl.time_t sem_ctime;
                
                public nint __unused2;
                
                public ushort sem_nsems;
                
                public fixed byte __sem_nsems_pad[6];
                
                public nint __unused3;
                
                public nint __unused4;
            }
        }
    }
}