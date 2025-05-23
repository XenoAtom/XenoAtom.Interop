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
            
            /// <summary>
            /// There were no changes
            /// </summary>
            GIT_EUNCHANGED = unchecked((int)-38),
            
            /// <summary>
            /// An option is not supported
            /// </summary>
            GIT_ENOTSUPPORTED = unchecked((int)-39),
            
            /// <summary>
            /// The subject is read-only
            /// </summary>
            GIT_EREADONLY = unchecked((int)-40),
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
        /// There were no changes
        /// </summary>
        public const libgit2.git_error_code GIT_EUNCHANGED = git_error_code.GIT_EUNCHANGED;
        
        /// <summary>
        /// An option is not supported
        /// </summary>
        public const libgit2.git_error_code GIT_ENOTSUPPORTED = git_error_code.GIT_ENOTSUPPORTED;
        
        /// <summary>
        /// The subject is read-only
        /// </summary>
        public const libgit2.git_error_code GIT_EREADONLY = git_error_code.GIT_EREADONLY;
        
        /// <summary>
        /// Error classes
        /// </summary>
        public enum git_error_t : uint
        {
            GIT_ERROR_NONE = unchecked((uint)0),
            
            GIT_ERROR_NOMEMORY = unchecked((uint)1),
            
            GIT_ERROR_OS = unchecked((uint)2),
            
            GIT_ERROR_INVALID = unchecked((uint)3),
            
            GIT_ERROR_REFERENCE = unchecked((uint)4),
            
            GIT_ERROR_ZLIB = unchecked((uint)5),
            
            GIT_ERROR_REPOSITORY = unchecked((uint)6),
            
            GIT_ERROR_CONFIG = unchecked((uint)7),
            
            GIT_ERROR_REGEX = unchecked((uint)8),
            
            GIT_ERROR_ODB = unchecked((uint)9),
            
            GIT_ERROR_INDEX = unchecked((uint)10),
            
            GIT_ERROR_OBJECT = unchecked((uint)11),
            
            GIT_ERROR_NET = unchecked((uint)12),
            
            GIT_ERROR_TAG = unchecked((uint)13),
            
            GIT_ERROR_TREE = unchecked((uint)14),
            
            GIT_ERROR_INDEXER = unchecked((uint)15),
            
            GIT_ERROR_SSL = unchecked((uint)16),
            
            GIT_ERROR_SUBMODULE = unchecked((uint)17),
            
            GIT_ERROR_THREAD = unchecked((uint)18),
            
            GIT_ERROR_STASH = unchecked((uint)19),
            
            GIT_ERROR_CHECKOUT = unchecked((uint)20),
            
            GIT_ERROR_FETCHHEAD = unchecked((uint)21),
            
            GIT_ERROR_MERGE = unchecked((uint)22),
            
            GIT_ERROR_SSH = unchecked((uint)23),
            
            GIT_ERROR_FILTER = unchecked((uint)24),
            
            GIT_ERROR_REVERT = unchecked((uint)25),
            
            GIT_ERROR_CALLBACK = unchecked((uint)26),
            
            GIT_ERROR_CHERRYPICK = unchecked((uint)27),
            
            GIT_ERROR_DESCRIBE = unchecked((uint)28),
            
            GIT_ERROR_REBASE = unchecked((uint)29),
            
            GIT_ERROR_FILESYSTEM = unchecked((uint)30),
            
            GIT_ERROR_PATCH = unchecked((uint)31),
            
            GIT_ERROR_WORKTREE = unchecked((uint)32),
            
            GIT_ERROR_SHA = unchecked((uint)33),
            
            GIT_ERROR_HTTP = unchecked((uint)34),
            
            GIT_ERROR_INTERNAL = unchecked((uint)35),
            
            GIT_ERROR_GRAFTS = unchecked((uint)36),
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
        /// <returns>A git_error object.</returns>
        /// <remarks>
        /// This function will never return NULL.Callers should not rely on this to determine whether an error has
        /// occurred. For error checking, callers should examine the return
        /// codes of libgit2 functions.This call can only reliably report error messages when an error
        /// has occurred. (It may contain stale information if it is called
        /// after a different function that succeeds.)The memory for this object is managed by libgit2. It should not
        /// be freed.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_error_last")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_error* git_error_last();
    }
}
