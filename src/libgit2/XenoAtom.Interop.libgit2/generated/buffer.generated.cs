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
    
    public static unsafe partial class libgit2
    {
        /// <summary>
        /// A data buffer for exporting data from libgit2
        /// </summary>
        /// <remarks>
        /// Sometimes libgit2 wants to return an allocated data buffer to the
        /// caller and have the caller take responsibility for freeing that memory.
        /// To make ownership clear in these cases, libgit2 uses  `git_buf` to
        /// return this data.  Callers should use `git_buf_dispose()` to release
        /// the memory when they are done.A `git_buf` contains a pointer to a NUL-terminated C string, and
        /// the length of the string (not including the NUL terminator).
        /// </remarks>
        public partial struct git_buf
        {
            /// <summary>
            /// The buffer contents.  `ptr` points to the start of the buffer
            /// being returned.  The buffer's length (in bytes) is specified
            /// by the `size` member of the structure, and contains a NUL
            /// terminator at position `(size + 1)`.
            /// </summary>
            public byte* ptr;
            
            /// <summary>
            /// This field is reserved and unused.
            /// </summary>
            public libgit2.size_t reserved;
            
            /// <summary>
            /// The length (in bytes) of the buffer pointed to by `ptr`,
            /// not including a NUL terminator.
            /// </summary>
            public libgit2.size_t size;
        }
        
        /// <summary>
        /// Free the memory referred to by the git_buf.
        /// </summary>
        /// <param name="buffer">The buffer to deallocate</param>
        /// <remarks>
        /// Note that this does not free the `git_buf` itself, just the memory
        /// pointed to by `buffer-&gt;ptr`.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_buf_dispose")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_buf_dispose(libgit2.git_buf* buffer);
    }
}