// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.
using System.Threading.Tasks;
using CppAst.CodeGen.CSharp;

namespace XenoAtom.Interop.CodeGen.common;

internal class EmptyGenerator(LibDescriptor descriptor) : GeneratorBase(descriptor)
{
    protected override Task<CSharpCompilation?> Generate() => Task.FromResult((CSharpCompilation?)null);
}