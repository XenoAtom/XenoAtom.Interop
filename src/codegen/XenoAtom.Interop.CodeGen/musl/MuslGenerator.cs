// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CppAst;
using CppAst.CodeGen.CSharp;
using Zio;

namespace XenoAtom.Interop.CodeGen.musl;

internal partial class MuslGenerator : GeneratorBase
{
    public const string DefaultNamespace = "XenoAtom.Interop";

    public const string DefaultClassLib = "musl";

    private readonly Dictionary<UPath, CSharpClass> _mapArchFileToClass = new Dictionary<UPath, CSharpClass>();

    private readonly Dictionary<string, ManFunction> _mapFunctionToSummary = new();
    private readonly Dictionary<string, List<string>> _symbols = new();
    private readonly Dictionary<string, List<string>> _functionArguments = new();

    private readonly HashSet<string> KnownVariadicFunctions = new HashSet<string>()
    {
        // discarded
        "clone",
        "execl",
        "execle",
        "execlp",
        // overloads added
        "fcntl",
        "ioctl",
        "makecontext",
        "mq_open",
        "mremap",
        "open",
        "openat",
        "prctl",
        "ptrace",
        "semctl",
        "syscall",
        "syslog",
    };

    public MuslGenerator(LibDescriptor descriptor) : base(descriptor)
    {
    }

