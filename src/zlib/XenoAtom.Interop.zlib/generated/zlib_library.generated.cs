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
    
    public static unsafe partial class zlib
    {
        public readonly partial struct internal_state : IEquatable<internal_state>
        {
            public internal_state(nint handle) => Handle = handle;
            
            public nint Handle { get; }
            
            public bool Equals(internal_state other) => Handle.Equals(other.Handle);
            
            public override bool Equals(object obj) => obj is internal_state other && Equals(other);
            
            public override int GetHashCode() => Handle.GetHashCode();
            
            public override string ToString() => "0x" + (nint.Size == 8 ? Handle.ToString("X16") : Handle.ToString("X8"));
            
            public static bool operator ==(internal_state left, internal_state right) => left.Equals(right);
            
            public static bool operator !=(internal_state left, internal_state right) => !left.Equals(right);
        }
        
        /// <summary>
        /// 32K LZ77 window
        /// </summary>
        public const int MAX_WBITS = 15;
        
        public const string ZLIB_VERSION = "1.3.1";
        
        public const int ZLIB_VERNUM = 0x1310;
        
        public const int ZLIB_VER_MAJOR = 1;
        
        public const int ZLIB_VER_MINOR = 3;
        
        public const int ZLIB_VER_REVISION = 1;
        
        public const int ZLIB_VER_SUBREVISION = 0;
        
        public const int Z_NO_FLUSH = 0;
        
        public const int Z_PARTIAL_FLUSH = 1;
        
        public const int Z_SYNC_FLUSH = 2;
        
        public const int Z_FULL_FLUSH = 3;
        
        public const int Z_FINISH = 4;
        
        public const int Z_BLOCK = 5;
        
        public const int Z_TREES = 6;
        
        public const int Z_OK = 0;
        
        public const int Z_STREAM_END = 1;
        
        public const int Z_NEED_DICT = 2;
        
        public const int Z_ERRNO = (-1);
        
        public const int Z_STREAM_ERROR = (-2);
        
        public const int Z_DATA_ERROR = (-3);
        
        public const int Z_MEM_ERROR = (-4);
        
        public const int Z_BUF_ERROR = (-5);
        
        public const int Z_VERSION_ERROR = (-6);
        
        public const int Z_NO_COMPRESSION = 0;
        
        public const int Z_BEST_SPEED = 1;
        
        public const int Z_BEST_COMPRESSION = 9;
        
        public const int Z_DEFAULT_COMPRESSION = (-1);
        
        public const int Z_FILTERED = 1;
        
        public const int Z_HUFFMAN_ONLY = 2;
        
        public const int Z_RLE = 3;
        
        public const int Z_FIXED = 4;
        
        public const int Z_DEFAULT_STRATEGY = 0;
        
        public const int Z_BINARY = 0;
        
        public const int Z_TEXT = 1;
        
        /// <summary>
        /// for compatibility with 1.2.2 and earlier
        /// </summary>
        public const int Z_ASCII = 1;
        
        public const int Z_UNKNOWN = 2;
        
        public const int Z_DEFLATED = 8;
        
        /// <summary>
        /// for initializing zalloc, zfree, opaque
        /// </summary>
        public const int Z_NULL = 0;
    }
}