namespace Bogos.core
{

    public class GitAuthorModeData
    {
        public Dictionary<string, GitFrequencyModeData> AuthorToDateAndFrequency { get; set; }

        public GitAuthorModeData() => AuthorToDateAndFrequency = new();
    }

    public class GitFrequencyModeData
    {
        public Dictionary<string, int> DateToFrequency { get; set; }
        public GitFrequencyModeData()
        {
            DateToFrequency = new Dictionary<string, int>();
        }
    }
}