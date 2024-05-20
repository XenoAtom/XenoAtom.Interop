using System.Runtime.CompilerServices;

namespace XenoAtom.Interop.Tests;

using static XenoAtom.Interop.vulkan;

[TestClass]
public class BasicTests
{
    [TestMethod]
    public unsafe void TestSimple()
    {
        VkInstance instance = default;

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

        var result = vkCreateInstance(createInfo, Unsafe.NullRef<VkAllocationCallbacks>(), ref instance);
        Assert.AreEqual(VK_SUCCESS, result);

        // Destroy the instance
        vkDestroyInstance(instance, Unsafe.NullRef<VkAllocationCallbacks>());
    }
}