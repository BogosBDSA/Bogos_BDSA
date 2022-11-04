namespace Bogos.tests;

public class DataBaseTest{

    DateTimeOffset time1 = new DateTimeOffset(new DateTime(2000, 05, 05));
    DateTimeOffset time2 = new DateTimeOffset(new DateTime(2010, 12, 10));
    DateTimeOffset time3 = new DateTimeOffset(new DateTime(2022, 01, 31));
    Repository testRepo; 
    String repoPath;


    [Fact]
    public void DataBaseCheckHowManyCommitsPerAuthor(){

        //arrange
        string expected = "Oh deer";
        var name = "Ninolais";
        
        //act
        var results = DataBase.GetAllCommitsFromAuthor(name);

        //assert
        results.Should().BeEquivalentTo(expected);
        
    }

    [Fact]
    public void GetNumberOfCommitsFromAuthor(){
        int expected = 2;
        var name = "Rosco";

        var results = DataBase.GetNumberOfCommitsFromAuthor(name);

        results.Should().Be(expected);
    }

}
