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
        public enum z_flush_t : uint
        {
            Z_NO_FLUSH = unchecked((uint)0),
            
            Z_PARTIAL_FLUSH = unchecked((uint)1),
            
            Z_SYNC_FLUSH = unchecked((uint)2),
            
            Z_FULL_FLUSH = unchecked((uint)3),
            
            Z_FINISH = unchecked((uint)4),
            
            Z_BLOCK = unchecked((uint)5),
            
            Z_TREES = unchecked((uint)6),
        }
        
        public const zlib.z_flush_t Z_NO_FLUSH = z_flush_t.Z_NO_FLUSH;
        
        public const zlib.z_flush_t Z_PARTIAL_FLUSH = z_flush_t.Z_PARTIAL_FLUSH;
        
        public const zlib.z_flush_t Z_SYNC_FLUSH = z_flush_t.Z_SYNC_FLUSH;
        
        public const zlib.z_flush_t Z_FULL_FLUSH = z_flush_t.Z_FULL_FLUSH;
        
        public const zlib.z_flush_t Z_FINISH = z_flush_t.Z_FINISH;
        
        public const zlib.z_flush_t Z_BLOCK = z_flush_t.Z_BLOCK;
        
        public const zlib.z_flush_t Z_TREES = z_flush_t.Z_TREES;
        
        public enum z_result_t : int
        {
            Z_OK = unchecked((int)0),
            
            Z_STREAM_END = unchecked((int)1),
            
            Z_NEED_DICT = unchecked((int)2),
            
            Z_ERRNO = unchecked((int)-1),
            
            Z_STREAM_ERROR = unchecked((int)-2),
            
            Z_DATA_ERROR = unchecked((int)-3),
            
            Z_MEM_ERROR = unchecked((int)-4),
            
            Z_BUF_ERROR = unchecked((int)-5),
            
            Z_VERSION_ERROR = unchecked((int)-6),
        }
        
        public const zlib.z_result_t Z_OK = z_result_t.Z_OK;
        
        public const zlib.z_result_t Z_STREAM_END = z_result_t.Z_STREAM_END;
        
        public const zlib.z_result_t Z_NEED_DICT = z_result_t.Z_NEED_DICT;
        
        public const zlib.z_result_t Z_ERRNO = z_result_t.Z_ERRNO;
        
        public const zlib.z_result_t Z_STREAM_ERROR = z_result_t.Z_STREAM_ERROR;
        
        public const zlib.z_result_t Z_DATA_ERROR = z_result_t.Z_DATA_ERROR;
        
        public const zlib.z_result_t Z_MEM_ERROR = z_result_t.Z_MEM_ERROR;
        
        public const zlib.z_result_t Z_BUF_ERROR = z_result_t.Z_BUF_ERROR;
        
        public const zlib.z_result_t Z_VERSION_ERROR = z_result_t.Z_VERSION_ERROR;
        
        public enum z_strategy_t : uint
        {
            Z_FILTERED = unchecked((uint)1),
            
            Z_HUFFMAN_ONLY = unchecked((uint)2),
            
            Z_RLE = unchecked((uint)3),
            
            Z_FIXED = unchecked((uint)4),
            
            Z_DEFAULT_STRATEGY = unchecked((uint)0),
        }
        
        public const zlib.z_strategy_t Z_FILTERED = z_strategy_t.Z_FILTERED;
        
        public const zlib.z_strategy_t Z_HUFFMAN_ONLY = z_strategy_t.Z_HUFFMAN_ONLY;
        
        public const zlib.z_strategy_t Z_RLE = z_strategy_t.Z_RLE;
        
        public const zlib.z_strategy_t Z_FIXED = z_strategy_t.Z_FIXED;
        
        public const zlib.z_strategy_t Z_DEFAULT_STRATEGY = z_strategy_t.Z_DEFAULT_STRATEGY;
        
        public enum z_datatype_t : uint
        {
            Z_BINARY = unchecked((uint)0),
            
            Z_TEXT = unchecked((uint)1),
            
            /// <summary>
            /// for compatibility with 1.2.2 and earlier
            /// </summary>
            Z_ASCII = unchecked((uint)1),
            
            Z_UNKNOWN = unchecked((uint)2),
            
            Z_DEFLATED = unchecked((uint)8),
        }
        
        public const zlib.z_datatype_t Z_BINARY = z_datatype_t.Z_BINARY;
        
        public const zlib.z_datatype_t Z_TEXT = z_datatype_t.Z_TEXT;
        
        /// <summary>
        /// for compatibility with 1.2.2 and earlier
        /// </summary>
        public const zlib.z_datatype_t Z_ASCII = z_datatype_t.Z_ASCII;
        
        public const zlib.z_datatype_t Z_UNKNOWN = z_datatype_t.Z_UNKNOWN;
        
        public const zlib.z_datatype_t Z_DEFLATED = z_datatype_t.Z_DEFLATED;
        
        public readonly partial struct internal_state : IEquatable<zlib.internal_state>
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
    }
}
