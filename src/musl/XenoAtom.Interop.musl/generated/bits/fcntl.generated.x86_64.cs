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
    public static unsafe partial class musl
    {
        public static unsafe partial class x86_64
        {
            public const int O_DIRECT = 16384;
            
            /// <summary>
            /// If \fIpathname\fP is not a directory, cause the open to fail.
            /// But see the following and its replies:
            /// http://marc.theaimsgroup.com/?t=112748702800001&amp;r=1&amp;w=2
            /// [PATCH] open: O_DIRECTORY and O_CREAT together should fail
            /// O_DIRECTORY | O_CREAT causes O_DIRECTORY to be ignored.
            /// This flag was added in Linux 2.1.126, to
            /// avoid denial-of-service problems if
            /// opendir (3)
            /// is called on a
            /// FIFO or tape device.
            /// </summary>
            public const int O_DIRECTORY = 65536;
            
            /// <summary>
            /// Enable support for files exceeding 2\ GB.
            /// Failing to set this flag will result in an
            /// EOVERFLOW
            /// error when trying to open a large file which is monitored by
            /// an fanotify group on a 32-bit system.
            /// </summary>
            public const int O_LARGEFILE = 32768;
            
            /// <summary>
            /// If the trailing component (i.e., basename) of
            /// pathname
            /// is a symbolic link, then the open fails, with the error
            /// ELOOP .
            /// Symbolic links in earlier components of the pathname will still be
            /// followed.
            /// (Note that the
            /// ELOOP
            /// error that can occur in this case is indistinguishable from the case where
            /// an open fails because there are too many symbolic links found
            /// while resolving components in the prefix part of the pathname.)
            /// 
            /// This flag is a FreeBSD extension, which was added in Linux 2.1.126,
            /// and has subsequently been standardized in POSIX.1-2008.
            /// 
            /// See also
            /// O_PATH
            /// below.
            /// The headers from glibc 2.0.100 and later include a
            /// definition of this flag; \fIkernels before Linux 2.1.126 will ignore it if
            /// used\fP.
            /// </summary>
            public const int O_NOFOLLOW = 131072;
            
            public const int O_TMPFILE = 4259840;
        }
    }
}