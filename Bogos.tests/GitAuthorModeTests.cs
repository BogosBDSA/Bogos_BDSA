
public class GitAuthorModeTests : IDisposable
{
    readonly string branchName = "origin/GitAuthorModeTest";
    CommitOptions commitOptions;
    Signature author;
    Signature committer1;
    Signature committer2;
    readonly string _workingPath;
    Repository _repo;
    public GitAuthorModeTests()
    {
        _workingPath = Repository.Init(@"./testRepoAuthorMode");
        _repo = new Repository(_workingPath);

        author = new Signature("Osnic", "dw1@bout.it", new DateTime(2022, 05, 10, 12, 10, 20));
        committer1 = new Signature("Clarpat", "dw2@bout.it", new DateTime(2022, 03, 10, 3, 10, 20));
        committer2 = new Signature("Sigmo", "dw3@bout.it", new DateTime(2020, 05, 10, 12, 55, 20));

        commitOptions = new();
        commitOptions.AllowEmptyCommit = true;

        if (_repo.Commits.Count() == 0) _repo.Commit("Initial", author, author, commitOptions);
        if (_repo.Branches[branchName] == null) _repo.CreateBranch(branchName);

        Commands.Checkout(_repo, _repo.Branches[branchName]);

    }

    [Fact]
    public void returns_ThreeDifferentCommitsFromAllSignatures_usingGitAuthorMode()
    {
        // Given
        var commit1 = _repo.Commit("First commit from author", author, author, commitOptions);
        var commit2 = _repo.Commit("First commit from committer1", author, committer1, commitOptions);
        var commit3 = _repo.Commit("First commit from committer2", author, committer2, commitOptions);
        var expected = new List<string?> {
            commit1.Committer.ToString(),
            $"\t2 {commit1.Committer.When.ToString("dd/MM/yyyy")}",
            commit2.Committer.ToString(),
            $"\t1 {commit2.Committer.When.ToString(@"dd/MM/yyyy")}",
            commit3.Committer.ToString(),
            $"\t1 {commit3.Committer.When.ToString(@"dd/MM/yyyy")}"
        };

        // When
        var console = new TestableConsole();
        var authorMode = new GitAuthorMode(console, _repo);
        var writtenLines = console.WrittenLines;

        // Then
        writtenLines.Should().ContainInOrder(expected);
        //Dispose();
    }

    public void Dispose()
    {
        Commands.Checkout(_repo, _repo.Branches["master"]);
        _repo.Branches.Remove(branchName);
        _repo.Dispose();
    }
}