// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace XenoAtom.Interop;

[SuppressMessage("ReSharper", "InconsistentNaming")]
partial class vulkan
{
    /// <summary>
    /// Attribute that defines the version number of Vulkan.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class VkVersionAttribute : Attribute
    {
        /// <summary>
        /// Creates a new instance of <see cref="VkVersionAttribute"/>.
        /// </summary>
        /// <param name="version">The raw version number (e.g. <see cref="vulkan.VK_API_VERSION_1_0"/></param>).
        public VkVersionAttribute(uint version)
        {
            Version = new(version);
        }

        /// <summary>
        /// Gets the version number
        /// </summary>
        public VkVersion Version { get; }
    }

    /// <summary>
    /// Represents a Vulkan version.
    /// </summary>
    /// <remarks>
    /// Compatible with the raw version numbers e.g. <see cref="vulkan.VK_API_VERSION_1_0"/>, <see cref="vulkan.VK_API_VERSION_1_1"/>, <see cref="vulkan.VK_API_VERSION_1_2"/>.
    /// </remarks>
    public readonly struct VkVersion : IEquatable<VkVersion>
    {
        private const int MajorMinorNbBits = 10;
        private const int PatchNbBits = 12;
        private const int MajorMinorMask = (1 << MajorMinorNbBits) - 1;
        private const int PatchMask = (1 << PatchNbBits) - 1;
        
        private readonly uint _version;

        /// <summary>
        /// Creates a new instance of <see cref="VkVersion"/>.
        /// </summary>
        /// <param name="version">The raw version number combining major.minor.patch.</param>
        public VkVersion(uint version) => _version = version;

        /// <summary>
        /// Creates a new instance of <see cref="VkVersion"/>.
        /// </summary>
        public VkVersion(int major, int minor, int patch)
            => _version = (uint)(((major & MajorMinorMask) << (MajorMinorNbBits + PatchNbBits)) | ((minor & MajorMinorMask) << PatchNbBits) | (patch & PatchMask));

        /// <summary>
        /// Gets the major version.
        /// </summary>
        public int Major => (int)(_version >> (MajorMinorNbBits + PatchNbBits));

        /// <summary>
        /// Gets the minor version.
        /// </summary>
        public int Minor => (int)((_version >> PatchNbBits) & MajorMinorMask);

        /// <summary>
        /// Gets the patch version.
        /// </summary>
        public int Patch => (int)(_version & PatchMask);

        /// <inheritdoc />
        public override string ToString() => $"{Major}.{Minor}.{Patch}";

        /// <inheritdoc />
        public bool Equals(VkVersion other) => _version == other._version;

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is VkVersion other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => (int)_version;

        public static bool operator ==(VkVersion left, VkVersion right) => left.Equals(right);

        public static bool operator !=(VkVersion left, VkVersion right) => !left.Equals(right);

        public static implicit operator uint(VkVersion version) => version._version;

        public static implicit operator VkVersion(uint version) => new(version);
    }
}
