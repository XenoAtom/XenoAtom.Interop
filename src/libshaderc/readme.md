# XenoAtom.Interop.libshaderc [![Build Status](https://github.com/XenoAtom/XenoAtom.Interop/actions/workflows/ci_build_libshaderc.yml/badge.svg)](https://github.com/XenoAtom/XenoAtom.Interop/actions/workflows/ci_build_libshaderc.yml) [![NuGet](https://img.shields.io/nuget/v/XenoAtom.Interop.libshaderc.svg)](https://www.nuget.org/packages/XenoAtom.Interop.libshaderc/)

This package provides a low-level and modern .NET P/Invoke wrapper around the libshaderc API.

## ♻️ XenoAtom.Interop

This package is part of the [XenoAtom.Interop](https://github.com/XenoAtom/XenoAtom.Interop) project.

libshaderc is a library for compiling GLSL/HLSL to SPIR-V. For more information, see [libshaderc](https://github.com/google/shaderc) website.
## 💻 Usage

After installing the package, you can access the library through the static class `XenoAtom.Interop.libshaderc`.

For more information, see the official documentation at https://github.com/google/shaderc.

## 📦 Compatible Native Binaries

This library does not provide C native binaries but only P/Invoke .NET bindings to `libshaderc` `2023.7-r0`.

If the native library is already installed on your system, check the version installed. If you are using this library on Alpine Linux, see the compatible version in the [Supported API](#supported-api) section below.
Other OS might require a different setup.

For convenience, you can install an existing NuGet package (outside of XenoAtom.Interop project) that is providing native binaries.
The following packages were tested and are compatible with **XenoAtom.Interop.libshaderc**:

| NuGet Package with Native Binaries | Version |
|------------------------------------|---------|
| [Silk.NET.Shaderc.Native](https://www.nuget.org/packages/Silk.NET.Shaderc.Native) | `2.21.0`


## 📚 Supported API

> This package is based on the following header version:
> 
> - libshaderc C include headers: [`shaderc-dev`](https://pkgs.alpinelinux.org/package/v3.19/community/x86_64/shaderc-dev)
> - Version: `2023.7-r0`
> - Distribution: AlpineLinux `v3.19`

The following API were automatically generated from the C/C++ code:

- shaderc.h: `shaderc_assemble_into_spv`, `shaderc_compile_into_preprocessed_text`, `shaderc_compile_into_spv`, `shaderc_compile_into_spv_assembly`, `shaderc_compile_options_add_macro_definition`, `shaderc_compile_options_clone`, `shaderc_compile_options_initialize`, `shaderc_compile_options_release`, `shaderc_compile_options_set_auto_bind_uniforms`, `shaderc_compile_options_set_auto_combined_image_sampler`, `shaderc_compile_options_set_auto_map_locations`, `shaderc_compile_options_set_binding_base`, `shaderc_compile_options_set_binding_base_for_stage`, `shaderc_compile_options_set_forced_version_profile`, `shaderc_compile_options_set_generate_debug_info`, `shaderc_compile_options_set_hlsl_16bit_types`, `shaderc_compile_options_set_hlsl_functionality1`, `shaderc_compile_options_set_hlsl_io_mapping`, `shaderc_compile_options_set_hlsl_offsets`, `shaderc_compile_options_set_hlsl_register_set_and_binding`, `shaderc_compile_options_set_hlsl_register_set_and_binding_for_stage`, `shaderc_compile_options_set_include_callbacks`, `shaderc_compile_options_set_invert_y`, `shaderc_compile_options_set_limit`, `shaderc_compile_options_set_nan_clamp`, `shaderc_compile_options_set_optimization_level`, `shaderc_compile_options_set_preserve_bindings`, `shaderc_compile_options_set_source_language`, `shaderc_compile_options_set_suppress_warnings`, `shaderc_compile_options_set_target_env`, `shaderc_compile_options_set_target_spirv`, `shaderc_compile_options_set_warnings_as_errors`, `shaderc_compiler_initialize`, `shaderc_compiler_release`, `shaderc_get_spv_version`, `shaderc_parse_version_profile`, `shaderc_result_get_bytes`, `shaderc_result_get_compilation_status`, `shaderc_result_get_error_message`, `shaderc_result_get_length`, `shaderc_result_get_num_errors`, `shaderc_result_get_num_warnings`, `shaderc_result_release`


## 🪪 License

This software is released under the [BSD-2-Clause license](https://opensource.org/licenses/BSD-2-Clause). 

## 🤗 Author

Alexandre Mutel aka [xoofx](https://xoofx.github.io).
