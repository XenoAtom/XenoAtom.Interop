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
        /// <summary>
        /// Flush the instruction cache.
        /// </summary>
        public const int ICACHE = 1;
        
        /// <summary>
        /// Write back to memory and invalidate the affected valid cache lines.
        /// </summary>
        public const int DCACHE = 2;
        
        /// <summary>
        /// Same as
        /// (ICACHE|DCACHE) .
        /// </summary>
        public const int BCACHE = 3;
        
        public const int CACHEABLE = 0;
        
        public const int UNCACHEABLE = 1;
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "cachectl")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int cachectl(void* arg0, int arg1, int arg2);
        
        /// <summary>
        /// Flush contents of instruction and/or data cache
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "cacheflush")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int cacheflush(void* addr, int nbytes, int cache);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "_flush_cache")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int _flush_cache(void* arg0, int arg1, int arg2);
    }
}