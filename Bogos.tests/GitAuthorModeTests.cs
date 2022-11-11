

public class GitAuthorModeTests
{
    readonly GitSignature author, committer1, committer2;
    readonly GitRepo _repo;

    public GitAuthorModeTests()
    {

        _repo = new GitRepo();

        author = new GitSignature("Osnic", "dw1@bout.it", new DateTime(2022, 05, 10, 12, 10, 20).ToUniversalTime());
        committer1 = new GitSignature("Clarpat", "dw2@bout.it", new DateTime(2022, 03, 10, 3, 10, 20).ToUniversalTime());
        committer2 = new GitSignature("Sigmo", "dw3@bout.it", new DateTime(2020, 05, 10, 12, 55, 20).ToUniversalTime());

        var commit1 = new GitCommit(_repo, author, "First commit from author");
        var commit2 = new GitCommit(_repo, committer1, "First commit from committer1");
        var commit3 = new GitCommit(_repo, committer1, "second commit from committer1");
        var commit4 = new GitCommit(_repo, committer1, "third commit from committer1");
        var commit5 = new GitCommit(_repo, committer2, "First commit from committer2");

        _repo.Commits.Add(commit1);
        _repo.Commits.Add(commit2);
        _repo.Commits.Add(commit3);
        _repo.Commits.Add(commit4);
        _repo.Commits.Add(commit5);

    }

    [Fact]
    public void returns_ThreeDifferentCommitsFromAllGitSignatures_usingGitAuthorMode()
    {

        // Arrange
        var expected = new GitAuthorModeDTO();

        var FrequencyMode1 = new GitFrequencyModeDTO();
        FrequencyMode1.DateToFrequency.Add(author.When.ToString("dd-MM-yyyy"), 1);
        expected.AuthorToDateAndFrequency.Add(author.ToString()!, FrequencyMode1);

        var FrequencyMode2 = new GitFrequencyModeDTO();
        FrequencyMode2.DateToFrequency.Add(committer1.When.ToString("dd-MM-yyyy"), 3);
        expected.AuthorToDateAndFrequency.Add(committer1.ToString()!, FrequencyMode2);

        var FrequencyMode3 = new GitFrequencyModeDTO();
        FrequencyMode3.DateToFrequency.Add(committer2.When.ToString("dd-MM-yyyy"), 1);
        expected.AuthorToDateAndFrequency.Add(committer2.ToString()!, FrequencyMode3);

        // Act
        var result = GitInsight.AuthorMode(_repo);


        // Assert
        result.Should().BeEquivalentTo(expected);
    }


}