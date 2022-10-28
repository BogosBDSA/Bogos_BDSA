using System.Collections;
using LibGit2Sharp;

namespace Bogos;

public static class frequency
{
    /*
Repository repo;

    public frequency(string path)
    {
    this.repo = new Repository(path);
    }
*/

    public static IEnumerable<string> gitInsightfrequency()
    {
        using (var repo = new Repository(@"/home/lunero/ITU/3. Semester/BDSA (analysis, design and software architecture)/Big Project/testing-for-bdsa/"))
        {
            var dates = new ArrayList();
            var dict = new Dictionary<string, int>();
            foreach (Commit commit in repo.Commits)
            {
                dates.Add(commit.Author.When.Date.ToString());
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
                returnList.Add(element.Key + element.Value.ToString());
            }
            return returnList;
        }
    }
}