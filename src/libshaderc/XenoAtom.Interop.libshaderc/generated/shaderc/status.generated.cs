//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
namespace XenoAtom.Interop
{
    public static unsafe partial class libshaderc
    {
        /// <summary>
        /// Indicate the status of a compilation.
        /// </summary>
        public enum shaderc_compilation_status : uint
        {
            shaderc_compilation_status_success = unchecked((uint)0),
            
            /// <summary>
            /// error stage deduction
            /// </summary>
            shaderc_compilation_status_invalid_stage = unchecked((uint)1),
            
            shaderc_compilation_status_compilation_error = unchecked((uint)2),
            
            /// <summary>
            /// unexpected failure
            /// </summary>
            shaderc_compilation_status_internal_error = unchecked((uint)3),
            
            shaderc_compilation_status_null_result_object = unchecked((uint)4),
            
            shaderc_compilation_status_invalid_assembly = unchecked((uint)5),
            
            shaderc_compilation_status_validation_error = unchecked((uint)6),
            
            shaderc_compilation_status_transformation_error = unchecked((uint)7),
            
            shaderc_compilation_status_configuration_error = unchecked((uint)8),
        }
        
        public const libshaderc.shaderc_compilation_status shaderc_compilation_status_success = shaderc_compilation_status.shaderc_compilation_status_success;
        
        /// <summary>
        /// error stage deduction
        /// </summary>
        public const libshaderc.shaderc_compilation_status shaderc_compilation_status_invalid_stage = shaderc_compilation_status.shaderc_compilation_status_invalid_stage;
        
        public const libshaderc.shaderc_compilation_status shaderc_compilation_status_compilation_error = shaderc_compilation_status.shaderc_compilation_status_compilation_error;
        
        /// <summary>
        /// unexpected failure
        /// </summary>
        public const libshaderc.shaderc_compilation_status shaderc_compilation_status_internal_error = shaderc_compilation_status.shaderc_compilation_status_internal_error;
        
        public const libshaderc.shaderc_compilation_status shaderc_compilation_status_null_result_object = shaderc_compilation_status.shaderc_compilation_status_null_result_object;
        
        public const libshaderc.shaderc_compilation_status shaderc_compilation_status_invalid_assembly = shaderc_compilation_status.shaderc_compilation_status_invalid_assembly;
        
        public const libshaderc.shaderc_compilation_status shaderc_compilation_status_validation_error = shaderc_compilation_status.shaderc_compilation_status_validation_error;
        
        public const libshaderc.shaderc_compilation_status shaderc_compilation_status_transformation_error = shaderc_compilation_status.shaderc_compilation_status_transformation_error;
        
        public const libshaderc.shaderc_compilation_status shaderc_compilation_status_configuration_error = shaderc_compilation_status.shaderc_compilation_status_configuration_error;
    }
}
