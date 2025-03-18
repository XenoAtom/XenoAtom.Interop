# XenoAtom.Interop.libgbm [![Build Status](https://github.com/XenoAtom/XenoAtom.Interop/actions/workflows/ci_build_libgbm.yml/badge.svg)](https://github.com/XenoAtom/XenoAtom.Interop/actions/workflows/ci_build_libgbm.yml) [![NuGet](https://img.shields.io/nuget/v/XenoAtom.Interop.libgbm.svg)](https://www.nuget.org/packages/XenoAtom.Interop.libgbm/)

This package provides a low-level and modern .NET P/Invoke wrapper around the libgbm Mesa API.

## ♻️ XenoAtom.Interop

This package is part of the [XenoAtom.Interop](https://github.com/XenoAtom/XenoAtom.Interop) project.

libgbm is a userspace library that provides an abstraction for buffer management used by graphics drivers. For more information, see [libgbm](https://gitlab.freedesktop.org/mesa/mesa) website.
## 💻 Usage

After installing the package, you can access the library through the static class `XenoAtom.Interop.libgbm`.

For more information, see the official documentation at https://gitlab.freedesktop.org/mesa/mesa.

## 📦 Compatible Native Binaries

This library does not provide C native binaries but only P/Invoke .NET bindings to `libgbm` `24.2.8-r0`.

If the native library is already installed on your system, check the version installed. If you are using this library on Alpine Linux, see the compatible version in the [Supported API](#supported-api) section below.
Other OS might require a different setup.


## 📚 Supported API

> This package is based on the following header version:
> 
> - libgbm C include headers: [`mesa-dev`](https://pkgs.alpinelinux.org/package/v3.21/main/x86_64/mesa-dev)
> - Version: `24.2.8-r0`
> - Distribution: AlpineLinux `v3.21`

The following API were automatically generated from the C/C++ code:

- gbm.h: `gbm_bo_create`, `gbm_bo_create_with_modifiers`, `gbm_bo_create_with_modifiers2`, `gbm_bo_destroy`, `gbm_bo_get_bpp`, `gbm_bo_get_device`, `gbm_bo_get_fd`, `gbm_bo_get_fd_for_plane`, `gbm_bo_get_format`, `gbm_bo_get_handle`, `gbm_bo_get_handle_for_plane`, `gbm_bo_get_height`, `gbm_bo_get_modifier`, `gbm_bo_get_offset`, `gbm_bo_get_plane_count`, `gbm_bo_get_stride`, `gbm_bo_get_stride_for_plane`, `gbm_bo_get_user_data`, `gbm_bo_get_width`, `gbm_bo_import`, `gbm_bo_map`, `gbm_bo_set_user_data`, `gbm_bo_unmap`, `gbm_bo_write`, `gbm_create_device`, `gbm_device_destroy`, `gbm_device_get_backend_name`, `gbm_device_get_fd`, `gbm_device_get_format_modifier_plane_count`, `gbm_device_is_format_supported`, `gbm_format_get_name`, `gbm_surface_create`, `gbm_surface_create_with_modifiers`, `gbm_surface_create_with_modifiers2`, `gbm_surface_destroy`, `gbm_surface_has_free_buffers`, `gbm_surface_lock_front_buffer`, `gbm_surface_release_buffer`


## 🪪 License

This software is released under the [BSD-2-Clause license](https://opensource.org/licenses/BSD-2-Clause). 

## 🤗 Author

Alexandre Mutel aka [xoofx](https://xoofx.github.io).
