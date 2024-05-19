// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;

namespace XenoAtom.Interop.CodeGen;

public record LibDescriptor
{
    public required string Name { get; init; }

    public required string Summary { get; init; }

    public string? CppDescription { get; init; }

    public required string Url { get; init; }

    public string? UrlDocumentation { get; init; }

    public CompatibleNativeNuGet[]? NativeNuGets { get; init; } = null;

    public string[] ApkDeps { get; init; } = [];

    public required Func<LibDescriptor, GeneratorBase>? Generator { get; init; } = null;

    public bool HasGeneratedFolder { get; init; } = true;

    public string? UsageInCSharp { get; init; }

    public string[] SupportedArchitectures { get; init; } = ["all"];
}

public record CompatibleNativeNuGet(string Name, string Version);