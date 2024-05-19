# XenoAtom.Interop 🚀

<img align="right" width="160px" height="160px" src="https://raw.githubusercontent.com/XenoAtom/XenoAtom.Interop/main/img/XenoAtom.Interop.png">

This **XenoAtom.Interop** project provides a set of C# libraries to interop with C/C++ libraries.

## ✨ Features

- **API generated automatically** from C/C++ headers providing a near **100% API coverage**.
- **Low-level interop** with C/C++ libraries
  - The C/C++ API exposed is raw and will use pointers...etc.
  - Pure [Function Pointers](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/function-pointers) generated for callbacks, no managed delegates.
  - Similarly for `ReadOnlySpan<byte>` whenever possible.
- **Modern interop** using [`[LibraryImport]`](https://learn.microsoft.com/en-us/dotnet/standard/native-interop/pinvoke-source-generation) with P/Invoke source generation.
  - Some functions taking or returning strings try to offer a more user-friendly API by using `string` (but the raw function is still accessible!)
  - Fast UTF16 to UTF8 string marshalling with zero allocations (for small strings).
  - [`DllImportResolver`](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.nativelibrary.setdllimportresolver) support for each library to customize loading the native library.
- No native binaries are provided, **only the P/Invoke bindings**.
  - But some 3rd party NuGet packages might provide compatible native libraries. See the list of available compatible packages in each library below.
- **Simple API XML documentation** is provided for each library (extracted from the C/C++ headers).
- Support only from `net8.0`+

## 📦 Libraries

<!-- XENOATOM_INTEROP BEGIN - DO NOT EDIT: this section below is automatically generated --->

The C/C++ header files from the [Alpine Linux](https://www.alpinelinux.org/) `v3.19` were used to generated automatically the .NET P/Invoke bindings.

The following libraries are available:

| Library | Native Version | Arch | NuGet |
| ------- | -------------- | ---- | ----- |
| [XenoAtom.Interop](https://github.com/XenoAtom/XenoAtom.Interop/tree/main/src/common)<br>This package provides the shared types `FixedArray#<T>` to interop between .NET and C/C++. | <br>`-` | `all` | [![NuGet](https://img.shields.io/nuget/v/XenoAtom.Interop.svg)](https://www.nuget.org/packages/XenoAtom.Interop) |
| [XenoAtom.Interop.musl](https://github.com/XenoAtom/XenoAtom.Interop/tree/main/src/musl)<br>musl libc is an implementation of the C standard library providing access to the Linux kernel syscalls. | [musl](https://musl.libc.org/)<br>`1.2.4_git20230717` | `linux-x64`, `linux-arm64` | [![NuGet](https://img.shields.io/nuget/v/XenoAtom.Interop.musl.svg)](https://www.nuget.org/packages/XenoAtom.Interop.musl) |
| [XenoAtom.Interop.libgit2](https://github.com/XenoAtom/XenoAtom.Interop/tree/main/src/libgit2)<br>libgit2 is a pure C implementation of the git core methods. | [libgit2](https://libgit2.org/)<br>`1.7.2` | `all` | [![NuGet](https://img.shields.io/nuget/v/XenoAtom.Interop.libgit2.svg)](https://www.nuget.org/packages/XenoAtom.Interop.libgit2) |
| [XenoAtom.Interop.sqlite](https://github.com/XenoAtom/XenoAtom.Interop/tree/main/src/sqlite)<br>SQLite is a small and fast SQL database engine. | [sqlite](https://www.sqlite.org/)<br>`3.44.2` | `all` | [![NuGet](https://img.shields.io/nuget/v/XenoAtom.Interop.sqlite.svg)](https://www.nuget.org/packages/XenoAtom.Interop.sqlite) |
| [XenoAtom.Interop.zlib](https://github.com/XenoAtom/XenoAtom.Interop/tree/main/src/zlib)<br>zlib compression library. | [zlib](https://zlib.net/)<br>`1.3.1` | `all` | [![NuGet](https://img.shields.io/nuget/v/XenoAtom.Interop.zlib.svg)](https://www.nuget.org/packages/XenoAtom.Interop.zlib) |

<!-- XENOATOM_INTEROP END - DO NOT EDIT --->

- `all`: The library is available for all supported architectures.
- For `musl` library, it will work if `musl` is installed on the system. Typically on Alpine Linux you don't need to install anything.
  So the targets `linux-musl-x64` and `linux-musl-arm64` are supported by default.

## 📜 User Guide

For more details on how to use XenoAtom.Interop, please visit the [user guide](https://github.com/XenoAtom/XenoAtom.Interop/blob/main/doc/readme.md).

## 🪪 License

This software is released under the [BSD-2-Clause license](https://opensource.org/licenses/BSD-2-Clause). 

## 🤗 Author

Alexandre Mutel aka [XenoAtom](https://XenoAtom.github.io).
