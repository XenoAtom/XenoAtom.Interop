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
        /// Allocate a new mailmap object.
        /// </summary>
        /// <param name="out">pointer to store the new mailmap</param>
        /// <returns>@return 0 on success, or an error code</returns>
        /// <remarks>
        /// This object is empty, so you'll have to add a mailmap file before you can do
        /// anything with it. The mailmap must be freed with 'git_mailmap_free'.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_mailmap_new")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_mailmap_new(out libgit2.git_mailmap @out);
        
        /// <summary>
        /// Free the mailmap and its associated memory.
        /// </summary>
        /// <param name="mm">the mailmap to free</param>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_mailmap_free")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_mailmap_free(libgit2.git_mailmap mm);
        
        /// <summary>
        /// Add a single entry to the given mailmap object. If the entry already exists,
        /// it will be replaced with the new entry.
        /// </summary>
        /// <param name="mm">mailmap to add the entry to</param>
        /// <param name="real_name">the real name to use, or NULL</param>
        /// <param name="real_email">the real email to use, or NULL</param>
        /// <param name="replace_name">the name to replace, or NULL</param>
        /// <param name="replace_email">the email to replace</param>
        /// <returns>@return 0 on success, or an error code</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_mailmap_add_entry")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_mailmap_add_entry(libgit2.git_mailmap mm, byte* real_name, byte* real_email, byte* replace_name, byte* replace_email);
        
        /// <summary>
        /// Add a single entry to the given mailmap object. If the entry already exists,
        /// it will be replaced with the new entry.
        /// </summary>
        /// <param name="mm">mailmap to add the entry to</param>
        /// <param name="real_name">the real name to use, or NULL</param>
        /// <param name="real_email">the real email to use, or NULL</param>
        /// <param name="replace_name">the name to replace, or NULL</param>
        /// <param name="replace_email">the email to replace</param>
        /// <returns>@return 0 on success, or an error code</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_mailmap_add_entry")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_mailmap_add_entry(libgit2.git_mailmap mm, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> real_name, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> real_email, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> replace_name, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> replace_email);
        
        /// <summary>
        /// Create a new mailmap instance containing a single mailmap file
        /// </summary>
        /// <param name="out">pointer to store the new mailmap</param>
        /// <param name="buf">buffer to parse the mailmap from</param>
        /// <param name="len">the length of the input buffer</param>
        /// <returns>@return 0 on success, or an error code</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_mailmap_from_buffer")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_mailmap_from_buffer(out libgit2.git_mailmap @out, byte* buf, nuint len);
        
        /// <summary>
        /// Create a new mailmap instance from a repository, loading mailmap files based
        /// on the repository's configuration.
        /// </summary>
        /// <param name="out">pointer to store the new mailmap</param>
        /// <param name="repo">repository to load mailmap information from</param>
        /// <returns>@return 0 on success, or an error code</returns>
        /// <remarks>
        /// Mailmaps are loaded in the following order:
        /// 1. '.mailmap' in the root of the repository's working directory, if present.
        /// 2. The blob object identified by the 'mailmap.blob' config entry, if set.
        /// [NOTE: 'mailmap.blob' defaults to 'HEAD:.mailmap' in bare repositories]
        /// 3. The path in the 'mailmap.file' config entry, if set.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_mailmap_from_repository")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_mailmap_from_repository(out libgit2.git_mailmap @out, libgit2.git_repository repo);
        
        /// <summary>
        /// Resolve a name and email to the corresponding real name and email.
        /// </summary>
        /// <param name="real_name">pointer to store the real name</param>
        /// <param name="real_email">pointer to store the real email</param>
        /// <param name="mm">the mailmap to perform a lookup with (may be NULL)</param>
        /// <param name="name">the name to look up</param>
        /// <param name="email">the email to look up</param>
        /// <returns>@return 0 on success, or an error code</returns>
        /// <remarks>
        /// The lifetime of the strings are tied to `mm`, `name`, and `email` parameters.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_mailmap_resolve")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_mailmap_resolve(out byte* real_name, out byte* real_email, libgit2.git_mailmap mm, byte* name, byte* email);
        
        /// <summary>
        /// Resolve a name and email to the corresponding real name and email.
        /// </summary>
        /// <param name="real_name">pointer to store the real name</param>
        /// <param name="real_email">pointer to store the real email</param>
        /// <param name="mm">the mailmap to perform a lookup with (may be NULL)</param>
        /// <param name="name">the name to look up</param>
        /// <param name="email">the email to look up</param>
        /// <returns>@return 0 on success, or an error code</returns>
        /// <remarks>
        /// The lifetime of the strings are tied to `mm`, `name`, and `email` parameters.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_mailmap_resolve")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_mailmap_resolve(out byte* real_name, out byte* real_email, libgit2.git_mailmap mm, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> name, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> email);
        
        /// <summary>
        /// Resolve a signature to use real names and emails with a mailmap.
        /// </summary>
        /// <param name="out">new signature</param>
        /// <param name="mm">mailmap to resolve with</param>
        /// <param name="sig">signature to resolve</param>
        /// <returns>@return 0 or an error code</returns>
        /// <remarks>
        /// Call `git_signature_free()` to free the data.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_mailmap_resolve_signature")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_mailmap_resolve_signature(out libgit2.git_signature* @out, libgit2.git_mailmap mm, in libgit2.git_signature sig);
    }
}