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
    
    public static unsafe partial class musl
    {
        public readonly partial struct __locale_struct : IEquatable<__locale_struct>
        {
            public __locale_struct(nint handle) => Handle = handle;
            
            public nint Handle { get; }
            
            public bool Equals(__locale_struct other) => Handle.Equals(other.Handle);
            
            public override bool Equals(object obj) => obj is __locale_struct other && Equals(other);
            
            public override int GetHashCode() => Handle.GetHashCode();
            
            public override string ToString() => "0x" + (nint.Size == 8 ? Handle.ToString("X16") : Handle.ToString("X8"));
            
            public static bool operator ==(__locale_struct left, __locale_struct right) => left.Equals(right);
            
            public static bool operator !=(__locale_struct left, __locale_struct right) => !left.Equals(right);
        }
    }
}