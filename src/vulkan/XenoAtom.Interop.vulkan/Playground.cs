// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

namespace XenoAtom.Interop
{
    public class Playground
    {
        public static int GetSize()
        {
            return BlockSize(VkFormat.A2B10G10R10SscaledPack32);
        }


        /// <summary>
        /// Vulkan format definitions
        /// </summary>
        public enum VkFormat
        {
            /// <unmanaged>VK_FORMAT_UNDEFINED</unmanaged>
            Undefined = 0,
            /// <unmanaged>VK_FORMAT_R4G4_UNORM_PACK8</unmanaged>
            R4G4UnormPack8 = 1,
            /// <unmanaged>VK_FORMAT_R4G4B4A4_UNORM_PACK16</unmanaged>
            R4G4B4A4UnormPack16 = 2,
            /// <unmanaged>VK_FORMAT_B4G4R4A4_UNORM_PACK16</unmanaged>
            B4G4R4A4UnormPack16 = 3,
            /// <unmanaged>VK_FORMAT_R5G6B5_UNORM_PACK16</unmanaged>
            R5G6B5UnormPack16 = 4,
            /// <unmanaged>VK_FORMAT_B5G6R5_UNORM_PACK16</unmanaged>
            B5G6R5UnormPack16 = 5,
            /// <unmanaged>VK_FORMAT_R5G5B5A1_UNORM_PACK16</unmanaged>
            R5G5B5A1UnormPack16 = 6,
            /// <unmanaged>VK_FORMAT_B5G5R5A1_UNORM_PACK16</unmanaged>
            B5G5R5A1UnormPack16 = 7,
            /// <unmanaged>VK_FORMAT_A1R5G5B5_UNORM_PACK16</unmanaged>
            A1R5G5B5UnormPack16 = 8,
            /// <unmanaged>VK_FORMAT_R8_UNORM</unmanaged>
            R8Unorm = 9,
            /// <unmanaged>VK_FORMAT_R8_SNORM</unmanaged>
            R8Snorm = 10,
            /// <unmanaged>VK_FORMAT_R8_USCALED</unmanaged>
            R8Uscaled = 11,
            /// <unmanaged>VK_FORMAT_R8_SSCALED</unmanaged>
            R8Sscaled = 12,
            /// <unmanaged>VK_FORMAT_R8_UINT</unmanaged>
            R8Uint = 13,
            /// <unmanaged>VK_FORMAT_R8_SINT</unmanaged>
            R8Sint = 14,
            /// <unmanaged>VK_FORMAT_R8_SRGB</unmanaged>
            R8Srgb = 15,
            /// <unmanaged>VK_FORMAT_R8G8_UNORM</unmanaged>
            R8G8Unorm = 16,
            /// <unmanaged>VK_FORMAT_R8G8_SNORM</unmanaged>
            R8G8Snorm = 17,
            /// <unmanaged>VK_FORMAT_R8G8_USCALED</unmanaged>
            R8G8Uscaled = 18,
            /// <unmanaged>VK_FORMAT_R8G8_SSCALED</unmanaged>
            R8G8Sscaled = 19,
            /// <unmanaged>VK_FORMAT_R8G8_UINT</unmanaged>
            R8G8Uint = 20,
            /// <unmanaged>VK_FORMAT_R8G8_SINT</unmanaged>
            R8G8Sint = 21,
            /// <unmanaged>VK_FORMAT_R8G8_SRGB</unmanaged>
            R8G8Srgb = 22,
            /// <unmanaged>VK_FORMAT_R8G8B8_UNORM</unmanaged>
            R8G8B8Unorm = 23,
            /// <unmanaged>VK_FORMAT_R8G8B8_SNORM</unmanaged>
            R8G8B8Snorm = 24,
            /// <unmanaged>VK_FORMAT_R8G8B8_USCALED</unmanaged>
            R8G8B8Uscaled = 25,
            /// <unmanaged>VK_FORMAT_R8G8B8_SSCALED</unmanaged>
            R8G8B8Sscaled = 26,
            /// <unmanaged>VK_FORMAT_R8G8B8_UINT</unmanaged>
            R8G8B8Uint = 27,
            /// <unmanaged>VK_FORMAT_R8G8B8_SINT</unmanaged>
            R8G8B8Sint = 28,
            /// <unmanaged>VK_FORMAT_R8G8B8_SRGB</unmanaged>
            R8G8B8Srgb = 29,
            /// <unmanaged>VK_FORMAT_B8G8R8_UNORM</unmanaged>
            B8G8R8Unorm = 30,
            /// <unmanaged>VK_FORMAT_B8G8R8_SNORM</unmanaged>
            B8G8R8Snorm = 31,
            /// <unmanaged>VK_FORMAT_B8G8R8_USCALED</unmanaged>
            B8G8R8Uscaled = 32,
            /// <unmanaged>VK_FORMAT_B8G8R8_SSCALED</unmanaged>
            B8G8R8Sscaled = 33,
            /// <unmanaged>VK_FORMAT_B8G8R8_UINT</unmanaged>
            B8G8R8Uint = 34,
            /// <unmanaged>VK_FORMAT_B8G8R8_SINT</unmanaged>
            B8G8R8Sint = 35,
            /// <unmanaged>VK_FORMAT_B8G8R8_SRGB</unmanaged>
            B8G8R8Srgb = 36,
            /// <unmanaged>VK_FORMAT_R8G8B8A8_UNORM</unmanaged>
            R8G8B8A8Unorm = 37,
            /// <unmanaged>VK_FORMAT_R8G8B8A8_SNORM</unmanaged>
            R8G8B8A8Snorm = 38,
            /// <unmanaged>VK_FORMAT_R8G8B8A8_USCALED</unmanaged>
            R8G8B8A8Uscaled = 39,
            /// <unmanaged>VK_FORMAT_R8G8B8A8_SSCALED</unmanaged>
            R8G8B8A8Sscaled = 40,
            /// <unmanaged>VK_FORMAT_R8G8B8A8_UINT</unmanaged>
            R8G8B8A8Uint = 41,
            /// <unmanaged>VK_FORMAT_R8G8B8A8_SINT</unmanaged>
            R8G8B8A8Sint = 42,
            /// <unmanaged>VK_FORMAT_R8G8B8A8_SRGB</unmanaged>
            R8G8B8A8Srgb = 43,
            /// <unmanaged>VK_FORMAT_B8G8R8A8_UNORM</unmanaged>
            B8G8R8A8Unorm = 44,
            /// <unmanaged>VK_FORMAT_B8G8R8A8_SNORM</unmanaged>
            B8G8R8A8Snorm = 45,
            /// <unmanaged>VK_FORMAT_B8G8R8A8_USCALED</unmanaged>
            B8G8R8A8Uscaled = 46,
            /// <unmanaged>VK_FORMAT_B8G8R8A8_SSCALED</unmanaged>
            B8G8R8A8Sscaled = 47,
            /// <unmanaged>VK_FORMAT_B8G8R8A8_UINT</unmanaged>
            B8G8R8A8Uint = 48,
            /// <unmanaged>VK_FORMAT_B8G8R8A8_SINT</unmanaged>
            B8G8R8A8Sint = 49,
            /// <unmanaged>VK_FORMAT_B8G8R8A8_SRGB</unmanaged>
            B8G8R8A8Srgb = 50,
            /// <unmanaged>VK_FORMAT_A8B8G8R8_UNORM_PACK32</unmanaged>
            A8B8G8R8UnormPack32 = 51,
            /// <unmanaged>VK_FORMAT_A8B8G8R8_SNORM_PACK32</unmanaged>
            A8B8G8R8SnormPack32 = 52,
            /// <unmanaged>VK_FORMAT_A8B8G8R8_USCALED_PACK32</unmanaged>
            A8B8G8R8UscaledPack32 = 53,
            /// <unmanaged>VK_FORMAT_A8B8G8R8_SSCALED_PACK32</unmanaged>
            A8B8G8R8SscaledPack32 = 54,
            /// <unmanaged>VK_FORMAT_A8B8G8R8_UINT_PACK32</unmanaged>
            A8B8G8R8UintPack32 = 55,
            /// <unmanaged>VK_FORMAT_A8B8G8R8_SINT_PACK32</unmanaged>
            A8B8G8R8SintPack32 = 56,
            /// <unmanaged>VK_FORMAT_A8B8G8R8_SRGB_PACK32</unmanaged>
            A8B8G8R8SrgbPack32 = 57,
            /// <unmanaged>VK_FORMAT_A2R10G10B10_UNORM_PACK32</unmanaged>
            A2R10G10B10UnormPack32 = 58,
            /// <unmanaged>VK_FORMAT_A2R10G10B10_SNORM_PACK32</unmanaged>
            A2R10G10B10SnormPack32 = 59,
            /// <unmanaged>VK_FORMAT_A2R10G10B10_USCALED_PACK32</unmanaged>
            A2R10G10B10UscaledPack32 = 60,
            /// <unmanaged>VK_FORMAT_A2R10G10B10_SSCALED_PACK32</unmanaged>
            A2R10G10B10SscaledPack32 = 61,
            /// <unmanaged>VK_FORMAT_A2R10G10B10_UINT_PACK32</unmanaged>
            A2R10G10B10UintPack32 = 62,
            /// <unmanaged>VK_FORMAT_A2R10G10B10_SINT_PACK32</unmanaged>
            A2R10G10B10SintPack32 = 63,
            /// <unmanaged>VK_FORMAT_A2B10G10R10_UNORM_PACK32</unmanaged>
            A2B10G10R10UnormPack32 = 64,
            /// <unmanaged>VK_FORMAT_A2B10G10R10_SNORM_PACK32</unmanaged>
            A2B10G10R10SnormPack32 = 65,
            /// <unmanaged>VK_FORMAT_A2B10G10R10_USCALED_PACK32</unmanaged>
            A2B10G10R10UscaledPack32 = 66,
            /// <unmanaged>VK_FORMAT_A2B10G10R10_SSCALED_PACK32</unmanaged>
            A2B10G10R10SscaledPack32 = 67,
            /// <unmanaged>VK_FORMAT_A2B10G10R10_UINT_PACK32</unmanaged>
            A2B10G10R10UintPack32 = 68,
            /// <unmanaged>VK_FORMAT_A2B10G10R10_SINT_PACK32</unmanaged>
            A2B10G10R10SintPack32 = 69,
            /// <unmanaged>VK_FORMAT_R16_UNORM</unmanaged>
            R16Unorm = 70,
            /// <unmanaged>VK_FORMAT_R16_SNORM</unmanaged>
            R16Snorm = 71,
            /// <unmanaged>VK_FORMAT_R16_USCALED</unmanaged>
            R16Uscaled = 72,
            /// <unmanaged>VK_FORMAT_R16_SSCALED</unmanaged>
            R16Sscaled = 73,
            /// <unmanaged>VK_FORMAT_R16_UINT</unmanaged>
            R16Uint = 74,
            /// <unmanaged>VK_FORMAT_R16_SINT</unmanaged>
            R16Sint = 75,
            /// <unmanaged>VK_FORMAT_R16_SFLOAT</unmanaged>
            R16Sfloat = 76,
            /// <unmanaged>VK_FORMAT_R16G16_UNORM</unmanaged>
            R16G16Unorm = 77,
            /// <unmanaged>VK_FORMAT_R16G16_SNORM</unmanaged>
            R16G16Snorm = 78,
            /// <unmanaged>VK_FORMAT_R16G16_USCALED</unmanaged>
            R16G16Uscaled = 79,
            /// <unmanaged>VK_FORMAT_R16G16_SSCALED</unmanaged>
            R16G16Sscaled = 80,
            /// <unmanaged>VK_FORMAT_R16G16_UINT</unmanaged>
            R16G16Uint = 81,
            /// <unmanaged>VK_FORMAT_R16G16_SINT</unmanaged>
            R16G16Sint = 82,
            /// <unmanaged>VK_FORMAT_R16G16_SFLOAT</unmanaged>
            R16G16Sfloat = 83,
            /// <unmanaged>VK_FORMAT_R16G16B16_UNORM</unmanaged>
            R16G16B16Unorm = 84,
            /// <unmanaged>VK_FORMAT_R16G16B16_SNORM</unmanaged>
            R16G16B16Snorm = 85,
            /// <unmanaged>VK_FORMAT_R16G16B16_USCALED</unmanaged>
            R16G16B16Uscaled = 86,
            /// <unmanaged>VK_FORMAT_R16G16B16_SSCALED</unmanaged>
            R16G16B16Sscaled = 87,
            /// <unmanaged>VK_FORMAT_R16G16B16_UINT</unmanaged>
            R16G16B16Uint = 88,
            /// <unmanaged>VK_FORMAT_R16G16B16_SINT</unmanaged>
            R16G16B16Sint = 89,
            /// <unmanaged>VK_FORMAT_R16G16B16_SFLOAT</unmanaged>
            R16G16B16Sfloat = 90,
            /// <unmanaged>VK_FORMAT_R16G16B16A16_UNORM</unmanaged>
            R16G16B16A16Unorm = 91,
            /// <unmanaged>VK_FORMAT_R16G16B16A16_SNORM</unmanaged>
            R16G16B16A16Snorm = 92,
            /// <unmanaged>VK_FORMAT_R16G16B16A16_USCALED</unmanaged>
            R16G16B16A16Uscaled = 93,
            /// <unmanaged>VK_FORMAT_R16G16B16A16_SSCALED</unmanaged>
            R16G16B16A16Sscaled = 94,
            /// <unmanaged>VK_FORMAT_R16G16B16A16_UINT</unmanaged>
            R16G16B16A16Uint = 95,
            /// <unmanaged>VK_FORMAT_R16G16B16A16_SINT</unmanaged>
            R16G16B16A16Sint = 96,
            /// <unmanaged>VK_FORMAT_R16G16B16A16_SFLOAT</unmanaged>
            R16G16B16A16Sfloat = 97,
            /// <unmanaged>VK_FORMAT_R32_UINT</unmanaged>
            R32Uint = 98,
            /// <unmanaged>VK_FORMAT_R32_SINT</unmanaged>
            R32Sint = 99,
            /// <unmanaged>VK_FORMAT_R32_SFLOAT</unmanaged>
            R32Sfloat = 100,
            /// <unmanaged>VK_FORMAT_R32G32_UINT</unmanaged>
            R32G32Uint = 101,
            /// <unmanaged>VK_FORMAT_R32G32_SINT</unmanaged>
            R32G32Sint = 102,
            /// <unmanaged>VK_FORMAT_R32G32_SFLOAT</unmanaged>
            R32G32Sfloat = 103,
            /// <unmanaged>VK_FORMAT_R32G32B32_UINT</unmanaged>
            R32G32B32Uint = 104,
            /// <unmanaged>VK_FORMAT_R32G32B32_SINT</unmanaged>
            R32G32B32Sint = 105,
            /// <unmanaged>VK_FORMAT_R32G32B32_SFLOAT</unmanaged>
            R32G32B32Sfloat = 106,
            /// <unmanaged>VK_FORMAT_R32G32B32A32_UINT</unmanaged>
            R32G32B32A32Uint = 107,
            /// <unmanaged>VK_FORMAT_R32G32B32A32_SINT</unmanaged>
            R32G32B32A32Sint = 108,
            /// <unmanaged>VK_FORMAT_R32G32B32A32_SFLOAT</unmanaged>
            R32G32B32A32Sfloat = 109,
            /// <unmanaged>VK_FORMAT_R64_UINT</unmanaged>
            R64Uint = 110,
            /// <unmanaged>VK_FORMAT_R64_SINT</unmanaged>
            R64Sint = 111,
            /// <unmanaged>VK_FORMAT_R64_SFLOAT</unmanaged>
            R64Sfloat = 112,
            /// <unmanaged>VK_FORMAT_R64G64_UINT</unmanaged>
            R64G64Uint = 113,
            /// <unmanaged>VK_FORMAT_R64G64_SINT</unmanaged>
            R64G64Sint = 114,
            /// <unmanaged>VK_FORMAT_R64G64_SFLOAT</unmanaged>
            R64G64Sfloat = 115,
            /// <unmanaged>VK_FORMAT_R64G64B64_UINT</unmanaged>
            R64G64B64Uint = 116,
            /// <unmanaged>VK_FORMAT_R64G64B64_SINT</unmanaged>
            R64G64B64Sint = 117,
            /// <unmanaged>VK_FORMAT_R64G64B64_SFLOAT</unmanaged>
            R64G64B64Sfloat = 118,
            /// <unmanaged>VK_FORMAT_R64G64B64A64_UINT</unmanaged>
            R64G64B64A64Uint = 119,
            /// <unmanaged>VK_FORMAT_R64G64B64A64_SINT</unmanaged>
            R64G64B64A64Sint = 120,
            /// <unmanaged>VK_FORMAT_R64G64B64A64_SFLOAT</unmanaged>
            R64G64B64A64Sfloat = 121,
            /// <unmanaged>VK_FORMAT_B10G11R11_UFLOAT_PACK32</unmanaged>
            B10G11R11UfloatPack32 = 122,
            /// <unmanaged>VK_FORMAT_E5B9G9R9_UFLOAT_PACK32</unmanaged>
            E5B9G9R9UfloatPack32 = 123,
            /// <unmanaged>VK_FORMAT_D16_UNORM</unmanaged>
            D16Unorm = 124,
            /// <unmanaged>VK_FORMAT_X8_D24_UNORM_PACK32</unmanaged>
            X8D24UnormPack32 = 125,
            /// <unmanaged>VK_FORMAT_D32_SFLOAT</unmanaged>
            D32Sfloat = 126,
            /// <unmanaged>VK_FORMAT_S8_UINT</unmanaged>
            S8Uint = 127,
            /// <unmanaged>VK_FORMAT_D16_UNORM_S8_UINT</unmanaged>
            D16UnormS8Uint = 128,
            /// <unmanaged>VK_FORMAT_D24_UNORM_S8_UINT</unmanaged>
            D24UnormS8Uint = 129,
            /// <unmanaged>VK_FORMAT_D32_SFLOAT_S8_UINT</unmanaged>
            D32SfloatS8Uint = 130,
            /// <unmanaged>VK_FORMAT_BC1_RGB_UNORM_BLOCK</unmanaged>
            Bc1RgbUnormBlock = 131,
            /// <unmanaged>VK_FORMAT_BC1_RGB_SRGB_BLOCK</unmanaged>
            Bc1RgbSrgbBlock = 132,
            /// <unmanaged>VK_FORMAT_BC1_RGBA_UNORM_BLOCK</unmanaged>
            Bc1RgbaUnormBlock = 133,
            /// <unmanaged>VK_FORMAT_BC1_RGBA_SRGB_BLOCK</unmanaged>
            Bc1RgbaSrgbBlock = 134,
            /// <unmanaged>VK_FORMAT_BC2_UNORM_BLOCK</unmanaged>
            Bc2UnormBlock = 135,
            /// <unmanaged>VK_FORMAT_BC2_SRGB_BLOCK</unmanaged>
            Bc2SrgbBlock = 136,
            /// <unmanaged>VK_FORMAT_BC3_UNORM_BLOCK</unmanaged>
            Bc3UnormBlock = 137,
            /// <unmanaged>VK_FORMAT_BC3_SRGB_BLOCK</unmanaged>
            Bc3SrgbBlock = 138,
            /// <unmanaged>VK_FORMAT_BC4_UNORM_BLOCK</unmanaged>
            Bc4UnormBlock = 139,
            /// <unmanaged>VK_FORMAT_BC4_SNORM_BLOCK</unmanaged>
            Bc4SnormBlock = 140,
            /// <unmanaged>VK_FORMAT_BC5_UNORM_BLOCK</unmanaged>
            Bc5UnormBlock = 141,
            /// <unmanaged>VK_FORMAT_BC5_SNORM_BLOCK</unmanaged>
            Bc5SnormBlock = 142,
            /// <unmanaged>VK_FORMAT_BC6H_UFLOAT_BLOCK</unmanaged>
            Bc6hUfloatBlock = 143,
            /// <unmanaged>VK_FORMAT_BC6H_SFLOAT_BLOCK</unmanaged>
            Bc6hSfloatBlock = 144,
            /// <unmanaged>VK_FORMAT_BC7_UNORM_BLOCK</unmanaged>
            Bc7UnormBlock = 145,
            /// <unmanaged>VK_FORMAT_BC7_SRGB_BLOCK</unmanaged>
            Bc7SrgbBlock = 146,
            /// <unmanaged>VK_FORMAT_ETC2_R8G8B8_UNORM_BLOCK</unmanaged>
            Etc2R8G8B8UnormBlock = 147,
            /// <unmanaged>VK_FORMAT_ETC2_R8G8B8_SRGB_BLOCK</unmanaged>
            Etc2R8G8B8SrgbBlock = 148,
            /// <unmanaged>VK_FORMAT_ETC2_R8G8B8A1_UNORM_BLOCK</unmanaged>
            Etc2R8G8B8A1UnormBlock = 149,
            /// <unmanaged>VK_FORMAT_ETC2_R8G8B8A1_SRGB_BLOCK</unmanaged>
            Etc2R8G8B8A1SrgbBlock = 150,
            /// <unmanaged>VK_FORMAT_ETC2_R8G8B8A8_UNORM_BLOCK</unmanaged>
            Etc2R8G8B8A8UnormBlock = 151,
            /// <unmanaged>VK_FORMAT_ETC2_R8G8B8A8_SRGB_BLOCK</unmanaged>
            Etc2R8G8B8A8SrgbBlock = 152,
            /// <unmanaged>VK_FORMAT_EAC_R11_UNORM_BLOCK</unmanaged>
            EacR11UnormBlock = 153,
            /// <unmanaged>VK_FORMAT_EAC_R11_SNORM_BLOCK</unmanaged>
            EacR11SnormBlock = 154,
            /// <unmanaged>VK_FORMAT_EAC_R11G11_UNORM_BLOCK</unmanaged>
            EacR11G11UnormBlock = 155,
            /// <unmanaged>VK_FORMAT_EAC_R11G11_SNORM_BLOCK</unmanaged>
            EacR11G11SnormBlock = 156,
            /// <unmanaged>VK_FORMAT_ASTC_4x4_UNORM_BLOCK</unmanaged>
            Astc4x4UnormBlock = 157,
            /// <unmanaged>VK_FORMAT_ASTC_4x4_SRGB_BLOCK</unmanaged>
            Astc4x4SrgbBlock = 158,
            /// <unmanaged>VK_FORMAT_ASTC_5x4_UNORM_BLOCK</unmanaged>
            Astc5x4UnormBlock = 159,
            /// <unmanaged>VK_FORMAT_ASTC_5x4_SRGB_BLOCK</unmanaged>
            Astc5x4SrgbBlock = 160,
            /// <unmanaged>VK_FORMAT_ASTC_5x5_UNORM_BLOCK</unmanaged>
            Astc5x5UnormBlock = 161,
            /// <unmanaged>VK_FORMAT_ASTC_5x5_SRGB_BLOCK</unmanaged>
            Astc5x5SrgbBlock = 162,
            /// <unmanaged>VK_FORMAT_ASTC_6x5_UNORM_BLOCK</unmanaged>
            Astc6x5UnormBlock = 163,
            /// <unmanaged>VK_FORMAT_ASTC_6x5_SRGB_BLOCK</unmanaged>
            Astc6x5SrgbBlock = 164,
            /// <unmanaged>VK_FORMAT_ASTC_6x6_UNORM_BLOCK</unmanaged>
            Astc6x6UnormBlock = 165,
            /// <unmanaged>VK_FORMAT_ASTC_6x6_SRGB_BLOCK</unmanaged>
            Astc6x6SrgbBlock = 166,
            /// <unmanaged>VK_FORMAT_ASTC_8x5_UNORM_BLOCK</unmanaged>
            Astc8x5UnormBlock = 167,
            /// <unmanaged>VK_FORMAT_ASTC_8x5_SRGB_BLOCK</unmanaged>
            Astc8x5SrgbBlock = 168,
            /// <unmanaged>VK_FORMAT_ASTC_8x6_UNORM_BLOCK</unmanaged>
            Astc8x6UnormBlock = 169,
            /// <unmanaged>VK_FORMAT_ASTC_8x6_SRGB_BLOCK</unmanaged>
            Astc8x6SrgbBlock = 170,
            /// <unmanaged>VK_FORMAT_ASTC_8x8_UNORM_BLOCK</unmanaged>
            Astc8x8UnormBlock = 171,
            /// <unmanaged>VK_FORMAT_ASTC_8x8_SRGB_BLOCK</unmanaged>
            Astc8x8SrgbBlock = 172,
            /// <unmanaged>VK_FORMAT_ASTC_10x5_UNORM_BLOCK</unmanaged>
            Astc10x5UnormBlock = 173,
            /// <unmanaged>VK_FORMAT_ASTC_10x5_SRGB_BLOCK</unmanaged>
            Astc10x5SrgbBlock = 174,
            /// <unmanaged>VK_FORMAT_ASTC_10x6_UNORM_BLOCK</unmanaged>
            Astc10x6UnormBlock = 175,
            /// <unmanaged>VK_FORMAT_ASTC_10x6_SRGB_BLOCK</unmanaged>
            Astc10x6SrgbBlock = 176,
            /// <unmanaged>VK_FORMAT_ASTC_10x8_UNORM_BLOCK</unmanaged>
            Astc10x8UnormBlock = 177,
            /// <unmanaged>VK_FORMAT_ASTC_10x8_SRGB_BLOCK</unmanaged>
            Astc10x8SrgbBlock = 178,
            /// <unmanaged>VK_FORMAT_ASTC_10x10_UNORM_BLOCK</unmanaged>
            Astc10x10UnormBlock = 179,
            /// <unmanaged>VK_FORMAT_ASTC_10x10_SRGB_BLOCK</unmanaged>
            Astc10x10SrgbBlock = 180,
            /// <unmanaged>VK_FORMAT_ASTC_12x10_UNORM_BLOCK</unmanaged>
            Astc12x10UnormBlock = 181,
            /// <unmanaged>VK_FORMAT_ASTC_12x10_SRGB_BLOCK</unmanaged>
            Astc12x10SrgbBlock = 182,
            /// <unmanaged>VK_FORMAT_ASTC_12x12_UNORM_BLOCK</unmanaged>
            Astc12x12UnormBlock = 183,
            /// <unmanaged>VK_FORMAT_ASTC_12x12_SRGB_BLOCK</unmanaged>
            Astc12x12SrgbBlock = 184,
            /// <unmanaged>VK_FORMAT_G8B8G8R8_422_UNORM</unmanaged>
            G8B8G8R8422Unorm = 1000156000,
            /// <unmanaged>VK_FORMAT_B8G8R8G8_422_UNORM</unmanaged>
            B8G8R8G8422Unorm = 1000156001,
            /// <unmanaged>VK_FORMAT_G8_B8_R8_3PLANE_420_UNORM</unmanaged>
            G8B8R83Plane420Unorm = 1000156002,
            /// <unmanaged>VK_FORMAT_G8_B8R8_2PLANE_420_UNORM</unmanaged>
            G8B8R82Plane420Unorm = 1000156003,
            /// <unmanaged>VK_FORMAT_G8_B8_R8_3PLANE_422_UNORM</unmanaged>
            G8B8R83Plane422Unorm = 1000156004,
            /// <unmanaged>VK_FORMAT_G8_B8R8_2PLANE_422_UNORM</unmanaged>
            G8B8R82Plane422Unorm = 1000156005,
            /// <unmanaged>VK_FORMAT_G8_B8_R8_3PLANE_444_UNORM</unmanaged>
            G8B8R83Plane444Unorm = 1000156006,
            /// <unmanaged>VK_FORMAT_R10X6_UNORM_PACK16</unmanaged>
            R10X6UnormPack16 = 1000156007,
            /// <unmanaged>VK_FORMAT_R10X6G10X6_UNORM_2PACK16</unmanaged>
            R10X6G10X6Unorm2Pack16 = 1000156008,
            /// <unmanaged>VK_FORMAT_R10X6G10X6B10X6A10X6_UNORM_4PACK16</unmanaged>
            R10X6G10X6B10X6A10X6Unorm4Pack16 = 1000156009,
            /// <unmanaged>VK_FORMAT_G10X6B10X6G10X6R10X6_422_UNORM_4PACK16</unmanaged>
            G10X6B10X6G10X6R10X6422Unorm4Pack16 = 1000156010,
            /// <unmanaged>VK_FORMAT_B10X6G10X6R10X6G10X6_422_UNORM_4PACK16</unmanaged>
            B10X6G10X6R10X6G10X6422Unorm4Pack16 = 1000156011,
            /// <unmanaged>VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_420_UNORM_3PACK16</unmanaged>
            G10X6B10X6R10X63Plane420Unorm3Pack16 = 1000156012,
            /// <unmanaged>VK_FORMAT_G10X6_B10X6R10X6_2PLANE_420_UNORM_3PACK16</unmanaged>
            G10X6B10X6R10X62Plane420Unorm3Pack16 = 1000156013,
            /// <unmanaged>VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_422_UNORM_3PACK16</unmanaged>
            G10X6B10X6R10X63Plane422Unorm3Pack16 = 1000156014,
            /// <unmanaged>VK_FORMAT_G10X6_B10X6R10X6_2PLANE_422_UNORM_3PACK16</unmanaged>
            G10X6B10X6R10X62Plane422Unorm3Pack16 = 1000156015,
            /// <unmanaged>VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_444_UNORM_3PACK16</unmanaged>
            G10X6B10X6R10X63Plane444Unorm3Pack16 = 1000156016,
            /// <unmanaged>VK_FORMAT_R12X4_UNORM_PACK16</unmanaged>
            R12X4UnormPack16 = 1000156017,
            /// <unmanaged>VK_FORMAT_R12X4G12X4_UNORM_2PACK16</unmanaged>
            R12X4G12X4Unorm2Pack16 = 1000156018,
            /// <unmanaged>VK_FORMAT_R12X4G12X4B12X4A12X4_UNORM_4PACK16</unmanaged>
            R12X4G12X4B12X4A12X4Unorm4Pack16 = 1000156019,
            /// <unmanaged>VK_FORMAT_G12X4B12X4G12X4R12X4_422_UNORM_4PACK16</unmanaged>
            G12X4B12X4G12X4R12X4422Unorm4Pack16 = 1000156020,
            /// <unmanaged>VK_FORMAT_B12X4G12X4R12X4G12X4_422_UNORM_4PACK16</unmanaged>
            B12X4G12X4R12X4G12X4422Unorm4Pack16 = 1000156021,
            /// <unmanaged>VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_420_UNORM_3PACK16</unmanaged>
            G12X4B12X4R12X43Plane420Unorm3Pack16 = 1000156022,
            /// <unmanaged>VK_FORMAT_G12X4_B12X4R12X4_2PLANE_420_UNORM_3PACK16</unmanaged>
            G12X4B12X4R12X42Plane420Unorm3Pack16 = 1000156023,
            /// <unmanaged>VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_422_UNORM_3PACK16</unmanaged>
            G12X4B12X4R12X43Plane422Unorm3Pack16 = 1000156024,
            /// <unmanaged>VK_FORMAT_G12X4_B12X4R12X4_2PLANE_422_UNORM_3PACK16</unmanaged>
            G12X4B12X4R12X42Plane422Unorm3Pack16 = 1000156025,
            /// <unmanaged>VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_444_UNORM_3PACK16</unmanaged>
            G12X4B12X4R12X43Plane444Unorm3Pack16 = 1000156026,
            /// <unmanaged>VK_FORMAT_G16B16G16R16_422_UNORM</unmanaged>
            G16B16G16R16422Unorm = 1000156027,
            /// <unmanaged>VK_FORMAT_B16G16R16G16_422_UNORM</unmanaged>
            B16G16R16G16422Unorm = 1000156028,
            /// <unmanaged>VK_FORMAT_G16_B16_R16_3PLANE_420_UNORM</unmanaged>
            G16B16R163Plane420Unorm = 1000156029,
            /// <unmanaged>VK_FORMAT_G16_B16R16_2PLANE_420_UNORM</unmanaged>
            G16B16R162Plane420Unorm = 1000156030,
            /// <unmanaged>VK_FORMAT_G16_B16_R16_3PLANE_422_UNORM</unmanaged>
            G16B16R163Plane422Unorm = 1000156031,
            /// <unmanaged>VK_FORMAT_G16_B16R16_2PLANE_422_UNORM</unmanaged>
            G16B16R162Plane422Unorm = 1000156032,
            /// <unmanaged>VK_FORMAT_G16_B16_R16_3PLANE_444_UNORM</unmanaged>
            G16B16R163Plane444Unorm = 1000156033,
            /// <unmanaged>VK_FORMAT_G8_B8R8_2PLANE_444_UNORM</unmanaged>
            G8B8R82Plane444Unorm = 1000330000,
            /// <unmanaged>VK_FORMAT_G10X6_B10X6R10X6_2PLANE_444_UNORM_3PACK16</unmanaged>
            G10X6B10X6R10X62Plane444Unorm3Pack16 = 1000330001,
            /// <unmanaged>VK_FORMAT_G12X4_B12X4R12X4_2PLANE_444_UNORM_3PACK16</unmanaged>
            G12X4B12X4R12X42Plane444Unorm3Pack16 = 1000330002,
            /// <unmanaged>VK_FORMAT_G16_B16R16_2PLANE_444_UNORM</unmanaged>
            G16B16R162Plane444Unorm = 1000330003,
            /// <unmanaged>VK_FORMAT_A4R4G4B4_UNORM_PACK16</unmanaged>
            A4R4G4B4UnormPack16 = 1000340000,
            /// <unmanaged>VK_FORMAT_A4B4G4R4_UNORM_PACK16</unmanaged>
            A4B4G4R4UnormPack16 = 1000340001,
            /// <unmanaged>VK_FORMAT_ASTC_4x4_SFLOAT_BLOCK</unmanaged>
            Astc4x4SfloatBlock = 1000066000,
            /// <unmanaged>VK_FORMAT_ASTC_5x4_SFLOAT_BLOCK</unmanaged>
            Astc5x4SfloatBlock = 1000066001,
            /// <unmanaged>VK_FORMAT_ASTC_5x5_SFLOAT_BLOCK</unmanaged>
            Astc5x5SfloatBlock = 1000066002,
            /// <unmanaged>VK_FORMAT_ASTC_6x5_SFLOAT_BLOCK</unmanaged>
            Astc6x5SfloatBlock = 1000066003,
            /// <unmanaged>VK_FORMAT_ASTC_6x6_SFLOAT_BLOCK</unmanaged>
            Astc6x6SfloatBlock = 1000066004,
            /// <unmanaged>VK_FORMAT_ASTC_8x5_SFLOAT_BLOCK</unmanaged>
            Astc8x5SfloatBlock = 1000066005,
            /// <unmanaged>VK_FORMAT_ASTC_8x6_SFLOAT_BLOCK</unmanaged>
            Astc8x6SfloatBlock = 1000066006,
            /// <unmanaged>VK_FORMAT_ASTC_8x8_SFLOAT_BLOCK</unmanaged>
            Astc8x8SfloatBlock = 1000066007,
            /// <unmanaged>VK_FORMAT_ASTC_10x5_SFLOAT_BLOCK</unmanaged>
            Astc10x5SfloatBlock = 1000066008,
            /// <unmanaged>VK_FORMAT_ASTC_10x6_SFLOAT_BLOCK</unmanaged>
            Astc10x6SfloatBlock = 1000066009,
            /// <unmanaged>VK_FORMAT_ASTC_10x8_SFLOAT_BLOCK</unmanaged>
            Astc10x8SfloatBlock = 1000066010,
            /// <unmanaged>VK_FORMAT_ASTC_10x10_SFLOAT_BLOCK</unmanaged>
            Astc10x10SfloatBlock = 1000066011,
            /// <unmanaged>VK_FORMAT_ASTC_12x10_SFLOAT_BLOCK</unmanaged>
            Astc12x10SfloatBlock = 1000066012,
            /// <unmanaged>VK_FORMAT_ASTC_12x12_SFLOAT_BLOCK</unmanaged>
            Astc12x12SfloatBlock = 1000066013,
            /// <unmanaged>VK_FORMAT_PVRTC1_2BPP_UNORM_BLOCK_IMG</unmanaged>
            PVRTC12BPPUnormBlockImg = 1000054000,
            /// <unmanaged>VK_FORMAT_PVRTC1_4BPP_UNORM_BLOCK_IMG</unmanaged>
            PVRTC14BPPUnormBlockImg = 1000054001,
            /// <unmanaged>VK_FORMAT_PVRTC2_2BPP_UNORM_BLOCK_IMG</unmanaged>
            PVRTC22BPPUnormBlockImg = 1000054002,
            /// <unmanaged>VK_FORMAT_PVRTC2_4BPP_UNORM_BLOCK_IMG</unmanaged>
            PVRTC24BPPUnormBlockImg = 1000054003,
            /// <unmanaged>VK_FORMAT_PVRTC1_2BPP_SRGB_BLOCK_IMG</unmanaged>
            PVRTC12BPPSrgbBlockImg = 1000054004,
            /// <unmanaged>VK_FORMAT_PVRTC1_4BPP_SRGB_BLOCK_IMG</unmanaged>
            PVRTC14BPPSrgbBlockImg = 1000054005,
            /// <unmanaged>VK_FORMAT_PVRTC2_2BPP_SRGB_BLOCK_IMG</unmanaged>
            PVRTC22BPPSrgbBlockImg = 1000054006,
            /// <unmanaged>VK_FORMAT_PVRTC2_4BPP_SRGB_BLOCK_IMG</unmanaged>
            PVRTC24BPPSrgbBlockImg = 1000054007,
            /// <unmanaged>VK_FORMAT_R16G16_SFIXED5_NV</unmanaged>
            R16G16SFixed5NV = 1000464000,
            /// <unmanaged>VK_FORMAT_A1B5G5R5_UNORM_PACK16_KHR</unmanaged>
            A1B5G5R5UnormPack16KHR = 1000470000,
            /// <unmanaged>VK_FORMAT_A8_UNORM_KHR</unmanaged>
            A8UnormKHR = 1000470001,
            /// <unmanaged>VK_FORMAT_ASTC_4x4_SFLOAT_BLOCK_EXT</unmanaged>
            Astc4x4SfloatBlockEXT = Astc4x4SfloatBlock,
            /// <unmanaged>VK_FORMAT_ASTC_5x4_SFLOAT_BLOCK_EXT</unmanaged>
            Astc5x4SfloatBlockEXT = Astc5x4SfloatBlock,
            /// <unmanaged>VK_FORMAT_ASTC_5x5_SFLOAT_BLOCK_EXT</unmanaged>
            Astc5x5SfloatBlockEXT = Astc5x5SfloatBlock,
            /// <unmanaged>VK_FORMAT_ASTC_6x5_SFLOAT_BLOCK_EXT</unmanaged>
            Astc6x5SfloatBlockEXT = Astc6x5SfloatBlock,
            /// <unmanaged>VK_FORMAT_ASTC_6x6_SFLOAT_BLOCK_EXT</unmanaged>
            Astc6x6SfloatBlockEXT = Astc6x6SfloatBlock,
            /// <unmanaged>VK_FORMAT_ASTC_8x5_SFLOAT_BLOCK_EXT</unmanaged>
            Astc8x5SfloatBlockEXT = Astc8x5SfloatBlock,
            /// <unmanaged>VK_FORMAT_ASTC_8x6_SFLOAT_BLOCK_EXT</unmanaged>
            Astc8x6SfloatBlockEXT = Astc8x6SfloatBlock,
            /// <unmanaged>VK_FORMAT_ASTC_8x8_SFLOAT_BLOCK_EXT</unmanaged>
            Astc8x8SfloatBlockEXT = Astc8x8SfloatBlock,
            /// <unmanaged>VK_FORMAT_ASTC_10x5_SFLOAT_BLOCK_EXT</unmanaged>
            Astc10x5SfloatBlockEXT = Astc10x5SfloatBlock,
            /// <unmanaged>VK_FORMAT_ASTC_10x6_SFLOAT_BLOCK_EXT</unmanaged>
            Astc10x6SfloatBlockEXT = Astc10x6SfloatBlock,
            /// <unmanaged>VK_FORMAT_ASTC_10x8_SFLOAT_BLOCK_EXT</unmanaged>
            Astc10x8SfloatBlockEXT = Astc10x8SfloatBlock,
            /// <unmanaged>VK_FORMAT_ASTC_10x10_SFLOAT_BLOCK_EXT</unmanaged>
            Astc10x10SfloatBlockEXT = Astc10x10SfloatBlock,
            /// <unmanaged>VK_FORMAT_ASTC_12x10_SFLOAT_BLOCK_EXT</unmanaged>
            Astc12x10SfloatBlockEXT = Astc12x10SfloatBlock,
            /// <unmanaged>VK_FORMAT_ASTC_12x12_SFLOAT_BLOCK_EXT</unmanaged>
            Astc12x12SfloatBlockEXT = Astc12x12SfloatBlock,
            /// <unmanaged>VK_FORMAT_G8B8G8R8_422_UNORM_KHR</unmanaged>
            G8B8G8R8422UnormKHR = G8B8G8R8422Unorm,
            /// <unmanaged>VK_FORMAT_B8G8R8G8_422_UNORM_KHR</unmanaged>
            B8G8R8G8422UnormKHR = B8G8R8G8422Unorm,
            /// <unmanaged>VK_FORMAT_G8_B8_R8_3PLANE_420_UNORM_KHR</unmanaged>
            G8B8R83Plane420UnormKHR = G8B8R83Plane420Unorm,
            /// <unmanaged>VK_FORMAT_G8_B8R8_2PLANE_420_UNORM_KHR</unmanaged>
            G8B8R82Plane420UnormKHR = G8B8R82Plane420Unorm,
            /// <unmanaged>VK_FORMAT_G8_B8_R8_3PLANE_422_UNORM_KHR</unmanaged>
            G8B8R83Plane422UnormKHR = G8B8R83Plane422Unorm,
            /// <unmanaged>VK_FORMAT_G8_B8R8_2PLANE_422_UNORM_KHR</unmanaged>
            G8B8R82Plane422UnormKHR = G8B8R82Plane422Unorm,
            /// <unmanaged>VK_FORMAT_G8_B8_R8_3PLANE_444_UNORM_KHR</unmanaged>
            G8B8R83Plane444UnormKHR = G8B8R83Plane444Unorm,
            /// <unmanaged>VK_FORMAT_R10X6_UNORM_PACK16_KHR</unmanaged>
            R10X6UnormPack16KHR = R10X6UnormPack16,
            /// <unmanaged>VK_FORMAT_R10X6G10X6_UNORM_2PACK16_KHR</unmanaged>
            R10X6G10X6Unorm2Pack16KHR = R10X6G10X6Unorm2Pack16,
            /// <unmanaged>VK_FORMAT_R10X6G10X6B10X6A10X6_UNORM_4PACK16_KHR</unmanaged>
            R10X6G10X6B10X6A10X6Unorm4Pack16KHR = R10X6G10X6B10X6A10X6Unorm4Pack16,
            /// <unmanaged>VK_FORMAT_G10X6B10X6G10X6R10X6_422_UNORM_4PACK16_KHR</unmanaged>
            G10X6B10X6G10X6R10X6422Unorm4Pack16KHR = G10X6B10X6G10X6R10X6422Unorm4Pack16,
            /// <unmanaged>VK_FORMAT_B10X6G10X6R10X6G10X6_422_UNORM_4PACK16_KHR</unmanaged>
            B10X6G10X6R10X6G10X6422Unorm4Pack16KHR = B10X6G10X6R10X6G10X6422Unorm4Pack16,
            /// <unmanaged>VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_420_UNORM_3PACK16_KHR</unmanaged>
            G10X6B10X6R10X63Plane420Unorm3Pack16KHR = G10X6B10X6R10X63Plane420Unorm3Pack16,
            /// <unmanaged>VK_FORMAT_G10X6_B10X6R10X6_2PLANE_420_UNORM_3PACK16_KHR</unmanaged>
            G10X6B10X6R10X62Plane420Unorm3Pack16KHR = G10X6B10X6R10X62Plane420Unorm3Pack16,
            /// <unmanaged>VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_422_UNORM_3PACK16_KHR</unmanaged>
            G10X6B10X6R10X63Plane422Unorm3Pack16KHR = G10X6B10X6R10X63Plane422Unorm3Pack16,
            /// <unmanaged>VK_FORMAT_G10X6_B10X6R10X6_2PLANE_422_UNORM_3PACK16_KHR</unmanaged>
            G10X6B10X6R10X62Plane422Unorm3Pack16KHR = G10X6B10X6R10X62Plane422Unorm3Pack16,
            /// <unmanaged>VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_444_UNORM_3PACK16_KHR</unmanaged>
            G10X6B10X6R10X63Plane444Unorm3Pack16KHR = G10X6B10X6R10X63Plane444Unorm3Pack16,
            /// <unmanaged>VK_FORMAT_R12X4_UNORM_PACK16_KHR</unmanaged>
            R12X4UnormPack16KHR = R12X4UnormPack16,
            /// <unmanaged>VK_FORMAT_R12X4G12X4_UNORM_2PACK16_KHR</unmanaged>
            R12X4G12X4Unorm2Pack16KHR = R12X4G12X4Unorm2Pack16,
            /// <unmanaged>VK_FORMAT_R12X4G12X4B12X4A12X4_UNORM_4PACK16_KHR</unmanaged>
            R12X4G12X4B12X4A12X4Unorm4Pack16KHR = R12X4G12X4B12X4A12X4Unorm4Pack16,
            /// <unmanaged>VK_FORMAT_G12X4B12X4G12X4R12X4_422_UNORM_4PACK16_KHR</unmanaged>
            G12X4B12X4G12X4R12X4422Unorm4Pack16KHR = G12X4B12X4G12X4R12X4422Unorm4Pack16,
            /// <unmanaged>VK_FORMAT_B12X4G12X4R12X4G12X4_422_UNORM_4PACK16_KHR</unmanaged>
            B12X4G12X4R12X4G12X4422Unorm4Pack16KHR = B12X4G12X4R12X4G12X4422Unorm4Pack16,
            /// <unmanaged>VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_420_UNORM_3PACK16_KHR</unmanaged>
            G12X4B12X4R12X43Plane420Unorm3Pack16KHR = G12X4B12X4R12X43Plane420Unorm3Pack16,
            /// <unmanaged>VK_FORMAT_G12X4_B12X4R12X4_2PLANE_420_UNORM_3PACK16_KHR</unmanaged>
            G12X4B12X4R12X42Plane420Unorm3Pack16KHR = G12X4B12X4R12X42Plane420Unorm3Pack16,
            /// <unmanaged>VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_422_UNORM_3PACK16_KHR</unmanaged>
            G12X4B12X4R12X43Plane422Unorm3Pack16KHR = G12X4B12X4R12X43Plane422Unorm3Pack16,
            /// <unmanaged>VK_FORMAT_G12X4_B12X4R12X4_2PLANE_422_UNORM_3PACK16_KHR</unmanaged>
            G12X4B12X4R12X42Plane422Unorm3Pack16KHR = G12X4B12X4R12X42Plane422Unorm3Pack16,
            /// <unmanaged>VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_444_UNORM_3PACK16_KHR</unmanaged>
            G12X4B12X4R12X43Plane444Unorm3Pack16KHR = G12X4B12X4R12X43Plane444Unorm3Pack16,
            /// <unmanaged>VK_FORMAT_G16B16G16R16_422_UNORM_KHR</unmanaged>
            G16B16G16R16422UnormKHR = G16B16G16R16422Unorm,
            /// <unmanaged>VK_FORMAT_B16G16R16G16_422_UNORM_KHR</unmanaged>
            B16G16R16G16422UnormKHR = B16G16R16G16422Unorm,
            /// <unmanaged>VK_FORMAT_G16_B16_R16_3PLANE_420_UNORM_KHR</unmanaged>
            G16B16R163Plane420UnormKHR = G16B16R163Plane420Unorm,
            /// <unmanaged>VK_FORMAT_G16_B16R16_2PLANE_420_UNORM_KHR</unmanaged>
            G16B16R162Plane420UnormKHR = G16B16R162Plane420Unorm,
            /// <unmanaged>VK_FORMAT_G16_B16_R16_3PLANE_422_UNORM_KHR</unmanaged>
            G16B16R163Plane422UnormKHR = G16B16R163Plane422Unorm,
            /// <unmanaged>VK_FORMAT_G16_B16R16_2PLANE_422_UNORM_KHR</unmanaged>
            G16B16R162Plane422UnormKHR = G16B16R162Plane422Unorm,
            /// <unmanaged>VK_FORMAT_G16_B16_R16_3PLANE_444_UNORM_KHR</unmanaged>
            G16B16R163Plane444UnormKHR = G16B16R163Plane444Unorm,
            /// <unmanaged>VK_FORMAT_G8_B8R8_2PLANE_444_UNORM_EXT</unmanaged>
            G8B8R82Plane444UnormEXT = G8B8R82Plane444Unorm,
            /// <unmanaged>VK_FORMAT_G10X6_B10X6R10X6_2PLANE_444_UNORM_3PACK16_EXT</unmanaged>
            G10X6B10X6R10X62Plane444Unorm3Pack16EXT = G10X6B10X6R10X62Plane444Unorm3Pack16,
            /// <unmanaged>VK_FORMAT_G12X4_B12X4R12X4_2PLANE_444_UNORM_3PACK16_EXT</unmanaged>
            G12X4B12X4R12X42Plane444Unorm3Pack16EXT = G12X4B12X4R12X42Plane444Unorm3Pack16,
            /// <unmanaged>VK_FORMAT_G16_B16R16_2PLANE_444_UNORM_EXT</unmanaged>
            G16B16R162Plane444UnormEXT = G16B16R162Plane444Unorm,
            /// <unmanaged>VK_FORMAT_A4R4G4B4_UNORM_PACK16_EXT</unmanaged>
            A4R4G4B4UnormPack16EXT = A4R4G4B4UnormPack16,
            /// <unmanaged>VK_FORMAT_A4B4G4R4_UNORM_PACK16_EXT</unmanaged>
            A4B4G4R4UnormPack16EXT = A4B4G4R4UnormPack16,
            /// <unmanaged>VK_FORMAT_R16G16_S10_5_NV</unmanaged>
            R16G16S105NV = R16G16SFixed5NV,
        }

