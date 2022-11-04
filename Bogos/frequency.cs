using System.Collections;
using LibGit2Sharp;

namespace Bogos;

public class frequency
{
    Repository repo;
    string day, month, year, date;

    public frequency(Repository repo)
    {
        this.repo = repo;
        gitInsightfrequency();


    }
    public frequency(string path)
    {
        this.repo = new Repository(path);
        Run();
    }


    public void Run()
    {

        foreach (var commit in gitInsightfrequency())
        {
            Console.WriteLine(commit);
        }
    }
    public string DbRun(){
        foreach (var commit in gitInsightfrequency())
        {
            return commit;
        }
        return "";
    }

    public IEnumerable<string> gitInsightfrequency()
    {
        var dates = new List<String>();
        var dict = new Dictionary<string, int>();

        foreach (Commit commit in repo.Commits)
        {
            day = commit.Author.When.Date.Day.ToString();
            month = commit.Author.When.Date.Month.ToString();
            year = commit.Author.When.Date.Year.ToString();
            date = " " + year + "-" + month + "-" + day;
            dates.Add(date);
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
            returnList.Add(element.Value + element.Key);
        }
        returnList.Reverse();

        return returnList;
    }
}