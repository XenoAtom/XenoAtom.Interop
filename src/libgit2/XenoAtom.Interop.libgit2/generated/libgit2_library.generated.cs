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
    
    public static unsafe partial class libgit2
    {
        public readonly partial struct _LIBSSH2_SESSION : IEquatable<_LIBSSH2_SESSION>
        {
            public _LIBSSH2_SESSION(nint handle) => Handle = handle;
            
            public nint Handle { get; }
            
            public bool Equals(_LIBSSH2_SESSION other) => Handle.Equals(other.Handle);
            
            public override bool Equals(object obj) => obj is _LIBSSH2_SESSION other && Equals(other);
            
            public override int GetHashCode() => Handle.GetHashCode();
            
            public override string ToString() => "0x" + (nint.Size == 8 ? Handle.ToString("X16") : Handle.ToString("X8"));
            
            public static bool operator ==(_LIBSSH2_SESSION left, _LIBSSH2_SESSION right) => left.Equals(right);
            
            public static bool operator !=(_LIBSSH2_SESSION left, _LIBSSH2_SESSION right) => !left.Equals(right);
        }
        
        public readonly partial struct _LIBSSH2_USERAUTH_KBDINT_PROMPT : IEquatable<_LIBSSH2_USERAUTH_KBDINT_PROMPT>
        {
            public _LIBSSH2_USERAUTH_KBDINT_PROMPT(nint handle) => Handle = handle;
            
            public nint Handle { get; }
            
            public bool Equals(_LIBSSH2_USERAUTH_KBDINT_PROMPT other) => Handle.Equals(other.Handle);
            
            public override bool Equals(object obj) => obj is _LIBSSH2_USERAUTH_KBDINT_PROMPT other && Equals(other);
            
            public override int GetHashCode() => Handle.GetHashCode();
            
            public override string ToString() => "0x" + (nint.Size == 8 ? Handle.ToString("X16") : Handle.ToString("X8"));
            
            public static bool operator ==(_LIBSSH2_USERAUTH_KBDINT_PROMPT left, _LIBSSH2_USERAUTH_KBDINT_PROMPT right) => left.Equals(right);
            
            public static bool operator !=(_LIBSSH2_USERAUTH_KBDINT_PROMPT left, _LIBSSH2_USERAUTH_KBDINT_PROMPT right) => !left.Equals(right);
        }
        
        public readonly partial struct _LIBSSH2_USERAUTH_KBDINT_RESPONSE : IEquatable<_LIBSSH2_USERAUTH_KBDINT_RESPONSE>
        {
            public _LIBSSH2_USERAUTH_KBDINT_RESPONSE(nint handle) => Handle = handle;
            
            public nint Handle { get; }
            
            public bool Equals(_LIBSSH2_USERAUTH_KBDINT_RESPONSE other) => Handle.Equals(other.Handle);
            
            public override bool Equals(object obj) => obj is _LIBSSH2_USERAUTH_KBDINT_RESPONSE other && Equals(other);
            
            public override int GetHashCode() => Handle.GetHashCode();
            
            public override string ToString() => "0x" + (nint.Size == 8 ? Handle.ToString("X16") : Handle.ToString("X8"));
            
            public static bool operator ==(_LIBSSH2_USERAUTH_KBDINT_RESPONSE left, _LIBSSH2_USERAUTH_KBDINT_RESPONSE right) => left.Equals(right);
            
            public static bool operator !=(_LIBSSH2_USERAUTH_KBDINT_RESPONSE left, _LIBSSH2_USERAUTH_KBDINT_RESPONSE right) => !left.Equals(right);
        }
        
        public readonly partial struct git_iterator : IEquatable<git_iterator>
        {
            public git_iterator(nint handle) => Handle = handle;
            
            public nint Handle { get; }
            
            public bool Equals(git_iterator other) => Handle.Equals(other.Handle);
            
            public override bool Equals(object obj) => obj is git_iterator other && Equals(other);
            
            public override int GetHashCode() => Handle.GetHashCode();
            
            public override string ToString() => "0x" + (nint.Size == 8 ? Handle.ToString("X16") : Handle.ToString("X8"));
            
            public static bool operator ==(git_iterator left, git_iterator right) => left.Equals(right);
            
            public static bool operator !=(git_iterator left, git_iterator right) => !left.Equals(right);
        }
        
        /// <summary>
        /// A result integer from a git function. 0 if successful, 
        /// &lt;
        /// 0 if an error.
        /// </summary>
        public readonly partial struct git_result : IEquatable<git_result>
        {
            public git_result(int value) => this.Value = value;
            
            public int Value { get; }
            
            public bool Equals(git_result other) =>  Value.Equals(other.Value);
            
            public override bool Equals(object obj) => obj is git_result other && Equals(other);
            
            public override int GetHashCode() => Value.GetHashCode();
            
            public override string ToString() => Value.ToString();
            
            public static implicit operator int(git_result from) => from.Value;
            
            public static implicit operator git_result(int from) => new git_result(from);
            
            public static bool operator ==(git_result left, git_result right) => left.Equals(right);
            
            public static bool operator !=(git_result left, git_result right) => !left.Equals(right);
        }
        
        public const uint GIT_REPOSITORY_INIT_OPTIONS_VERSION = 1;
        
        public const uint GIT_DIFF_OPTIONS_VERSION = 1;
        
        public const uint GIT_DIFF_FIND_OPTIONS_VERSION = 1;
        
        public const uint GIT_DIFF_PARSE_OPTIONS_VERSION = 1;
        
        public const uint GIT_DIFF_PATCHID_OPTIONS_VERSION = 1;
        
        public const uint GIT_APPLY_OPTIONS_VERSION = 1;
        
        public const uint GIT_ATTR_OPTIONS_VERSION = 1;
        
        public const uint GIT_BLOB_FILTER_OPTIONS_VERSION = 1;
        
        public const uint GIT_BLAME_OPTIONS_VERSION = 1;
        
        public const uint GIT_CHECKOUT_OPTIONS_VERSION = 1;
        
        public const uint GIT_INDEXER_OPTIONS_VERSION = 1;
        
        public const uint GIT_MERGE_FILE_INPUT_VERSION = 1;
        
        public const uint GIT_MERGE_FILE_OPTIONS_VERSION = 1;
        
        public const uint GIT_MERGE_OPTIONS_VERSION = 1;
        
        public const uint GIT_CHERRYPICK_OPTIONS_VERSION = 1;
        
        public const uint GIT_PROXY_OPTIONS_VERSION = 1;
        
        public const uint GIT_REMOTE_CREATE_OPTIONS_VERSION = 1;
        
        public const uint GIT_REMOTE_CALLBACKS_VERSION = 1;
        
        public const uint GIT_FETCH_OPTIONS_VERSION = 1;
        
        public const uint GIT_PUSH_OPTIONS_VERSION = 1;
        
        public const uint GIT_REMOTE_CONNECT_OPTIONS_VERSION = 1;
        
        public const uint GIT_CLONE_OPTIONS_VERSION = 1;
        
        public const uint GIT_DESCRIBE_OPTIONS_VERSION = 1;
        
        public const uint GIT_DESCRIBE_FORMAT_OPTIONS_VERSION = 1;
        
        public const uint GIT_FILTER_OPTIONS_VERSION = 1;
        
        public const uint GIT_REBASE_OPTIONS_VERSION = 1;
        
        public const uint GIT_REVERT_OPTIONS_VERSION = 1;
        
        public const uint GIT_STASH_SAVE_OPTIONS_VERSION = 1;
        
        public const uint GIT_STASH_APPLY_OPTIONS_VERSION = 1;
        
        public const uint GIT_STATUS_OPTIONS_VERSION = 1;
        
        public const uint GIT_SUBMODULE_UPDATE_OPTIONS_VERSION = 1;
        
        public const uint GIT_WORKTREE_ADD_OPTIONS_VERSION = 1;
        
        public const uint GIT_WORKTREE_PRUNE_OPTIONS_VERSION = 1;
        
        public const uint GIT_EMAIL_CREATE_OPTIONS_VERSION = 1;
        
        public const uint GIT_ODB_OPTIONS_VERSION = 1;
        
        public const uint GIT_ODB_BACKEND_PACK_OPTIONS_VERSION = 1;
        
        public const uint GIT_ODB_BACKEND_LOOSE_OPTIONS_VERSION = 1;
        
        public const string LIBGIT2_VERSION = "1.7.2";
        
        public const int LIBGIT2_VER_MAJOR = 1;
        
        public const int LIBGIT2_VER_MINOR = 7;
        
        public const int LIBGIT2_VER_REVISION = 2;
        
        public const int LIBGIT2_VER_PATCH = 0;
        
        public const string LIBGIT2_SOVERSION = "1.7";
    }
}