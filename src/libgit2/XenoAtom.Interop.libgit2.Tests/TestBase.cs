// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

namespace XenoAtom.Interop.Tests;

public abstract class TestBase
{
    private const string TestFolderName = "tests";

    public TestContext? TestContext { get; set; }

    public string? CurrentTestFolder { get; private set; }

    [TestInitialize()]
    public void Initialize()
    {
        // Ensure that the library is loaded
        var status = libgit2.git_libgit2_init();
        Assert.IsTrue(status.Success, "Failed to initialize libgit2");

        if (TestContext is null)
        {
            Assert.Fail("TestContext is null");
            return;
        }

        if (TestContext.TestName is null)
        {
            Assert.Fail("TestName is null");
            return;
        }
        
        var folder = Path.Combine(AppContext.BaseDirectory, TestFolderName, TestContext.TestName);
        RecursiveDelete(folder);
        Directory.CreateDirectory(folder);

        CurrentTestFolder = folder;
        Directory.SetCurrentDirectory(folder);
    }

    [TestCleanup()]
    public void Cleanup()
    {
        // Ensure that the library is unloaded
        var status = libgit2.git_libgit2_shutdown();
        Assert.IsTrue(status.Success, "Failed to shutdown libgit2");
        CurrentTestFolder = null;
    }

    private static void RecursiveDelete(string folder)
    {
        try
        {
            if (!Directory.Exists(folder))
            {
                return;
            }

            foreach (var file in Directory.EnumerateFiles(folder))
            {
                var fileInfo = new FileInfo(file);
                // Make sure that we can delete readonly files
                if (fileInfo.Attributes != FileAttributes.Normal)
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                }
                fileInfo.Delete();
            }

            foreach (var subFolder in Directory.EnumerateDirectories(folder))
            {
                RecursiveDelete(subFolder);
            }

            Directory.Delete(folder);
        }
        catch
        {
            // ignore
        }
    }
}