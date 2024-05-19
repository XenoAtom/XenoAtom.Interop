# XenoAtom.Interop [![Build Status](https://github.com/XenoAtom/XenoAtom.Interop/actions/workflows/ci_build_common.yml/badge.svg)](https://github.com/XenoAtom/XenoAtom.Interop/actions/workflows/ci_build_common.yml) [![NuGet](https://img.shields.io/nuget/v/XenoAtom.Interop.svg)](https://www.nuget.org/packages/XenoAtom.Interop/)

This package provides the shared types `FixedArray#<T>` to interop between .NET and C/C++.

## ♻️ XenoAtom.Interop

This package is part of the [XenoAtom.Interop](https://github.com/XenoAtom/XenoAtom.Interop) project.


## 💻 Usage

Example of using this library in C#:

```csharp
using XenoAtom.Interop;

// Create a fixed array of 10 integers
var array = new FixedArray10<int>();
array[0] = 42;
array[1] = 43;
// ...
```


## 🪪 License

This software is released under the [BSD-2-Clause license](https://opensource.org/licenses/BSD-2-Clause). 

## 🤗 Author

Alexandre Mutel aka [xoofx](https://xoofx.github.io).
