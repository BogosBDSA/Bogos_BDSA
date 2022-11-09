using Bogos.entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Bogos.tests;

public class GitRepoRepositoryTest : IDisposable
{
    private readonly GitContext _context;
    private readonly GitRepoRepository _repository;
    private readonly List<GitRepo> _AllRepos;


    public GitRepoRepositoryTest()
    {

        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<GitContext>()
        .UseSqlite(connection);
        var context = new GitContext(builder.Options);
        context.Database.EnsureCreated();

        var _RepoWithCommits = new GitRepo();
        var _RepoWithoutCommits = new GitRepo();
        var _TotallyNewRepo = new GitRepo();

        var _author = new GitSignature("Osnic", "dw1@bout.it", new DateTime(2022, 05, 10, 12, 10, 20));
        var _committer1 = new GitSignature("Clarpat", "dw2@bout.it", new DateTime(2022, 03, 10, 3, 10, 20));
        var _committer2 = new GitSignature("Sigmo", "dw3@bout.it", new DateTime(2020, 05, 10, 12, 55, 20));

        var _commit1 = new GitCommit(_RepoWithCommits, _author, "Init commit", sha: "1");
        var _commit2 = new GitCommit(_RepoWithCommits, _committer1, "First commit from committer1", sha: "2");
        var _commit3 = new GitCommit(_RepoWithCommits, _committer2, "First commit from committer2", sha: "3");

        _RepoWithCommits.Commits.Add(_commit1);
        _RepoWithCommits.Commits.Add(_commit2);
        _RepoWithCommits.Commits.Add(_commit3);

        context.Repos.Add(_RepoWithCommits);
        context.Repos.Add(_RepoWithoutCommits);

        context.SaveChanges();

        var _commit4 = new GitCommit(_TotallyNewRepo, _committer2, "First commit from committer2", sha: "4");
        _TotallyNewRepo.Commits.Add(_commit4);


        _context = context;
        _repository = new GitRepoRepository(_context);
        _AllRepos = new() { _RepoWithCommits, _RepoWithoutCommits, _TotallyNewRepo };
    }



    [Theory]
    [InlineData(0, Status.CONFLICT)]
    [InlineData(2, Status.CREATED)]
    public void CreateRepo_using_GitRepo_should_return__expected_status(int repoIndex, Status status)
    {
        // Arrange
        var testRepo = _AllRepos[repoIndex];
        var expected = status;

        // Act 
        var result = _repository.CreateRepo(testRepo);

        // Assert
        result.Should().Be(expected);
    }

    //
    public void DeleteRepo_usingGitRepository_shouldreturnStatusDELETED() { }
    
    public void GetAllRepos_ShouldReturnAlistOf3Repositories_forDBwith3Repositories() 
    {
        //Arrange
        var expected = _AllRepos;

        //Act 
        var result = _repository.ReadAllRepos();

        //Assert
        result.Should().BeEquivalentTo(expected);
    }
    public void GetRepoByID_ShouldReturnTheFirstRepoWithID1_ForInput1() { }
    public void GetRepoByUri_ShouldReturnRepoWithNameX_ForARepoWithNameX() { }
    public void UpdateRepo_ShouldReturnStatusUPDATED_ForRepoWithID1() { }

    public void Dispose()
    {
        _context.Dispose();
    }
}