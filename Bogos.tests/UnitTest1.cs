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

public class gitInsight
{
    [Fact]
    public void Test_Number_Of_Commits_Per_Day()
    {
        //arrange
        var expected = new List<string> {"[26-10-2022 00:00:00, 7]", "[25-10-2022 00:00:00, 24]"};
        var path = @"../";

        //act 
        //var results = new frequency(path);
        var results = frequency.gitInsightfrequency(); 

        //assert
        results.Should().BeEquivalentTo(expected);

    }
}