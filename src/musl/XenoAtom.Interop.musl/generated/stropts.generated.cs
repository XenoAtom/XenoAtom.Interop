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
    
    public static unsafe partial class musl
    {
        public partial struct bandinfo
        {
            public byte bi_pri;
            
            public int bi_flag;
        }
        
        public partial struct strbuf
        {
            public int maxlen;
            
            public int len;
            
            public byte* buf;
        }
        
        public partial struct strpeek
        {
            public musl.strbuf ctlbuf;
            
            public musl.strbuf databuf;
            
            public uint flags;
        }
        
        public partial struct strfdinsert
        {
            public musl.strbuf ctlbuf;
            
            public musl.strbuf databuf;
            
            public uint flags;
            
            public int fildes;
            
            public int offset;
        }
        
        public partial struct strioctl
        {
            public int ic_cmd;
            
            public int ic_timout;
            
            public int ic_len;
            
            public byte* ic_dp;
        }
        
        public unsafe partial struct strrecvfd
        {
            public int fd;
            
            public int uid;
            
            public int gid;
            
            public fixed byte __fill[8];
        }
        
        public unsafe partial struct str_mlist
        {
            public fixed byte l_name[9];
        }
        
        public partial struct str_list
        {
            public int sl_nmods;
            
            public musl.str_mlist* sl_modlist;
        }
        
        public const int I_NREAD = 21249;
        
        public const int I_PUSH = 21250;
        
        public const int I_POP = 21251;
        
        public const int I_LOOK = 21252;
        
        public const int I_FLUSH = 21253;
        
        public const int I_SRDOPT = 21254;
        
        public const int I_GRDOPT = 21255;
        
        public const int I_STR = 21256;
        
        public const int I_SETSIG = 21257;
        
        public const int I_GETSIG = 21258;
        
        public const int I_FIND = 21259;
        
        public const int I_LINK = 21260;
        
        public const int I_UNLINK = 21261;
        
        public const int I_PEEK = 21263;
        
        public const int I_FDINSERT = 21264;
        
        public const int I_SENDFD = 21265;
        
        public const int I_RECVFD = 21262;
        
        public const int I_SWROPT = 21267;
        
        public const int I_GWROPT = 21268;
        
        public const int I_LIST = 21269;
        
        public const int I_PLINK = 21270;
        
        public const int I_PUNLINK = 21271;
        
        public const int I_FLUSHBAND = 21276;
        
        public const int I_CKBAND = 21277;
        
        public const int I_GETBAND = 21278;
        
        public const int I_ATMARK = 21279;
        
        public const int I_SETCLTIME = 21280;
        
        public const int I_GETCLTIME = 21281;
        
        public const int I_CANPUT = 21282;
        
        public const int FLUSHR = 1;
        
        public const int FLUSHW = 2;
        
        public const int FLUSHRW = 3;
        
        public const int FLUSHBAND = 4;
        
        public const int S_INPUT = 1;
        
        public const int S_HIPRI = 2;
        
        public const int S_OUTPUT = 4;
        
        public const int S_MSG = 8;
        
        public const int S_ERROR = 16;
        
        public const int S_HANGUP = 32;
        
        public const int S_RDNORM = 64;
        
        public const int S_WRNORM = 4;
        
        public const int S_RDBAND = 128;
        
        public const int S_WRBAND = 256;
        
        public const int S_BANDURG = 512;
        
        public const int RS_HIPRI = 1;
        
        public const int RNORM = 0;
        
        public const int RMSGD = 1;
        
        public const int RMSGN = 2;
        
        public const int RPROTDAT = 4;
        
        public const int RPROTDIS = 8;
        
        public const int RPROTNORM = 16;
        
        public const int RPROTMASK = 28;
        
        public const int SNDZERO = 1;
        
        public const int SNDPIPE = 2;
        
        public const int ANYMARK = 1;
        
        public const int LASTMARK = 2;
        
        public const int MUXID_ALL = -1;
        
        public const int MSG_HIPRI = 1;
        
        public const int MSG_ANY = 2;
        
        public const int MSG_BAND = 4;
        
        public const int MORECTL = 1;
        
        public const int MOREDATA = 2;
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "isastream")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int isastream(int arg0);
    }
}