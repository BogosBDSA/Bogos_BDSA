using Bogos.entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Bogos.tests;

public class RepoRepositoryTest : IDisposable
{
    private readonly GitContext _context;
    private readonly GitRepoRepository _repository;
    readonly GitCommit _commit1, _commit2, _commit3;
    readonly GitSignature _author, _committer1, _committer2;
    readonly GitRepo _repo;


    public RepoRepositoryTest()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<GitContext>()
        .UseSqlite(connection);
        var context = new GitContext(builder.Options);
        context.Database.EnsureCreated();

        _repo = new GitRepo();

        _author = new GitSignature("Osnic", "dw1@bout.it", new DateTime(2022, 05, 10, 12, 10, 20));
        _committer1 = new GitSignature("Clarpat", "dw2@bout.it", new DateTime(2022, 03, 10, 3, 10, 20));
        _committer2 = new GitSignature("Sigmo", "dw3@bout.it", new DateTime(2020, 05, 10, 12, 55, 20));

        _commit1 = new GitCommit(_repo, _author, "Init commit", sha: "1");
        _commit2 = new GitCommit(_repo, _committer1, "First commit from committer1", sha: "2");
        _commit3 = new GitCommit(_repo, _committer2, "First commit from committer2", sha: "3");

        _repo.commits.Add(_commit1);
        _repo.commits.Add(_commit2);
        _repo.commits.Add(_commit3);
        context.Repos.Add(_repo);

        context.SaveChanges();
        _context = context;
    }



    [Fact]
    public void Create_Using_GitRepo_Should_Return_Status_CREATED()
    {
        // Arrange
        var testRepo = new GitRepo();
        var expected = Status.CREATED;

        // Act
        var result = _repository.CreateRepo(testRepo);

        // Assert
        result.Should().Be(expected);
    }






    //
    public void DeleteRepo_usingGitRepository_shouldreturnStatusDELETED() { }
    public void GetAllCommits_shouldreturnalistof3Commit_forarepowith3commits() { }
    public void GetAllRepos_ShouldReturnAlistOf2Repositories_forDBwith2Repositories() { }
    public void GetLatestCommit_ShouldReturnACommitFrom06112022_foraRepositorywith1Commit() { }
    public void GetRepoByID_ShouldReturnTheFirstRepoWithID1_ForInput1() { }
    public void GetRepoByName_ShouldReturnRepoWithNameX_ForARepoWithNameX() { }
    public void UpdateRepo_ShouldReturnStatusUPDATED_ForRepoWithID1() { }

    public void Dispose()
    {
        _context.Dispose();
    }
}