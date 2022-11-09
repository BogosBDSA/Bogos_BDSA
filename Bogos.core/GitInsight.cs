namespace Bogos.core
{

    public static class GitInsight
    {
        public static GitFrequencyModeData FrequencyMode(GitRepo Repo)
        {
            var dates = new List<String>();
            var data = new GitFrequencyModeData();

            foreach (var commit in Repo.Commits)
            {
                dates.Add(commit.CommitterWhen.ToString("dd-MM-yyyy"));
            }

            foreach (string date in dates)
            {
                if (data.DateToFrequency.ContainsKey(date))
                {
                    data.DateToFrequency[date]++;
                }
                else data.DateToFrequency.Add(date, 1);
            }

            data.DateToFrequency.Reverse();

            return data;
        }

        public static GitAuthorModeData AuthorMode(GitRepo Repo)
        {
            var data = new GitAuthorModeData();

            foreach (var UniqueCommitter in Repo.Commits.Select(c => c.Committer).DistinctBy(c => c.Name))
            {
                var freqData = new GitFrequencyModeData();
                var Commits = Repo.Commits
                                    .Where(c => c.Committer.Name == UniqueCommitter.Name)
                                    .GroupBy(k => new DateTime(
                                        k.Committer.When.Year,
                                        k.Committer.When.Month,
                                        k.Committer.When.Day
                                        ))
                                    .Select(g => (g.Key.ToString("dd-MM-yyyy"), g.Count()));

                foreach (var commit in Commits)
                {
                    freqData.DateToFrequency.Add(commit.Item1, commit.Item2);
                }
                data.AuthorToDateAndFrequency.Add(UniqueCommitter.ToString()!, freqData);

            }
            return data;

        }
    }
}