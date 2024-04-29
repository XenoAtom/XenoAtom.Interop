// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
static unsafe partial class libgit2
{
    partial struct git_config_entry : IDisposable
    {
        /// <summary>
        /// Name of the entry (normalised)
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string? name_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(name);
            set => name = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <summary>
        /// String value of the entry
        /// </summary>
        /// <remarks>
        /// When setting this field, this struct instance must be disposed.
        /// </remarks>
        public string value_string
        {
            get => LibGit2Helper.UnmanagedUtf8StringToString(value)!;
            set => this.value = LibGit2Helper.StringToUnmanagedUtf8String(value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            LibGit2Helper.FreeUnmanagedUtf8String(name);
            name = null;
            LibGit2Helper.FreeUnmanagedUtf8String(value);
            value = null;
        }
    }

    /// <summary>
    /// Get the value of a string config variable.
    /// </summary>
    /// <param name="out">pointer to the string</param>
    /// <param name="cfg">where to look for the variable</param>
    /// <param name="name">the variable's name</param>
    /// <returns>@return 0 or an error code</returns>
    /// <remarks>
    /// This function can only be used on snapshot config objects. The
    /// string is owned by the config and should not be freed by the
    /// user. The pointer will be valid until the config is freed.All config files will be looked into, in the order of their
    /// defined level. A higher level means a higher priority. The
    /// first occurrence of the variable will be returned here.
    /// </remarks>
    public static libgit2.git_result git_config_get_string(out string? @out, libgit2.git_config cfg, string name)
    {
        byte* out_ptr;
        var error = libgit2.git_config_get_string(out @out_ptr, cfg, name);
        @out = error == 0 ? LibGit2Helper.UnmanagedUtf8StringToString(@out_ptr) : null;
        return error;
    }

    /// <summary>
    /// Query the value of a config variable and return it mapped to
    /// an integer constant.
    /// </summary>
    /// <param name="out">place to store the result of the mapping</param>
    /// <param name="cfg">config file to get the variables from</param>
    /// <param name="name">name of the config variable to lookup</param>
    /// <param name="maps">array of `git_configmap` objects specifying the possible mappings</param>
    /// <returns>@return 0 on success, error code otherwise</returns>
    /// <remarks>
    /// This is a helper method to easily map different possible values
    /// to a variable to integer constants that easily identify them.A mapping array looks as follows:git_configmap autocrlf_mapping[] = {
    /// {GIT_CVAR_FALSE, NULL, GIT_AUTO_CRLF_FALSE},
    /// {GIT_CVAR_TRUE, NULL, GIT_AUTO_CRLF_TRUE},
    /// {GIT_CVAR_STRING, "input", GIT_AUTO_CRLF_INPUT},
    /// {GIT_CVAR_STRING, "default", GIT_AUTO_CRLF_DEFAULT}};On any "false" value for the variable (e.g. "false", "FALSE", "no"), the
    /// mapping will store `GIT_AUTO_CRLF_FALSE` in the `out` parameter.The same thing applies for any "true" value such as "true", "yes" or "1", storing
    /// the `GIT_AUTO_CRLF_TRUE` variable.Otherwise, if the value matches the string "input" (with case insensitive comparison),
    /// the given constant will be stored in `out`, and likewise for "default".If not a single match can be made to store in `out`, an error code will be
    /// returned.
    /// </remarks>
    public static libgit2.git_result git_config_get_mapped(out int @out, libgit2.git_config cfg, byte* name, Span<libgit2.git_configmap> maps)
    {
        fixed (libgit2.git_configmap* mapsPtr = maps)
        {
            return git_config_get_mapped(out @out, cfg, name, mapsPtr, (uint)maps.Length);
        }
    }

    /// <summary>
    /// Query the value of a config variable and return it mapped to
    /// an integer constant.
    /// </summary>
    /// <param name="out">place to store the result of the mapping</param>
    /// <param name="cfg">config file to get the variables from</param>
    /// <param name="name">name of the config variable to lookup</param>
    /// <param name="maps">array of `git_configmap` objects specifying the possible mappings</param>
    /// <returns>@return 0 on success, error code otherwise</returns>
    /// <remarks>
    /// This is a helper method to easily map different possible values
    /// to a variable to integer constants that easily identify them.A mapping array looks as follows:git_configmap autocrlf_mapping[] = {
    /// {GIT_CVAR_FALSE, NULL, GIT_AUTO_CRLF_FALSE},
    /// {GIT_CVAR_TRUE, NULL, GIT_AUTO_CRLF_TRUE},
    /// {GIT_CVAR_STRING, "input", GIT_AUTO_CRLF_INPUT},
    /// {GIT_CVAR_STRING, "default", GIT_AUTO_CRLF_DEFAULT}};On any "false" value for the variable (e.g. "false", "FALSE", "no"), the
    /// mapping will store `GIT_AUTO_CRLF_FALSE` in the `out` parameter.The same thing applies for any "true" value such as "true", "yes" or "1", storing
    /// the `GIT_AUTO_CRLF_TRUE` variable.Otherwise, if the value matches the string "input" (with case insensitive comparison),
    /// the given constant will be stored in `out`, and likewise for "default".If not a single match can be made to store in `out`, an error code will be
    /// returned.
    /// </remarks>
    public static libgit2.git_result git_config_get_mapped(out int @out, libgit2.git_config cfg, string name,
        Span<libgit2.git_configmap> maps)
    {
        fixed (libgit2.git_configmap* mapsPtr = maps)
        {
            return git_config_get_mapped(out @out, cfg, name, mapsPtr, (uint)maps.Length);
        }
    }


    /// <summary>
    /// Maps a string value to an integer constant
    /// </summary>
    /// <param name="out">place to store the result of the parsing</param>
    /// <param name="maps">array of `git_configmap` objects specifying the possible mappings</param>
    /// <param name="value">value to parse</param>
    /// <returns>@return 0 or an error code.</returns>
    public static libgit2.git_result git_config_lookup_map_value(out int @out, Span<libgit2.git_configmap> maps, byte* value)
    {
        fixed (libgit2.git_configmap* mapsPtr = maps)
        {
            return git_config_lookup_map_value(out @out, mapsPtr, (uint)maps.Length, value);
        }
    }

    /// <summary>
    /// Maps a string value to an integer constant
    /// </summary>
    /// <param name="out">place to store the result of the parsing</param>
    /// <param name="maps">array of `git_configmap` objects specifying the possible mappings</param>
    /// <param name="value">value to parse</param>
    /// <returns>@return 0 or an error code.</returns>
    public static libgit2.git_result git_config_lookup_map_value(out int @out, Span<libgit2.git_configmap> maps, string value)
    {
        fixed (libgit2.git_configmap* mapsPtr = maps)
        {
            return git_config_lookup_map_value(out @out, mapsPtr, (uint)maps.Length, value);
        }
    }
}