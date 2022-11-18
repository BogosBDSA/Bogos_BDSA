

namespace Bogos.tests;

public class GitRepoRepositoryTest : IDisposable
{
    private readonly GitContext _context;
    private readonly GitRepoRepository _repository;
    private readonly List<GitRepo> _AllRepos;
    private readonly List<GitRepo> _AllReposWithEmpty;

    public GitRepoRepositoryTest()
    {

        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<GitContext>()
        .UseSqlite(connection);
        var context = new GitContext(builder.Options);
        context.Database.EnsureCreated();

        var _RepoWithCommits = new GitRepo("http://www.github.com/_RepoWithCommits.git");
        var _RepoWithoutCommits = new GitRepo("http://www.github.com/_RepoWithoutCommits.git");
        var _TotallyNewRepo = new GitRepo("http://www.github.com/_TotallyNewRepo.git");

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
        _AllReposWithEmpty = new() { _RepoWithCommits, _RepoWithoutCommits, _TotallyNewRepo };
        _AllRepos = new() { _RepoWithCommits, _RepoWithoutCommits };
    }

    [Fact]
    public void HandleUri_using_existing_repo_with_fake_uri_should_return_UPDATED() 
    {
        // Arrange
        var uri = _AllRepos.First().Uri;
        uri.Should().NotBeNull();

        // Act
        var (result, handledRepo) = _repository.HandleUri(uri);

        // Assert
        handledRepo.Should().NotBeNull();
        result.Should().Be(Status.UPDATED);
    }

    [Fact]
    public void HandleUri_using_existing_repo_with_real_uri_should_return_UNCHANGED() 
    {
        // Arrange
        var uri = "https://github.com/oskaitu/distributed-mutex.git";
        var repo = new GitRepo(uri);
        _repository.CreateRepo(repo);
        _context.SaveChanges();

        // Act
        var (result, handledRepo) = _repository.HandleUri(uri);

        // Assert
        handledRepo.Should().NotBeNull();
        result.Should().Be(Status.UNCHANGED);
    }

    [Fact]
    public void HandleUri_using_nonexisting_uri_should_return_CREATED() 
    {
        // Arrange
        var newUri = "http://www.github.com/newUri.git";

        // Act
        var (result, handledRepo) = _repository.HandleUri(newUri);

        // Assert
        handledRepo.Should().NotBeNull();
        result.Should().Be(Status.CREATED);
    }

    [Theory]
    [InlineData(0, Status.CONFLICT)]
    [InlineData(2, Status.CREATED)]
    public void CreateRepo_using_GitRepo_should_return_expected_status(int repoIndex, Status status)
    {
        // Arrange
        var testRepo = _AllReposWithEmpty[repoIndex];
        var expected = status;

        // Act 
        var (result, _) = _repository.CreateRepo(testRepo);

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
        var (result, createdRepo) = _repository.CreateRepo(repoWithSameUri);

        // Assert
        createdRepo.Should().BeNull();
        result.Should().Be(Status.CONFLICT);
    }

    [Fact]
    public void DeleteRepo_using_existing_repo_should_return_DELETED() 
    { 
        // Arrange
        var repo = new GitRepo();
        _repository.CreateRepo(repo);

        // Act
        var result = _repository.DeleteRepo(repo);
        
        // Assert
        result.Should().Be(Status.DELETED);
    }

    [Fact]
    public void DeleteRepo_using_nonexisting_repo_should_return_NOTFOUND() 
    { 
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
    public void ReadRepoByUri_Should_Return_RepoWithoutCommits_given_RepoWithoutCommitsUri() 
    {
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
        // Arrange 
        var expected = status;
        var existingRepo = _AllRepos[repoIndex];
        var updatedRepo = new GitRepo();
        updatedRepo.Uri = existingRepo.Uri;
        var _author = new GitSignature("bobo", "dut@to.it", new DateTime(2022, 06, 10, 12, 10, 20).ToUniversalTime());
        var newcommit = new GitCommit(updatedRepo, _author, "more stuff", sha: "4");
        updatedRepo.Commits.Add(newcommit);

        // Act
        var (result, _) = _repository.UpdateRepo(updatedRepo);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void Api_Call_Should_Return_Converted_CSharp_Object_To_Json_AuthorMode()
    {
        // Arrange
        var path = _repository.ReadRepoByUri(_AllReposWithEmpty[0].Uri).Uri;

        var expected = "{\"AuthorToDateAndFrequency\":{\"Osnic dw1@bout.it\":{\"DateToFrequency\":{\"10-05-2022\":1}},\"Clarpat dw2@bout.it\":{\"DateToFrequency\":{\"10-03-2022\":1}},\"Sigmo dw3@bout.it\":{\"DateToFrequency\":{\"10-05-2020\":1}}}}";
        
        var repo = GitInsight.AuthorMode(_AllReposWithEmpty[0]);
        var result = Newtonsoft.Json.JsonConvert.SerializeObject(repo);

        // Then
        result.Should().Be(expected);
    }
    [Fact]
    public void Api_Call_Should_Return_Converted_CSharp_Object_To_Json_FrequencyMode()
    {
        // Arrange
        var path = _repository.ReadRepoByUri(_AllReposWithEmpty[0].Uri).Uri;

        var expected = "{\"DateToFrequency\":{\"10-05-2022\":1,\"10-03-2022\":1,\"10-05-2020\":1}}";
        
        var repo = GitInsight.FrequencyMode(_AllReposWithEmpty[0]);
        var result = Newtonsoft.Json.JsonConvert.SerializeObject(repo);

        // Then
        result.Should().Be(expected);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}