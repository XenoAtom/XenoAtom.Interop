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
        public const int MAP_FAILED = -1;
        
        /// <summary>
        /// Share this mapping.
        /// Updates to the mapping are visible to other processes mapping the same region,
        /// and (in the case of file-backed mappings)
        /// are carried through to the underlying file.
        /// (To precisely control when updates are carried through
        /// to the underlying file requires the use of
        /// msync (2).)
        /// </summary>
        public const int MAP_SHARED = 1;
        
        /// <summary>
        /// Create a private copy-on-write mapping.
        /// Updates to the mapping are not visible to other processes
        /// mapping the same file, and are not carried through to
        /// the underlying file.
        /// It is unspecified whether changes made to the file after the
        /// mmap ()
        /// call are visible in the mapped region.
        /// 
        /// Both
        /// MAP_SHARED
        /// and
        /// MAP_PRIVATE
        /// are described in POSIX.1-2001 and POSIX.1-2008.
        /// MAP_SHARED_VALIDATE
        /// is a Linux extension.
        /// 
        /// In addition, zero or more of the following values can be ORed in
        /// flags :
        /// </summary>
        public const int MAP_PRIVATE = 2;
        
        public const int MAP_SHARED_VALIDATE = 3;
        
        public const int MAP_TYPE = 15;
        
        /// <summary>
        /// Don't interpret
        /// addr
        /// as a hint: place the mapping at exactly that address.
        /// addr
        /// must be suitably aligned: for most architectures a multiple of the page
        /// size is sufficient; however, some architectures may impose additional
        /// restrictions.
        /// If the memory region specified by
        /// addr
        /// and
        /// length
        /// overlaps pages of any existing mapping(s), then the overlapped
        /// part of the existing mapping(s) will be discarded.
        /// If the specified address cannot be used,
        /// mmap ()
        /// will fail.
        /// 
        /// Software that aspires to be portable should use the
        /// MAP_FIXED
        /// flag with care,
        /// keeping in mind that the exact layout of a process's memory mappings
        /// is allowed to change significantly between Linux versions,
        /// C library versions, and operating system releases.
        /// Carefully read the discussion of this flag in NOTES!
        /// </summary>
        public const int MAP_FIXED = 16;
        
        /// <summary>
        /// Synonym for
        /// MAP_ANONYMOUS ;
        /// provided for compatibility with other implementations.
        /// </summary>
        public const int MAP_ANON = 32;
        
        /// <summary>
        /// The mapping is not backed by any file;
        /// its contents are initialized to zero.
        /// The
        /// fd
        /// argument is ignored;
        /// however, some implementations require
        /// fd
        /// to be \-1 if
        /// MAP_ANONYMOUS
        /// (or
        /// MAP_ANON )
        /// is specified,
        /// and portable applications should ensure this.
        /// The
        /// offset
        /// argument should be zero.
        /// See the pgoff overflow check in do_mmap().
        /// See the offset check in sys_mmap in arch/x86/kernel/sys_x86_64.c.
        /// Support for
        /// MAP_ANONYMOUS
        /// in conjunction with
        /// MAP_SHARED
        /// was added in Linux 2.4.
        /// </summary>
        public const int MAP_ANONYMOUS = 32;
        
        /// <summary>
        /// Do not reserve swap space for this mapping.
        /// When swap space is reserved, one has the guarantee
        /// that it is possible to modify the mapping.
        /// When swap space is not reserved one might get
        /// SIGSEGV
        /// upon a write
        /// if no physical memory is available.
        /// See also the discussion of the file
        /// /proc/sys/vm/overcommit_memory
        /// in
        /// proc (5).
        /// Before Linux 2.6, this flag had effect only for
        /// private writable mappings.
        /// </summary>
        public const int MAP_NORESERVE = 16384;
        
        /// <summary>
        /// This flag is used for stacks.
        /// It indicates to the kernel virtual memory system that the mapping
        /// should extend downward in memory.
        /// The return address is one page lower than the memory area that is
        /// actually created in the process's virtual address space.
        /// Touching an address in the "guard" page below the mapping will cause
        /// the mapping to grow by a page.
        /// This growth can be repeated until the mapping grows to within a
        /// page of the high end of the next lower mapping,
        /// at which point touching the "guard" page will result in a
        /// SIGSEGV
        /// signal.
        /// </summary>
        public const int MAP_GROWSDOWN = 256;
        
        /// <summary>
        /// This flag is ignored.
        /// Introduced in 1.1.36, removed in 1.3.24.
        /// (Long ago\[em]Linux 2.0 and earlier\[em]it signaled
        /// that attempts to write to the underlying file should fail with
        /// ETXTBSY .
        /// But this was a source of denial-of-service attacks.)
        /// </summary>
        public const int MAP_DENYWRITE = 2048;
        
        /// <summary>
        /// This flag is ignored.
        /// Introduced in 1.1.38, removed in 1.3.24. Flag tested in proc_follow_link.
        /// (Long ago, it signaled that the underlying file is an executable.
        /// However, that information was not really used anywhere.)
        /// Linus talked about DOS related to MAP_EXECUTABLE, but he was thinking of
        /// MAP_DENYWRITE?
        /// </summary>
        public const int MAP_EXECUTABLE = 4096;
        
        public const int MAP_LOCKED = 8192;
        
        public const int MAP_POPULATE = 32768;
        
        public const int MAP_NONBLOCK = 65536;
        
        public const int MAP_STACK = 131072;
        
        public const int MAP_HUGETLB = 262144;
        
        public const int MAP_SYNC = 524288;
        
        public const int MAP_FIXED_NOREPLACE = 1048576;
        
        /// <summary>
        /// Compatibility flag.
        /// Ignored.
        /// On some systems, this was required as the opposite of
        /// MAP_ANONYMOUS -- mtk, 1 May 2007
        /// </summary>
        public const int MAP_FILE = 0;
        
        public const int MAP_HUGE_SHIFT = 26;
        
        public const int MAP_HUGE_MASK = 63;
        
        public const int MAP_HUGE_16KB = 939524096;
        
        public const int MAP_HUGE_64KB = 1073741824;
        
        public const int MAP_HUGE_512KB = 1275068416;
        
        public const int MAP_HUGE_1MB = 1342177280;
        
        /// <summary>
        /// MAP_HUGE_1GB " (since Linux 3.8)"
        /// See https://lwn.net/Articles/533499/
        /// Used in conjunction with
        /// MAP_HUGETLB
        /// to select alternative hugetlb page sizes (respectively, 2\ MB and 1\ GB)
        /// on systems that support multiple hugetlb page sizes.
        /// 
        /// More generally, the desired huge page size can be configured by encoding
        /// the base-2 logarithm of the desired page size in the six bits at the offset
        /// MAP_HUGE_SHIFT .
        /// (A value of zero in this bit field provides the default huge page size;
        /// the default huge page size can be discovered via the
        /// Hugepagesize
        /// field exposed by
        /// /proc/meminfo .)
        /// Thus, the above two constants are defined as:
        /// 
        /// +4n
        /// 
        /// #define MAP_HUGE_2MB    (21 &lt;&lt; MAP_HUGE_SHIFT)
        /// #define MAP_HUGE_1GB    (30 &lt;&lt; MAP_HUGE_SHIFT)
        /// 
        /// 
        /// 
        /// The range of huge page sizes that are supported by the system
        /// can be discovered by listing the subdirectories in
        /// /sys/kernel/mm/hugepages .
        /// </summary>
        public const int MAP_HUGE_2MB = 1409286144;
        
        public const int MAP_HUGE_8MB = 1543503872;
        
        public const int MAP_HUGE_16MB = 1610612736;
        
        public const int MAP_HUGE_32MB = 1677721600;
        
        public const int MAP_HUGE_256MB = 1879048192;
        
        public const int MAP_HUGE_512MB = 1946157056;
        
        public const int MAP_HUGE_1GB = 2013265920;
        
        public const int MAP_HUGE_2GB = 2080374784;
        
        public const int MAP_HUGE_16GB = -2013265920;
        
        /// <summary>
        /// Pages may not be accessed.
        /// </summary>
        public const int PROT_NONE = 0;
        
        /// <summary>
        /// Pages may be read.
        /// </summary>
        public const int PROT_READ = 1;
        
        /// <summary>
        /// Pages may be written.
        /// </summary>
        public const int PROT_WRITE = 2;
        
        /// <summary>
        /// Pages may be executed.
        /// </summary>
        public const int PROT_EXEC = 4;
        
        /// <summary>
        /// Apply the protection mode down to the beginning of a mapping
        /// that grows downward
        /// (which should be a stack segment or a segment mapped with the
        /// MAP_GROWSDOWN
        /// flag set).
        /// 
        /// Like
        /// mprotect (),
        /// pkey_mprotect ()
        /// changes the protection on the pages specified by
        /// addr
        /// and
        /// len .
        /// The
        /// pkey
        /// argument specifies the protection key (see
        /// pkeys (7))
        /// to assign to the memory.
        /// The protection key must be allocated with
        /// pkey_alloc (2)
        /// before it is passed to
        /// pkey_mprotect ().
        /// For an example of the use of this system call, see
        /// pkeys (7).
        /// </summary>
        public const int PROT_GROWSDOWN = 16777216;
        
        public const int PROT_GROWSUP = 33554432;
        
        /// <summary>
        /// Specifies that an update be scheduled, but the call returns immediately.
        /// </summary>
        public const int MS_ASYNC = 1;
        
        /// <summary>
        /// Since Linux 2.4, this seems to be a no-op (other than the
        /// EBUSY check for VM_LOCKED).
        /// Asks to invalidate other mappings of the same file
        /// (so that they can be updated with the fresh values just written).
        /// </summary>
        public const int MS_INVALIDATE = 2;
        
        /// <summary>
        /// Requests an update and waits for it to complete.
        /// </summary>
        public const int MS_SYNC = 4;
        
        /// <summary>
        /// Lock all pages which are currently mapped into the address space of
        /// the process.
        /// </summary>
        public const int MCL_CURRENT = 1;
        
        /// <summary>
        /// Lock all pages which will become mapped into the address space of the
        /// process in the future.
        /// These could be, for instance, new pages required
        /// by a growing heap and stack as well as new memory-mapped files or
        /// shared memory regions.
        /// </summary>
        public const int MCL_FUTURE = 2;
        
        public const int MCL_ONFAULT = 4;
        
        /// <summary>
        /// The application has no special advice regarding its memory usage patterns
        /// for the specified address range.
        /// This is the default behavior.
        /// </summary>
        public const int POSIX_MADV_NORMAL = 0;
        
        /// <summary>
        /// The application expects to access the specified address range randomly.
        /// Thus, read ahead may be less useful than normally.
        /// </summary>
        public const int POSIX_MADV_RANDOM = 1;
        
        /// <summary>
        /// The application expects to access the specified address range sequentially,
        /// running from lower addresses to higher addresses.
        /// Hence, pages in this region can be aggressively read ahead,
        /// and may be freed soon after they are accessed.
        /// </summary>
        public const int POSIX_MADV_SEQUENTIAL = 2;
        
        /// <summary>
        /// The application expects to access the specified address range
        /// in the near future.
        /// Thus, read ahead may be beneficial.
        /// </summary>
        public const int POSIX_MADV_WILLNEED = 3;
        
        /// <summary>
        /// The application expects that it will not access the specified address range
        /// in the near future.
        /// </summary>
        public const int POSIX_MADV_DONTNEED = 4;
        
        /// <summary>
        /// No special treatment.
        /// This is the default.
        /// </summary>
        public const int MADV_NORMAL = 0;
        
        /// <summary>
        /// Expect page references in random order.
        /// (Hence, read ahead may be less useful than normally.)
        /// </summary>
        public const int MADV_RANDOM = 1;
        
        /// <summary>
        /// Expect page references in sequential order.
        /// (Hence, pages in the given range can be aggressively read ahead,
        /// and may be freed soon after they are accessed.)
        /// </summary>
        public const int MADV_SEQUENTIAL = 2;
        
        /// <summary>
        /// Expect access in the near future.
        /// (Hence, it might be a good idea to read some pages ahead.)
        /// </summary>
        public const int MADV_WILLNEED = 3;
        
        /// <summary>
        /// Do not expect access in the near future.
        /// (For the time being, the application is finished with the given range,
        /// so the kernel can free resources associated with it.)
        /// 
        /// After a successful
        /// MADV_DONTNEED
        /// operation,
        /// the semantics of memory access in the specified region are changed:
        /// subsequent accesses of pages in the range will succeed, but will result
        /// in either repopulating the memory contents from the
        /// up-to-date contents of the underlying mapped file
        /// (for shared file mappings, shared anonymous mappings,
        /// and shmem-based techniques such as System V shared memory segments)
        /// or zero-fill-on-demand pages for anonymous private mappings.
        /// 
        /// Note that, when applied to shared mappings,
        /// MADV_DONTNEED
        /// might not lead to immediate freeing of the pages in the range.
        /// The kernel is free to delay freeing the pages until an appropriate moment.
        /// The resident set size (RSS) of the calling process will be immediately
        /// reduced however.
        /// 
        /// MADV_DONTNEED
        /// cannot be applied to locked pages, or
        /// VM_PFNMAP
        /// pages.
        /// (Pages marked with the kernel-internal
        /// VM_PFNMAP
        /// http://lwn.net/Articles/162860/
        /// flag are special memory areas that are not managed
        /// by the virtual memory subsystem.
        /// Such pages are typically created by device drivers that
        /// map the pages into user space.)
        /// 
        /// Support for Huge TLB pages was added in Linux v5.18.
        /// Addresses within a mapping backed by Huge TLB pages must be aligned
        /// to the underlying Huge TLB page size,
        /// and the range length is rounded up
        /// to a multiple of the underlying Huge TLB page size.
        /// 
        /// ======================================================================
        /// </summary>
        public const int MADV_DONTNEED = 4;
        
        public const int MADV_FREE = 8;
        
        public const int MADV_REMOVE = 9;
        
        public const int MADV_DONTFORK = 10;
        
        public const int MADV_DOFORK = 11;
        
        public const int MADV_MERGEABLE = 12;
        
        public const int MADV_UNMERGEABLE = 13;
        
        public const int MADV_HUGEPAGE = 14;
        
        public const int MADV_NOHUGEPAGE = 15;
        
        public const int MADV_DONTDUMP = 16;
        
        public const int MADV_DODUMP = 17;
        
        public const int MADV_WIPEONFORK = 18;
        
        public const int MADV_KEEPONFORK = 19;
        
        /// <summary>
        /// See
        /// madvise (2).
        /// </summary>
        public const int MADV_COLD = 20;
        
        /// <summary>
        /// See
        /// madvise (2).
        /// </summary>
        public const int MADV_PAGEOUT = 21;
        
        public const int MADV_HWPOISON = 100;
        
        public const int MADV_SOFT_OFFLINE = 101;
        
        /// <summary>
        /// By default, if there is not sufficient space to expand a mapping
        /// at its current location, then
        /// mremap ()
        /// fails.
        /// If this flag is specified, then the kernel is permitted to
        /// relocate the mapping to a new virtual address, if necessary.
        /// If the mapping is relocated,
        /// then absolute pointers into the old mapping location
        /// become invalid (offsets relative to the starting address of
        /// the mapping should be employed).
        /// </summary>
        public const int MREMAP_MAYMOVE = 1;
        
        public const int MREMAP_FIXED = 2;
        
        public const int MREMAP_DONTUNMAP = 4;
        
        /// <summary>
        /// Lock pages that are currently resident and mark the entire range so
        /// that the remaining nonresident pages are locked when they are populated
        /// by a page fault.
        /// 
        /// If
        /// flags
        /// is 0,
        /// mlock2 ()
        /// behaves exactly the same as
        /// mlock ().
        /// 
        /// munlock ()
        /// unlocks pages in the address range starting at
        /// addr
        /// and continuing for
        /// len
        /// bytes.
        /// After this call, all pages that contain a part of the specified
        /// memory range can be moved to external swap space again by the kernel.
        /// </summary>
        public const int MLOCK_ONFAULT = 1;
        
        /// <summary>
        /// Set the close-on-exec
        /// ( FD_CLOEXEC )
        /// flag on the new file descriptor.
        /// See the description of the
        /// O_CLOEXEC
        /// flag in
        /// open (2)
        /// for reasons why this may be useful.
        /// </summary>
        public const int MFD_CLOEXEC = 1;
        
        /// <summary>
        /// Allow sealing operations on this file.
        /// See the discussion of the
        /// F_ADD_SEALS
        /// and
        /// F_GET_SEALS
        /// operations in
        /// fcntl (2),
        /// and also NOTES, below.
        /// The initial set of seals is empty.
        /// If this flag is not set, the initial set of seals will be
        /// F_SEAL_SEAL ,
        /// meaning that no other seals can be set on the file.
        /// FIXME Why is the MFD_ALLOW_SEALING behavior not simply the default?
        /// Is it worth adding some text explaining this?
        /// </summary>
        public const int MFD_ALLOW_SEALING = 2;
        
        public const int MFD_HUGETLB = 4;
        
        /// <summary>
        /// Map or unmap files or devices into memory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mmap")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void* mmap(void* addr, nuint length, int prot, int flags, int fd, musl.off_t offset);
        
        /// <summary>
        /// Map or unmap files or devices into memory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "munmap")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int munmap(void* addr, nuint length);
        
        /// <summary>
        /// Set protection on a region of memory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mprotect")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mprotect(void* addr, nuint len, int prot);
        
        /// <summary>
        /// Synchronize a file with a memory map
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "msync")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int msync(void* addr, nuint length, int flags);
        
        /// <summary>
        /// Give advice about patterns of memory usage
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "posix_madvise")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int posix_madvise(void* addr, nuint len, int advice);
        
        /// <summary>
        /// Lock and unlock memory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mlock")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mlock(void* addr, nuint len);
        
        /// <summary>
        /// Lock and unlock memory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "munlock")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int munlock(void* addr, nuint len);
        
        /// <summary>
        /// Lock and unlock memory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mlockall")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mlockall(int flags);
        
        /// <summary>
        /// Lock and unlock memory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "munlockall")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int munlockall();
        
        /// <summary>
        /// Remap a virtual memory address
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mremap")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void* mremap(void* old_address, nuint old_size, nuint new_size, int flags);
        
        /// <summary>
        /// Create a nonlinear file mapping
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "remap_file_pages")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int remap_file_pages(void* addr, nuint size, int prot, nuint pgoff, int flags);
        
        /// <summary>
        /// Create an anonymous file
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "memfd_create")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int memfd_create(byte* name, uint flags);
        
        /// <summary>
        /// Create an anonymous file
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "memfd_create")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int memfd_create([global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> name, uint flags);
        
        /// <summary>
        /// Lock and unlock memory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mlock2")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mlock2(void* addr, nuint len, uint flags);
        
        /// <summary>
        /// Give advice about use of memory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "madvise")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int madvise(void* addr, nuint length, int advice);
        
        /// <summary>
        /// Determine whether pages are resident in memory
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mincore")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int mincore(void* addr, nuint length, byte* vec);
        
        /// <summary>
        /// Create/open or unlink POSIX shared memory objects
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "shm_open")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int shm_open(byte* name, int oflag, musl.mode_t mode);
        
        /// <summary>
        /// Create/open or unlink POSIX shared memory objects
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "shm_open")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int shm_open([global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> name, int oflag, musl.mode_t mode);
        
        /// <summary>
        /// Create/open or unlink POSIX shared memory objects
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "shm_unlink")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int shm_unlink(byte* name);
        
        /// <summary>
        /// Create/open or unlink POSIX shared memory objects
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "shm_unlink")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int shm_unlink([global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> name);
    }
}
