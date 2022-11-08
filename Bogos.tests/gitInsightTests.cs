namespace Bogos.tests;

public class gitInsightTests : IDisposable
{
    DateTimeOffset time1 = new DateTimeOffset(new DateTime(2000, 05, 05));
    DateTimeOffset time2 = new DateTimeOffset(new DateTime(2010, 12, 10));
    DateTimeOffset time3 = new DateTimeOffset(new DateTime(2022, 01, 31));
    Repository testRepo; 
    String repoPath;
    public gitInsightTests()
    {
    repoPath = Repository.Init(@"./testRepo");
    testRepo = new Repository(repoPath);
    var options = new CommitOptions();
    options.AllowEmptyCommit=true;
    var signature1 = new Signature("Nicolai", "bøvmail@123", time1);
    var signature2 = new Signature("Nicolai", "bøvmail@123", time1);
    var signature3 = new Signature("Nicolai", "bøvmail@123", time2);
    var signature4 = new Signature("Nicolai", "bøvmail@123", time3);
    testRepo.Commit("inital commit",signature1,signature1,options); 
    testRepo.Commit("second commit",signature2,signature2,options); 
    testRepo.Commit("third commit",signature3,signature3,options); 
    testRepo.Commit("last commit",signature4,signature4,options);  

    }

    [Fact]
    public void Test_Number_Of_Commits_Per_Day_when_there_is_only_one()
    {
        //arrange
        var expected = new List<GitCommitInfo> {new GitCommitInfo(2 ," 2000-5-5"), new GitCommitInfo(1, " 2010-12-10"), new GitCommitInfo(1, " 2022-1-31")};
        //act 
        var repo = new frequency(testRepo);
        var results = repo.gitInsightfrequency(); 
        Dispose();
        //assert
        results.Should().BeEquivalentTo(expected);
        DeleteReadOnlyDirectory(repoPath);
    }

    public void Dispose()
    {
        testRepo.Dispose();
    }
   public static void DeleteReadOnlyDirectory(string directory)
{
    
    foreach (var subdirectory in Directory.EnumerateDirectories(directory)) 
    {
        DeleteReadOnlyDirectory(subdirectory);
    }
    
    foreach (var fileName in Directory.EnumerateFiles(directory))
    {
        var fileInfo = new FileInfo(fileName);
        fileInfo.Attributes = FileAttributes.Normal;
        fileInfo.Delete();
    }
    Directory.Delete(directory);
}
}