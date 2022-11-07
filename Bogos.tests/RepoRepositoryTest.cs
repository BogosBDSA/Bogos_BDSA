using Bogos.entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Bogos.tests;

public class RepoRepositoryTest : IDisposable
{
    private readonly GitContext _context;
    private readonly RepoRepository _repository;
    readonly string _BranchName = "origin/RepoRepositoryTest";
    readonly Commit commit1, commit2, commit3;
    readonly CommitOptions commitOptions;
    readonly Signature _author, _committer1, _committer2;
    readonly string _workingPath;
    readonly Repository _repo;


    public RepoRepositoryTest()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<GitContext>()
        .UseSqlite(connection);
        var context = new GitContext(builder.Options);
        context.Database.EnsureCreated();

        _workingPath = Repository.Init(@"./testRepoAuthorMode");
        _repo = new Repository(_workingPath);

        _author = new Signature("Osnic", "dw1@bout.it", new DateTime(2022, 05, 10, 12, 10, 20));
        _committer1 = new Signature("Clarpat", "dw2@bout.it", new DateTime(2022, 03, 10, 3, 10, 20));
        _committer2 = new Signature("Sigmo", "dw3@bout.it", new DateTime(2020, 05, 10, 12, 55, 20));

        commitOptions = new();
        commitOptions.AllowEmptyCommit = true;

        if (_repo.Commits.Count() == 0) _repo.Commit("Initial", _author, _author, commitOptions);
        if (_repo.Branches[_BranchName] == null) _repo.CreateBranch(_BranchName);

        Commands.Checkout(_repo, _repo.Branches[_BranchName]);


        commit1 = _repo.Commit("First commit from author", _author, _author, commitOptions);
        commit2 = _repo.Commit("First commit from committer1", _author, _committer1, commitOptions);
        commit3 = _repo.Commit("First commit from committer2", _author, _committer2, commitOptions);

        context.repos.Add(new GitRepo(_repo, "1"));

        context.SaveChanges();
    }

    [Fact]
    public void Create_usingGitRepository_shouldreturnStatusCREATED() { }
    public void DeleteRepo_usingGitRepository_shouldreturnStatusDELETED() { }
    public void GetAllCommits_shouldreturnalistof3Commit_forarepowith3commits() { }
    public void GetAllRepos_ShouldReturnAlistOf2Repositories_forDBwith2Repositories() { }
    public void GetLatestCommit_ShouldReturnACommitFrom06112022_foraRepositorywith1Commit() { }
    public void GetRepoByID_ShouldReturnTheFirstRepoWithID1_ForInput1() { }
    public void GetRepoByName_ShouldReturnRepoWithNameX_ForARepoWithNameX() { }
    public void UpdateRepo_ShouldReturnStatusUPDATED_ForRepoWithID1() { }

    public void Dispose()
    {
        Commands.Checkout(_repo, _repo.Branches["master"]);
        _repo.Branches.Remove(_BranchName);
        _repo.Dispose();
        _context.Dispose();
    }
}