
public class GitAuthorModeTests : IDisposable
{
    readonly string branchName = "origin/GitAuthorModeTest";
    readonly DateTime _Datetime = new DateTime(2022, 05, 05);
    readonly string _workingPath;
    Repository _repo;
    public GitAuthorModeTests()
    {
        _workingPath = Repository.Init(@"./testRepo");
        _repo = new Repository(_workingPath);

        Signature author = new Signature("Osnic", "dw@bout.it", _Datetime);
        Signature committer = author;
        CommitOptions commitOptions = new();
        commitOptions.AllowEmptyCommit = true;

        if (_repo.Commits.Count() == 0)
        {

            _repo.Commit("Initial", author, committer, commitOptions);
        }

        if (_repo.Branches[branchName] == null)
        {
            _repo.CreateBranch(branchName);
        }

        Commands.Checkout(_repo, _repo.Branches[branchName]);
        _repo.Commit("First commit2", author, committer, commitOptions);
    }

    [Fact]
    public void hallo()
    {
        true.Should().BeTrue();
        Dispose();
    }

    public void Dispose()
    {
        var branch = Commands.Checkout(_repo, _repo.Branches["master"]);
        _repo.Branches.Remove(branchName);
        _repo.Dispose();
    }
}