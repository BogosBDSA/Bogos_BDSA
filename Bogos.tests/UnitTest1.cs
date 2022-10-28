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
        try{
    repoPath = Repository.Init(@"../../.././testRepo");
    testRepo = new Repository(repoPath);
    var options = new CommitOptions();
    options.AllowEmptyCommit=true;
    var signature1 = new Signature("Nicolai", "bøvmail@123", time1);
    testRepo.Commit("inital commit",signature1,signature1,options);  
        } finally {Dispose();}
    }

    [Fact]
    public void Test_Number_Of_Commits_Per_Day()
    {
        //arrange
        var expected = new List<string> {"[05-05-1945 00:00:00, 1]"};
        var path = repoPath;

        //act 
        var repo = new frequency(path);
        var results = repo.gitInsightfrequency(); 

        //assert
        results.Should().BeEquivalentTo(expected);
    }

    public void Dispose()
    {
        //used to delete repo when we're done
        testRepo.Dispose();
        Directory.Delete(repoPath,true);
    }
}