using System.Text.RegularExpressions;

namespace XenoAtom.Interop.Tests;

using static XenoAtom.Interop.libgit2;

[TestClass]
public class BasicTests : TestBase
{
    [TestMethod]
    public void TestSimple()
    {
        // Discover the repository path
        var result = git_repository_discover(out var bufOut, AppContext.BaseDirectory, 0, ReadOnlySpan<char>.Empty);
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
    }

    [TestMethod]
    public void TestGitError()
    {
        var ex = Assert.ThrowsException<LibGit2Exception>(() => git_repository_head(out var head, default).Check());
        Assert.AreEqual(git_error_code.GIT_ERROR, ex.ErrorCode);
    }

    [TestMethod]
    public unsafe void TestRepoCommitsAndTags()
    {
        SetupTestFolder();

        var result = git_repository_init(out var repo, Environment.CurrentDirectory, 0);
        Assert.IsTrue(result.Success, $"Cannot initialize git repository at {Environment.CurrentDirectory}");

        File.WriteAllText("readme.md", "This is a HelloWorld file.");

        result = git_repository_index(out var index, repo);
        Assert.IsTrue(result.Success, "Cannot get the index");

        result = git_index_add_bypath(index, "readme.md");
        Assert.IsTrue(result.Success, "Cannot add the file to the index");

        result = git_index_write(index);
        Assert.IsTrue(result.Success, "Cannot write the index");

        result = git_index_write_tree(out var treeOid, index);
        Assert.IsTrue(result.Success, "Cannot write the tree");

        result = git_index_write(index);
        Assert.IsTrue(result.Success, "Cannot write the index");

        result = git_tree_lookup(out var tree, repo, treeOid);
        Assert.IsTrue(result.Success, "Cannot lookup the tree");

        result = git_signature_now(out var signature, "John", "john@corp.com");
        Assert.IsTrue(result.Success, "Cannot create a signature");

        result = git_commit_create(out var commitOid, repo, "HEAD", *signature, *signature, "UTF-8", "Initial commit", tree, 0, null);
        Assert.IsTrue(result.Success, "Cannot create a commit");

        var idAsString = commitOid.ToString();
        Assert.IsFalse(Regex.Match(idAsString, "^0{40}$").Success, "Invalid commit id");

        result = git_object_lookup(out var commitObj, repo, commitOid, GIT_OBJECT_COMMIT);
        Assert.IsTrue(result.Success, "Cannot lookup the object");

        // create a tag
        result = git_tag_create(out var tagOid, repo, "v1.0", commitObj, *signature, "This is a tag", 0);
        Assert.IsTrue(result.Success, "Cannot create a tag");

        result = git_tag_list(out var tag_names, repo);
        Assert.IsTrue(result.Success, "Cannot list tags");

        Assert.IsTrue(tag_names.count == 1, $"No tags found. {tag_names.count}");
        Assert.IsTrue(tag_names.strings != null, "No tags found");

        var tagNames = tag_names.ToArray();
        Assert.IsTrue(tagNames.Length == 1, $"No tags found. {tagNames.Length}");
        Assert.AreEqual("v1.0", tagNames[0]);

        git_strarray_dispose(ref tag_names);

        git_signature_free(signature);
        git_tree_free(tree);
        git_index_free(index);

        git_repository_free(repo);
    }
}