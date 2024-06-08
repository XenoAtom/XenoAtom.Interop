// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

namespace XenoAtom.Interop.Tests;

using static XenoAtom.Interop.libshaderc;

[TestClass]
public class BasicTests
{
    [TestMethod]
    public unsafe void TestSimple()
    {
        var compiler = shaderc_compiler_initialize();

        var options = shaderc_compile_options_initialize();

        shaderc_compile_options_set_optimization_level(options, shaderc_optimization_level_zero);

        var result = shaderc_compile_into_spv(compiler, "#version 460\nvoid main(){ gl_Position = vec4(1.); }", shaderc_glsl_vertex_shader, "test", "main", options);

        var status = shaderc_result_get_compilation_status(result);
        string errorMessage = "Error compiling shader";
        if (status != shaderc_compilation_status_success)
        {
            errorMessage = shaderc_result_get_error_message(result);
        }

        Assert.AreEqual(shaderc_compilation_status_success, status, errorMessage);

        var data = shaderc_result_get_bytes(result);
        var size = shaderc_result_get_length(result);
        var span = new Span<byte>(data, (int)size);
        
        Assert.IsTrue(span.Length > 0);

        shaderc_result_release(result);
        shaderc_compile_options_release(options);
        shaderc_compiler_release(compiler);
    }
}