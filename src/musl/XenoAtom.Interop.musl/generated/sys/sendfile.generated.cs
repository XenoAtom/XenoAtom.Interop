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
        /// Transfer data between file descriptors
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "sendfile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial nint sendfile(int out_fd, int in_fd, ref musl.off_t offset, nuint count);
    }
}
