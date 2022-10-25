namespace Bogos.tests;

public class UnitTest1
{
    

    [Fact]
    public void testThatTestingWorks()
    {  
    var test1 = new Onlytest();

    test1.testmethod("it").Equals("it works"); 
    test1.testmethod("works").Should().Be("works works");  
    }
}