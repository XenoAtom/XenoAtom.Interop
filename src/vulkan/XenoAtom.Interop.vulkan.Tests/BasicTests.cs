using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XenoAtom.Interop.Tests;

using static XenoAtom.Interop.vulkan;

[TestClass]
public class BasicTests
{
    [TestMethod]
    public unsafe void TestSimple()
    {
        // Vulkan application information
        VkApplicationInfo appInfo = new () {
            sType = VK_STRUCTURE_TYPE_APPLICATION_INFO,
            apiVersion = VK_API_VERSION_1_0
        };

        // Vulkan instance creation information
        VkInstanceCreateInfo createInfo = new () {
            sType = VK_STRUCTURE_TYPE_INSTANCE_CREATE_INFO,
            pApplicationInfo = &appInfo,
        };

        var result = vkCreateInstance(createInfo, null, out var instance);
        Assert.AreEqual(VK_SUCCESS, result);
        result.VkCheck(); // Should not throw

        result = VkResult.VK_ERROR_DEVICE_LOST;
        Assert.ThrowsException<VulkanException>(() => result.VkCheck());

        // Destroy the instance
        vkDestroyInstance(instance, default);
    }

    [TestMethod]
    public unsafe void TestListExtensions()
    {
        vkEnumerateInstanceExtensionProperties(default, out var count);
        if (count > 0)
        {
            Span<VkExtensionProperties> propArrays = stackalloc VkExtensionProperties[(int)count];
            vkEnumerateInstanceExtensionProperties(default, propArrays);

            for (int i = 0; i < count; i++)
            {
                var prop = propArrays[i];
                Console.WriteLine($"Extension: {Marshal.PtrToStringUTF8((nint)prop.extensionName)}");
            }
        }
    }

    [TestMethod]
    public void TestVkVersion()
    {
        var version = new VkVersion(VK_API_VERSION_1_0);
        Assert.AreEqual(1, version.Major);
        Assert.AreEqual(0, version.Minor);
        Assert.AreEqual(0, version.Patch);
        Assert.AreEqual("1.0.0", version.ToString());

        version = new VkVersion(1, 1, 0);
        Assert.AreEqual(1, version.Major);
        Assert.AreEqual(1, version.Minor);
        Assert.AreEqual(0, version.Patch);
        Assert.AreEqual("1.1.0", version.ToString());

        version = new VkVersion(1, 2, 3);
        Assert.AreEqual(1, version.Major);
        Assert.AreEqual(2, version.Minor);
        Assert.AreEqual(3, version.Patch);
        Assert.AreEqual("1.2.3", version.ToString());

        version = VK_API_VERSION_1_0;
        VkVersion version2 = VK_API_VERSION_1_0;
        Assert.AreEqual(version, version2);

        version = VK_API_VERSION_1_1;
        Assert.AreNotEqual(version, version2);
    }
}