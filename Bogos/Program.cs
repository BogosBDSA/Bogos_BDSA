using System.Collections;
using LibGit2Sharp;

namespace Bogos;

    public class Program
    {
        public static void Main(string[] args)
      {
          using (var repo = new Repository(@"../"))
          
          {
            var dates = new ArrayList(); 
            foreach (Commit commit in repo.Commits)
            {
            dates.Add(commit.Author.When.Date.ToString());
            }
            var counter = 1; 
            var dict = new Dictionary<string, int>(); 
            foreach (string date in dates)
            {
            if(dict.ContainsKey(date))
            {
            dict[date]=dict[date]+1;
            } else {
              dict.Add(date, counter); 
            } 
            }
            foreach (var number in dict)
            {
              Console.WriteLine(number); 
            }
          }
      }
    }