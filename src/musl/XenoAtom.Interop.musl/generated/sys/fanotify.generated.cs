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
        public partial struct fanotify_event_metadata
        {
            public uint event_len;
            
            public byte vers;
            
            public byte reserved;
            
            public ushort metadata_len;
            
            public ulong mask;
            
            public int fd;
            
            public int pid;
        }
        
        public partial struct fanotify_event_info_header
        {
            public byte info_type;
            
            public byte pad;
            
            public ushort len;
        }
        
        public unsafe partial struct fanotify_event_info_fid
        {
            public musl.fanotify_event_info_header hdr;
            
            public musl.fsid_t fsid;
            
            public fixed byte handle[1];
        }
        
        public partial struct fanotify_response
        {
            public int fd;
            
            public uint response;
        }
        
        /// <summary>
        /// Create an event when a file or directory (but see BUGS) is accessed (read).
        /// </summary>
        public const int FAN_ACCESS = 1;
        
        /// <summary>
        /// Create an event when a file is modified (write).
        /// </summary>
        public const int FAN_MODIFY = 2;
        
        public const int FAN_ATTRIB = 4;
        
        /// <summary>
        /// Create an event when a writable file is closed.
        /// </summary>
        public const int FAN_CLOSE_WRITE = 8;
        
        /// <summary>
        /// Create an event when a read-only file or directory is closed.
        /// </summary>
        public const int FAN_CLOSE_NOWRITE = 16;
        
        /// <summary>
        /// Create an event when a file or directory is opened.
        /// </summary>
        public const int FAN_OPEN = 32;
        
        public const int FAN_MOVED_FROM = 64;
        
        public const int FAN_MOVED_TO = 128;
        
        public const int FAN_CREATE = 256;
        
        public const int FAN_DELETE = 512;
        
        public const int FAN_DELETE_SELF = 1024;
        
        public const int FAN_MOVE_SELF = 2048;
        
        public const int FAN_OPEN_EXEC = 4096;
        
        public const int FAN_Q_OVERFLOW = 16384;
        
        /// <summary>
        /// Create an event when a permission to open a file or directory is requested.
        /// An fanotify file descriptor created with
        /// FAN_CLASS_PRE_CONTENT
        /// or
        /// FAN_CLASS_CONTENT
        /// is required.
        /// </summary>
        public const int FAN_OPEN_PERM = 65536;
        
        /// <summary>
        /// Create an event when a permission to read a file or directory is requested.
        /// An fanotify file descriptor created with
        /// FAN_CLASS_PRE_CONTENT
        /// or
        /// FAN_CLASS_CONTENT
        /// is required.
        /// </summary>
        public const int FAN_ACCESS_PERM = 131072;
        
        public const int FAN_OPEN_EXEC_PERM = 262144;
        
        public const int FAN_DIR_MODIFY = 524288;
        
        /// <summary>
        /// Events for the immediate children of marked directories shall be created.
        /// The flag has no effect when marking mounts and filesystems.
        /// Note that events are not generated for children of the subdirectories
        /// of marked directories.
        /// More specifically, the directory entry modification events
        /// FAN_CREATE ,
        /// FAN_DELETE ,
        /// FAN_MOVED_FROM ,
        /// and
        /// FAN_MOVED_TO
        /// are not generated for any entry modifications performed inside subdirectories
        /// of marked directories.
        /// Note that the events
        /// FAN_DELETE_SELF
        /// and
        /// FAN_MOVE_SELF
        /// are not generated for children of marked directories.
        /// To monitor complete directory trees it is necessary to mark the relevant
        /// mount or filesystem.
        /// 
        /// The following composed values are defined:
        /// </summary>
        public const int FAN_EVENT_ON_CHILD = 134217728;
        
        /// <summary>
        /// Create events for directories\[em]for example, when
        /// opendir (3),
        /// readdir (3)
        /// (but see BUGS), and
        /// closedir (3)
        /// are called.
        /// Without this flag, events are created only for files.
        /// In the context of directory entry events, such as
        /// FAN_CREATE ,
        /// FAN_DELETE ,
        /// FAN_MOVED_FROM ,
        /// and
        /// FAN_MOVED_TO ,
        /// specifying the flag
        /// FAN_ONDIR
        /// is required in order to create events when subdirectory entries are
        /// modified (i.e.,
        /// mkdir (2)/
        /// rmdir (2)).
        /// </summary>
        public const int FAN_ONDIR = 1073741824;
        
        /// <summary>
        /// A file is closed
        /// ( FAN_CLOSE_WRITE | FAN_CLOSE_NOWRITE ).
        /// </summary>
        public const int FAN_CLOSE = 24;
        
        /// <summary>
        /// A file or directory has been moved
        /// ( FAN_MOVED_FROM | FAN_MOVED_TO ).
        /// 
        /// The filesystem object to be marked is determined by the file descriptor
        /// dirfd
        /// and the pathname specified in
        /// pathname :
        /// \[bu] 3
        /// If
        /// pathname
        /// is NULL,
        /// dirfd
        /// defines the filesystem object to be marked.
        /// \[bu]
        /// If
        /// pathname
        /// is NULL, and
        /// dirfd
        /// takes the special value
        /// AT_FDCWD ,
        /// the current working directory is to be marked.
        /// \[bu]
        /// If
        /// pathname
        /// is absolute, it defines the filesystem object to be marked, and
        /// dirfd
        /// is ignored.
        /// \[bu]
        /// If
        /// pathname
        /// is relative, and
        /// dirfd
        /// does not have the value
        /// AT_FDCWD ,
        /// then the filesystem object to be marked is determined by interpreting
        /// pathname
        /// relative the directory referred to by
        /// dirfd .
        /// \[bu]
        /// If
        /// pathname
        /// is relative, and
        /// dirfd
        /// has the value
        /// AT_FDCWD ,
        /// then the filesystem object to be marked is determined by interpreting
        /// pathname
        /// relative to the current working directory.
        /// (See
        /// openat (2)
        /// for an explanation of why the
        /// dirfd
        /// argument is useful.)
        /// </summary>
        public const int FAN_MOVE = 192;
        
        /// <summary>
        /// Set the close-on-exec flag
        /// ( FD_CLOEXEC )
        /// on the new file descriptor.
        /// See the description of the
        /// O_CLOEXEC
        /// flag in
        /// open (2).
        /// </summary>
        public const int FAN_CLOEXEC = 1;
        
        /// <summary>
        /// Enable the nonblocking flag
        /// ( O_NONBLOCK )
        /// for the file descriptor.
        /// Reading from the file descriptor will not block.
        /// Instead, if no data is available,
        /// read (2)
        /// fails with the error
        /// EAGAIN .
        /// </summary>
        public const int FAN_NONBLOCK = 2;
        
        /// <summary>
        /// This is the default value.
        /// It does not need to be specified.
        /// This value only allows the receipt of events notifying that a file has been
        /// accessed.
        /// Permission decisions before the file is accessed are not possible.
        /// 
        /// Listeners with different notification classes will receive events in the
        /// order
        /// FAN_CLASS_PRE_CONTENT ,
        /// FAN_CLASS_CONTENT ,
        /// FAN_CLASS_NOTIF .
        /// The order of notification for listeners in the same notification class
        /// is undefined.
        /// 
        /// The following bits can additionally be set in
        /// flags :
        /// </summary>
        public const int FAN_CLASS_NOTIF = 0;
        
        /// <summary>
        /// This value allows the receipt of events notifying that a file has been
        /// accessed and events for permission decisions if a file may be accessed.
        /// It is intended for event listeners that need to access files when they
        /// already contain their final content.
        /// This notification class might be used by malware detection programs, for
        /// example.
        /// Use of this flag requires the
        /// CAP_SYS_ADMIN
        /// capability.
        /// </summary>
        public const int FAN_CLASS_CONTENT = 4;
        
        /// <summary>
        /// This value allows the receipt of events notifying that a file has been
        /// accessed and events for permission decisions if a file may be accessed.
        /// It is intended for event listeners that need to access files before they
        /// contain their final data.
        /// This notification class might be used by hierarchical storage managers,
        /// for example.
        /// Use of this flag requires the
        /// CAP_SYS_ADMIN
        /// capability.
        /// </summary>
        public const int FAN_CLASS_PRE_CONTENT = 8;
        
        public const int FAN_ALL_CLASS_BITS = 12;
        
        /// <summary>
        /// Remove the limit on the number of events in the event queue.
        /// See
        /// fanotify (7)
        /// for details about this limit.
        /// Use of this flag requires the
        /// CAP_SYS_ADMIN
        /// capability.
        /// </summary>
        public const int FAN_UNLIMITED_QUEUE = 16;
        
        /// <summary>
        /// Remove the limit on the number of fanotify marks per user.
        /// See
        /// fanotify (7)
        /// for details about this limit.
        /// Use of this flag requires the
        /// CAP_SYS_ADMIN
        /// capability.
        /// </summary>
        public const int FAN_UNLIMITED_MARKS = 32;
        
        public const int FAN_ENABLE_AUDIT = 64;
        
        public const int FAN_REPORT_TID = 256;
        
        public const int FAN_REPORT_FID = 512;
        
        public const int FAN_REPORT_DIR_FID = 1024;
        
        public const int FAN_REPORT_NAME = 2048;
        
        /// <summary>
        /// This is a synonym for
        /// ( FAN_REPORT_DIR_FID | FAN_REPORT_NAME ).
        /// </summary>
        public const int FAN_REPORT_DFID_NAME = 3072;
        
        public const int FAN_ALL_INIT_FLAGS = 63;
        
        /// <summary>
        /// The events in
        /// mask
        /// will be added to the mark mask (or to the ignore mask).
        /// mask
        /// must be nonempty or the error
        /// EINVAL
        /// will occur.
        /// </summary>
        public const int FAN_MARK_ADD = 1;
        
        /// <summary>
        /// The events in argument
        /// mask
        /// will be removed from the mark mask (or from the ignore mask).
        /// mask
        /// must be nonempty or the error
        /// EINVAL
        /// will occur.
        /// </summary>
        public const int FAN_MARK_REMOVE = 2;
        
        /// <summary>
        /// If
        /// pathname
        /// is a symbolic link, mark the link itself, rather than the file to which it
        /// refers.
        /// (By default,
        /// fanotify_mark ()
        /// dereferences
        /// pathname
        /// if it is a symbolic link.)
        /// </summary>
        public const int FAN_MARK_DONT_FOLLOW = 4;
        
        /// <summary>
        /// If the filesystem object to be marked is not a directory, the error
        /// ENOTDIR
        /// shall be raised.
        /// </summary>
        public const int FAN_MARK_ONLYDIR = 8;
        
        /// <summary>
        /// The events in
        /// mask
        /// shall be added to or removed from the ignore mask.
        /// Note that the flags
        /// FAN_ONDIR ,
        /// and
        /// FAN_EVENT_ON_CHILD
        /// have no effect when provided with this flag.
        /// The effect of setting the flags
        /// FAN_ONDIR ,
        /// and
        /// FAN_EVENT_ON_CHILD
        /// in the mark mask
        /// on the events that are set in the ignore mask
        /// is undefined and depends on the Linux kernel version.
        /// Specifically, prior to Linux 5.9,
        /// commit 497b0c5a7c0688c1b100a9c2e267337f677c198e
        /// setting a mark mask on a file
        /// and a mark with ignore mask on its parent directory
        /// would not result in ignoring events on the file,
        /// regardless of the
        /// FAN_EVENT_ON_CHILD
        /// flag in the parent directory's mark mask.
        /// When the ignore mask is updated with the
        /// FAN_MARK_IGNORED_MASK
        /// flag
        /// on a mark that was previously updated with the
        /// FAN_MARK_IGNORE
        /// flag,
        /// the update fails with
        /// EEXIST
        /// error.
        /// </summary>
        public const int FAN_MARK_IGNORED_MASK = 32;
        
        /// <summary>
        /// The ignore mask shall survive modify events.
        /// If this flag is not set,
        /// the ignore mask is cleared when a modify event occurs
        /// on the marked object.
        /// Omitting this flag is typically used to suppress events
        /// (e.g.,
        /// FAN_OPEN )
        /// for a specific file,
        /// until that specific file's content has been modified.
        /// It is far less useful to suppress events
        /// on an entire filesystem,
        /// or mount,
        /// or on all files inside a directory,
        /// until some file's content has been modified.
        /// For this reason,
        /// the
        /// FAN_MARK_IGNORE
        /// flag requires the
        /// FAN_MARK_IGNORED_SURV_MODIFY
        /// flag on a mount,
        /// filesystem,
        /// or directory inode mark.
        /// This flag cannot be removed from a mark once set.
        /// When the ignore mask is updated without this flag
        /// on a mark that was previously updated with the
        /// FAN_MARK_IGNORE
        /// and
        /// FAN_MARK_IGNORED_SURV_MODIFY
        /// flags,
        /// the update fails with
        /// EEXIST
        /// error.
        /// </summary>
        public const int FAN_MARK_IGNORED_SURV_MODIFY = 64;
        
        /// <summary>
        /// Remove either all marks for filesystems, all marks for mounts, or all
        /// marks for directories and files from the fanotify group.
        /// If
        /// flags
        /// contains
        /// FAN_MARK_MOUNT ,
        /// all marks for mounts are removed from the group.
        /// If
        /// flags
        /// contains
        /// FAN_MARK_FILESYSTEM ,
        /// all marks for filesystems are removed from the group.
        /// Otherwise, all marks for directories and files are removed.
        /// No flag other than, and at most one of, the flags
        /// FAN_MARK_MOUNT
        /// or
        /// FAN_MARK_FILESYSTEM
        /// can be used in conjunction with
        /// FAN_MARK_FLUSH .
        /// mask
        /// is ignored.
        /// 
        /// If none of the values above is specified, or more than one is specified,
        /// the call fails with the error
        /// EINVAL .
        /// 
        /// In addition,
        /// zero or more of the following values may be ORed into
        /// flags :
        /// </summary>
        public const int FAN_MARK_FLUSH = 128;
        
        public const int FAN_MARK_INODE = 0;
        
        /// <summary>
        /// Mark the mount specified by
        /// pathname .
        /// If
        /// pathname
        /// is not itself a mount point, the mount containing
        /// pathname
        /// will be marked.
        /// All directories, subdirectories, and the contained files of the mount
        /// will be monitored.
        /// The events which require that filesystem objects are identified by file handles,
        /// such as
        /// FAN_CREATE ,
        /// FAN_ATTRIB ,
        /// FAN_MOVE ,
        /// and
        /// FAN_DELETE_SELF ,
        /// cannot be provided as a
        /// mask
        /// when
        /// flags
        /// contains
        /// FAN_MARK_MOUNT .
        /// Attempting to do so will result in the error
        /// EINVAL
        /// being returned.
        /// Use of this flag requires the
        /// CAP_SYS_ADMIN
        /// capability.
        /// </summary>
        public const int FAN_MARK_MOUNT = 16;
        
        public const int FAN_MARK_FILESYSTEM = 256;
        
        public const int FAN_MARK_TYPE_MASK = 272;
        
        public const int FAN_ALL_MARK_FLAGS = 255;
        
        public const int FAN_ALL_EVENTS = 59;
        
        public const int FAN_ALL_PERM_EVENTS = 196608;
        
        public const int FAN_ALL_OUTGOING_EVENTS = 213051;
        
        public const int FAN_EVENT_INFO_TYPE_FID = 1;
        
        public const int FAN_EVENT_INFO_TYPE_DFID_NAME = 2;
        
        public const int FAN_EVENT_INFO_TYPE_DFID = 3;
        
        public const int FAN_ALLOW = 1;
        
        public const int FAN_DENY = 2;
        
        public const int FAN_AUDIT = 16;
        
        public const int FAN_NOFD = -1;
        
        public const int FAN_EVENT_METADATA_LEN = 24;
        
        /// <summary>
        /// Create and initialize fanotify group
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "fanotify_init")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int fanotify_init(uint flags, uint event_f_flags);
        
        /// <summary>
        /// Add, remove, or modify an fanotify mark on a filesystem
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "fanotify_mark")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int fanotify_mark(int fanotify_fd, uint flags, ulong mask, int dirfd, byte* pathname);
        
        /// <summary>
        /// Add, remove, or modify an fanotify mark on a filesystem
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "fanotify_mark")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int fanotify_mark(int fanotify_fd, uint flags, ulong mask, int dirfd, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> pathname);
    }
}
