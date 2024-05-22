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

        var result = vkCreateInstance(createInfo, Unsafe.NullRef<VkAllocationCallbacks>(), out var instance);
        Assert.AreEqual(VK_SUCCESS, result);

        // Destroy the instance
        vkDestroyInstance(instance, Unsafe.NullRef<VkAllocationCallbacks>());
    }

    [TestMethod]
    public unsafe void TestListExtensions()
    {
        uint count = 0;
        vkEnumerateInstanceExtensionProperties(null, ref count, null);
        if (count > 0)
        {
            VkExtensionProperties* propArrays = stackalloc VkExtensionProperties[(int)count];
            vkEnumerateInstanceExtensionProperties(null, ref count, propArrays);

            for (uint i = 0; i < count; i++)
            {
                var prop = propArrays[i];
                Console.WriteLine($"Extension: {Marshal.PtrToStringUTF8((nint)prop.extensionName)}");
            }
        }
    }
}