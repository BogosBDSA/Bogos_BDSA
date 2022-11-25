

public class GitAuthorModeTests
{
    readonly GitSignature committer1, committer2, committer3;
    readonly DateTime date1, date2, date3;
    readonly GitRepo _repo;

    public GitAuthorModeTests()
    {

        _repo = new GitRepo("https://www.fakehub.com/fakeuser/fakeproject/fakerepo.git");

        committer1 = new GitSignature("Osnic", "dw1@bout.it");
        committer2 = new GitSignature("Clarpat", "dw2@bout.it");
        committer3 = new GitSignature("Sigmo", "dw3@bout.it");

        date1 = new DateTime(2022, 05, 10, 12, 10, 20);
        date2 = new DateTime(2022, 03, 10, 3, 10, 20);
        date3 = new DateTime(2020, 05, 10, 12, 55, 20);

        var commit1 = new GitCommit(_repo, committer1, date1, "First commit from author");
        var commit2 = new GitCommit(_repo, committer2, date2, "First commit from committer1");
        var commit3 = new GitCommit(_repo, committer2, date2, "second commit from committer1");
        var commit4 = new GitCommit(_repo, committer2, date3, "third commit from committer1");
        var commit5 = new GitCommit(_repo, committer3, date2, "First commit from committer2");

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
    
        var FrequencyForCommitter1 = new Dictionary<string,int>();
        FrequencyForCommitter1.Add(date1.ToString("dd-MM-yyyy"), 1);
        expected.AuthorToDateAndFrequency.Add(committer1.ToString(), FrequencyForCommitter1);

        var FrequencyForCommitter2 = new Dictionary<string,int>();
        FrequencyForCommitter2.Add(date2.ToString("dd-MM-yyyy"), 2);
        FrequencyForCommitter2.Add(date3.ToString("dd-MM-yyyy"), 1);
        expected.AuthorToDateAndFrequency.Add(committer2.ToString(), FrequencyForCommitter2);

        var FrequencyForCommitter3 = new Dictionary<string,int>();
        FrequencyForCommitter3.Add(date2.ToString("dd-MM-yyyy"), 1);
        expected.AuthorToDateAndFrequency.Add(committer3.ToString(), FrequencyForCommitter3);

        // Act
        var result = GitInsight.AuthorMode(_repo);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }


}