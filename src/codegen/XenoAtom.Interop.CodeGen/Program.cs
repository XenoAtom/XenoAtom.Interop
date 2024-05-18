// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XenoAtom.Interop.CodeGen;

public static partial class Program
{
    static async Task Main(string[] args)
    {
        var apkHelper = new ApkManager();
        // Multi-arch is only used by musl, the other libraries are multi-arch by default using the same x86_64 default headers
        apkHelper.Architectures = [ApkManager.DefaultArch, "aarch64"]; // "x86", "armv7" <- not supporting these for now (only for musl)
        await apkHelper.Initialize();

        // Always ensure that musl-dev is included
        await apkHelper.EnsureIncludes("musl-dev");

        var generators = new Dictionary<LibDescriptor, GeneratorBase>();

        // Initialize all generators
        foreach (var libDescriptor in LibDescriptors)
        {
            Console.WriteLine($"=================================================");
            Console.WriteLine($"Initializing {libDescriptor.Name} generator");
            Console.WriteLine($"=================================================");
            var generator = libDescriptor.Generator!(libDescriptor);
            generators[libDescriptor] = generator;
            await generator.Initialize(apkHelper);
        }

        Console.WriteLine();

        // Run all generators
        foreach (var libDescriptor in LibDescriptors)
        {
            Console.WriteLine($"=================================================");
            Console.WriteLine($"Generating {libDescriptor.Name} bindings");
            Console.WriteLine($"=================================================");
            var generator = generators[libDescriptor];
            await generator.Run();
        }
    }
}