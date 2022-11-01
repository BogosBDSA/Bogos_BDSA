namespace Bogos.tests;

public class DataBaseTest{

    DateTimeOffset time1 = new DateTimeOffset(new DateTime(2000, 05, 05));
    DateTimeOffset time2 = new DateTimeOffset(new DateTime(2010, 12, 10));
    DateTimeOffset time3 = new DateTimeOffset(new DateTime(2022, 01, 31));
    Repository testRepo; 
    String repoPath;

    public void DataBaseTester(){

        repoPath = Repository.Init(@"./testRepo");
        testRepo = new Repository(repoPath);

        var options = new CommitOptions();
        options.AllowEmptyCommit=true;

        var signature1 = new Signature("Nicolai", "bøvmail@123", time1);
        var signature2 = new Signature("Mo", "bøvmail@123", time1);
        var signature3 = new Signature("Sigurd", "bøvmail@123", time2);
        var signature4 = new Signature("Nicolai", "bøvmail@123", time3);

        testRepo.Commit("inital commit",signature1,signature1,options); 
        testRepo.Commit("second commit",signature2,signature2,options); 
        testRepo.Commit("third commit",signature3,signature3,options); 
        testRepo.Commit("last commit",signature4,signature4,options);  

    }

    [Fact]
    public void DataBaseCheckHowManyCommitsPerAuthor(){

        //arrange
        var expectedResult = 2;
        //act

        var repo = new DataBase(testRepo);
        //var results = repo.CountCommitsFromAuthor();

        //assert
        repo.Should().BeEquivalentTo(expectedResult);
        
    }

    public static void DeleteReadOnlyDirectory(string directory){
    
    foreach (var subdirectory in Directory.EnumerateDirectories(directory)) 
        {
        DeleteReadOnlyDirectory(subdirectory);
        }
    
        foreach (var fileName in Directory.EnumerateFiles(directory)){
        var fileInfo = new FileInfo(fileName);
        fileInfo.Attributes = FileAttributes.Normal;
        fileInfo.Delete();
        }
        Directory.Delete(directory);
    }


}
