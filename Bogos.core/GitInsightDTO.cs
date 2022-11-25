namespace Bogos.core
{

    public class GitAuthorModeDTO
    {
        public Dictionary<string, Dictionary<string,int>> AuthorToDateAndFrequency { get; set; }

        public GitAuthorModeDTO() => AuthorToDateAndFrequency = new();
    }

    public class GitFrequencyModeDTO
    {
        public Dictionary<string, int> DateToFrequency { get; set; }
        public GitFrequencyModeDTO()
        {
            DateToFrequency = new Dictionary<string, int>();
        }
    }
}