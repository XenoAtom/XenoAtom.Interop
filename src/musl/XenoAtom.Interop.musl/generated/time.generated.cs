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
        public partial struct tm
        {
            public int tm_sec;
            
            public int tm_min;
            
            public int tm_hour;
            
            public int tm_mday;
            
            public int tm_mon;
            
            public int tm_year;
            
            public int tm_wday;
            
            public int tm_yday;
            
            public int tm_isdst;
            
            public nint tm_gmtoff;
            
            public byte* tm_zone;
        }
        
        public partial struct itimerspec
        {
            public musl.timespec it_interval;
            
            public musl.timespec it_value;
        }
        
        public const int TIME_UTC = 1;
        
        /// <summary>
        /// Determine processor time
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "clock")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial musl.clock_t clock();
        
        /// <summary>
        /// Get time in seconds
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "time")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial musl.time_t time(ref musl.time_t tloc);
        
        /// <summary>
        /// Calculate time difference
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "difftime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial double difftime(musl.time_t time1, musl.time_t time0);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "mktime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial musl.time_t mktime(ref musl.tm tm);
        
        /// <summary>
        /// Format date and time
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "strftime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial nuint strftime(byte* s, nuint max, byte* format, in musl.tm tm);
        
        /// <summary>
        /// Format date and time
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "strftime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial nuint strftime(byte* s, nuint max, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> format, in musl.tm tm);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "gmtime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial musl.tm* gmtime(ref musl.time_t timep);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "localtime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial musl.tm* localtime(ref musl.time_t timep);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "asctime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial byte* asctime_(in musl.tm tm);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "asctime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        [return:global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))]
        public static partial string asctime(in musl.tm tm);
        
        /// <summary>
        /// asctime, ctime, gmtime, localtime, mktime, asctime_r, ctime_r, gmtime_r,
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "ctime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial byte* ctime_(ref musl.time_t timep);
        
        /// <summary>
        /// asctime, ctime, gmtime, localtime, mktime, asctime_r, ctime_r, gmtime_r,
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "ctime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        [return:global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))]
        public static partial string ctime(ref musl.time_t timep);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "timespec_get")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int timespec_get(ref musl.timespec arg0, int arg1);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "strftime_l")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial nuint strftime_l(byte* s, nuint max, byte* format, in musl.tm tm, musl.locale_t locale);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "strftime_l")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial nuint strftime_l(byte* s, nuint max, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> format, in musl.tm tm, musl.locale_t locale);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "gmtime_r")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial musl.tm* gmtime_r(ref musl.time_t timep, ref musl.tm result);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "localtime_r")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial musl.tm* localtime_r(ref musl.time_t timep, ref musl.tm result);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "asctime_r")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial byte* asctime_r_(in musl.tm tm, byte* buf);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "asctime_r")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        [return:global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))]
        public static partial string asctime_r(in musl.tm tm, byte* buf);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "ctime_r")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial byte* ctime_r_(ref musl.time_t timep, byte* buf);
        
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "ctime_r")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        [return:global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))]
        public static partial string ctime_r(ref musl.time_t timep, byte* buf);
        
        /// <summary>
        /// Initialize time conversion information
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "tzset")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial void tzset();
        
        /// <summary>
        /// High-resolution sleep
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "nanosleep")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int nanosleep(in musl.timespec req, ref musl.timespec rem);
        
        /// <summary>
        /// Clock and time functions
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "clock_getres")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int clock_getres(musl.clockid_t clockid, ref musl.timespec res);
        
        /// <summary>
        /// Clock and time functions
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "clock_gettime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int clock_gettime(musl.clockid_t clockid, ref musl.timespec tp);
        
        /// <summary>
        /// Clock and time functions
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "clock_settime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int clock_settime(musl.clockid_t clockid, in musl.timespec tp);
        
        /// <summary>
        /// High-resolution sleep with specifiable clock
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "clock_nanosleep")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int clock_nanosleep(musl.clockid_t clockid, int flags, in musl.timespec request, ref musl.timespec remain);
        
        /// <summary>
        /// Obtain ID of a process CPU-time clock
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "clock_getcpuclockid")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int clock_getcpuclockid(musl.pid_t pid, ref musl.clockid_t clockid);
        
        /// <summary>
        /// Create a POSIX per-process timer
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "timer_create")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int timer_create(musl.clockid_t clockid, ref musl.sigevent sevp, ref musl.timer_t timerid);
        
        /// <summary>
        /// Delete a POSIX per-process timer
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "timer_delete")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int timer_delete(musl.timer_t timerid);
        
        /// <summary>
        /// Arm/disarm and fetch
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "timer_settime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int timer_settime(musl.timer_t timerid, int flags, in musl.itimerspec new_value, ref musl.itimerspec old_value);
        
        /// <summary>
        /// Arm/disarm and fetch
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "timer_gettime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int timer_gettime(musl.timer_t timerid, ref musl.itimerspec curr_value);
        
        /// <summary>
        /// Get overrun count for a POSIX per-process timer
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "timer_getoverrun")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int timer_getoverrun(musl.timer_t timerid);
        
        /// <summary>
        /// Convert a string representation of time to a time tm structure
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "strptime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial byte* strptime_(byte* s, byte* format, ref musl.tm tm);
        
        /// <summary>
        /// Convert a string representation of time to a time tm structure
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "strptime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        [return:global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))]
        public static partial string strptime([global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> s, [global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> format, ref musl.tm tm);
        
        /// <summary>
        /// Convert a date-plus-time string to broken-down time
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "getdate")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial musl.tm* getdate(byte* @string);
        
        /// <summary>
        /// Convert a date-plus-time string to broken-down time
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "getdate")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial musl.tm* getdate([global::System.Runtime.InteropServices.Marshalling.MarshalUsing(typeof(Utf8CustomMarshaller))] ReadOnlySpan<char> @string);
        
        /// <summary>
        /// Set time
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "stime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial int stime(ref musl.time_t t);
        
        /// <summary>
        /// Inverses of gmtime and localtime
        /// </summary>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "timegm")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial musl.time_t timegm(ref musl.tm tm);
    }
}
