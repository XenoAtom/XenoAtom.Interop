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
        /// Flags for indicating option behavior for git_blame APIs.
        /// </summary>
        [Flags]
        public enum git_blame_flag_t : int
        {
            /// <summary>
            /// Normal blame, the default
            /// </summary>
            GIT_BLAME_NORMAL = unchecked((int)0),
            
            /// <summary>
            /// Track lines that have moved within a file (like `git blame -M`).
            /// </summary>
            /// <remarks>
            /// This is not yet implemented and reserved for future use.
            /// </remarks>
            GIT_BLAME_TRACK_COPIES_SAME_FILE = unchecked((int)(1  << (int) 0)),
            
            /// <summary>
            /// Track lines that have moved across files in the same commit
            /// (like `git blame -C`).
            /// </summary>
            /// <remarks>
            /// This is not yet implemented and reserved for future use.
            /// </remarks>
            GIT_BLAME_TRACK_COPIES_SAME_COMMIT_MOVES = unchecked((int)(1  << (int) 1)),
            
            /// <summary>
            /// Track lines that have been copied from another file that exists
            /// in the same commit (like `git blame -CC`).  Implies SAME_FILE.
            /// </summary>
            /// <remarks>
            /// This is not yet implemented and reserved for future use.
            /// </remarks>
            GIT_BLAME_TRACK_COPIES_SAME_COMMIT_COPIES = unchecked((int)(1  << (int) 2)),
            
            /// <summary>
            /// Track lines that have been copied from another file that exists in
            /// *any* commit (like `git blame -CCC`).  Implies SAME_COMMIT_COPIES.
            /// </summary>
            /// <remarks>
            /// This is not yet implemented and reserved for future use.
            /// </remarks>
            GIT_BLAME_TRACK_COPIES_ANY_COMMIT_COPIES = unchecked((int)(1  << (int) 3)),
            
            /// <summary>
            /// Restrict the search of commits to those reachable following only
            /// the first parents.
            /// </summary>
            GIT_BLAME_FIRST_PARENT = unchecked((int)(1  << (int) 4)),
            
            /// <summary>
            /// Use mailmap file to map author and committer names and email
            /// addresses to canonical real names and email addresses. The
            /// mailmap will be read from the working directory, or HEAD in a
            /// bare repository.
            /// </summary>
            GIT_BLAME_USE_MAILMAP = unchecked((int)(1  << (int) 5)),
            
            /// <summary>
            /// Ignore whitespace differences
            /// </summary>
            GIT_BLAME_IGNORE_WHITESPACE = unchecked((int)(1  << (int) 6)),
        }
        
        /// <summary>
        /// Normal blame, the default
        /// </summary>
        public const libgit2.git_blame_flag_t GIT_BLAME_NORMAL = git_blame_flag_t.GIT_BLAME_NORMAL;
        
        /// <summary>
        /// Track lines that have moved within a file (like `git blame -M`).
        /// </summary>
        /// <remarks>
        /// This is not yet implemented and reserved for future use.
        /// </remarks>
        public const libgit2.git_blame_flag_t GIT_BLAME_TRACK_COPIES_SAME_FILE = git_blame_flag_t.GIT_BLAME_TRACK_COPIES_SAME_FILE;
        
        /// <summary>
        /// Track lines that have moved across files in the same commit
        /// (like `git blame -C`).
        /// </summary>
        /// <remarks>
        /// This is not yet implemented and reserved for future use.
        /// </remarks>
        public const libgit2.git_blame_flag_t GIT_BLAME_TRACK_COPIES_SAME_COMMIT_MOVES = git_blame_flag_t.GIT_BLAME_TRACK_COPIES_SAME_COMMIT_MOVES;
        
        /// <summary>
        /// Track lines that have been copied from another file that exists
        /// in the same commit (like `git blame -CC`).  Implies SAME_FILE.
        /// </summary>
        /// <remarks>
        /// This is not yet implemented and reserved for future use.
        /// </remarks>
        public const libgit2.git_blame_flag_t GIT_BLAME_TRACK_COPIES_SAME_COMMIT_COPIES = git_blame_flag_t.GIT_BLAME_TRACK_COPIES_SAME_COMMIT_COPIES;
        
        /// <summary>
        /// Track lines that have been copied from another file that exists in
        /// *any* commit (like `git blame -CCC`).  Implies SAME_COMMIT_COPIES.
        /// </summary>
        /// <remarks>
        /// This is not yet implemented and reserved for future use.
        /// </remarks>
        public const libgit2.git_blame_flag_t GIT_BLAME_TRACK_COPIES_ANY_COMMIT_COPIES = git_blame_flag_t.GIT_BLAME_TRACK_COPIES_ANY_COMMIT_COPIES;
        
        /// <summary>
        /// Restrict the search of commits to those reachable following only
        /// the first parents.
        /// </summary>
        public const libgit2.git_blame_flag_t GIT_BLAME_FIRST_PARENT = git_blame_flag_t.GIT_BLAME_FIRST_PARENT;
        
        /// <summary>
        /// Use mailmap file to map author and committer names and email
        /// addresses to canonical real names and email addresses. The
        /// mailmap will be read from the working directory, or HEAD in a
        /// bare repository.
        /// </summary>
        public const libgit2.git_blame_flag_t GIT_BLAME_USE_MAILMAP = git_blame_flag_t.GIT_BLAME_USE_MAILMAP;
        
        /// <summary>
        /// Ignore whitespace differences
        /// </summary>
        public const libgit2.git_blame_flag_t GIT_BLAME_IGNORE_WHITESPACE = git_blame_flag_t.GIT_BLAME_IGNORE_WHITESPACE;
        
        /// <summary>
        /// Blame options structure
        /// </summary>
        /// <remarks>
        /// Initialize with `GIT_BLAME_OPTIONS_INIT`. Alternatively, you can
        /// use `git_blame_options_init`.
        /// </remarks>
        public partial struct git_blame_options
        {
            public uint version;
            
            /// <summary>
            /// A combination of `git_blame_flag_t`
            /// </summary>
            public libgit2.git_blame_flag_t flags;
            
            /// <summary>
            /// The lower bound on the number of alphanumeric characters that
            /// must be detected as moving/copying within a file for it to
            /// associate those lines with the parent commit. The default value
            /// is 20.
            /// </summary>
            /// <remarks>
            /// This value only takes effect if any of the `GIT_BLAME_TRACK_COPIES_*`
            /// flags are specified.
            /// </remarks>
            public ushort min_match_characters;
            
            /// <summary>
            /// The id of the newest commit to consider. The default is HEAD.
            /// </summary>
            public libgit2.git_oid newest_commit;
            
            /// <summary>
            /// The id of the oldest commit to consider.
            /// The default is the first commit encountered with a NULL parent.
            /// </summary>
            public libgit2.git_oid oldest_commit;
            
            /// <summary>
            /// The first line in the file to blame.
            /// The default is 1 (line numbers start with 1).
            /// </summary>
            public libgit2.size_t min_line;
            
            /// <summary>
            /// The last line in the file to blame.
            /// The default is the last line of the file.
            /// </summary>
            public libgit2.size_t max_line;
        }
        
        /// <summary>
        /// Structure that represents a blame hunk.
        /// </summary>
        public partial struct git_blame_hunk
        {
            /// <summary>
            /// The number of lines in this hunk.
            /// </summary>
            public libgit2.size_t lines_in_hunk;
            
            /// <summary>
            /// The OID of the commit where this line was last changed.
            /// </summary>
            public libgit2.git_oid final_commit_id;
            
            /// <summary>
            /// The 1-based line number where this hunk begins, in the final version
            /// of the file.
            /// </summary>
            public libgit2.size_t final_start_line_number;
            
            /// <summary>
            /// The author of `final_commit_id`. If `GIT_BLAME_USE_MAILMAP` has been
            /// specified, it will contain the canonical real name and email address.
            /// </summary>
            public libgit2.git_signature* final_signature;
            
            /// <summary>
            /// The OID of the commit where this hunk was found.
            /// This will usually be the same as `final_commit_id`, except when
            /// `GIT_BLAME_TRACK_COPIES_ANY_COMMIT_COPIES` has been specified.
            /// </summary>
            public libgit2.git_oid orig_commit_id;
            
            /// <summary>
            /// The path to the file where this hunk originated, as of the commit
            /// specified by `orig_commit_id`.
            /// </summary>
            public readonly byte* orig_path;
            
            /// <summary>
            /// The 1-based line number where this hunk begins in the file named by
            /// `orig_path` in the commit specified by `orig_commit_id`.
            /// </summary>
            public libgit2.size_t orig_start_line_number;
            
            /// <summary>
            /// The author of `orig_commit_id`. If `GIT_BLAME_USE_MAILMAP` has been
            /// specified, it will contain the canonical real name and email address.
            /// </summary>
            public libgit2.git_signature* orig_signature;
            
            /// <summary>
            /// The 1 iff the hunk has been tracked to a boundary commit (the root,
            /// or the commit specified in git_blame_options.oldest_commit)
            /// </summary>
            public byte boundary;
        }
        
        /// <summary>
        /// Opaque structure to hold blame results
        /// </summary>
        public readonly partial struct git_blame : IEquatable<git_blame>
        {
            public git_blame(nint handle) => Handle = handle;
            
            public readonly nint Handle;
            
            public bool Equals(git_blame other) => Handle.Equals(other.Handle);
            
            public override bool Equals(object obj) => obj is git_blame other && Equals(other);
            
            public override int GetHashCode() => Handle.GetHashCode();
            
            public override string ToString() => "0x" + (nint.Size == 8 ? Handle.ToString("X16") : Handle.ToString("X8"));
            
            public static bool operator ==(git_blame left, git_blame right) => left.Equals(right);
            
            public static bool operator !=(git_blame left, git_blame right) => !left.Equals(right);
        }
        
        /// <summary>
        /// Initialize git_blame_options structure
        /// </summary>
        /// <param name="opts">The `git_blame_options` struct to initialize.</param>
        /// <param name="version">The struct version; pass `GIT_BLAME_OPTIONS_VERSION`.</param>
        /// <returns>@return Zero on success; -1 on failure.</returns>
        /// <remarks>
        /// Initializes a `git_blame_options` with default values. Equivalent to creating
        /// an instance with GIT_BLAME_OPTIONS_INIT.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_blame_options_init")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_blame_options_init(ref libgit2.git_blame_options opts, uint version);
        
        /// <summary>
        /// Gets the number of hunks that exist in the blame structure.
        /// </summary>
        /// <param name="blame">The blame structure to query.</param>
        /// <returns>@return The number of hunks.</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_blame_get_hunk_count")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial uint git_blame_get_hunk_count(libgit2.git_blame blame);
        
        /// <summary>
        /// Gets the blame hunk at the given index.
        /// </summary>
        /// <param name="blame">the blame structure to query</param>
        /// <param name="index">index of the hunk to retrieve</param>
        /// <returns>@return the hunk at the given index, or NULL on error</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_blame_get_hunk_byindex")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_blame_hunk* git_blame_get_hunk_byindex(libgit2.git_blame blame, uint index);
        
        /// <summary>
        /// Gets the hunk that relates to the given line number in the newest commit.
        /// </summary>
        /// <param name="blame">the blame structure to query</param>
        /// <param name="lineno">the (1-based) line number to find a hunk for</param>
        /// <returns>@return the hunk that contains the given line, or NULL on error</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_blame_get_hunk_byline")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_blame_hunk* git_blame_get_hunk_byline(libgit2.git_blame blame, libgit2.size_t lineno);
        
        /// <summary>
        /// Get the blame for a single file.
        /// </summary>
        /// <param name="out">pointer that will receive the blame object</param>
        /// <param name="repo">repository whose history is to be walked</param>
        /// <param name="path">path to file to consider</param>
        /// <param name="options">options for the blame operation.  If NULL, this is treated as
        /// though GIT_BLAME_OPTIONS_INIT were passed.</param>
        /// <returns>@return 0 on success, or an error code. (use git_error_last for information
        /// about the error.)</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_blame_file")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_blame_file(out libgit2.git_blame @out, libgit2.git_repository repo, byte* path, ref libgit2.git_blame_options options);
        
        /// <summary>
        /// Get the blame for a single file.
        /// </summary>
        /// <param name="out">pointer that will receive the blame object</param>
        /// <param name="repo">repository whose history is to be walked</param>
        /// <param name="path">path to file to consider</param>
        /// <param name="options">options for the blame operation.  If NULL, this is treated as
        /// though GIT_BLAME_OPTIONS_INIT were passed.</param>
        /// <returns>@return 0 on success, or an error code. (use git_error_last for information
        /// about the error.)</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_blame_file")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_blame_file(out libgit2.git_blame @out, libgit2.git_repository repo, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(UTF8MarshallerRelaxedNoCleanup))] string path, ref libgit2.git_blame_options options);
        
        /// <summary>
        /// Get blame data for a file that has been modified in memory. The `reference`
        /// parameter is a pre-calculated blame for the in-odb history of the file. This
        /// means that once a file blame is completed (which can be expensive), updating
        /// the buffer blame is very fast.
        /// </summary>
        /// <param name="out">pointer that will receive the resulting blame data</param>
        /// <param name="reference">cached blame from the history of the file (usually the output
        /// from git_blame_file)</param>
        /// <param name="buffer">the (possibly) modified contents of the file</param>
        /// <param name="buffer_len">number of valid bytes in the buffer</param>
        /// <returns>@return 0 on success, or an error code. (use git_error_last for information
        /// about the error)</returns>
        /// <remarks>
        /// Lines that differ between the buffer and the committed version are marked as
        /// having a zero OID for their final_commit_id.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_blame_buffer")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_blame_buffer(out libgit2.git_blame @out, libgit2.git_blame reference, byte* buffer, libgit2.size_t buffer_len);
        
        /// <summary>
        /// Get blame data for a file that has been modified in memory. The `reference`
        /// parameter is a pre-calculated blame for the in-odb history of the file. This
        /// means that once a file blame is completed (which can be expensive), updating
        /// the buffer blame is very fast.
        /// </summary>
        /// <param name="out">pointer that will receive the resulting blame data</param>
        /// <param name="reference">cached blame from the history of the file (usually the output
        /// from git_blame_file)</param>
        /// <param name="buffer">the (possibly) modified contents of the file</param>
        /// <param name="buffer_len">number of valid bytes in the buffer</param>
        /// <returns>@return 0 on success, or an error code. (use git_error_last for information
        /// about the error)</returns>
        /// <remarks>
        /// Lines that differ between the buffer and the committed version are marked as
        /// having a zero OID for their final_commit_id.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_blame_buffer")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_blame_buffer(out libgit2.git_blame @out, libgit2.git_blame reference, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(UTF8MarshallerRelaxedNoCleanup))] string buffer, libgit2.size_t buffer_len);
        
        /// <summary>
        /// Free memory allocated by git_blame_file or git_blame_buffer.
        /// </summary>
        /// <param name="blame">the blame structure to free</param>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_blame_free")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_blame_free(libgit2.git_blame blame);
    }
}