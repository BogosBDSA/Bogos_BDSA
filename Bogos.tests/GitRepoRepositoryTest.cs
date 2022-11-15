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
    private readonly List<GitRepo> _AllReposwithempty;


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
       
       _RepoWithCommits.Uri = ("http://www.github.com/_RepoWithCommits.git");
       _RepoWithoutCommits.Uri = ("http://www.github.com/_RepoWithoutCommits.git");
       _TotallyNewRepo.Uri = ("http://www.github.com/_TotallyNewRepo.git");

        var _author = new GitSignature("Osnic", "dw1@bout.it", new DateTime(2022, 05, 10, 12, 10, 20).ToUniversalTime());
        var _committer1 = new GitSignature("Clarpat", "dw2@bout.it", new DateTime(2022, 03, 10, 3, 10, 20).ToUniversalTime());
        var _committer2 = new GitSignature("Sigmo", "dw3@bout.it", new DateTime(2020, 05, 10, 12, 55, 20).ToUniversalTime());

        var _commit1 = new GitCommit(_RepoWithCommits, _author, "Init commit", sha: "1");
        var _commit2 = new GitCommit(_RepoWithCommits, _committer1, "First commit from committer1", sha: "2");
        var _commit3 = new GitCommit(_RepoWithCommits, _committer2, "First commit from committer2", sha: "3");

        _RepoWithCommits.Commits.Add(_commit1);
        _RepoWithCommits.Commits.Add(_commit2);
        _RepoWithCommits.Commits.Add(_commit3);

        context.repos.Add(_RepoWithCommits);
        context.repos.Add(_RepoWithoutCommits);

        context.SaveChanges();

        var _commit4 = new GitCommit(_TotallyNewRepo, _committer2, "First commit from committer2", sha: "4");
        _TotallyNewRepo.Commits.Add(_commit4);


        _context = context;
        _repository = new GitRepoRepository(_context);
        _AllReposwithempty = new() { _RepoWithCommits, _RepoWithoutCommits, _TotallyNewRepo };
        _AllRepos = new() { _RepoWithCommits, _RepoWithoutCommits };
    }



    [Theory]
    [InlineData(0, Status.CONFLICT)]
    [InlineData(2, Status.CREATED)]
    public void CreateRepo_using_GitRepo_should_return__expected_status(int repoIndex, Status status)
    {
        // Arrange
        var testRepo = _AllReposwithempty[repoIndex];
        var expected = status;

        // Act 
        var result = _repository.CreateRepo(testRepo).Item1;

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void CreateRepo_using_repo_with_same_uri_as_existing_repo_should_return_CONFLICT()
    {
        // Arrange
        var repoWithSameUri = new GitRepo();
        repoWithSameUri.Uri = _AllRepos[0].Uri;

        // Act
        var result = _repository.CreateRepo(repoWithSameUri).Item1;

        // Assert
        result.Should().Be(Status.CONFLICT);
    }

    [Fact]
    public void DeleteRepo_using_existing_repo_should_return_DELETED() { 
        // Arrange
        var repo = new GitRepo();
        _repository.CreateRepo(repo);

        // Act
        var result = _repository.DeleteRepo(repo);
        
        // Assert
        result.Should().Be(Status.DELETED);
    }

    [Fact]
    public void DeleteRepo_using_nonexisting_repo_should_return_NOTFOUND() { 
        // Arrange
        var repo = new GitRepo();

        // Act
        var result = _repository.DeleteRepo(repo);
        
        // Assert
        result.Should().Be(Status.NOTFOUND);
    }

    [Fact]
    public void ReadRepoByID_should_return_null_for_deleted_repo()
    {
        // Arrange
        var repo = new GitRepo();
        _repository.CreateRepo(repo);
        
        // Ensure it is created
        var createdRepo = _repository.ReadRepoByID(repo.Id);
        createdRepo.Should().NotBeNull();

        // Act 
        _repository.DeleteRepo(repo);
        var deletedRepo = _repository.ReadRepoByID(repo.Id);

        // Assert
        deletedRepo.Should().BeNull(); 
    }

    [Fact]
    public void ReadAllRepos_ShouldReturnAlistOf3Repositories_forDBwith3Repositories()
    {
        //Arrange
        var expected = _AllRepos;

        //Act 
        var result = _repository.ReadAllRepos();

        //Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(3, 4)]
    public void ReadRepoByID_should_return_the_correct_GitRepo_or_null_for_inputs(int repoIndex, int repoId)
    {
        // Arrange
        var inRange = repoIndex >= 0 && repoIndex < _AllRepos.Count();
        var expected = inRange ? _AllRepos[repoIndex] : null;

        // Act 
        var result = _repository.ReadRepoByID(repoId);

        // Assert
        result.Should().Be(expected);


    }

    [Fact]
    public void ReadRepoByUri_Should_Return_RepoWithoutCommits_given_RepoWithoutCommitsUri() {
        // Arrange 

        var repo = _AllRepos[1];
        var expected = repo;

        // Act 
        var result = _repository.ReadRepoByUri(_AllRepos[1].Uri!);

        // Assert
        result.Should().BeEquivalentTo(expected);

     }


    [Theory]
    [InlineData(0, Status.UPDATED)]
    public void UpdateRepo_ShouldReturnStatusUPDATED_ForRepoWithID1(int repoIndex, Status status)
    {
        //arrange
        var expected = status;
        var repo = _AllRepos[repoIndex];
        var _author = new GitSignature("bobo", "dut@to.it", new DateTime(2022, 06, 10, 12, 10, 20).ToUniversalTime());
        var newcommit = new GitCommit(repo, _author, "more stuff", sha: "4");
        //act
        repo.Commits.Add(newcommit);
        var result = _repository.UpdateRepo(repo);

        //assert
        result.Should().Be(expected);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}