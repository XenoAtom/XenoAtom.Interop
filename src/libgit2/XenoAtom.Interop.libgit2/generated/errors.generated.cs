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
        /// Generic return codes
        /// </summary>
        public enum git_error_code : int
        {
            /// <summary>
            /// No error
            /// </summary>
            GIT_OK = unchecked((int)0),
            
            /// <summary>
            /// Generic error
            /// </summary>
            GIT_ERROR = unchecked((int)-1),
            
            /// <summary>
            /// Requested object could not be found
            /// </summary>
            GIT_ENOTFOUND = unchecked((int)-3),
            
            /// <summary>
            /// Object exists preventing operation
            /// </summary>
            GIT_EEXISTS = unchecked((int)-4),
            
            /// <summary>
            /// More than one object matches
            /// </summary>
            GIT_EAMBIGUOUS = unchecked((int)-5),
            
            /// <summary>
            /// Output buffer too short to hold data
            /// </summary>
            GIT_EBUFS = unchecked((int)-6),
            
            /// <summary>
            /// GIT_EUSER is a special error that is never generated by libgit2
            /// code.  You can return it from a callback (e.g to stop an iteration)
            /// to know that it was generated by the callback and not by libgit2.
            /// </summary>
            GIT_EUSER = unchecked((int)-7),
            
            /// <summary>
            /// Operation not allowed on bare repository
            /// </summary>
            GIT_EBAREREPO = unchecked((int)-8),
            
            /// <summary>
            /// HEAD refers to branch with no commits
            /// </summary>
            GIT_EUNBORNBRANCH = unchecked((int)-9),
            
            /// <summary>
            /// Merge in progress prevented operation
            /// </summary>
            GIT_EUNMERGED = unchecked((int)-10),
            
            /// <summary>
            /// Reference was not fast-forwardable
            /// </summary>
            GIT_ENONFASTFORWARD = unchecked((int)-11),
            
            /// <summary>
            /// Name/ref spec was not in a valid format
            /// </summary>
            GIT_EINVALIDSPEC = unchecked((int)-12),
            
            /// <summary>
            /// Checkout conflicts prevented operation
            /// </summary>
            GIT_ECONFLICT = unchecked((int)-13),
            
            /// <summary>
            /// Lock file prevented operation
            /// </summary>
            GIT_ELOCKED = unchecked((int)-14),
            
            /// <summary>
            /// Reference value does not match expected
            /// </summary>
            GIT_EMODIFIED = unchecked((int)-15),
            
            /// <summary>
            /// Authentication error
            /// </summary>
            GIT_EAUTH = unchecked((int)-16),
            
            /// <summary>
            /// Server certificate is invalid
            /// </summary>
            GIT_ECERTIFICATE = unchecked((int)-17),
            
            /// <summary>
            /// Patch/merge has already been applied
            /// </summary>
            GIT_EAPPLIED = unchecked((int)-18),
            
            /// <summary>
            /// The requested peel operation is not possible
            /// </summary>
            GIT_EPEEL = unchecked((int)-19),
            
            /// <summary>
            /// Unexpected EOF
            /// </summary>
            GIT_EEOF = unchecked((int)-20),
            
            /// <summary>
            /// Invalid operation or input
            /// </summary>
            GIT_EINVALID = unchecked((int)-21),
            
            /// <summary>
            /// Uncommitted changes in index prevented operation
            /// </summary>
            GIT_EUNCOMMITTED = unchecked((int)-22),
            
            /// <summary>
            /// The operation is not valid for a directory
            /// </summary>
            GIT_EDIRECTORY = unchecked((int)-23),
            
            /// <summary>
            /// A merge conflict exists and cannot continue
            /// </summary>
            GIT_EMERGECONFLICT = unchecked((int)-24),
            
            /// <summary>
            /// A user-configured callback refused to act
            /// </summary>
            GIT_PASSTHROUGH = unchecked((int)-30),
            
            /// <summary>
            /// Signals end of iteration with iterator
            /// </summary>
            GIT_ITEROVER = unchecked((int)-31),
            
            /// <summary>
            /// Internal only
            /// </summary>
            GIT_RETRY = unchecked((int)-32),
            
            /// <summary>
            /// Hashsum mismatch in object
            /// </summary>
            GIT_EMISMATCH = unchecked((int)-33),
            
            /// <summary>
            /// Unsaved changes in the index would be overwritten
            /// </summary>
            GIT_EINDEXDIRTY = unchecked((int)-34),
            
            /// <summary>
            /// Patch application failed
            /// </summary>
            GIT_EAPPLYFAIL = unchecked((int)-35),
            
            /// <summary>
            /// The object is not owned by the current user
            /// </summary>
            GIT_EOWNER = unchecked((int)-36),
            
            /// <summary>
            /// The operation timed out
            /// </summary>
            GIT_TIMEOUT = unchecked((int)-37),
        }
        
        /// <summary>
        /// No error
        /// </summary>
        public const libgit2.git_error_code GIT_OK = git_error_code.GIT_OK;
        
        /// <summary>
        /// Generic error
        /// </summary>
        public const libgit2.git_error_code GIT_ERROR = git_error_code.GIT_ERROR;
        
        /// <summary>
        /// Requested object could not be found
        /// </summary>
        public const libgit2.git_error_code GIT_ENOTFOUND = git_error_code.GIT_ENOTFOUND;
        
        /// <summary>
        /// Object exists preventing operation
        /// </summary>
        public const libgit2.git_error_code GIT_EEXISTS = git_error_code.GIT_EEXISTS;
        
        /// <summary>
        /// More than one object matches
        /// </summary>
        public const libgit2.git_error_code GIT_EAMBIGUOUS = git_error_code.GIT_EAMBIGUOUS;
        
        /// <summary>
        /// Output buffer too short to hold data
        /// </summary>
        public const libgit2.git_error_code GIT_EBUFS = git_error_code.GIT_EBUFS;
        
        /// <summary>
        /// GIT_EUSER is a special error that is never generated by libgit2
        /// code.  You can return it from a callback (e.g to stop an iteration)
        /// to know that it was generated by the callback and not by libgit2.
        /// </summary>
        public const libgit2.git_error_code GIT_EUSER = git_error_code.GIT_EUSER;
        
        /// <summary>
        /// Operation not allowed on bare repository
        /// </summary>
        public const libgit2.git_error_code GIT_EBAREREPO = git_error_code.GIT_EBAREREPO;
        
        /// <summary>
        /// HEAD refers to branch with no commits
        /// </summary>
        public const libgit2.git_error_code GIT_EUNBORNBRANCH = git_error_code.GIT_EUNBORNBRANCH;
        
        /// <summary>
        /// Merge in progress prevented operation
        /// </summary>
        public const libgit2.git_error_code GIT_EUNMERGED = git_error_code.GIT_EUNMERGED;
        
        /// <summary>
        /// Reference was not fast-forwardable
        /// </summary>
        public const libgit2.git_error_code GIT_ENONFASTFORWARD = git_error_code.GIT_ENONFASTFORWARD;
        
        /// <summary>
        /// Name/ref spec was not in a valid format
        /// </summary>
        public const libgit2.git_error_code GIT_EINVALIDSPEC = git_error_code.GIT_EINVALIDSPEC;
        
        /// <summary>
        /// Checkout conflicts prevented operation
        /// </summary>
        public const libgit2.git_error_code GIT_ECONFLICT = git_error_code.GIT_ECONFLICT;
        
        /// <summary>
        /// Lock file prevented operation
        /// </summary>
        public const libgit2.git_error_code GIT_ELOCKED = git_error_code.GIT_ELOCKED;
        
        /// <summary>
        /// Reference value does not match expected
        /// </summary>
        public const libgit2.git_error_code GIT_EMODIFIED = git_error_code.GIT_EMODIFIED;
        
        /// <summary>
        /// Authentication error
        /// </summary>
        public const libgit2.git_error_code GIT_EAUTH = git_error_code.GIT_EAUTH;
        
        /// <summary>
        /// Server certificate is invalid
        /// </summary>
        public const libgit2.git_error_code GIT_ECERTIFICATE = git_error_code.GIT_ECERTIFICATE;
        
        /// <summary>
        /// Patch/merge has already been applied
        /// </summary>
        public const libgit2.git_error_code GIT_EAPPLIED = git_error_code.GIT_EAPPLIED;
        
        /// <summary>
        /// The requested peel operation is not possible
        /// </summary>
        public const libgit2.git_error_code GIT_EPEEL = git_error_code.GIT_EPEEL;
        
        /// <summary>
        /// Unexpected EOF
        /// </summary>
        public const libgit2.git_error_code GIT_EEOF = git_error_code.GIT_EEOF;
        
        /// <summary>
        /// Invalid operation or input
        /// </summary>
        public const libgit2.git_error_code GIT_EINVALID = git_error_code.GIT_EINVALID;
        
        /// <summary>
        /// Uncommitted changes in index prevented operation
        /// </summary>
        public const libgit2.git_error_code GIT_EUNCOMMITTED = git_error_code.GIT_EUNCOMMITTED;
        
        /// <summary>
        /// The operation is not valid for a directory
        /// </summary>
        public const libgit2.git_error_code GIT_EDIRECTORY = git_error_code.GIT_EDIRECTORY;
        
        /// <summary>
        /// A merge conflict exists and cannot continue
        /// </summary>
        public const libgit2.git_error_code GIT_EMERGECONFLICT = git_error_code.GIT_EMERGECONFLICT;
        
        /// <summary>
        /// A user-configured callback refused to act
        /// </summary>
        public const libgit2.git_error_code GIT_PASSTHROUGH = git_error_code.GIT_PASSTHROUGH;
        
        /// <summary>
        /// Signals end of iteration with iterator
        /// </summary>
        public const libgit2.git_error_code GIT_ITEROVER = git_error_code.GIT_ITEROVER;
        
        /// <summary>
        /// Internal only
        /// </summary>
        public const libgit2.git_error_code GIT_RETRY = git_error_code.GIT_RETRY;
        
        /// <summary>
        /// Hashsum mismatch in object
        /// </summary>
        public const libgit2.git_error_code GIT_EMISMATCH = git_error_code.GIT_EMISMATCH;
        
        /// <summary>
        /// Unsaved changes in the index would be overwritten
        /// </summary>
        public const libgit2.git_error_code GIT_EINDEXDIRTY = git_error_code.GIT_EINDEXDIRTY;
        
        /// <summary>
        /// Patch application failed
        /// </summary>
        public const libgit2.git_error_code GIT_EAPPLYFAIL = git_error_code.GIT_EAPPLYFAIL;
        
        /// <summary>
        /// The object is not owned by the current user
        /// </summary>
        public const libgit2.git_error_code GIT_EOWNER = git_error_code.GIT_EOWNER;
        
        /// <summary>
        /// The operation timed out
        /// </summary>
        public const libgit2.git_error_code GIT_TIMEOUT = git_error_code.GIT_TIMEOUT;
        
        /// <summary>
        /// Error classes
        /// </summary>
        public enum git_error_t : int
        {
            GIT_ERROR_NONE = unchecked((int)0),
            
            GIT_ERROR_NOMEMORY,
            
            GIT_ERROR_OS,
            
            GIT_ERROR_INVALID,
            
            GIT_ERROR_REFERENCE,
            
            GIT_ERROR_ZLIB,
            
            GIT_ERROR_REPOSITORY,
            
            GIT_ERROR_CONFIG,
            
            GIT_ERROR_REGEX,
            
            GIT_ERROR_ODB,
            
            GIT_ERROR_INDEX,
            
            GIT_ERROR_OBJECT,
            
            GIT_ERROR_NET,
            
            GIT_ERROR_TAG,
            
            GIT_ERROR_TREE,
            
            GIT_ERROR_INDEXER,
            
            GIT_ERROR_SSL,
            
            GIT_ERROR_SUBMODULE,
            
            GIT_ERROR_THREAD,
            
            GIT_ERROR_STASH,
            
            GIT_ERROR_CHECKOUT,
            
            GIT_ERROR_FETCHHEAD,
            
            GIT_ERROR_MERGE,
            
            GIT_ERROR_SSH,
            
            GIT_ERROR_FILTER,
            
            GIT_ERROR_REVERT,
            
            GIT_ERROR_CALLBACK,
            
            GIT_ERROR_CHERRYPICK,
            
            GIT_ERROR_DESCRIBE,
            
            GIT_ERROR_REBASE,
            
            GIT_ERROR_FILESYSTEM,
            
            GIT_ERROR_PATCH,
            
            GIT_ERROR_WORKTREE,
            
            GIT_ERROR_SHA,
            
            GIT_ERROR_HTTP,
            
            GIT_ERROR_INTERNAL,
            
            GIT_ERROR_GRAFTS,
        }
        
        public const libgit2.git_error_t GIT_ERROR_NONE = git_error_t.GIT_ERROR_NONE;
        
        public const libgit2.git_error_t GIT_ERROR_NOMEMORY = git_error_t.GIT_ERROR_NOMEMORY;
        
        public const libgit2.git_error_t GIT_ERROR_OS = git_error_t.GIT_ERROR_OS;
        
        public const libgit2.git_error_t GIT_ERROR_INVALID = git_error_t.GIT_ERROR_INVALID;
        
        public const libgit2.git_error_t GIT_ERROR_REFERENCE = git_error_t.GIT_ERROR_REFERENCE;
        
        public const libgit2.git_error_t GIT_ERROR_ZLIB = git_error_t.GIT_ERROR_ZLIB;
        
        public const libgit2.git_error_t GIT_ERROR_REPOSITORY = git_error_t.GIT_ERROR_REPOSITORY;
        
        public const libgit2.git_error_t GIT_ERROR_CONFIG = git_error_t.GIT_ERROR_CONFIG;
        
        public const libgit2.git_error_t GIT_ERROR_REGEX = git_error_t.GIT_ERROR_REGEX;
        
        public const libgit2.git_error_t GIT_ERROR_ODB = git_error_t.GIT_ERROR_ODB;
        
        public const libgit2.git_error_t GIT_ERROR_INDEX = git_error_t.GIT_ERROR_INDEX;
        
        public const libgit2.git_error_t GIT_ERROR_OBJECT = git_error_t.GIT_ERROR_OBJECT;
        
        public const libgit2.git_error_t GIT_ERROR_NET = git_error_t.GIT_ERROR_NET;
        
        public const libgit2.git_error_t GIT_ERROR_TAG = git_error_t.GIT_ERROR_TAG;
        
        public const libgit2.git_error_t GIT_ERROR_TREE = git_error_t.GIT_ERROR_TREE;
        
        public const libgit2.git_error_t GIT_ERROR_INDEXER = git_error_t.GIT_ERROR_INDEXER;
        
        public const libgit2.git_error_t GIT_ERROR_SSL = git_error_t.GIT_ERROR_SSL;
        
        public const libgit2.git_error_t GIT_ERROR_SUBMODULE = git_error_t.GIT_ERROR_SUBMODULE;
        
        public const libgit2.git_error_t GIT_ERROR_THREAD = git_error_t.GIT_ERROR_THREAD;
        
        public const libgit2.git_error_t GIT_ERROR_STASH = git_error_t.GIT_ERROR_STASH;
        
        public const libgit2.git_error_t GIT_ERROR_CHECKOUT = git_error_t.GIT_ERROR_CHECKOUT;
        
        public const libgit2.git_error_t GIT_ERROR_FETCHHEAD = git_error_t.GIT_ERROR_FETCHHEAD;
        
        public const libgit2.git_error_t GIT_ERROR_MERGE = git_error_t.GIT_ERROR_MERGE;
        
        public const libgit2.git_error_t GIT_ERROR_SSH = git_error_t.GIT_ERROR_SSH;
        
        public const libgit2.git_error_t GIT_ERROR_FILTER = git_error_t.GIT_ERROR_FILTER;
        
        public const libgit2.git_error_t GIT_ERROR_REVERT = git_error_t.GIT_ERROR_REVERT;
        
        public const libgit2.git_error_t GIT_ERROR_CALLBACK = git_error_t.GIT_ERROR_CALLBACK;
        
        public const libgit2.git_error_t GIT_ERROR_CHERRYPICK = git_error_t.GIT_ERROR_CHERRYPICK;
        
        public const libgit2.git_error_t GIT_ERROR_DESCRIBE = git_error_t.GIT_ERROR_DESCRIBE;
        
        public const libgit2.git_error_t GIT_ERROR_REBASE = git_error_t.GIT_ERROR_REBASE;
        
        public const libgit2.git_error_t GIT_ERROR_FILESYSTEM = git_error_t.GIT_ERROR_FILESYSTEM;
        
        public const libgit2.git_error_t GIT_ERROR_PATCH = git_error_t.GIT_ERROR_PATCH;
        
        public const libgit2.git_error_t GIT_ERROR_WORKTREE = git_error_t.GIT_ERROR_WORKTREE;
        
        public const libgit2.git_error_t GIT_ERROR_SHA = git_error_t.GIT_ERROR_SHA;
        
        public const libgit2.git_error_t GIT_ERROR_HTTP = git_error_t.GIT_ERROR_HTTP;
        
        public const libgit2.git_error_t GIT_ERROR_INTERNAL = git_error_t.GIT_ERROR_INTERNAL;
        
        public const libgit2.git_error_t GIT_ERROR_GRAFTS = git_error_t.GIT_ERROR_GRAFTS;
        
        /// <summary>
        /// Structure to store extra details of the last error that occurred.
        /// </summary>
        /// <remarks>
        /// This is kept on a per-thread basis if GIT_THREADS was defined when the
        /// library was build, otherwise one is kept globally for the library
        /// </remarks>
        public partial struct git_error
        {
            public byte* message;
            
            public int klass;
        }
        
        /// <summary>
        /// Return the last `git_error` object that was generated for the
        /// current thread.
        /// </summary>
        /// <returns>@return A git_error object.</returns>
        /// <remarks>
        /// The default behaviour of this function is to return NULL if no previous error has occurred.
        /// However, libgit2's error strings are not cleared aggressively, so a prior
        /// (unrelated) error may be returned. This can be avoided by only calling
        /// this function if the prior call to a libgit2 API returned an error.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_error_last")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_error* git_error_last();
        
        /// <summary>
        /// Clear the last library error that occurred for this thread.
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_error_clear")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_error_clear();
        
        /// <summary>
        /// Set the error message string for this thread, using `printf`-style
        /// formatting.
        /// </summary>
        /// <param name="error_class">One of the `git_error_t` enum above describing the
        /// general subsystem that is responsible for the error.</param>
        /// <param name="fmt">The `printf`-style format string; subsequent arguments must
        /// be the arguments for the format string.</param>
        /// <remarks>
        /// This function is public so that custom ODB backends and the like can
        /// relay an error message through libgit2.  Most regular users of libgit2
        /// will never need to call this function -- actually, calling it in most
        /// circumstances (for example, calling from within a callback function)
        /// will just end up having the value overwritten by libgit2 internals.This error message is stored in thread-local storage and only applies
        /// to the particular thread that this libgit2 call is made from.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_error_set")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_error_set(libgit2.git_error_t error_class, byte* fmt);
        
        /// <summary>
        /// Set the error message string for this thread, using `printf`-style
        /// formatting.
        /// </summary>
        /// <param name="error_class">One of the `git_error_t` enum above describing the
        /// general subsystem that is responsible for the error.</param>
        /// <param name="fmt">The `printf`-style format string; subsequent arguments must
        /// be the arguments for the format string.</param>
        /// <remarks>
        /// This function is public so that custom ODB backends and the like can
        /// relay an error message through libgit2.  Most regular users of libgit2
        /// will never need to call this function -- actually, calling it in most
        /// circumstances (for example, calling from within a callback function)
        /// will just end up having the value overwritten by libgit2 internals.This error message is stored in thread-local storage and only applies
        /// to the particular thread that this libgit2 call is made from.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_error_set")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_error_set(libgit2.git_error_t error_class, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(UTF8MarshallerRelaxedNoCleanup))] string fmt);
        
        /// <summary>
        /// Set the error message string for this thread.  This function is like
        /// `git_error_set` but takes a static string instead of a `printf`-style
        /// format.
        /// </summary>
        /// <param name="error_class">One of the `git_error_t` enum above describing the
        /// general subsystem that is responsible for the error.</param>
        /// <param name="string">The error message to keep</param>
        /// <returns>@return 0 on success or -1 on failure</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_error_set_str")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_error_set_str(libgit2.git_error_t error_class, byte* @string);
        
        /// <summary>
        /// Set the error message string for this thread.  This function is like
        /// `git_error_set` but takes a static string instead of a `printf`-style
        /// format.
        /// </summary>
        /// <param name="error_class">One of the `git_error_t` enum above describing the
        /// general subsystem that is responsible for the error.</param>
        /// <param name="string">The error message to keep</param>
        /// <returns>@return 0 on success or -1 on failure</returns>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_error_set_str")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_error_set_str(libgit2.git_error_t error_class, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(UTF8MarshallerRelaxedNoCleanup))] string @string);
        
        /// <summary>
        /// Set the error message to a special value for memory allocation failure.
        /// </summary>
        /// <remarks>
        /// The normal `git_error_set_str()` function attempts to `strdup()` the
        /// string that is passed in.  This is not a good idea when the error in
        /// question is a memory allocation failure.  That circumstance has a
        /// special setter function that sets the error string to a known and
        /// statically allocated internal value.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_error_set_oom")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void git_error_set_oom();
    }
}