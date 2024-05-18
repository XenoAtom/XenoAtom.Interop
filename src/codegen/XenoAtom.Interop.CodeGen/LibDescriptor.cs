// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;

namespace XenoAtom.Interop.CodeGen;

public record LibDescriptor
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string Url { get; init; }

    public CompatibleNativeNuGet[]? NativeNuGets { get; init; } = null;

    public required Func<LibDescriptor, GeneratorBase>? Generator { get; init; } = null;

    public bool HasGeneratedFolder { get; init; } = true;
}

public record CompatibleNativeNuGet(string Name, string Version);