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
        /// Formatting options for diff e-mail generation
        /// </summary>
        [Flags]
        public enum git_email_create_flags_t : uint
        {
            /// <summary>
            /// Normal patch, the default
            /// </summary>
            GIT_EMAIL_CREATE_DEFAULT = unchecked((uint)0),
            
            /// <summary>
            /// Do not include patch numbers in the subject prefix.
            /// </summary>
            GIT_EMAIL_CREATE_OMIT_NUMBERS = unchecked((uint)(1u << 0)),
            
            /// <summary>
            /// Include numbers in the subject prefix even when the
            /// patch is for a single commit (1/1).
            /// </summary>
            GIT_EMAIL_CREATE_ALWAYS_NUMBER = unchecked((uint)(1u << 1)),
            
            /// <summary>
            /// Do not perform rename or similarity detection.
            /// </summary>
            GIT_EMAIL_CREATE_NO_RENAMES = unchecked((uint)(1u << 2)),
        }
        
        /// <summary>
        /// Normal patch, the default
        /// </summary>
        public const libgit2.git_email_create_flags_t GIT_EMAIL_CREATE_DEFAULT = git_email_create_flags_t.GIT_EMAIL_CREATE_DEFAULT;
        
        /// <summary>
        /// Do not include patch numbers in the subject prefix.
        /// </summary>
        public const libgit2.git_email_create_flags_t GIT_EMAIL_CREATE_OMIT_NUMBERS = git_email_create_flags_t.GIT_EMAIL_CREATE_OMIT_NUMBERS;
        
        /// <summary>
        /// Include numbers in the subject prefix even when the
        /// patch is for a single commit (1/1).
        /// </summary>
        public const libgit2.git_email_create_flags_t GIT_EMAIL_CREATE_ALWAYS_NUMBER = git_email_create_flags_t.GIT_EMAIL_CREATE_ALWAYS_NUMBER;
        
        /// <summary>
        /// Do not perform rename or similarity detection.
        /// </summary>
        public const libgit2.git_email_create_flags_t GIT_EMAIL_CREATE_NO_RENAMES = git_email_create_flags_t.GIT_EMAIL_CREATE_NO_RENAMES;
        
        /// <summary>
        /// Options for controlling the formatting of the generated e-mail.
        /// </summary>
        public partial struct git_email_create_options
        {
            public uint version;
            
            /// <summary>
            /// see `git_email_create_flags_t` above
            /// </summary>
            public libgit2.git_email_create_flags_t flags;
            
            /// <summary>
            /// Options to use when creating diffs
            /// </summary>
            public libgit2.git_diff_options diff_opts;
            
            /// <summary>
            /// Options for finding similarities within diffs
            /// </summary>
            public libgit2.git_diff_find_options diff_find_opts;
            
            /// <summary>
            /// The subject prefix, by default "PATCH".  If set to an empty
            /// string ("") then only the patch numbers will be shown in the
            /// prefix.  If the subject_prefix is empty and patch numbers
            /// are not being shown, the prefix will be omitted entirely.
            /// </summary>
            public byte* subject_prefix;
            
            /// <summary>
            /// The starting patch number; this cannot be 0.  By default,
            /// this is 1.
            /// </summary>
            public nuint start_number;
            
            /// <summary>
            /// The "re-roll" number.  By default, there is no re-roll.
            /// </summary>
            public nuint reroll_number;
        }
        
        /// <summary>
        /// Create a diff for a commit in mbox format for sending via email.
        /// </summary>
        /// <param name="out">buffer to store the e-mail patch in</param>
        /// <param name="diff">the changes to include in the email</param>
        /// <param name="patch_idx">the patch index</param>
        /// <param name="patch_count">the total number of patches that will be included</param>
        /// <param name="commit_id">the commit id for this change</param>
        /// <param name="summary">the commit message for this change</param>
        /// <param name="body">optional text to include above the diffstat</param>
        /// <param name="author">the person who authored this commit</param>
        /// <param name="opts">email creation options</param>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_email_create_from_diff")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_email_create_from_diff(out libgit2.git_buf @out, libgit2.git_diff diff, nuint patch_idx, nuint patch_count, in libgit2.git_oid commit_id, byte* summary, byte* body, in libgit2.git_signature author, in libgit2.git_email_create_options opts);
        
        /// <summary>
        /// Create a diff for a commit in mbox format for sending via email.
        /// </summary>
        /// <param name="out">buffer to store the e-mail patch in</param>
        /// <param name="diff">the changes to include in the email</param>
        /// <param name="patch_idx">the patch index</param>
        /// <param name="patch_count">the total number of patches that will be included</param>
        /// <param name="commit_id">the commit id for this change</param>
        /// <param name="summary">the commit message for this change</param>
        /// <param name="body">optional text to include above the diffstat</param>
        /// <param name="author">the person who authored this commit</param>
        /// <param name="opts">email creation options</param>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_email_create_from_diff")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_email_create_from_diff(out libgit2.git_buf @out, libgit2.git_diff diff, nuint patch_idx, nuint patch_count, in libgit2.git_oid commit_id, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> summary, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> body, in libgit2.git_signature author, in libgit2.git_email_create_options opts);
        
        /// <summary>
        /// Create a diff for a commit in mbox format for sending via email.
        /// The commit must not be a merge commit.
        /// </summary>
        /// <param name="out">buffer to store the e-mail patch in</param>
        /// <param name="commit">commit to create a patch for</param>
        /// <param name="opts">email creation options</param>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_email_create_from_commit")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_email_create_from_commit(out libgit2.git_buf @out, libgit2.git_commit commit, in libgit2.git_email_create_options opts);
    }
}