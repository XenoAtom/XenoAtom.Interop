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
        /// Reference lookup strategy
        /// </summary>
        /// <remarks>
        /// These behave like the --tags and --all options to git-describe,
        /// namely they say to look for any reference in either refs/tags/ or
        /// refs/ respectively.
        /// </remarks>
        public enum git_describe_strategy_t : int
        {
            GIT_DESCRIBE_DEFAULT,
            
            GIT_DESCRIBE_TAGS,
            
            GIT_DESCRIBE_ALL,
        }
        
        public const libgit2.git_describe_strategy_t GIT_DESCRIBE_DEFAULT = git_describe_strategy_t.GIT_DESCRIBE_DEFAULT;
        
        public const libgit2.git_describe_strategy_t GIT_DESCRIBE_TAGS = git_describe_strategy_t.GIT_DESCRIBE_TAGS;
        
        public const libgit2.git_describe_strategy_t GIT_DESCRIBE_ALL = git_describe_strategy_t.GIT_DESCRIBE_ALL;
        
        /// <summary>
        /// Describe options structure
        /// </summary>
        /// <remarks>
        /// Initialize with `GIT_DESCRIBE_OPTIONS_INIT`. Alternatively, you can
        /// use `git_describe_options_init`.
        /// </remarks>
        public partial struct git_describe_options
        {
            public uint version;
            
            /// <summary>
            /// default: 10
            /// </summary>
            public uint max_candidates_tags;
            
            /// <summary>
            /// default: GIT_DESCRIBE_DEFAULT
            /// </summary>
            public libgit2.git_describe_strategy_t describe_strategy;
            
            public readonly byte* pattern;
            
            /// <summary>
            /// When calculating the distance from the matching tag or
            /// reference, only walk down the first-parent ancestry.
            /// </summary>
            public int only_follow_first_parent;
            
            /// <summary>
            /// If no matching tag or reference is found, the describe
            /// operation would normally fail. If this option is set, it
            /// will instead fall back to showing the full id of the
            /// commit.
            /// </summary>
            public int show_commit_oid_as_fallback;
        }
        
        /// <summary>
        /// Describe format options structure
        /// </summary>
        /// <remarks>
        /// Initialize with `GIT_DESCRIBE_FORMAT_OPTIONS_INIT`. Alternatively, you can
        /// use `git_describe_format_options_init`.
        /// </remarks>
        public partial struct git_describe_format_options
        {
            public uint version;
            
            /// <summary>
            /// Size of the abbreviated commit id to use. This value is the
            /// lower bound for the length of the abbreviated string. The
            /// default is 7.
            /// </summary>
            public uint abbreviated_size;
            
            /// <summary>
            /// Set to use the long format even when a shorter name could be used.
            /// </summary>
            public int always_use_long_format;
            
            /// <summary>
            /// If the workdir is dirty and this is set, this string will
            /// be appended to the description string.
            /// </summary>
            public readonly byte* dirty_suffix;
        }
        
        /// <summary>
        /// A struct that stores the result of a describe operation.
        /// </summary>
        public readonly partial struct git_describe_result : IEquatable<git_describe_result>
        {
            public git_describe_result(nint handle) => Handle = handle;
            
            public readonly nint Handle;
            
            public bool Equals(git_describe_result other) => Handle.Equals(other.Handle);
            
            public override bool Equals(object obj) => obj is git_describe_result other && Equals(other);
            
            public override int GetHashCode() => Handle.GetHashCode();
            
            public override string ToString() => "0x" + (nint.Size == 8 ? Handle.ToString("X16") : Handle.ToString("X8"));
            
            public static bool operator ==(git_describe_result left, git_describe_result right) => left.Equals(right);
            
            public static bool operator !=(git_describe_result left, git_describe_result right) => !left.Equals(right);
        }
        
        /// <summary>
        /// Initialize git_describe_options structure
        /// </summary>
        /// <param name="opts">The `git_describe_options` struct to initialize.</param>
        /// <param name="version">The struct version; pass `GIT_DESCRIBE_OPTIONS_VERSION`.</param>
        /// <returns>@return Zero on success; -1 on failure.</returns>
        /// <remarks>
        /// Initializes a `git_describe_options` with default values. Equivalent to creating
        /// an instance with GIT_DESCRIBE_OPTIONS_INIT.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_describe_options_init")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_describe_options_init(out libgit2.git_describe_options opts, uint version);
        
        /// <summary>
        /// Initialize git_describe_format_options structure
        /// </summary>
        /// <param name="opts">The `git_describe_format_options` struct to initialize.</param>
        /// <param name="version">The struct version; pass `GIT_DESCRIBE_FORMAT_OPTIONS_VERSION`.</param>
        /// <returns>@return Zero on success; -1 on failure.</returns>
        /// <remarks>
        /// Initializes a `git_describe_format_options` with default values. Equivalent to creating
        /// an instance with GIT_DESCRIBE_FORMAT_OPTIONS_INIT.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_describe_format_options_init")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_describe_format_options_init(out libgit2.git_describe_format_options opts, uint version);
        
        /// <summary>
        /// Describe a commit
        /// </summary>
        /// <param name="result">pointer to store the result. You must free this once
        /// you're done with it.</param>
        /// <param name="committish">a committish to describe</param>
        /// <param name="opts">the lookup options (or NULL for defaults)</param>
        /// <returns>@return 0 or an error code.</returns>
        /// <remarks>
        /// Perform the describe operation on the given committish object.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_describe_commit")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_describe_commit(out libgit2.git_describe_result result, libgit2.git_object committish, in libgit2.git_describe_options opts);
        
        /// <summary>
        /// Describe a commit
        /// </summary>
        /// <param name="out">pointer to store the result. You must free this once
        /// you're done with it.</param>
        /// <param name="repo">the repository in which to perform the describe</param>
        /// <param name="opts">the lookup options (or NULL for defaults)</param>
        /// <returns>@return 0 or an error code.</returns>
        /// <remarks>
        /// Perform the describe operation on the current commit and the
        /// worktree. After performing describe on HEAD, a status is run and the
        /// description is considered to be dirty if there are.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_describe_workdir")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_describe_workdir(out libgit2.git_describe_result @out, libgit2.git_repository repo, in libgit2.git_describe_options opts);
        
        /// <summary>
        /// Print the describe result to a buffer
        /// </summary>
        /// <param name="out">The buffer to store the result</param>
        /// <param name="result">the result from `git_describe_commit()` or
        /// `git_describe_workdir()`.</param>
        /// <param name="opts">the formatting options (or NULL for defaults)</param>
        /// <returns>@return 0 or an error code.</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_describe_format")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_describe_format(out libgit2.git_buf @out, libgit2.git_describe_result result, in libgit2.git_describe_format_options opts);
        
        /// <summary>
        /// Free the describe result.
        /// </summary>
        /// <param name="result">The result to free.</param>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_describe_result_free")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_describe_result_free(libgit2.git_describe_result result);
    }
}