        public static int BlockSize(VkFormat format)
        {
            return format switch
            {
                VkFormat.R4G4UnormPack8 => 1,
                VkFormat.R4G4B4A4UnormPack16 => 2,
                VkFormat.B4G4R4A4UnormPack16 => 2,
                VkFormat.R5G6B5UnormPack16 => 2,
                VkFormat.B5G6R5UnormPack16 => 2,
                VkFormat.R5G5B5A1UnormPack16 => 2,
                VkFormat.B5G5R5A1UnormPack16 => 2,
                VkFormat.A1R5G5B5UnormPack16 => 2,
                VkFormat.R8Unorm => 1,
                VkFormat.R8Snorm => 1,
                VkFormat.R8Uscaled => 1,
                VkFormat.R8Sscaled => 1,
                VkFormat.R8Uint => 1,
                VkFormat.R8Sint => 1,
                VkFormat.R8Srgb => 1,
                VkFormat.R8G8Unorm => 2,
                VkFormat.R8G8Snorm => 2,
                VkFormat.R8G8Uscaled => 2,
                VkFormat.R8G8Sscaled => 2,
                VkFormat.R8G8Uint => 2,
                VkFormat.R8G8Sint => 2,
                VkFormat.R8G8Srgb => 2,
                VkFormat.R8G8B8Unorm => 3,
                VkFormat.R8G8B8Snorm => 3,
                VkFormat.R8G8B8Uscaled => 3,
                VkFormat.R8G8B8Sscaled => 3,
                VkFormat.R8G8B8Uint => 3,
                VkFormat.R8G8B8Sint => 3,
                VkFormat.R8G8B8Srgb => 3,
                VkFormat.B8G8R8Unorm => 3,
                VkFormat.B8G8R8Snorm => 3,
                VkFormat.B8G8R8Uscaled => 3,
                VkFormat.B8G8R8Sscaled => 3,
                VkFormat.B8G8R8Uint => 3,
                VkFormat.B8G8R8Sint => 3,
                VkFormat.B8G8R8Srgb => 3,
                VkFormat.R8G8B8A8Unorm => 4,
                VkFormat.R8G8B8A8Snorm => 4,
                VkFormat.R8G8B8A8Uscaled => 4,
                VkFormat.R8G8B8A8Sscaled => 4,
                VkFormat.R8G8B8A8Uint => 4,
                VkFormat.R8G8B8A8Sint => 4,
                VkFormat.R8G8B8A8Srgb => 4,
                VkFormat.B8G8R8A8Unorm => 4,
                VkFormat.B8G8R8A8Snorm => 4,
                VkFormat.B8G8R8A8Uscaled => 4,
                VkFormat.B8G8R8A8Sscaled => 4,
                VkFormat.B8G8R8A8Uint => 4,
                VkFormat.B8G8R8A8Sint => 4,
                VkFormat.B8G8R8A8Srgb => 4,
                VkFormat.A8B8G8R8UnormPack32 => 4,
                VkFormat.A8B8G8R8SnormPack32 => 4,
                VkFormat.A8B8G8R8UscaledPack32 => 4,
                VkFormat.A8B8G8R8SscaledPack32 => 4,
                VkFormat.A8B8G8R8UintPack32 => 4,
                VkFormat.A8B8G8R8SintPack32 => 4,
                VkFormat.A8B8G8R8SrgbPack32 => 4,
                VkFormat.A2R10G10B10UnormPack32 => 4,
                VkFormat.A2R10G10B10SnormPack32 => 4,
                VkFormat.A2R10G10B10UscaledPack32 => 4,
                VkFormat.A2R10G10B10SscaledPack32 => 4,
                VkFormat.A2R10G10B10UintPack32 => 4,
                VkFormat.A2R10G10B10SintPack32 => 4,
                VkFormat.A2B10G10R10UnormPack32 => 4,
                VkFormat.A2B10G10R10SnormPack32 => 4,
                VkFormat.A2B10G10R10UscaledPack32 => 4,
                VkFormat.A2B10G10R10SscaledPack32 => 4,
                VkFormat.A2B10G10R10UintPack32 => 4,
                VkFormat.A2B10G10R10SintPack32 => 4,
                VkFormat.R16Unorm => 2,
                VkFormat.R16Snorm => 2,
                VkFormat.R16Uscaled => 2,
                VkFormat.R16Sscaled => 2,
                VkFormat.R16Uint => 2,
                VkFormat.R16Sint => 2,
                VkFormat.R16Sfloat => 2,
                VkFormat.R16G16Unorm => 4,
                VkFormat.R16G16Snorm => 4,
                VkFormat.R16G16Uscaled => 4,
                VkFormat.R16G16Sscaled => 4,
                VkFormat.R16G16Uint => 4,
                VkFormat.R16G16Sint => 4,
                VkFormat.R16G16Sfloat => 4,
                VkFormat.R16G16B16Unorm => 6,
                VkFormat.R16G16B16Snorm => 6,
                VkFormat.R16G16B16Uscaled => 6,
                VkFormat.R16G16B16Sscaled => 6,
                VkFormat.R16G16B16Uint => 6,
                VkFormat.R16G16B16Sint => 6,
                VkFormat.R16G16B16Sfloat => 6,
                VkFormat.R16G16B16A16Unorm => 8,
                VkFormat.R16G16B16A16Snorm => 8,
                VkFormat.R16G16B16A16Uscaled => 8,
                VkFormat.R16G16B16A16Sscaled => 8,
                VkFormat.R16G16B16A16Uint => 8,
                VkFormat.R16G16B16A16Sint => 8,
                VkFormat.R16G16B16A16Sfloat => 8,
                VkFormat.R32Uint => 4,
                VkFormat.R32Sint => 4,
                VkFormat.R32Sfloat => 4,
                VkFormat.R32G32Uint => 8,
                VkFormat.R32G32Sint => 8,
                VkFormat.R32G32Sfloat => 8,
                VkFormat.R32G32B32Uint => 12,
                VkFormat.R32G32B32Sint => 12,
                VkFormat.R32G32B32Sfloat => 12,
                VkFormat.R32G32B32A32Uint => 16,
                VkFormat.R32G32B32A32Sint => 16,
                VkFormat.R32G32B32A32Sfloat => 16,
                VkFormat.R64Uint => 8,
                VkFormat.R64Sint => 8,
                VkFormat.R64Sfloat => 8,
                VkFormat.R64G64Uint => 16,
                VkFormat.R64G64Sint => 16,
                VkFormat.R64G64Sfloat => 16,
                VkFormat.R64G64B64Uint => 24,
                VkFormat.R64G64B64Sint => 24,
                VkFormat.R64G64B64Sfloat => 24,
                VkFormat.R64G64B64A64Uint => 32,
                VkFormat.R64G64B64A64Sint => 32,
                VkFormat.R64G64B64A64Sfloat => 32,
                VkFormat.B10G11R11UfloatPack32 => 4,
                VkFormat.E5B9G9R9UfloatPack32 => 4,
                VkFormat.D16Unorm => 2,
                VkFormat.X8D24UnormPack32 => 4,
                VkFormat.D32Sfloat => 4,
                VkFormat.S8Uint => 1,
                VkFormat.D16UnormS8Uint => 3,
                VkFormat.D24UnormS8Uint => 4,
                VkFormat.D32SfloatS8Uint => 5,
                VkFormat.Bc1RgbUnormBlock => 8,
                VkFormat.Bc1RgbSrgbBlock => 8,
                VkFormat.Bc1RgbaUnormBlock => 8,
                VkFormat.Bc1RgbaSrgbBlock => 8,
                VkFormat.Bc2UnormBlock => 16,
                VkFormat.Bc2SrgbBlock => 16,
                VkFormat.Bc3UnormBlock => 16,
                VkFormat.Bc3SrgbBlock => 16,
                VkFormat.Bc4UnormBlock => 8,
                VkFormat.Bc4SnormBlock => 8,
                VkFormat.Bc5UnormBlock => 16,
                VkFormat.Bc5SnormBlock => 16,
                VkFormat.Bc6hUfloatBlock => 16,
                VkFormat.Bc6hSfloatBlock => 16,
                VkFormat.Bc7UnormBlock => 16,
                VkFormat.Bc7SrgbBlock => 16,
                VkFormat.Etc2R8G8B8UnormBlock => 8,
                VkFormat.Etc2R8G8B8SrgbBlock => 8,
                VkFormat.Etc2R8G8B8A1UnormBlock => 8,
                VkFormat.Etc2R8G8B8A1SrgbBlock => 8,
                VkFormat.Etc2R8G8B8A8UnormBlock => 16,
                VkFormat.Etc2R8G8B8A8SrgbBlock => 16,
                VkFormat.EacR11UnormBlock => 8,
                VkFormat.EacR11SnormBlock => 8,
                VkFormat.EacR11G11UnormBlock => 16,
                VkFormat.EacR11G11SnormBlock => 16,
                VkFormat.Astc4x4UnormBlock => 16,
                VkFormat.Astc4x4SrgbBlock => 16,
                VkFormat.Astc5x4UnormBlock => 16,
                VkFormat.Astc5x4SrgbBlock => 16,
                VkFormat.Astc5x5UnormBlock => 16,
                VkFormat.Astc5x5SrgbBlock => 16,
                VkFormat.Astc6x5UnormBlock => 16,
                VkFormat.Astc6x5SrgbBlock => 16,
                VkFormat.Astc6x6UnormBlock => 16,
                VkFormat.Astc6x6SrgbBlock => 16,
                VkFormat.Astc8x5UnormBlock => 16,
                VkFormat.Astc8x5SrgbBlock => 16,
                VkFormat.Astc8x6UnormBlock => 16,
                VkFormat.Astc8x6SrgbBlock => 16,
                VkFormat.Astc8x8UnormBlock => 16,
                VkFormat.Astc8x8SrgbBlock => 16,
                VkFormat.Astc10x5UnormBlock => 16,
                VkFormat.Astc10x5SrgbBlock => 16,
                VkFormat.Astc10x6UnormBlock => 16,
                VkFormat.Astc10x6SrgbBlock => 16,
                VkFormat.Astc10x8UnormBlock => 16,
                VkFormat.Astc10x8SrgbBlock => 16,
                VkFormat.Astc10x10UnormBlock => 16,
                VkFormat.Astc10x10SrgbBlock => 16,
                VkFormat.Astc12x10UnormBlock => 16,
                VkFormat.Astc12x10SrgbBlock => 16,
                VkFormat.Astc12x12UnormBlock => 16,
                VkFormat.Astc12x12SrgbBlock => 16,
                VkFormat.G8B8G8R8422Unorm => 4,
                VkFormat.B8G8R8G8422Unorm => 4,
                VkFormat.G8B8R83Plane420Unorm => 3,
                VkFormat.G8B8R82Plane420Unorm => 3,
                VkFormat.G8B8R83Plane422Unorm => 3,
                VkFormat.G8B8R82Plane422Unorm => 3,
                VkFormat.G8B8R83Plane444Unorm => 3,
                VkFormat.R10X6UnormPack16 => 2,
                VkFormat.R10X6G10X6Unorm2Pack16 => 4,
                VkFormat.R10X6G10X6B10X6A10X6Unorm4Pack16 => 8,
                VkFormat.G10X6B10X6G10X6R10X6422Unorm4Pack16 => 8,
                VkFormat.B10X6G10X6R10X6G10X6422Unorm4Pack16 => 8,
                VkFormat.G10X6B10X6R10X63Plane420Unorm3Pack16 => 6,
                VkFormat.G10X6B10X6R10X62Plane420Unorm3Pack16 => 6,
                VkFormat.G10X6B10X6R10X63Plane422Unorm3Pack16 => 6,
                VkFormat.G10X6B10X6R10X62Plane422Unorm3Pack16 => 6,
                VkFormat.G10X6B10X6R10X63Plane444Unorm3Pack16 => 6,
                VkFormat.R12X4UnormPack16 => 2,
                VkFormat.R12X4G12X4Unorm2Pack16 => 4,
                VkFormat.R12X4G12X4B12X4A12X4Unorm4Pack16 => 8,
                VkFormat.G12X4B12X4G12X4R12X4422Unorm4Pack16 => 8,
                VkFormat.B12X4G12X4R12X4G12X4422Unorm4Pack16 => 8,
                VkFormat.G12X4B12X4R12X43Plane420Unorm3Pack16 => 6,
                VkFormat.G12X4B12X4R12X42Plane420Unorm3Pack16 => 6,
                VkFormat.G12X4B12X4R12X43Plane422Unorm3Pack16 => 6,
                VkFormat.G12X4B12X4R12X42Plane422Unorm3Pack16 => 6,
                VkFormat.G12X4B12X4R12X43Plane444Unorm3Pack16 => 6,
                VkFormat.G16B16G16R16422Unorm => 8,
                VkFormat.B16G16R16G16422Unorm => 8,
                VkFormat.G16B16R163Plane420Unorm => 6,
                VkFormat.G16B16R162Plane420Unorm => 6,
                VkFormat.G16B16R163Plane422Unorm => 6,
                VkFormat.G16B16R162Plane422Unorm => 6,
                VkFormat.G16B16R163Plane444Unorm => 6,
                VkFormat.PVRTC12BPPUnormBlockImg => 8,
                VkFormat.PVRTC14BPPUnormBlockImg => 8,
                VkFormat.PVRTC22BPPUnormBlockImg => 8,
                VkFormat.PVRTC24BPPUnormBlockImg => 8,
                VkFormat.PVRTC12BPPSrgbBlockImg => 8,
                VkFormat.PVRTC14BPPSrgbBlockImg => 8,
                VkFormat.PVRTC22BPPSrgbBlockImg => 8,
                VkFormat.PVRTC24BPPSrgbBlockImg => 8,
                VkFormat.Astc4x4SfloatBlock => 16,
                VkFormat.Astc5x4SfloatBlock => 16,
                VkFormat.Astc5x5SfloatBlock => 16,
                VkFormat.Astc6x5SfloatBlock => 16,
                VkFormat.Astc6x6SfloatBlock => 16,
                VkFormat.Astc8x5SfloatBlock => 16,
                VkFormat.Astc8x6SfloatBlock => 16,
                VkFormat.Astc8x8SfloatBlock => 16,
                VkFormat.Astc10x5SfloatBlock => 16,
                VkFormat.Astc10x6SfloatBlock => 16,
                VkFormat.Astc10x8SfloatBlock => 16,
                VkFormat.Astc10x10SfloatBlock => 16,
                VkFormat.Astc12x10SfloatBlock => 16,
                VkFormat.Astc12x12SfloatBlock => 16,
                VkFormat.G8B8R82Plane444Unorm => 3,
                VkFormat.G10X6B10X6R10X62Plane444Unorm3Pack16 => 6,
                VkFormat.G12X4B12X4R12X42Plane444Unorm3Pack16 => 6,
                VkFormat.G16B16R162Plane444Unorm => 6,
                VkFormat.A4R4G4B4UnormPack16 => 2,
                VkFormat.A4B4G4R4UnormPack16 => 2,
                VkFormat.R16G16S105NV => 4,
                _ => 0
            };
        }
    }
}