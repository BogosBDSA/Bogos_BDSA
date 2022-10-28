using Bogos.tests;

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
        options.AllowEmptyCommit = true;
        var signature1 = new Signature("Nicolai", "bøvmail@123", time1);
        testRepo.Commit("inital commit", signature1, signature1, options);
    }

    public void Dispose()
    {
        //used to delete repo when we're done
        testRepo.Dispose();
        DeleteReadOnlyDirectory(repoPath);
    }
}