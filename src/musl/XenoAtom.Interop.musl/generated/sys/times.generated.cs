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
        public partial struct tms
        {
            public musl.clock_t tms_utime;
            
            public musl.clock_t tms_stime;
            
            public musl.clock_t tms_cutime;
            
            public musl.clock_t tms_cstime;
        }
        
        /// <summary>
        /// Get process times
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "times")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial musl.clock_t times(ref musl.tms buf);
    }
}