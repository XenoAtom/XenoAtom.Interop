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
    public static unsafe partial class vulkan
    {
        public enum StdVideoDecodeH264FieldOrderCount : uint
        {
            STD_VIDEO_DECODE_H264_FIELD_ORDER_COUNT_TOP = unchecked((uint)0),
            
            STD_VIDEO_DECODE_H264_FIELD_ORDER_COUNT_BOTTOM = unchecked((uint)1),
            
            STD_VIDEO_DECODE_H264_FIELD_ORDER_COUNT_INVALID = unchecked((uint)2147483647),
            
            STD_VIDEO_DECODE_H264_FIELD_ORDER_COUNT_MAX_ENUM = unchecked((uint)2147483647),
        }
        
        public const vulkan.StdVideoDecodeH264FieldOrderCount STD_VIDEO_DECODE_H264_FIELD_ORDER_COUNT_TOP = StdVideoDecodeH264FieldOrderCount.STD_VIDEO_DECODE_H264_FIELD_ORDER_COUNT_TOP;
        
        public const vulkan.StdVideoDecodeH264FieldOrderCount STD_VIDEO_DECODE_H264_FIELD_ORDER_COUNT_BOTTOM = StdVideoDecodeH264FieldOrderCount.STD_VIDEO_DECODE_H264_FIELD_ORDER_COUNT_BOTTOM;
        
        public const vulkan.StdVideoDecodeH264FieldOrderCount STD_VIDEO_DECODE_H264_FIELD_ORDER_COUNT_INVALID = StdVideoDecodeH264FieldOrderCount.STD_VIDEO_DECODE_H264_FIELD_ORDER_COUNT_INVALID;
        
        public const vulkan.StdVideoDecodeH264FieldOrderCount STD_VIDEO_DECODE_H264_FIELD_ORDER_COUNT_MAX_ENUM = StdVideoDecodeH264FieldOrderCount.STD_VIDEO_DECODE_H264_FIELD_ORDER_COUNT_MAX_ENUM;
        
        public partial struct StdVideoDecodeH264PictureInfoFlags
        {
            private uint __bitfield__0;
            
            public uint field_pic_flag
            {
                get
                {
                    return unchecked((uint)((__bitfield__0 >> 0) & 0b1));
                }
                set
                {
                    __bitfield__0 = (__bitfield__0 & unchecked((uint)0b11111111111111111111111111111110)) | ((((uint)value) & (unchecked((uint)0b1)) << 0));
                }
            }
            
            public uint is_intra
            {
                get
                {
                    return unchecked((uint)((__bitfield__0 >> 1) & 0b1));
                }
                set
                {
                    __bitfield__0 = (__bitfield__0 & unchecked((uint)0b11111111111111111111111111111101)) | ((((uint)value) & (unchecked((uint)0b1)) << 1));
                }
            }
            
            public uint IdrPicFlag
            {
                get
                {
                    return unchecked((uint)((__bitfield__0 >> 2) & 0b1));
                }
                set
                {
                    __bitfield__0 = (__bitfield__0 & unchecked((uint)0b11111111111111111111111111111011)) | ((((uint)value) & (unchecked((uint)0b1)) << 2));
                }
            }
            
            public uint bottom_field_flag
            {
                get
                {
                    return unchecked((uint)((__bitfield__0 >> 3) & 0b1));
                }
                set
                {
                    __bitfield__0 = (__bitfield__0 & unchecked((uint)0b11111111111111111111111111110111)) | ((((uint)value) & (unchecked((uint)0b1)) << 3));
                }
            }
            
            public uint is_reference
            {
                get
                {
                    return unchecked((uint)((__bitfield__0 >> 4) & 0b1));
                }
                set
                {
                    __bitfield__0 = (__bitfield__0 & unchecked((uint)0b11111111111111111111111111101111)) | ((((uint)value) & (unchecked((uint)0b1)) << 4));
                }
            }
            
            public uint complementary_field_pair
            {
                get
                {
                    return unchecked((uint)((__bitfield__0 >> 5) & 0b1));
                }
                set
                {
                    __bitfield__0 = (__bitfield__0 & unchecked((uint)0b11111111111111111111111111011111)) | ((((uint)value) & (unchecked((uint)0b1)) << 5));
                }
            }
        }
        
        public unsafe partial struct StdVideoDecodeH264PictureInfo
        {
            public vulkan.StdVideoDecodeH264PictureInfoFlags flags;
            
            public byte seq_parameter_set_id;
            
            public byte pic_parameter_set_id;
            
            public byte reserved1;
            
            public byte reserved2;
            
            public ushort frame_num;
            
            public ushort idr_pic_id;
            
            public fixed int PicOrderCnt[2];
        }
        
        public partial struct StdVideoDecodeH264ReferenceInfoFlags
        {
            private uint __bitfield__0;
            
            public uint top_field_flag
            {
                get
                {
                    return unchecked((uint)((__bitfield__0 >> 0) & 0b1));
                }
                set
                {
                    __bitfield__0 = (__bitfield__0 & unchecked((uint)0b11111111111111111111111111111110)) | ((((uint)value) & (unchecked((uint)0b1)) << 0));
                }
            }
            
            public uint bottom_field_flag
            {
                get
                {
                    return unchecked((uint)((__bitfield__0 >> 1) & 0b1));
                }
                set
                {
                    __bitfield__0 = (__bitfield__0 & unchecked((uint)0b11111111111111111111111111111101)) | ((((uint)value) & (unchecked((uint)0b1)) << 1));
                }
            }
            
            public uint used_for_long_term_reference
            {
                get
                {
                    return unchecked((uint)((__bitfield__0 >> 2) & 0b1));
                }
                set
                {
                    __bitfield__0 = (__bitfield__0 & unchecked((uint)0b11111111111111111111111111111011)) | ((((uint)value) & (unchecked((uint)0b1)) << 2));
                }
            }
            
            public uint is_non_existing
            {
                get
                {
                    return unchecked((uint)((__bitfield__0 >> 3) & 0b1));
                }
                set
                {
                    __bitfield__0 = (__bitfield__0 & unchecked((uint)0b11111111111111111111111111110111)) | ((((uint)value) & (unchecked((uint)0b1)) << 3));
                }
            }
        }
        
        public unsafe partial struct StdVideoDecodeH264ReferenceInfo
        {
            public vulkan.StdVideoDecodeH264ReferenceInfoFlags flags;
            
            public ushort FrameNum;
            
            public ushort reserved;
            
            public fixed int PicOrderCnt[2];
        }
    }
}
