// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using CppAst;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop.CodeGen;

public static class CppTypeExtensions
{
    public static bool TryGetElementTypeFromPointer(this CppType cppType, out bool isConst, [NotNullWhen(true)] out CppType? elementType)
    {
        isConst = false;
        if (cppType is CppPointerType cppPointerType)
        {
            if (cppPointerType.ElementType is CppQualifiedType elementType1 && elementType1.Qualifier == CppTypeQualifier.Const)
            {
                isConst = true;
                elementType = elementType1.ElementType;
                return true;
            }

            elementType = cppPointerType.ElementType;
            return true;
        }
        elementType = null;
        return false;
    }
}