// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using CppAst.CodeGen.CSharp;
using System;
using System.Linq;

namespace XenoAtom.Interop.CodeGen;

public static class CSharpCompilationExtension
{
    public static CSharpClass GetLibClassFromGeneratedFile(this CSharpGeneratedFile csGeneratedFile)
        => csGeneratedFile.Members.OfType<CSharpNamespace>().First().Members.OfType<CSharpClass>().First();
    public static CSharpClass GetLibClassFromGeneratedFile(this CSharpCompilation csCompilation, string fromGeneratedHeaderFile)
    {
        var csGeneratedFile = csCompilation.Members.OfType<CSharpGeneratedFile>().FirstOrDefault(x => x.FilePath.FullName == fromGeneratedHeaderFile);
        return csGeneratedFile == null ? throw new Exception($"Cannot find {fromGeneratedHeaderFile}") : GetLibClassFromGeneratedFile(csGeneratedFile);
    }
}