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
    
    public static unsafe partial class libgit2
    {
        /// <summary>
        /// The type of proxy to use.
        /// </summary>
        public enum git_proxy_t : int
        {
            /// <summary>
            /// Do not attempt to connect through a proxy
            /// </summary>
            /// <remarks>
            /// If built against libcurl, it itself may attempt to connect
            /// to a proxy if the environment variables specify it.
            /// </remarks>
            GIT_PROXY_NONE,
            
            /// <summary>
            /// Try to auto-detect the proxy from the git configuration.
            /// </summary>
            GIT_PROXY_AUTO,
            
            /// <summary>
            /// Connect via the URL given in the options
            /// </summary>
            GIT_PROXY_SPECIFIED,
        }
        
        /// <summary>
        /// Do not attempt to connect through a proxy
        /// </summary>
        /// <remarks>
        /// If built against libcurl, it itself may attempt to connect
        /// to a proxy if the environment variables specify it.
        /// </remarks>
        public const libgit2.git_proxy_t GIT_PROXY_NONE = git_proxy_t.GIT_PROXY_NONE;
        
        /// <summary>
        /// Try to auto-detect the proxy from the git configuration.
        /// </summary>
        public const libgit2.git_proxy_t GIT_PROXY_AUTO = git_proxy_t.GIT_PROXY_AUTO;
        
        /// <summary>
        /// Connect via the URL given in the options
        /// </summary>
        public const libgit2.git_proxy_t GIT_PROXY_SPECIFIED = git_proxy_t.GIT_PROXY_SPECIFIED;
        
        /// <summary>
        /// Options for connecting through a proxy
        /// </summary>
        /// <remarks>
        /// Note that not all types may be supported, depending on the platform
        /// and compilation options.
        /// </remarks>
        public partial struct git_proxy_options
        {
            public uint version;
            
            /// <summary>
            /// The type of proxy to use, by URL, auto-detect.
            /// </summary>
            public libgit2.git_proxy_t type;
            
            /// <summary>
            /// The URL of the proxy.
            /// </summary>
            public readonly byte* url;
            
            /// <summary>
            /// This will be called if the remote host requires
            /// authentication in order to connect to it.
            /// </summary>
            /// <remarks>
            /// Returning GIT_PASSTHROUGH will make libgit2 behave as
            /// though this field isn't set.
            /// </remarks>
            public libgit2.git_credential_acquire_cb credentials;
            
            /// <summary>
            /// If cert verification fails, this will be called to let the
            /// user make the final decision of whether to allow the
            /// connection to proceed. Returns 0 to allow the connection
            /// or a negative value to indicate an error.
            /// </summary>
            public libgit2.git_transport_certificate_check_cb certificate_check;
            
            /// <summary>
            /// Payload to be provided to the credentials and certificate
            /// check callbacks.
            /// </summary>
            public void* payload;
        }
        
        /// <summary>
        /// Initialize git_proxy_options structure
        /// </summary>
        /// <param name="opts">The `git_proxy_options` struct to initialize.</param>
        /// <param name="version">The struct version; pass `GIT_PROXY_OPTIONS_VERSION`.</param>
        /// <returns>@return Zero on success; -1 on failure.</returns>
        /// <remarks>
        /// Initializes a `git_proxy_options` with default values. Equivalent to
        /// creating an instance with `GIT_PROXY_OPTIONS_INIT`.
        /// </remarks>
        [global::System.Runtime.InteropServices.LibraryImport(LibraryName, EntryPoint = "git_proxy_options_init")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        public static partial libgit2.git_result git_proxy_options_init(ref libgit2.git_proxy_options opts, uint version);
    }
}