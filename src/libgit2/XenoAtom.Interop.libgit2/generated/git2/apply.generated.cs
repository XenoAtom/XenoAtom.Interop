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
        /// Flags controlling the behavior of git_apply
        /// </summary>
        [Flags]
        public enum git_apply_flags_t : uint
        {
            /// <summary>
            /// Don't actually make changes, just test that the patch applies.
            /// This is the equivalent of `git apply --check`.
            /// </summary>
            GIT_APPLY_CHECK = unchecked((uint)(1<<0)),
        }
        
        /// <summary>
        /// Don't actually make changes, just test that the patch applies.
        /// This is the equivalent of `git apply --check`.
        /// </summary>
        public const libgit2.git_apply_flags_t GIT_APPLY_CHECK = git_apply_flags_t.GIT_APPLY_CHECK;
        
        /// <summary>
        /// Possible application locations for git_apply
        /// </summary>
        public enum git_apply_location_t : uint
        {
            /// <summary>
            /// Apply the patch to the workdir, leaving the index untouched.
            /// This is the equivalent of `git apply` with no location argument.
            /// </summary>
            GIT_APPLY_LOCATION_WORKDIR = unchecked((uint)0),
            
            /// <summary>
            /// Apply the patch to the index, leaving the working directory
            /// untouched.  This is the equivalent of `git apply --cached`.
            /// </summary>
            GIT_APPLY_LOCATION_INDEX = unchecked((uint)1),
            
            /// <summary>
            /// Apply the patch to both the working directory and the index.
            /// This is the equivalent of `git apply --index`.
            /// </summary>
            GIT_APPLY_LOCATION_BOTH = unchecked((uint)2),
        }
        
        /// <summary>
        /// Apply the patch to the workdir, leaving the index untouched.
        /// This is the equivalent of `git apply` with no location argument.
        /// </summary>
        public const libgit2.git_apply_location_t GIT_APPLY_LOCATION_WORKDIR = git_apply_location_t.GIT_APPLY_LOCATION_WORKDIR;
        
        /// <summary>
        /// Apply the patch to the index, leaving the working directory
        /// untouched.  This is the equivalent of `git apply --cached`.
        /// </summary>
        public const libgit2.git_apply_location_t GIT_APPLY_LOCATION_INDEX = git_apply_location_t.GIT_APPLY_LOCATION_INDEX;
        
        /// <summary>
        /// Apply the patch to both the working directory and the index.
        /// This is the equivalent of `git apply --index`.
        /// </summary>
        public const libgit2.git_apply_location_t GIT_APPLY_LOCATION_BOTH = git_apply_location_t.GIT_APPLY_LOCATION_BOTH;
        
        /// <summary>
        /// Apply options structure
        /// </summary>
        /// <remarks>
        /// Initialize with `GIT_APPLY_OPTIONS_INIT`. Alternatively, you can
        /// use `git_apply_options_init`.
        /// </remarks>
        /// <seealso cref="git_apply_to_tree, git_apply"/>
        public partial struct git_apply_options
        {
            /// <summary>
            /// The version
            /// </summary>
            public uint version;
            
            /// <summary>
            /// When applying a patch, callback that will be made per delta (file).
            /// </summary>
            public libgit2.git_apply_delta_cb delta_cb;
            
            /// <summary>
            /// When applying a patch, callback that will be made per hunk.
            /// </summary>
            public libgit2.git_apply_hunk_cb hunk_cb;
            
            /// <summary>
            /// Payload passed to both delta_cb 
            /// &amp;
            /// hunk_cb.
            /// </summary>
            public void* payload;
            
            /// <summary>
            /// Bitmask of git_apply_flags_t
            /// </summary>
            public libgit2.git_apply_flags_t flags;
        }
        
        /// <summary>
        /// When applying a patch, callback that will be made per delta (file).
        /// </summary>
        /// <param name="delta">The delta to be applied</param>
        /// <param name="payload">User-specified payload</param>
        /// <returns>@return 0 if the delta is applied, 
        /// &lt;
        /// 0 if the apply process will be aborted
        /// or &gt; 0 if the delta will not be applied.</returns>
        /// <remarks>
        /// When the callback:
        /// - returns 
        /// &lt;
        /// 0, the apply process will be aborted.
        /// - returns &gt; 0, the delta will not be applied, but the apply process
        /// continues
        /// - returns 0, the delta is applied, and the apply process continues.
        /// </remarks>
        public readonly partial struct git_apply_delta_cb : IEquatable<git_apply_delta_cb>
        {
            public git_apply_delta_cb(delegate*unmanaged[Cdecl]<libgit2.git_diff_delta*, void*, int> value) => this.Value = value;
            
            public delegate*unmanaged[Cdecl]<libgit2.git_diff_delta*, void*, int> Value { get; }
            
            public bool Equals(git_apply_delta_cb other) =>  Value == other.Value;
            
            public override bool Equals(object obj) => obj is git_apply_delta_cb other && Equals(other);
            
            public override int GetHashCode() => ((nint)(void*)Value).GetHashCode();
            
            public override string ToString() => ((nint)(void*)Value).ToString();
            
            public static implicit operator delegate*unmanaged[Cdecl]<libgit2.git_diff_delta*, void*, int>(git_apply_delta_cb from) => from.Value;
            
            public static implicit operator git_apply_delta_cb(delegate*unmanaged[Cdecl]<libgit2.git_diff_delta*, void*, int> from) => new git_apply_delta_cb(from);
            
            public static bool operator ==(git_apply_delta_cb left, git_apply_delta_cb right) => left.Equals(right);
            
            public static bool operator !=(git_apply_delta_cb left, git_apply_delta_cb right) => !left.Equals(right);
        }
        
        /// <summary>
        /// When applying a patch, callback that will be made per hunk.
        /// </summary>
        /// <param name="hunk">The hunk to be applied</param>
        /// <param name="payload">User-specified payload</param>
        /// <returns>@return 0 if the hunk is applied, 
        /// &lt;
        /// 0 if the apply process will be aborted
        /// or &gt; 0 if the hunk will not be applied.</returns>
        /// <remarks>
        /// When the callback:
        /// - returns 
        /// &lt;
        /// 0, the apply process will be aborted.
        /// - returns &gt; 0, the hunk will not be applied, but the apply process
        /// continues
        /// - returns 0, the hunk is applied, and the apply process continues.
        /// </remarks>
        public readonly partial struct git_apply_hunk_cb : IEquatable<git_apply_hunk_cb>
        {
            public git_apply_hunk_cb(delegate*unmanaged[Cdecl]<libgit2.git_diff_hunk*, void*, int> value) => this.Value = value;
            
            public delegate*unmanaged[Cdecl]<libgit2.git_diff_hunk*, void*, int> Value { get; }
            
            public bool Equals(git_apply_hunk_cb other) =>  Value == other.Value;
            
            public override bool Equals(object obj) => obj is git_apply_hunk_cb other && Equals(other);
            
            public override int GetHashCode() => ((nint)(void*)Value).GetHashCode();
            
            public override string ToString() => ((nint)(void*)Value).ToString();
            
            public static implicit operator delegate*unmanaged[Cdecl]<libgit2.git_diff_hunk*, void*, int>(git_apply_hunk_cb from) => from.Value;
            
            public static implicit operator git_apply_hunk_cb(delegate*unmanaged[Cdecl]<libgit2.git_diff_hunk*, void*, int> from) => new git_apply_hunk_cb(from);
            
            public static bool operator ==(git_apply_hunk_cb left, git_apply_hunk_cb right) => left.Equals(right);
            
            public static bool operator !=(git_apply_hunk_cb left, git_apply_hunk_cb right) => !left.Equals(right);
        }
        
        /// <summary>
        /// Initialize git_apply_options structure
        /// </summary>
        /// <param name="opts">The `git_apply_options` struct to initialize.</param>
        /// <param name="version">The struct version; pass `GIT_APPLY_OPTIONS_VERSION`</param>
        /// <returns>@return 0 on success or -1 on failure.</returns>
        /// <remarks>
        /// Initialize a `git_apply_options` with default values. Equivalent to creating
        /// an instance with GIT_APPLY_OPTIONS_INIT.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_apply_options_init")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_apply_options_init(ref libgit2.git_apply_options opts, uint version);
        
        /// <summary>
        /// Apply a `git_diff` to a `git_tree`, and return the resulting image
        /// as an index.
        /// </summary>
        /// <param name="out">the postimage of the application</param>
        /// <param name="repo">the repository to apply</param>
        /// <param name="preimage">the tree to apply the diff to</param>
        /// <param name="diff">the diff to apply</param>
        /// <param name="options">the options for the apply (or null for defaults)</param>
        /// <returns>@return 0 or an error code</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_apply_to_tree")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_apply_to_tree(out libgit2.git_index @out, libgit2.git_repository repo, libgit2.git_tree preimage, libgit2.git_diff diff, in libgit2.git_apply_options options);
        
        /// <summary>
        /// Apply a `git_diff` to the given repository, making changes directly
        /// in the working directory, the index, or both.
        /// </summary>
        /// <param name="repo">the repository to apply to</param>
        /// <param name="diff">the diff to apply</param>
        /// <param name="location">the location to apply (workdir, index or both)</param>
        /// <param name="options">the options for the apply (or null for defaults)</param>
        /// <returns>@return 0 or an error code</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_apply")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_apply(libgit2.git_repository repo, libgit2.git_diff diff, libgit2.git_apply_location_t location, in libgit2.git_apply_options options);
    }
}