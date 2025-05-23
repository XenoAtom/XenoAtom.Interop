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
    public static unsafe partial class libdrm
    {
        /// <summary>
        /// SAREA drawable
        /// </summary>
        public partial struct drm_sarea_drawable
        {
            public uint stamp;
            
            public uint flags;
        }
        
        /// <summary>
        /// SAREA frame
        /// </summary>
        public partial struct drm_sarea_frame
        {
            public uint x;
            
            public uint y;
            
            public uint width;
            
            public uint height;
            
            public uint fullscreen;
        }
        
        /// <summary>
        /// SAREA
        /// </summary>
        public partial struct drm_sarea
        {
            /// <summary>
            /// first thing is always the DRM locking structure
            /// </summary>
            public libdrm.drm_hw_lock @lock;
            
            /// <todo>
            /// Use readers/writer lock for drm_sarea::drawable_lock
            /// </todo>
            public libdrm.drm_hw_lock drawable_lock;
            
            /// <summary>
            /// drawables
            /// </summary>
            public FixedArray256<libdrm.drm_sarea_drawable> drawableTable;
            
            /// <summary>
            /// frame
            /// </summary>
            public libdrm.drm_sarea_frame frame;
            
            public libdrm.drm_context_t dummy_context;
        }
    }
}
