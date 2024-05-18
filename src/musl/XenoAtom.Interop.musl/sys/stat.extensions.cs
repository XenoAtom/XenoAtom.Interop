// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class musl
{
    /// <summary>
    /// Stat structure (cross-platform without padding)
    /// </summary>
    public struct stat_t
    {
        public musl.dev_t st_dev;

        public musl.ino_t st_ino;

        public musl.nlink_t st_nlink;

        public musl.mode_t st_mode;

        public musl.uid_t st_uid;

        public musl.gid_t st_gid;

        public musl.dev_t st_rdev;

        public musl.off_t st_size;

        public musl.blksize_t st_blksize;

        public musl.blkcnt_t st_blocks;

        public musl.timespec st_atim;

        public musl.timespec st_mtim;

        public musl.timespec st_ctim;
    }

    /// <summary>
    /// Get file status
    /// </summary>
    public static int fstat(int fd, ref musl.stat_t statbuf)
    {
        switch (RuntimeInformation.OSArchitecture)
        {
            case Architecture.X64:
            {
                musl.x86_64.stat_t localStat = default;
                var result = musl.x86_64.fstat(fd, ref localStat);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            case Architecture.Arm64:
            {
                musl.aarch64.stat_t localStat = default;
                var result = musl.aarch64.fstat(fd, ref localStat);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            default:
                throw new PlatformNotSupportedException();
        }
    }

    /// <summary>
    /// Get file status
    /// </summary>
    public static int fstatat(int dirfd, byte* pathname, ref musl.stat_t statbuf, int flags)
    {
        switch (RuntimeInformation.OSArchitecture)
        {
            case Architecture.X64:
            {
                musl.x86_64.stat_t localStat = default;
                var result = musl.x86_64.fstatat(dirfd, pathname, ref localStat, flags);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            case Architecture.Arm64:
            {
                musl.aarch64.stat_t localStat = default;
                var result = musl.aarch64.fstatat(dirfd, pathname, ref localStat, flags);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            default:
                throw new PlatformNotSupportedException();
        }

    }

    /// <summary>
    /// Get file status
    /// </summary>
    public static int fstatat(int dirfd, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> pathname, ref musl.stat_t statbuf, int flags)
    {
        switch (RuntimeInformation.OSArchitecture)
        {
            case Architecture.X64:
            {
                musl.x86_64.stat_t localStat = default;
                var result = musl.x86_64.fstatat(dirfd, pathname, ref localStat, flags);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            case Architecture.Arm64:
            {
                musl.aarch64.stat_t localStat = default;
                var result = musl.aarch64.fstatat(dirfd, pathname, ref localStat, flags);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            default:
                throw new PlatformNotSupportedException();
        }
    }

    /// <summary>
    /// Get file status
    /// </summary>
    public static int lstat(byte* pathname, ref musl.stat_t statbuf)
    {
        switch (RuntimeInformation.OSArchitecture)
        {
            case Architecture.X64:
            {
                musl.x86_64.stat_t localStat = default;
                var result = musl.x86_64.lstat(pathname, ref localStat);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            case Architecture.Arm64:
            {
                musl.aarch64.stat_t localStat = default;
                var result = musl.aarch64.lstat(pathname, ref localStat);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            default:
                throw new PlatformNotSupportedException();
        }
    }

    /// <summary>
    /// Get file status
    /// </summary>
    public static int lstat([global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> pathname, ref musl.stat_t statbuf)
    {
        switch (RuntimeInformation.OSArchitecture)
        {
            case Architecture.X64:
            {
                musl.x86_64.stat_t localStat = default;
                var result = musl.x86_64.lstat(pathname, ref localStat);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            case Architecture.Arm64:
            {
                musl.aarch64.stat_t localStat = default;
                var result = musl.aarch64.lstat(pathname, ref localStat);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            default:
                throw new PlatformNotSupportedException();
        }
    }

    /// <summary>
    /// Get file status
    /// </summary>
    public static int stat(byte* pathname, ref musl.stat_t statbuf)
    {
        switch (RuntimeInformation.OSArchitecture)
        {
            case Architecture.X64:
            {
                musl.x86_64.stat_t localStat = default;
                var result = musl.x86_64.stat(pathname, ref localStat);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            case Architecture.Arm64:
            {
                musl.aarch64.stat_t localStat = default;
                var result = musl.aarch64.stat(pathname, ref localStat);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            default:
                throw new PlatformNotSupportedException();
        }
    }

    /// <summary>
    /// Get file status
    /// </summary>
    public static int stat([global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> pathname, ref musl.stat_t statbuf)
    {
        switch (RuntimeInformation.OSArchitecture)
        {
            case Architecture.X64:
            {
                musl.x86_64.stat_t localStat = default;
                var result = musl.x86_64.stat(pathname, ref localStat);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            case Architecture.Arm64:
            {
                musl.aarch64.stat_t localStat = default;
                var result = musl.aarch64.stat(pathname, ref localStat);
                CopyStat(localStat, ref statbuf);
                return result;
            }
            default:
                throw new PlatformNotSupportedException();
        }
    }
    private static void CopyStat(in musl.x86_64.stat_t localStat, ref musl.stat_t statbuf)
    {
        statbuf.st_dev = localStat.st_dev;
        statbuf.st_ino = localStat.st_ino;
        statbuf.st_nlink = localStat.st_nlink;
        statbuf.st_mode = localStat.st_mode;
        statbuf.st_uid = localStat.st_uid;
        statbuf.st_gid = localStat.st_gid;
        statbuf.st_rdev = localStat.st_rdev;
        statbuf.st_size = localStat.st_size;
        statbuf.st_blksize = localStat.st_blksize;
        statbuf.st_blocks = localStat.st_blocks;
        statbuf.st_atim = localStat.st_atim;
        statbuf.st_mtim = localStat.st_mtim;
        statbuf.st_ctim = localStat.st_ctim;
    }

    private static void CopyStat(in musl.aarch64.stat_t localStat, ref musl.stat_t statbuf)
    {
        statbuf.st_dev = localStat.st_dev;
        statbuf.st_ino = localStat.st_ino;
        statbuf.st_nlink = localStat.st_nlink.Value;
        statbuf.st_mode = localStat.st_mode;
        statbuf.st_uid = localStat.st_uid;
        statbuf.st_gid = localStat.st_gid;
        statbuf.st_rdev = localStat.st_rdev;
        statbuf.st_size = localStat.st_size;
        statbuf.st_blksize = localStat.st_blksize.Value;
        statbuf.st_blocks = localStat.st_blocks;
        statbuf.st_atim = localStat.st_atim;
        statbuf.st_mtim = localStat.st_mtim;
        statbuf.st_ctim = localStat.st_ctim;
    }
}