    protected override async Task<CSharpCompilation?> Generate()
    {
        await Apk.EnsureManPages();

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Loading Man Pages...");
        Console.WriteLine("----------------------------------------");
        LoadManPages();

        var functionWithArgumentsBuilder = new StringBuilder();
        functionWithArgumentsBuilder.AppendLine("""
                                                using System.Collections.Generic;
                                                
                                                namespace XenoAtom.Interop.CodeGen.musl;
                                                
                                                internal partial class MuslGenerator
                                                {
                                                    private static readonly Dictionary<string, string[]> MapLibCFunctionToArgumentNames = new()
                                                    {
                                                """);
        foreach (var pair in _functionArguments.OrderBy(x => x.Key, StringComparer.Ordinal))
        {
            functionWithArgumentsBuilder.AppendLine($"        {{ \"{pair.Key}\", [ {string.Join(", ", pair.Value.Select(x => $"\"{x}\""))} ] }},");
        }
        functionWithArgumentsBuilder.AppendLine("""
                                                    };
                                                }
                                                """);
        Console.WriteLine("Writing MuslGenerator.MapLibCFunctionToArgumentNames.cs");
        File.WriteAllText("MuslGenerator.MapLibCFunctionToArgumentNames.cs", functionWithArgumentsBuilder.ToString(), Encoding.UTF8);
        
        var mapArchToCompilation = new Dictionary<string, CSharpCompilation>();

        var allExpectedSysFunctions = new HashSet<string>(ListSysFunctions);

        foreach (var arch in Apk.Architectures)
        {
            var mainFolder = Apk.GetIncludeDirectory(arch, "main");
            var sysIncludes = Apk.GetSysIncludeDirectory(arch, "main");

            var csOptions = new CSharpConverterOptions()
            {
                DefaultClassLib = DefaultClassLib,
                DefaultNamespace = DefaultNamespace,
                DefaultOutputFilePath = "/musl_library.generated.cs",
                DefaultDllImportNameAndArguments = "LibraryName",
                TargetVendor = "pc",
                TargetSystem = "linux",
                Defines =
                {
                    // Defines for musl to override _Addr, _Int64, _Reg in bits/alltypes.h
                    // See ApkIncludeHelper.ExtractFiles
                    "XENO_ATOM_INTEROP",
                },
                PreHeaderText = @"",

                AdditionalArguments =
                {
                    "-nostdinc",
                },
                
                IncludeFolders =
                {
                    sysIncludes,
                    mainFolder
                },

                DispatchOutputPerInclude = true,
                DisableRuntimeMarshalling = true,
                AllowMarshalForString = false,

                // For musl, on Linux, we will simplify C long to map to a nint in C#
                MapCLongToIntPtr = true,

                MappingRules =
                {
                    e => e.Map<CppEnum>("EPOLL_EVENTS").Discard(),

                    // Discard these functions that are not supported (clone) or have more suitable equivalent (exec)
                    e => e.Map<CppFunction>("(clone|execl|execle|execlp)").Discard(),

                    // cachectl.h
                    e => e.MapMacroToConst("ICACHE", "int"),
                    e => e.MapMacroToConst("DCACHE", "int"),
                    e => e.MapMacroToConst("BCACHE", "int"),
                    e => e.MapMacroToConst("CACHEABLE", "int"),
                    e => e.MapMacroToConst("UNCACHEABLE", "int"),
                    e => e.MapMacroToConst("N_.*", "int"),
                    // errno.h
                    e => e.MapMacroToConst("E[A-Z]+", "int"),
                    // sysexits.h
                    e => e.MapMacroToConst("EX_.*", "int"),
                    // sys/eventfd.h
                    e => e.MapMacroToConst("EFD_.*", "int"),
                    // sys/fanotify.h
                    e => e.MapMacroToConst("FAN_.*", "int"),
                    // fcntl.h
                    e => e.MapMacroToConst("O_.*", "int"),
                    e => e.MapMacroToConst("F_.*", "int"),
                    e => e.MapMacroToConst("AT_.*", "int"),
                    e => e.MapMacroToConst("POSIX_FADV_.*", "int"),
                    e => e.MapMacroToConst("SEEK_.*", "int"),
                    e => e.MapMacroToConst("S_.*", "int"),
                    e => e.MapMacroToConst("R_.*", "int"),
                    e => e.MapMacroToConst("W_.*", "int"),
                    e => e.MapMacroToConst("X_.*", "int"),
                    e => e.MapMacroToConst("RWF_.*", "int"),
                    e => e.MapMacroToConst("RWH_.*", "int"),
                    e => e.MapMacroToConst("DN_.*", "int"),
                    e => e.Map<CppClass>("flock").Name("flock_t"),
                    // sys/file.h
                    e => e.MapMacroToConst("LOCK_.*", "int"),
                    e => e.MapMacroToConst("L_.*", "int"),
                    // sys/inotify.h
                    e => e.MapMacroToConst("IN_.*", "int"),
                    // sys/ipc.h
                    e => e.MapMacroToConst("IPC_PRIVATE", "int", overrideValue: "0"),
                    e => e.MapMacroToConst("IPC_(?!PRIVATE).*", "int"),
                    // sys/ioctl.h
                    e => e.MapMacroToConst("TC.*", "int"),
                    e => e.MapMacroToConst("TIO.*", "int"),
                    e => e.MapMacroToConst("FIO.*", "int"),
                    e => e.MapMacroToConst("SIO.*", "int"),
                    // sys/membarrier.h
                    e => e.MapMacroToConst("MEMBARRIER_CMD_.*", "int"),
                    // sys/mman.h
                    e => e.MapMacroToConst("MAP_FAILED", "int", overrideValue: "-1"),
                    e => e.MapMacroToConst("MAP_(?!FAILED).*", "int"),
                    e => e.MapMacroToConst("PROT_.*", "int"),
                    e => e.MapMacroToConst("MS_.*", "int"),
                    e => e.MapMacroToConst("MCL_.*", "int"),
                    e => e.MapMacroToConst("POSIX_MADV_.*", "int"),
                    e => e.MapMacroToConst("MADV_.*", "int"),
                    e => e.MapMacroToConst("MREMAP_.*", "int"),
                    e => e.MapMacroToConst("MLOCK_.*", "int"),
                    e => e.MapMacroToConst("MFD_.*", "int"),
                    // sys/mount.h
                    e => e.MapMacroToConst("BLK.*", "int"),
                    // sys/msg.h
                    e => e.MapMacroToConst("MSG_.*", "int"),
                    // sys/mtio.h
                    e => e.MapMacroToConst("MT(?!_TAPE_INFO|IOCTOP|IOCGET|IOCPOS|IOCGETCONFIG|IOCSETCONFIG).*", "int"),
                    // sys/personality.h
                    e => e.MapMacroToConst("PER_.*", "unsigned int"),
                    // poll.h
                    e => e.MapMacroToConst("POLL.*", "int"),
                    // sys/prctl.h
                    // PR_SET_PTRACER_ANY
                    e => e.MapMacroToConst("PR_SET_PTRACER_ANY", "int", overrideValue: "-1"),
                    e => e.MapMacroToConst("PR_(?!SET_PTRACER_ANY).*", "int"),
                    e => e.MapMacroToConst("SYSCALL_.*", "int"),
                    // sys/ptrace.h
                    e => e.MapMacroToConst("PTRACE_.*", "int"),
                    e => e.MapMacroToConst("PT_.*", "int"),
                    // sys/quota.h
                    e => e.MapMacroToConst("Q_.*", "int"),
                    e => e.MapMacroToConst("QFMT_.*", "int"),
                    e => e.MapMacroToConst("QIF_.*", "int"),
                    e => e.MapMacroToConst("SUBCMD.*", "int"),
                    e => e.MapMacroToConst("NR_.*", "int"),
                    e => e.MapMacroToConst("QUOTA.*", "char*"),
                    e => e.MapMacroToConst("MAXQUOTAS", "int"),
                    e => e.MapMacroToConst("USRQUOTA", "int"),
                    e => e.MapMacroToConst("GRPQUOTA", "int"),
                    e => e.MapMacroToConst("MAX_IQ_TIME", "int"),
                    e => e.MapMacroToConst("MAX_DQ_TIME", "int"),
                    e => e.MapMacroToConst("IIF_.*", "int"),
                    // sys/random.h
                    e => e.MapMacroToConst("GRND_.*", "int"),
                    // sys/reboot.h
                    e => e.MapMacroToConst("RB_.*", "int"),
                    // bits/reg.h
                    e => e.MapMacroToConst("(R15|R14|R13|R12|RBP|RBX|R11|R10|R9|R8|RAX|RCX|RDX|RSI|RDI|ORIG_RAX|RIP|CS|RSP|SS|FS_BASE|GS_BASE|DS|FS|GS)", "int"),
                    // sys/resource.h
                    e => e.MapMacroToConst("PRIO_.*", "int"),
                    e => e.MapMacroToConst("RUSAGE_.*", "int"),
                    e => e.MapMacroToConst("RLIM_INFINITY", "int", overrideValue: "-1"),
                    e => e.MapMacroToConst("RLIM_SAVED_CUR", "int", overrideValue: "-1"),
                    e => e.MapMacroToConst("RLIM_SAVED_MAX", "int", overrideValue: "-1"),
                    e => e.MapMacroToConst("RLIMIT_.*", "int"),
                    // sys/select.h
                    e => e.MapMacroToConst("FD_SETSIZE", "int"),
                    e => e.Map<CppField>("fd_set::fds_bits").Type("int", arraySize: 1024 / (sizeof(int) * 8)), // 1024 bits stored in ints. Make it portable instead of using nint/nuint
                    // sys/sem.h
                    e => e.MapMacroToConst("SEM_.*", "int"),
                    e => e.MapMacroToConst("GET.*", "int"),
                    e => e.MapMacroToConst("SET.*", "int"),
                    // sys/shm.h
                    e => e.MapMacroToConst("SHM_.*", "int"),
                    // signal.h
                    e => e.Map<CppClass>("sigaction").Name("sigaction_t"),
                    e => e.Map<CppClass>("sigaltstack").Name("sigaltstack_t"),
                    // sys/signalfd.h
                    e => e.MapMacroToConst("SFD_.*", "int"),
                    // sys/socket.h
                    e => e.MapMacroToConst("SHUT_.*", "int"),
                    e => e.MapMacroToConst("SOCK_.*", "int"),
                    e => e.MapMacroToConst("PF_.*", "int"),
                    e => e.MapMacroToConst("AF_.*", "int"),
                    e => e.MapMacroToConst("SO_.*", "int"),
                    e => e.MapMacroToConst("SCM_.*", "int"),
                    e => e.MapMacroToConst("SOL_.*", "int"),
                    e => e.MapMacroToConst("SOMAXCONN", "int"),
                    // sys/stat.h
                    e => e.Map<CppClass>("stat").Name("stat_t"),
                    e => e.Map<CppClass>("statx").Name("statx_t"),
                    e => e.MapMacroToConst("UTIME_.*", "int"),
                    // statfs.h
                    e => e.Map<CppClass>("statfs").Name("statfs_t"),
                    e => e.MapMacroToConst("ST_.*", "int"),
                    // statvfs.h
                    e => e.Map<CppClass>("statvfs").Name("statvfs_t").CppAction((converter, element) =>
                    {
                        var cppClass = (CppClass)element;
                        var bitField = cppClass.Fields.First(x => x.IsBitField);
                        cppClass.Fields.Remove(bitField);
                    }),
                    e => e.Map<CppClass>("acct").Name("acct_t"),
                    // stropts.h
                    e => e.MapMacroToConst("I_.*", "int"),
                    e => e.MapMacroToConst("FNAME.*", "int"),
                    e => e.MapMacroToConst("FLUSH.*", "int"),
                    e => e.MapMacroToConst("RS_.*", "int"),
                    e => e.MapMacroToConst("SND.*", "int"),
                    e => e.MapMacroToConst("RPRO.*", "int"),
                    e => e.MapMacroToConst("(RNORM|RMSGD|RMSGN|ANYMARK|LASTMARK|MUXID_ALL|MORECTL|MOREDATA)", "int"),
                    // sys/swap.h
                    e => e.MapMacroToConst("SWAP_.*", "int"),
                    // sys/syscall.h
                    e => e.MapMacroToConst("SYS_.*", "int"),
                    // sys/sysinfo.h
                    e => e.Map<CppClass>("sysinfo").Name("sysinfo_t"),
                    // syslog.h
                    e => e.MapMacroToConst("LOG_.*", "int"),
                    e => e.Map<CppFunction>("vsyslog").Discard(), // discard as it has a va_list
                    e => e.Map<CppTypedef>("va_list").Discard(), // discard va_list
                    e => e.Map<CppClass>("__va_list_tag").Discard(), // discard __va_list_tag
                    // termios.h
                    e => e.MapMacroToConst("NCCS", "int"),
                    // sys/time.h
                    e => e.MapMacroToConst("ITIMER_.*", "int"),
                    // sys/timerfd.h
                    e => e.MapMacroToConst("TFD_.*", "int"),
                    // sys/timex.h
                    e => e.MapMacroToConst("ADJ_.*", "int"),
                    e => e.MapMacroToConst("MOD_.*", "int"),
                    e => e.MapMacroToConst("STA_.*", "int"),
                    e => e.MapMacroToConst("TIME_.*", "int"),
                    e => e.MapMacroToConst("MAXTC", "int"),
                    // sys/ttydefaults.h
                    e => e.MapMacroToConst("TTYDEF_.*", "int"),
                    e => e.MapMacroToConst("(CEOF|CEOL|CSTATUS|CERASE|CINTR|CKILL|CMIN|CQUIT|CSUSP|CTIME|CDSUSP|CSTART|CSTOP|CLNEXT|CDISCARD|CWERASE|CREPRINT|CEOT|CBRK|CRPRNT|CFLUSH)", "int"),
                    // ucontext.h
                    e => e.MapMacroToConst("NGREG", "int"),
                    // sys/uio.h
                    e => e.MapMacroToConst("UIO_.*", "int"),
                    // sys/un.h
                    e => e.Map<CppFunction>("strlen").Discard(),
                    // sys/user.h
                    e => e.MapMacroToConst("ELF_.*", "int"),
                    e => e.MapMacroToConst("PAGE_MASK", "intptr_t"),
                    e => e.MapMacroToConst("NBPG", "int"),
                    e => e.MapMacroToConst("UPAGES", "int"),
                    // sys/vt.h
                    e => e.MapMacroToConst("VT_.*", "uint16_t"),
                    // sys/wait.h
                    e => e.MapMacroToConst("(WNOHANG|WUNTRACED|WSTOPPED|WEXITED|WCONTINUED|WNOWAIT|__WNOTHREAD|__WALL|__WCLONE)", "int"),
                    // sched.h
                    e => e.MapMacroToConst("SCHED_.*", "int"),
                    e => e.MapMacroToConst("CLONE_.*", "int"),
                    e => e.MapMacroToConst("CPU_SETSIZE", "int"),
                    // ----------------------------------------------------------------------------------
                    e => e.Map<CppFunction>(".*").CppAction(AssignArgumentNames),
                    e => e.Map<CppTypedef>("__kernel.*").Discard(),
                    e => e.Map<CppTypedef>("__kernel_old_uid_t").Discard(),
                    e => e.Map<CppTypedef>("__kernel_old_gid_t").Discard(),
                    e => e.Map<CppClass>("__kernel_fsid_t").Discard(),
                },
            };

            csOptions.TargetCpu = CppTargetCpu.X86_64;
            if (arch == "aarch64")
            {
                csOptions.MappingRules.Add(e => e.Map<CppField>("fpregset_t::vregs").Type("__u128", arraySize: 32));
                csOptions.MappingRules.Add(e => e.Map<CppField>("fpsimd_context::vregs").Type("__u128", arraySize: 32));
                csOptions.MappingRules.Add(e => e.Map<CppField>("user_fpsimd_struct::vregs").Type("__u128", arraySize: 32));
            }

            var files = new List<string>()
            {
                Path.Combine(sysIncludes, "unistd.h"),
                Path.Combine(sysIncludes, "errno.h"),
                Path.Combine(sysIncludes, "sysexits.h"),
                Path.Combine(sysIncludes, "sys", "acct.h"),
                //Path.Combine(sysIncludes, "sys", "auxv.h"),
                Path.Combine(sysIncludes, "sys", "cachectl.h"),
                Path.Combine(sysIncludes, "sys", "epoll.h"),
                Path.Combine(sysIncludes, "sys", "eventfd.h"),
                Path.Combine(sysIncludes, "sys", "fanotify.h"),
                Path.Combine(sysIncludes, "fcntl.h"),
                Path.Combine(sysIncludes, "sys", "file.h"),
                Path.Combine(sysIncludes, "sys", "fsuid.h"),
                Path.Combine(sysIncludes, "sys", "inotify.h"),
                Path.Combine(sysIncludes, "sys", "io.h"),
                Path.Combine(sysIncludes, "sys", "ioctl.h"),
                Path.Combine(sysIncludes, "sys", "ipc.h"),
                Path.Combine(sysIncludes, "sys", "kd.h"),
                Path.Combine(sysIncludes, "sys", "klog.h"),
                Path.Combine(sysIncludes, "sys", "membarrier.h"),
                Path.Combine(sysIncludes, "sys", "mman.h"),
                Path.Combine(sysIncludes, "sys", "mount.h"),
                Path.Combine(sysIncludes, "sys", "mount.h"),
                Path.Combine(sysIncludes, "mqueue.h"),
                Path.Combine(sysIncludes, "sys", "mtio.h"),
                Path.Combine(sysIncludes, "sys", "personality.h"),
                Path.Combine(sysIncludes, "poll.h"),
                Path.Combine(sysIncludes, "sys", "prctl.h"),
                Path.Combine(sysIncludes, "sys", "procfs.h"),
                Path.Combine(sysIncludes, "sys", "ptrace.h"),
                Path.Combine(sysIncludes, "sys", "quota.h"),
                Path.Combine(sysIncludes, "sys", "random.h"),
                Path.Combine(sysIncludes, "sys", "reboot.h"),
                Path.Combine(sysIncludes, "sys", "reg.h"),
                Path.Combine(sysIncludes, "sys", "resource.h"),
                Path.Combine(sysIncludes, "sys", "select.h"),
                Path.Combine(sysIncludes, "sys", "sem.h"),
                Path.Combine(sysIncludes, "sys", "sendfile.h"),
                Path.Combine(sysIncludes, "sys", "shm.h"),
                Path.Combine(sysIncludes, "signal.h"),
                Path.Combine(sysIncludes, "sys", "signalfd.h"),
                Path.Combine(sysIncludes, "sys", "socket.h"),
                //Path.Combine(sysIncludes, "sys", "soundcard.h"),
                Path.Combine(sysIncludes, "sys", "stat.h"),
                Path.Combine(sysIncludes, "sys", "statfs.h"),
                Path.Combine(sysIncludes, "sys", "statvfs.h"),
                Path.Combine(sysIncludes, "stropts.h"),
                Path.Combine(sysIncludes, "sys", "swap.h"),
                Path.Combine(sysIncludes, "sys", "syscall.h"),
                Path.Combine(sysIncludes, "sys", "sysinfo.h"),
                Path.Combine(sysIncludes, "syslog.h"),
                Path.Combine(sysIncludes, "termios.h"),
                Path.Combine(sysIncludes, "sys", "time.h"),
                Path.Combine(sysIncludes, "sys", "timeb.h"),
                Path.Combine(sysIncludes, "sys", "timerfd.h"),
                Path.Combine(sysIncludes, "sys", "times.h"),
                Path.Combine(sysIncludes, "sys", "timex.h"),
                Path.Combine(sysIncludes, "sys", "ttydefaults.h"),
                Path.Combine(sysIncludes, "ucontext.h"),
                Path.Combine(sysIncludes, "sys", "uio.h"),
                Path.Combine(sysIncludes, "sys", "un.h"),
                Path.Combine(sysIncludes, "sys", "user.h"),
                Path.Combine(sysIncludes, "utime.h"),
                Path.Combine(sysIncludes, "sys", "utsname.h"),
                Path.Combine(sysIncludes, "sys", "vfs.h"),
                Path.Combine(sysIncludes, "sys", "vt.h"),
                Path.Combine(sysIncludes, "sys", "wait.h"),
                Path.Combine(sysIncludes, "sys", "xattr.h"),
                // Missing files
                Path.Combine(sysIncludes, "sched.h"),
            };

            // Add default c types
            AddDefaultMuslAndKernelCTypes(csOptions);
            
            var csCompilation = CSharpConverter.Convert(files, csOptions);
            {
                foreach (var message in csCompilation.Diagnostics.Messages)
                {
                    Console.Error.WriteLine(message);
                }

                if (csCompilation.HasErrors)
                {
                    Console.Error.WriteLine("Unexpected parsing errors");
                    Environment.Exit(1);
                }
            }

            // Remove duplicates from unitstd.generated.cs
            var cs_unistd = csCompilation.GetLibClassFromGeneratedFile("/unistd.generated.cs");
            foreach (var element in cs_unistd.Members.OfType<ICSharpMember>().ToList())
            {
                switch (GetNativeMemberName(element))
                {
                    // macros to remove duplicate
                    case "SEEK_SET":
                    case "SEEK_CUR":
                    case "SEEK_END":
                    case "F_OK":
                    case "X_OK":
                    case "W_OK":
                    case "R_OK":
                    case "F_ULOCK":
                    case "F_LOCK":
                    case "F_TLOCK":
                    case "F_TEST":
                    case "L_SET":
                    case "L_INCR":
                    case "L_XTND":
                        cs_unistd.Members.Remove((CSharpElement)element);
                        break;
                }
            }

            // Remove duplicates from stropts.generated.cs
            var cs_stropts = csCompilation.GetLibClassFromGeneratedFile("/stropts.generated.cs");
            foreach (var element in cs_stropts.Members.OfType<ICSharpMember>().ToList())
            {
                switch (GetNativeMemberName(element))
                {
                    // Members to remove
                    case "ioctl":
                        cs_stropts.Members.Remove((CSharpElement)element);
                        break;
                }
            }

            var cppFunctions = csCompilation.AllFunctions.Select(x => x.CppElement).OfType<CppFunction>().ToHashSet();
            Console.WriteLine($"{cppFunctions.Count} functions generated for {arch}");

            foreach (var function in csCompilation.AllFunctions)
            {
                // Put the summary from man pages
                var functionName = GetNativeMemberName(function);
                var summary = GetFunctionSummaryFromManPage(functionName);
                if (summary != null)
                {
                    function.Comment = new CSharpFullComment()
                    {
                        Children =
                        {
                            new CSharpXmlComment("summary")
                            {
                                Children =
                                {
                                    new CSharpTextComment(summary)
                                }
                            }
                        }
                    };
                }

                // Remove any functions that we have generated from the expected list
                allExpectedSysFunctions.Remove(functionName);

                // acct function is doubled defined in acct.h and unitstd.h
                // So we remove it from acct.h
                var cppFunction = (CppFunction)function.CppElement!;
                if (cppFunction.Name == "acct" && Path.GetFileName(cppFunction.SourceFile) == "acct.h")
                {
                    ((CSharpClass)function.Parent!).Members.Remove(function);
                    continue;
                }

                if (cppFunction.Name == "lockf" && Path.GetFileName(cppFunction.SourceFile) == "fcntl.h")
                {
                    ((CSharpClass)function.Parent!).Members.Remove(function);
                    continue;
                }

                ProcessArguments(function);

                mapArchToCompilation[arch] = csCompilation;
            }


            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("Variadic functions:");
            Console.WriteLine("-------------------------------------------------------------------");
            foreach (var function in csCompilation.AllFunctions)
            {
                if ((((CppFunction)function.CppElement!).Flags & CppFunctionFlags.Variadic) != 0)
                {
                    Console.WriteLine($"Variadic function `{function.Name}`");
                }
            }
            Console.WriteLine();

            // Assign comments for other symbols
            foreach (var csClass in csCompilation.AllClasses)
            {
                foreach(var member in csClass.Members.Where(x => !(x is CSharpMethod)))
                {
                    var memberName = GetNativeMemberName((ICSharpMember)member);

                    string? symbolDescription = null;
                    if (!ListOfErrno.TryGetValue(memberName, out symbolDescription))
                    {
                        if (_symbols.TryGetValue(memberName, out var symbolDescriptions))
                        {
                            symbolDescription = symbolDescriptions[0];
                        }
                    }

                    if (symbolDescription != null)
                    {
                        if (member is ICSharpWithComment memberWithComment)
                        {
                            var comment = memberWithComment.Comment as CSharpFullComment;
                            if (comment == null)
                            {
                                comment = new CSharpFullComment();
                                memberWithComment.Comment = comment;
                            }

                            comment.Children.Add(new CSharpXmlComment("summary")
                            {
                                Children =
                                {
                                    new CSharpTextComment(symbolDescription) // Only take the first description
                                }
                            });
                        }
                    }
                }
            }
        }

        var sharedElements = new HashSet<CSharpElement>();
        var archElements = new Dictionary<string, HashSet<CSharpElement>>();

        var baseArchName = "x86_64";
        ProcessAndDispatchArchDependentElements(baseArchName, mapArchToCompilation, sharedElements, archElements);
        var baseCompilation = mapArchToCompilation[baseArchName];

        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($"{sharedElements.Count} items are shared between architectures");
        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine();
        foreach (var sharedElement in sharedElements.OrderBy(x => x.GetType().FullName).ThenBy(x => ((ICSharpMember)x).Name, StringComparer.Ordinal))
        {
            Console.WriteLine($"Shared {sharedElement.GetType().Name} `{((ICSharpMember)sharedElement).Name}`");
        }

        foreach (var archElement in archElements)
        {
            var archName = archElement.Key;
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine($"{archName} has {archElement.Value.Count} arch specific items");
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine();
            foreach (var element in archElement.Value.OrderBy(x => x.GetType().FullName).ThenBy(x => ((ICSharpMember)x).Name, StringComparer.Ordinal))
            {
                var fileContainer = GetContainer(element, out var ns);

                var newFilePath = fileContainer.FilePath.FullName.Replace(".cs", $".{archName}.cs");
                Console.WriteLine($"{archName} {element.GetType().Name} `{((ICSharpMember)element).Name}` in {newFilePath}");

                var newArchClass = GetLibClassFromGeneratedFile(baseCompilation, ns!, archName, newFilePath);
                ((CSharpClass)element.Parent!).Members.Remove(element);
                newArchClass.Members.Add(element);
            }
        }


        // If a class is empty in the final compilation, remove it
        foreach (var csClass in baseCompilation.AllClasses)
        {
            if (csClass.Members.Count == 0)
            {
                var fileContainer = GetContainer(csClass, out var ns);
                baseCompilation.Members.Remove(fileContainer);
            }
        }

        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($"{allExpectedSysFunctions.Count} functions not found");
        var sysInclude = Apk.GetSysIncludeDirectory("main");
        foreach (var expectedFunction in allExpectedSysFunctions.Order())
        {
            Console.WriteLine($"Linux Function `{expectedFunction}` was not found");

            var matchFunction = new Regex($@"\b{expectedFunction}\b");

            foreach (var file in Directory.EnumerateFiles(sysInclude, "*.h"))
            {
                var content = File.ReadAllText(file);
                if (matchFunction.Match(content).Success)
                {
                    Console.WriteLine($"    Found in {file}");
                }
            }
        }

        return baseCompilation;
    }

    protected override string? GetUrlDocumentationForCppFunction(CppFunction cppFunction)
    {
        if (_mapFunctionToSummary.TryGetValue(cppFunction.Name, out var manFunction))
        {
            return $"https://man7.org/linux/man-pages/man{manFunction.ManSection}/{manFunction.BaseFunctionName}.{manFunction.ManSection}.html";
        }

        return null;
    }
    
    private static string GetNativeMemberName(ICSharpMember member)
    {
        if (member is CSharpMethod method)
        {
            return ((CppFunction)method.CppElement!).Name;
        }
        return member.Name;
    }

    private CSharpClass GetLibClassFromGeneratedFile(CSharpCompilation compilation, CSharpNamespace sourceNs, string arch, UPath filePath)
    {
        CSharpClass? csArchClassLib;
        if (_mapArchFileToClass.TryGetValue(filePath, out csArchClassLib))
        {
            return csArchClassLib;
        }
        
        var csFile = new CSharpGeneratedFile(filePath);
        compilation.Members.Add(csFile);

        var csNamespace = new CSharpNamespace(DefaultNamespace);
        csFile.Members.Add(csNamespace);

        var csClassLib = new CSharpClass(DefaultClassLib);
        csClassLib.Modifiers |= CSharpModifiers.Partial | CSharpModifiers.Static | CSharpModifiers.Unsafe;
        CSharpConverter.ApplyDefaultVisibility(csClassLib, csNamespace, false);

        foreach (var usingDecl in sourceNs.Members.OfType<CSharpUsingDeclaration>())
        {
            csNamespace.Members.Add(new CSharpUsingDeclaration(usingDecl.Reference));
        }

        csNamespace.Members.Add(csClassLib);

        csArchClassLib = new CSharpClass(arch);
        csArchClassLib.Modifiers |= CSharpModifiers.Partial | CSharpModifiers.Static | CSharpModifiers.Unsafe;
        CSharpConverter.ApplyDefaultVisibility(csArchClassLib, csClassLib, false);
        csClassLib.Members.Add(csArchClassLib);

        // Register the file
        _mapArchFileToClass[filePath] = csArchClassLib;

        return csArchClassLib;
    }

    private static CSharpGeneratedFile GetContainer(CSharpElement element, out CSharpNamespace? ns)
    {
        ns = null;
        while (element.Parent != null)
        {
            if (element is CSharpNamespace namespaceElement)
            {
                ns = namespaceElement;
            }

            if (element.Parent is CSharpGeneratedFile file)
            {
                return file;
            }

            element = element.Parent;
        }

        throw new InvalidOperationException("No container found");

    }

    private static string GetMemberNameForComparer(ICSharpMember member)
    {
        if (member is CSharpMethod method && method.IsManaged)
        {

            return $"{member.Name}__managed";
        }

        return member.Name;
    }

    // pipe, pipe2 - create pipe
    [GeneratedRegex(@"\s*(?<names>.+)\s+\\-\s+(?<summary>.*)")]
    private static partial Regex RegexManSummary();


    [GeneratedRegex(@"^\.(?<name>[^\s]+)\s*(?<value>.*)")]
    private static partial Regex RegexMatchManMacro();

    private void LoadManPages()
    {
        var apk = Apk;
        var manIncludeFolder = apk.GetManDirectory(apk.Architectures[0], "main");

        string? currentSymbol = null;
        var currentSymbolBuilder = new StringBuilder();
        int indent = 0;

        for (int manSection = 2; manSection <= 3; manSection++)
        {
            var prototypeLines = new List<string>();
            foreach (var manPage in Directory.EnumerateFiles(Path.Combine(manIncludeFolder, $"man{manSection}"), "*.gz", SearchOption.AllDirectories))
            {
                var defaultFunctionName = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(manPage));

                var fileStream = File.OpenRead(manPage);
                using var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress);
                var reader = new StreamReader(gzipStream);

                while (true)
                {
                    var line = ReadLineOrMacro(reader);
                    if (line.Value is null)
                    {
                        break;
                    }

                    next_section:
                    var macroName = line.Name;

                    if (macroName == "SH")
                    {
                        FlushCurrentSymbol();
                        
                        if (line.Value == "NAME")
                        {
                            var summary = reader.ReadLine()!;

                            var match = RegexManSummary().Match(summary!);
                            if (match.Success)
                            {
                                var names = match.Groups["names"].Value.Split(',').Select(x => x.Trim()).ToList();
                                var newSummary = match.Groups["summary"].Value.Trim();
                                newSummary = char.ToUpper(newSummary[0]) + newSummary.Substring(1);
                                foreach (var name in names)
                                {
                                    _mapFunctionToSummary[name] = new ManFunction(manSection, defaultFunctionName, name, newSummary);
                                }
                            }
                            else
                            {
                                _mapFunctionToSummary[defaultFunctionName] = new ManFunction(manSection, defaultFunctionName, defaultFunctionName, summary);
                            }
                        }
                        else if (line.Value == "SYNOPSIS")
                        {
                            prototypeLines.Clear();
                            // Try to extract the prototypes
                            while (true)
                            {
                                line = ReadLineOrMacro(reader);
                                if (line.Value is null) throw new InvalidOperationException("Unexpected end of file");

                                string? fullLine = null;
                                while (line.Value.EndsWith('\\'))
                                {
                                    fullLine ??= string.Empty;
                                    fullLine += line.Value.Substring(0, line.Value.Length - 1);
                                    var previousName = line.Name; // Continue with the same name
                                    line = ReadLineOrMacro(reader);
                                    line.Name = previousName;
                                    if (line.Value is null) throw new InvalidOperationException("Unexpected end of file");
                                }

                                if (fullLine != null)
                                {
                                    line.Value = fullLine + line.Value;
                                }

                                if (line.Name == "SH")
                                {
                                    goto next_section;
                                }
                                else if (line.Name == "BI")
                                {
                                    if (line.Value.StartsWith("\""))
                                    {
                                        if (line.Value is null)
                                        {
                                            throw new InvalidOperationException("Unexpected end of file");
                                        }
                                        prototypeLines.Add(line.Value);
                                    }
                                }
                            }
                        }
                        else if (line.Value == "ERROR")
                        {
                            // We don't parse ERROR section to avoid collecting error codes
                            break;
                        }
                    }
                    else if (macroName == "TP")
                    {
                        if (indent == 0)
                        {
                            FlushCurrentSymbol();
                            line = ReadLineOrMacro(reader);
                            currentSymbol = line.Value;
                        }
                    }
                    else if (macroName == "SS")
                    {
                        FlushCurrentSymbol();
                    }
                    else if (macroName == "RS")
                    {
                        indent++;
                    }
                    else if (macroName == "RE")
                    {
                        indent--;
                    }
                    else
                    {
                        if (currentSymbol != null)
                        {
                            currentSymbolBuilder.AppendLine(line.Value);
                        }
                    }
                }

                if (prototypeLines.Count > 0)
                {
                    ParsePrototypeFunctionNameAndArgumentNames(prototypeLines, Path.GetFileName(manPage));
                }
            }
        }

        void FlushCurrentSymbol()
        {
            if (currentSymbol != null)
            {
                var symbolSummary = currentSymbolBuilder.ToString().Trim();
                currentSymbolBuilder.Clear();
                if (!_symbols.TryGetValue(currentSymbol, out var symbolValues))
                {
                    symbolValues = new List<string>();
                    _symbols[currentSymbol] = symbolValues;
                }
                symbolValues.Add(symbolSummary);
                currentSymbol = null;
            }
        }
    }


    [GeneratedRegex(@"(?<function>[A-Za-z0-9_]+)\(")]
    private static partial Regex RegexMatchFunctionName();

    [GeneratedRegex(@"(?<name>[A-Za-z0-9_]+)")]
    private static partial Regex RegexMatchArgumentName();

    private void ParsePrototypeFunctionNameAndArgumentNames(List<string> prototypeLines, string context)
    {
        // Example .BI alternates bold and italic, with argument names in italic:
        // 
        // .BI "int chown(const char *" pathname ", uid_t " owner ", gid_t " group );
        // .BI "int fchown(int " fd ", uid_t " owner ", gid_t " group );
        // .BI "int lchown(const char *" pathname ", uid_t " owner ", gid_t " group );
        // .BI "int fchownat(int " dirfd ", const char *" pathname ,
        // .BI "             uid_t " owner ", gid_t " group ", int " flags );
        //

        // Concat lines that are split
        for (var i = 0; i < prototypeLines.Count; i++)
        {
            var line = prototypeLines[i];
            if (char.IsWhiteSpace(line[1]))
            {
                if (i == 0)
                {
                    Console.WriteLine($"Warning Invalid Function parsing in {context}: Skipping {line}");
                    return;
                }

                prototypeLines[i - 1] += line;
                prototypeLines.RemoveAt(i);
                i--;
            }
        }

        foreach (var line in prototypeLines)
        {
            var elements = line.Split('"', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

            if (!elements[^1].EndsWith(";"))
            {
                Console.WriteLine($"Warning Function parsing in {context}: Function is not ending with `;`. Skipping {line}");
                continue;
            }
            
            var matchFunctionName = RegexMatchFunctionName().Match(elements[0]);
            if (!matchFunctionName.Success)
            {
                continue;
            }

            var functionName = matchFunctionName.Groups["function"].Value;

            if (KnownVariadicFunctions.Contains(functionName))
            {
                Console.WriteLine($"Warning Variadic function parsing in {context}: Skipping {functionName} in {line}");
                continue;
            }
            
            if (!_functionArguments.TryGetValue(functionName, out var argumentNames))
            {
                argumentNames = new List<string>();
                _functionArguments[functionName] = argumentNames;
            }
            else
            {
                Console.WriteLine($"Warning function parsing in {context}: Skipping redefinition of {functionName}");
                continue;
            }

            for (int i = 1; i < elements.Count; i +=2)
            {
                // Skip case when directive like [restrict is present:
                //
                // .BI "size_t strftime(char " s "[restrict ." max "], size_t " max ,
                // .BI "                const char *restrict " format ,
                // .BI "                const struct tm *restrict " tm );
                if (i > 1 && elements[i - 1].StartsWith("["))
                {
                    continue;
                }

                var argRaw = elements[i];
                var matchArgumentName = RegexMatchArgumentName().Match(argRaw);
                if (matchArgumentName.Success)
                {
                    argumentNames.Add(matchArgumentName.Groups["name"].Value);
                }
                else
                {
                    argumentNames.Add(argRaw);
                }
            }
        }
    }

    private static (string? Name, string? Value) ReadLineOrMacro(StreamReader reader)
    {
        var line = reader.ReadLine();
        if (line == null)
        {
            return (null, null);
        }

        var macroMatch = RegexMatchManMacro().Match(line);

        if (macroMatch.Success)
        {
            var macroName = macroMatch.Groups["name"].Value;
            var macroValue = macroMatch.Groups["value"].Value;
            return (macroName, macroValue);
        }

        return (null, line);
    }

    private string? GetFunctionSummaryFromManPage(string functionName)
    {
        if (_mapFunctionToSummary.TryGetValue(functionName, out var manFunction))
        {
            return manFunction.Summary;
        }

        return null;
    }

    private static void ProcessAndDispatchArchDependentElements(string baseArch, Dictionary<string, CSharpCompilation> mapArchToElements, HashSet<CSharpElement> sharedElements, Dictionary<string, HashSet<CSharpElement>> archElements)
    {
        var pairCompilation = new List<(string Name, Dictionary<string, CSharpElement> MapElements)>();
        foreach (var pair in mapArchToElements)
        {
            var archName = pair.Key;
            Dictionary<string, CSharpElement> mapNameToElement = new();
            foreach (var csClass in pair.Value.AllClasses)
            {
                var members = csClass.Members.OfType<ICSharpMember>().ToList();

                foreach (var member in members)
                {
                    var memberName = GetMemberNameForComparer(member);
                    var newElement = (CSharpElement)member;
                    if (mapNameToElement.TryGetValue(memberName, out var element))
                    {
                        Console.WriteLine($"Skipping redefinition of {memberName} - {newElement.CppElement}");
                    }
                    else
                    {
                        mapNameToElement.Add(memberName, newElement);
                    }
                }
            }

            pairCompilation.Add((archName, mapNameToElement));
        }
        
        var baseCompilationIndex = pairCompilation.FindIndex(x => x.Name == baseArch);
        var allFromElements = pairCompilation[baseCompilationIndex].MapElements;

        var allElementEquals = new HashSet<CSharpElement>();


        var elementEqualsLocal = new List<CSharpElement>();
        foreach (var fromElement in allFromElements.Values)
        {
            var fromName = GetMemberNameForComparer((ICSharpMember)fromElement);
            bool equal = true;
            elementEqualsLocal.Clear();
            for (int j = 0; j < pairCompilation.Count; j++)
            {
                if (j == baseCompilationIndex)
                {
                    continue;
                }

                var archToName = pairCompilation[j].Name;
                var allToElements = pairCompilation[j].MapElements;

                if (allToElements.TryGetValue(fromName, out var toElement))
                {
                    if (!CSharpElementComparer.Compare(fromElement, toElement))
                    {
                        AddArchElement(archElements, archToName, toElement);
                        equal = false;
                    }
                    else
                    {
                        elementEqualsLocal.Add(toElement);
                    }
                }
                else
                {
                    equal = false;
                }
            }

            if (equal)
            {
                sharedElements.Add(fromElement);
                foreach (var toElement in elementEqualsLocal)
                {
                    allElementEquals.Add(toElement);
                }
            }
            else
            {
                // Special case for blksize_t and nlink_t for x86_64
                // We want to have them in the standard interface even if they are not equal to the aarch64
                // So that we can create custom general stat function that will disptach to the correct arch stat function
                if (fromName == "blksize_t" || fromName == "nlink_t")
                {
                    sharedElements.Add(fromElement);
                }
                else
                {
                    AddArchElement(archElements, pairCompilation[baseCompilationIndex].Name, fromElement);
                }
            }
        }

        for (int j = 0; j < pairCompilation.Count; j++)
        {
            if (j == baseCompilationIndex)
            {
                continue;
            }
            
            var archToName = pairCompilation[j].Item1;
            var allToElements = pairCompilation[j].Item2;

            var elementsInArch = archElements[archToName];

            foreach (var toElement in allToElements.Values)
            {
                if (!allElementEquals.Contains(toElement) && !elementsInArch.Contains(toElement))
                {
                    elementsInArch.Add(toElement);
                }
            }
        }
    }

    private static void AddArchElement(Dictionary<string, HashSet<CSharpElement>> archElements, string arch, CSharpElement element)
    {
        if (!archElements.TryGetValue(arch, out var elements))
        {
            elements = new HashSet<CSharpElement>();
            archElements[arch] = elements;
        }

        elements.Add(element);
    }

    private void AssignArgumentNames(CSharpConverter arg1, CppElement arg2)
    {
        var cppFunction = (CppFunction)arg2;

        if (MapLibCFunctionToArgumentNames.TryGetValue(cppFunction.Name, out var names))
        {
            if (cppFunction.Parameters.Count > names.Length)
            {
                //Console.WriteLine($"ERROR: The number of parameters for function {cppFunction.Name} ({cppFunction.Parameters.Count}) doesn't match the number of expected parameters {names.Length}");
            }
            else
            {
                if (cppFunction.Parameters.Count != names.Length)
                {
                    //Console.WriteLine($"WARNING: The number of parameters for function {cppFunction.Name} ({cppFunction.Parameters.Count}) doesn't match the number of expected parameters {names.Length}");
                }
                for (var i = 0; i < cppFunction.Parameters.Count; i++)
                {
                    cppFunction.Parameters[i].Name = names[i];
                }
            }
        }
        else
        {
            //Console.WriteLine($"ERROR: Function {cppFunction.Name} was not found in libc list.");
        }
    }
    
    private void ProcessArguments(CSharpMethod csMethod)
    {
        // If we have a potential marshalling for return/parameter string, we will duplicate the method with string marshalling
        CSharpMethod? newManagedMethod = null;
        for (var i = 0; i < csMethod.Parameters.Count; i++)
        {
            var param = csMethod.Parameters[i];
            var cppType = (CppType)param.ParameterType!.CppElement!;
            if (cppType.TryGetElementTypeFromPointer(out var isConst, out var elementType) && isConst)
            {
                if (elementType is CppPrimitiveType { Kind: CppPrimitiveKind.Char })
                {
                    newManagedMethod ??= csMethod.Clone();
                    newManagedMethod.IsManaged = true;
                    newManagedMethod.Parameters[i].ParameterType = new CSharpTypeWithAttributes(new CSharpFreeType("ReadOnlySpan<char>"))
                    {
                        Attributes = { new CSharpMarshalUsingAttribute("typeof(Utf8CustomMarshaller)") }
                    };
                }
            }
        }

        var returnType = ((CppType)csMethod.ReturnType!.CppElement!);
        if (returnType.TryGetElementTypeFromPointer(out var isConstReturn, out var returnElementType))
        {
            if (returnElementType is CppPrimitiveType { Kind: CppPrimitiveKind.Char })
            {
                newManagedMethod ??= csMethod.Clone();
                csMethod.Name = $"{csMethod.Name}_";
                newManagedMethod.IsManaged = true;
                newManagedMethod.ReturnType = new CSharpTypeWithAttributes(CSharpPrimitiveType.String())
                {
                    Attributes = { new CSharpMarshalUsingAttribute("typeof(Utf8CustomMarshaller)") { Scope = CSharpAttributeScope.Return } }
                };
            }
        }

        if (newManagedMethod != null)
        {
            var parent = ((ICSharpContainer) csMethod.Parent!);
            var indexOf = parent.Members.IndexOf(csMethod);
            parent.Members.Insert(indexOf + 1, newManagedMethod);
        }
    }

    private record ManFunction(int ManSection, string BaseFunctionName, string FunctionName, string Summary);


    public static DefaultTypedefConverter AddDefaultMuslAndKernelCTypes(CSharpConverterOptions csOptions)
    {
        var typeDefConverter = csOptions.Plugins.OfType<DefaultTypedefConverter>().First();

        typeDefConverter.StandardCTypes.Add("__s128", () => new CSharpFreeType("global::System.Int128"));
        typeDefConverter.StandardCTypes.Add("__u128", () => new CSharpFreeType("global::System.UInt128"));
        typeDefConverter.StandardCTypes.Add("__s8", () => CSharpPrimitiveType.SByte());
        typeDefConverter.StandardCTypes.Add("__u8", () => CSharpPrimitiveType.Byte());
        typeDefConverter.StandardCTypes.Add("__s16", () => CSharpPrimitiveType.Short());
        typeDefConverter.StandardCTypes.Add("__u16", () => CSharpPrimitiveType.UShort());
        typeDefConverter.StandardCTypes.Add("__s32", () => CSharpPrimitiveType.Int());
        typeDefConverter.StandardCTypes.Add("__u32", () => CSharpPrimitiveType.UInt());
        typeDefConverter.StandardCTypes.Add("__s64", () => CSharpPrimitiveType.Long());
        typeDefConverter.StandardCTypes.Add("__u64", () => CSharpPrimitiveType.ULong());
        typeDefConverter.StandardCTypes.Add("__le16", () => CSharpPrimitiveType.UShort());
        typeDefConverter.StandardCTypes.Add("__be16", () => CSharpPrimitiveType.UShort()); // we only support little-endian
        typeDefConverter.StandardCTypes.Add("__le32", () => CSharpPrimitiveType.UInt());
        typeDefConverter.StandardCTypes.Add("__be32", () => CSharpPrimitiveType.UInt()); // we only support little-endian
        typeDefConverter.StandardCTypes.Add("__le64", () => CSharpPrimitiveType.ULong());
        typeDefConverter.StandardCTypes.Add("__be64", () => CSharpPrimitiveType.ULong()); // we only support little-endian
        typeDefConverter.StandardCTypes.Add("__sum16", () => CSharpPrimitiveType.UShort());
        typeDefConverter.StandardCTypes.Add("__wsum", () => CSharpPrimitiveType.UInt());
        typeDefConverter.StandardCTypes.Add("u_int8_t", () => CSharpPrimitiveType.Byte());
        typeDefConverter.StandardCTypes.Add("u_int16_t", () => CSharpPrimitiveType.UShort());
        typeDefConverter.StandardCTypes.Add("u_int32_t", () => CSharpPrimitiveType.UInt());
        typeDefConverter.StandardCTypes.Add("u_char", () => CSharpPrimitiveType.Byte());

        typeDefConverter.StandardCTypes.Add("int_fast8_t", () => CSharpPrimitiveType.SByte());
        typeDefConverter.StandardCTypes.Add("int_least8_t", () => CSharpPrimitiveType.SByte());
        typeDefConverter.StandardCTypes.Add("uint_fast8_t", () => CSharpPrimitiveType.Byte());
        typeDefConverter.StandardCTypes.Add("uint_least8_t", () => CSharpPrimitiveType.Byte());

        typeDefConverter.StandardCTypes.Add("int_least16_t", () => CSharpPrimitiveType.Short());
        typeDefConverter.StandardCTypes.Add("uint_least16_t", () => CSharpPrimitiveType.UShort());
        typeDefConverter.StandardCTypes.Add("int_fast16_t", () => CSharpPrimitiveType.Int());
        typeDefConverter.StandardCTypes.Add("uint_fast16_t", () => CSharpPrimitiveType.UInt());

        typeDefConverter.StandardCTypes.Add("int_fast32_t", () => CSharpPrimitiveType.Int());
        typeDefConverter.StandardCTypes.Add("uint_fast32_t", () => CSharpPrimitiveType.UInt());
        typeDefConverter.StandardCTypes.Add("int_least32_t", () => CSharpPrimitiveType.Int());
        typeDefConverter.StandardCTypes.Add("uint_least32_t", () => CSharpPrimitiveType.UInt());

        typeDefConverter.StandardCTypes.Add("int_least64_t", () => CSharpPrimitiveType.Long());
        typeDefConverter.StandardCTypes.Add("uint_least64_t", () => CSharpPrimitiveType.ULong());
        typeDefConverter.StandardCTypes.Add("int_fast64_t", () => CSharpPrimitiveType.Long());
        typeDefConverter.StandardCTypes.Add("uint_fast64_t", () => CSharpPrimitiveType.ULong());

        typeDefConverter.StandardCTypes.Add("u_short", () => CSharpPrimitiveType.UShort());
        typeDefConverter.StandardCTypes.Add("ushort", () => CSharpPrimitiveType.UShort());

        typeDefConverter.StandardCTypes.Add("u_int", () => CSharpPrimitiveType.UInt());
        typeDefConverter.StandardCTypes.Add("uint", () => CSharpPrimitiveType.UInt());

        typeDefConverter.StandardCTypes.Add("u_long", () => CSharpPrimitiveType.UIntPtr());
        typeDefConverter.StandardCTypes.Add("ulong", () => CSharpPrimitiveType.UIntPtr());

        typeDefConverter.StandardCTypes.Add("unsigned __int128", () => new CSharpFreeType("global::System.UInt128"));

        return typeDefConverter;
    }
}
