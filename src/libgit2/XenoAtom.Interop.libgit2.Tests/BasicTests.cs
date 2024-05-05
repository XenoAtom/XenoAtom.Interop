namespace XenoAtom.Interop.Tests;

using static XenoAtom.Interop.libgit2;

[TestClass]
public class BasicTests
{
    [TestMethod]
    public void TestSimple()
    {
        var result = git_libgit2_init();
        Assert.IsTrue(result.Success);

        // Discover the repository path
        result = git_repository_discover(out var bufOut, AppContext.BaseDirectory, 0, ReadOnlySpan<char>.Empty);
        Assert.IsTrue(result.Success);
        Assert.IsTrue(bufOut.size > 0);
        var repoPath = bufOut.AsString();
        git_buf_dispose(ref bufOut);

        // Open this repository
        // Alternatively git_repository_open(out var repo, repoPath);
        result = git_repository_open_ext(out var repo, AppContext.BaseDirectory, 0, default);
        Assert.IsTrue(result.Success);
        var repoPath2 = git_repository_path_string(repo);
        Assert.AreEqual(repoPath, repoPath2);

        // Iterate over the commits
        result = git_revwalk_new(out var revwalk, repo);
        Assert.IsTrue(result.Success);
        result = git_revwalk_sorting(revwalk, GIT_SORT_TIME | GIT_SORT_REVERSE);
        Assert.IsTrue(result.Success);
        result = git_revwalk_push_head(revwalk);
        Assert.IsTrue(result.Success);

        int hashCode = 0;
        while (git_revwalk_next(out var oid, revwalk) == 0)
        {
            result = git_commit_lookup(out var commit, repo, oid);
            Assert.IsTrue(result.Success);

            var message = git_commit_message_string(commit);
            hashCode += message.GetHashCode();
            git_commit_free(commit);
        }
        Assert.AreNotEqual(0, hashCode, "Invalid hashcode for all messages");
        git_revwalk_free(revwalk);

        // Close the repository
        git_repository_free(repo);
        
        result = git_libgit2_shutdown();
        Assert.IsTrue(result.Success);
    }
}
