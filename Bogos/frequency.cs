using System.Collections;
using LibGit2Sharp;

namespace Bogos;

public class frequency
{
Repository repo;

    public frequency(Repository repo)
    {
    this.repo = repo;
    }
    public frequency(string path)
    {
        this.repo = new Repository(path);
    }

    public IEnumerable<string> gitInsightfrequency()
    {
            var dates = new ArrayList();
            var dict = new Dictionary<string, int>();
            foreach (Commit commit in repo.Commits)
            {
                dates.Add(commit.Author.When.Date.ToString().Replace("-","/"));
            }
            foreach (string date in dates)
            {
                if (dict.ContainsKey(date))
                {
                    dict[date] = dict[date] + 1;
                }
                else dict.Add(date, 1);
            }
            var returnList = new List<string>();
            foreach (var element in dict)
            {
                returnList.Add(element.Value.ToString()+ " " + element.Key.Replace("00:00:00","").TrimEnd());
            }
            returnList.Reverse();
        
            return returnList;
    }
}