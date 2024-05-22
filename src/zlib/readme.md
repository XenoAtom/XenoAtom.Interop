# XenoAtom.Interop.zlib [![Build Status](https://github.com/XenoAtom/XenoAtom.Interop/actions/workflows/ci_build_zlib.yml/badge.svg)](https://github.com/XenoAtom/XenoAtom.Interop/actions/workflows/ci_build_zlib.yml) [![NuGet](https://img.shields.io/nuget/v/XenoAtom.Interop.zlib.svg)](https://www.nuget.org/packages/XenoAtom.Interop.zlib/)

This package provides a low-level and modern .NET P/Invoke wrapper around the zlib compression library.

## ♻️ XenoAtom.Interop

This package is part of the [XenoAtom.Interop](https://github.com/XenoAtom/XenoAtom.Interop) project.

zlib compression library. For more information, see [zlib](https://zlib.net/) website.
## 💻 Usage

After installing the package, you can access the library through the static class `XenoAtom.Interop.zlib`.

For more information, see the official documentation at https://zlib.net/manual.html.

Example of using this library in C#:

```csharp
using static XenoAtom.Interop.zlib;

// Compress a buffer
var data = Encoding.UTF8.GetBytes("Hello, World!");
var compressedData = new byte[32];
var ret = compress(compressedData, out var compressedSize, data);
if (ret != Z_OK)
{
    // ...
}

// Decompress a buffer
var decompressedData = new byte[32];
ret = uncompress(decompressedData, out var decompressedSize, compressedData);
if (ret != Z_OK)
{
    // ...
}
```
## 📦 Compatible Native Binaries

This library does not provide C native binaries but only P/Invoke .NET bindings to `zlib` `1.3.1-r0`.

If the native library is already installed on your system, check the version installed. If you are using this library on Alpine Linux, see the compatible version in the [Supported API](#supported-api) section below.
Other OS might require a different setup.

For convenience, you can install an existing NuGet package (outside of XenoAtom.Interop project) that is providing native binaries.
The following packages were tested and are compatible with **XenoAtom.Interop.zlib**:

| NuGet Package with Native Binaries | Version |
|------------------------------------|---------|
| [elskom.zlib.redist.win](https://www.nuget.org/packages/elskom.zlib.redist.win) | `1.2.13`
| [elskom.zlib.redist.linux](https://www.nuget.org/packages/elskom.zlib.redist.linux) | `1.2.13`
| [elskom.zlib.redist.osx](https://www.nuget.org/packages/elskom.zlib.redist.osx) | `1.2.13`


## 📚 Supported API

> This package is based on the following header version:
> 
> - zlib C include headers: [`zlib-dev`](https://pkgs.alpinelinux.org/package/v3.19/main/x86_64/zlib-dev)
> - Version: `1.3.1-r0`
> - Distribution: AlpineLinux `v3.19`

The following API were automatically generated from the C/C++ code:

- zlib.h: `adler32`, `adler32_combine`, `adler32_z`, `compress`, `compress2`, `compressBound`, `crc32`, `crc32_combine`, `crc32_combine_gen`, `crc32_combine_op`, `crc32_z`, `deflate`, `deflateBound`, `deflateCopy`, `deflateEnd`, `deflateGetDictionary`, `deflateInit2_`, `deflateInit_`, `deflateParams`, `deflatePending`, `deflatePrime`, `deflateReset`, `deflateSetDictionary`, `deflateSetHeader`, `deflateTune`, `inflate`, `inflateBack`, `inflateBackEnd`, `inflateBackInit_`, `inflateCopy`, `inflateEnd`, `inflateGetDictionary`, `inflateGetHeader`, `inflateInit2_`, `inflateInit_`, `inflateMark`, `inflatePrime`, `inflateReset`, `inflateReset2`, `inflateSetDictionary`, `inflateSync`, `uncompress`, `uncompress2`, `zlibCompileFlags`, `zlibVersion`


## 🪪 License

This software is released under the [BSD-2-Clause license](https://opensource.org/licenses/BSD-2-Clause). 

## 🤗 Author

Alexandre Mutel aka [xoofx](https://xoofx.github.io).
