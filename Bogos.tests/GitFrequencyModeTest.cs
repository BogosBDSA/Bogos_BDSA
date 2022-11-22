namespace Bogos.tests
{

    public class GitFrequencyModeTest
    {
        GitRepo testRepo;

        public GitFrequencyModeTest()
        {

            testRepo = new GitRepo("https://www.fakehub.com/fakeuser/fakeproject/fakerepo.git");
            var signature1 = new GitSignature("Nicolai", "bøvmail@123");
            var signature2 = new GitSignature("Nicolai", "bøvmail@123");
            var signature3 = new GitSignature("Nicolai", "bøvmail@123");
            var signature4 = new GitSignature("Nicolai", "bøvmail@123");
            testRepo.Commits.Add(new GitCommit(testRepo, signature1, new DateTime(1945, 05, 05,13,30,00), "inital commit"));
            testRepo.Commits.Add(new GitCommit(testRepo, signature2, new DateTime(2010, 12, 10,13,30,0), "second commit"));
            testRepo.Commits.Add(new GitCommit(testRepo, signature3, new DateTime(2010, 12, 10,13,30,00), "third commit"));
            testRepo.Commits.Add(new GitCommit(testRepo, signature4, new DateTime(2022, 01, 31,13,30,000), "last commit"));

        }


        // GEMME TIL DATABASEN <- ikke implementeret

        [Fact]
        public void Test_Number_Of_Commits_Per_Day_when_there_is_only_one()
        {

            //arrange
            var expected = new GitFrequencyModeDTO();
            expected.DateToFrequency.Add("05-05-1945", 1);
            expected.DateToFrequency.Add("10-12-2010", 2);
            expected.DateToFrequency.Add("31-01-2022", 1);

            //act 
            var results = GitInsight.FrequencyMode(testRepo);

            //assert
            results.Should().BeEquivalentTo(expected);
        }

    }
}