# XenoAtom.Interop.libkmod [![Build Status](https://github.com/XenoAtom/XenoAtom.Interop/actions/workflows/ci_build_libkmod.yml/badge.svg)](https://github.com/XenoAtom/XenoAtom.Interop/actions/workflows/ci_build_libkmod.yml) [![NuGet](https://img.shields.io/nuget/v/XenoAtom.Interop.libkmod.svg)](https://www.nuget.org/packages/XenoAtom.Interop.libkmod/)

This package provides a low-level and modern .NET P/Invoke wrapper around the libkmod API.

## ♻️ XenoAtom.Interop

This package is part of the [XenoAtom.Interop](https://github.com/XenoAtom/XenoAtom.Interop) project.

libkmod is a library for managing kernel modules. For more information, see [libkmod](https://github.com/kmod-project/kmod/) website.
## 💻 Usage

After installing the package, you can access the library through the static class `XenoAtom.Interop.libkmod`.

For more information, see the official documentation at https://github.com/kmod-project/kmod/.

## 📦 Compatible Native Binaries

This library does not provide C native binaries but only P/Invoke .NET bindings to `libkmod` `33-r2`.

If the native library is already installed on your system, check the version installed. If you are using this library on Alpine Linux, see the compatible version in the [Supported API](#supported-api) section below.
Other OS might require a different setup.


## 📚 Supported API

> This package is based on the following header version:
> 
> - libkmod C include headers: [`kmod-dev`](https://pkgs.alpinelinux.org/package/v3.21/main/x86_64/kmod-dev)
> - Version: `33-r2`
> - Distribution: AlpineLinux `v3.21`

The following API were automatically generated from the C/C++ code:

- libkmod.h: `kmod_config_get_aliases`, `kmod_config_get_blacklists`, `kmod_config_get_install_commands`, `kmod_config_get_options`, `kmod_config_get_remove_commands`, `kmod_config_get_softdeps`, `kmod_config_get_weakdeps`, `kmod_config_iter_free_iter`, `kmod_config_iter_get_key`, `kmod_config_iter_get_value`, `kmod_config_iter_next`, `kmod_dump_index`, `kmod_get_dirname`, `kmod_get_log_priority`, `kmod_get_userdata`, `kmod_list_last`, `kmod_list_next`, `kmod_list_prev`, `kmod_load_resources`, `kmod_module_apply_filter`, `kmod_module_dependency_symbol_get_bind`, `kmod_module_dependency_symbol_get_crc`, `kmod_module_dependency_symbol_get_symbol`, `kmod_module_dependency_symbols_free_list`, `kmod_module_get_dependencies`, `kmod_module_get_dependency_symbols`, `kmod_module_get_filtered_blacklist`, `kmod_module_get_holders`, `kmod_module_get_info`, `kmod_module_get_initstate`, `kmod_module_get_install_commands`, `kmod_module_get_module`, `kmod_module_get_name`, `kmod_module_get_options`, `kmod_module_get_path`, `kmod_module_get_refcnt`, `kmod_module_get_remove_commands`, `kmod_module_get_sections`, `kmod_module_get_size`, `kmod_module_get_softdeps`, `kmod_module_get_symbols`, `kmod_module_get_versions`, `kmod_module_get_weakdeps`, `kmod_module_info_free_list`, `kmod_module_info_get_key`, `kmod_module_info_get_value`, `kmod_module_initstate_str`, `kmod_module_insert_module`, `kmod_module_new_from_loaded`, `kmod_module_new_from_lookup`, `kmod_module_new_from_name`, `kmod_module_new_from_name_lookup`, `kmod_module_new_from_path`, `kmod_module_probe_insert_module`, `kmod_module_ref`, `kmod_module_remove_module`, `kmod_module_section_free_list`, `kmod_module_section_get_address`, `kmod_module_section_get_name`, `kmod_module_symbol_get_crc`, `kmod_module_symbol_get_symbol`, `kmod_module_symbols_free_list`, `kmod_module_unref`, `kmod_module_unref_list`, `kmod_module_version_get_crc`, `kmod_module_version_get_symbol`, `kmod_module_versions_free_list`, `kmod_new`, `kmod_ref`, `kmod_set_log_fn`, `kmod_set_log_priority`, `kmod_set_userdata`, `kmod_unload_resources`, `kmod_unref`, `kmod_validate_resources`


## 🪪 License

This software is released under the [BSD-2-Clause license](https://opensource.org/licenses/BSD-2-Clause). 

## 🤗 Author

Alexandre Mutel aka [xoofx](https://xoofx.github.io).
