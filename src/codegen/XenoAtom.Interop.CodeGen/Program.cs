// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Threading.Tasks;

namespace XenoAtom.Interop.CodeGen;

public static partial class Program
{
    static async Task Main(string[] args)
    {
        var genManager = new GeneratorManager(LibDescriptors);

        //genManager.LibFilter.Add("libdrm");
        
        await genManager.Initialize();
        await genManager.Run();
    }
}