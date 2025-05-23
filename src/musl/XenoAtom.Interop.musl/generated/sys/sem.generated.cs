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
        public partial struct seminfo
        {
            public int semmap;
            
            public int semmni;
            
            public int semmns;
            
            public int semmnu;
            
            public int semmsl;
            
            public int semopm;
            
            public int semume;
            
            public int semusz;
            
            public int semvmx;
            
            public int semaem;
        }
        
        public partial struct sembuf
        {
            public ushort sem_num;
            
            public short sem_op;
            
            public short sem_flg;
        }
        
        public const int SEM_UNDO = 4096;
        
        /// <summary>
        /// Return the
        /// sempid
        /// value for the
        /// semnum \-th
        /// semaphore of the set.
        /// This is the PID of the process that last performed an operation on
        /// that semaphore (but see NOTES).
        /// The calling process must have read permission on the semaphore set.
        /// </summary>
        public const int GETPID = 11;
        
        /// <summary>
        /// Return
        /// semval
        /// (i.e., the semaphore value) for the
        /// semnum \-th
        /// semaphore of the set.
        /// The calling process must have read permission on the semaphore set.
        /// </summary>
        public const int GETVAL = 12;
        
        /// <summary>
        /// Return
        /// semval
        /// (i.e., the current value)
        /// for all semaphores of the set into
        /// arg.array .
        /// The argument
        /// semnum
        /// is ignored.
        /// The calling process must have read permission on the semaphore set.
        /// </summary>
        public const int GETALL = 13;
        
        /// <summary>
        /// Return the
        /// semncnt
        /// value for the
        /// semnum \-th
        /// semaphore of the set
        /// (i.e., the number of processes waiting for the semaphore's value to increase).
        /// The calling process must have read permission on the semaphore set.
        /// </summary>
        public const int GETNCNT = 14;
        
        /// <summary>
        /// Return the
        /// semzcnt
        /// value for the
        /// semnum \-th
        /// semaphore of the set
        /// (i.e., the number of processes waiting for the semaphore value to become 0).
        /// The calling process must have read permission on the semaphore set.
        /// </summary>
        public const int GETZCNT = 15;
        
        /// <summary>
        /// Set the semaphore value
        /// ( semval )
        /// to
        /// arg.val
        /// for the
        /// semnum \-th
        /// semaphore of the set, updating also the
        /// sem_ctime
        /// member of the
        /// semid_ds
        /// structure associated with the set.
        /// Undo entries are cleared for altered semaphores in all processes.
        /// If the changes to semaphore values would permit blocked
        /// semop (2)
        /// calls in other processes to proceed, then those processes are woken up.
        /// The calling process must have alter permission on the semaphore set.
        /// </summary>
        public const int SETVAL = 16;
        
        /// <summary>
        /// Set the
        /// semval
        /// values for all semaphores of the set using
        /// arg.array ,
        /// updating also the
        /// sem_ctime
        /// member of the
        /// semid_ds
        /// structure associated with the set.
        /// Undo entries (see
        /// semop (2))
        /// are cleared for altered semaphores in all processes.
        /// If the changes to semaphore values would permit blocked
        /// semop (2)
        /// calls in other processes to proceed, then those processes are woken up.
        /// The argument
        /// semnum
        /// is ignored.
        /// The calling process must have alter (write) permission on
        /// the semaphore set.
        /// </summary>
        public const int SETALL = 17;
        
        /// <summary>
        /// the identifier of the semaphore set whose index was given in
        /// semid .
        /// </summary>
        public const int SEM_STAT = 18;
        
        /// <summary>
        /// as for
        /// IPC_INFO .
        /// </summary>
        public const int SEM_INFO = 19;
        
        /// <summary>
        /// as for
        /// SEM_STAT .
        /// 
        /// All other
        /// op
        /// values return 0 on success.
        /// 
        /// On failure,
        /// semctl ()
        /// returns \-1 and sets
        /// errno
        /// to indicate the error.
        /// </summary>
        public const int SEM_STAT_ANY = 20;
        
        /// <summary>
        /// System V semaphore control operations
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "semctl")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int semctl(int semid, int semnum, int cmd);
        
        /// <summary>
        /// Get a System V semaphore set identifier
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "semget")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int semget(musl.key_t arg0, int arg1, int arg2);
        
        /// <summary>
        /// System V semaphore operations
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "semop")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int semop(int semid, ref musl.sembuf sops, nuint nsops);
        
        /// <summary>
        /// System V semaphore operations
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "semtimedop")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int semtimedop(int semid, ref musl.sembuf sops, nuint nsops, in musl.timespec timeout);
    }
}
