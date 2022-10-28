namespace Bogos.tests;

public class UnitTest1
{
    [Fact]
    public void testThatTestingWorks()
    {  
    var test1 = new Onlytest();

    test1.testmethod("it").Equals("it works");     
    test1.testmethod("does").Should().Be("does works");  
    }
}

public class gitInsightTests : IDisposable
{
    DateTimeOffset time1 = new DateTimeOffset(new DateTime(2020, 05, 05));
    Repository testRepo; 
    String repoPath;
    public gitInsightTests() 
    {
    repoPath = Repository.Init(@"./testRepo");
    testRepo = new Repository(repoPath);
    var options = new CommitOptions();
    options.AllowEmptyCommit=true;
    var signature1 = new Signature("Nicolai", "bøvmail@123", time1);
    testRepo.Commit("inital commit",signature1,signature1,options);  
    }

    [Fact]
    public void Test_Number_Of_Commits_Per_Day()
    {
        //arrange
        var expected = new List<string> {"05/05/2020 00:00:00, 1"};
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
        //used to delete repo when we're done
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