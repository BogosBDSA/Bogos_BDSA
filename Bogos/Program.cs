using LibGit2Sharp;

namespace Bogos;

    public class Program
    {
        public static void Main(string[] args)
      {
          using (var repo = new Repository(@"../"))
          {
            foreach (Commit commit in repo.Commits)
            {
            Console.WriteLine("Author: {0}", commit.Author.Name);
            Console.WriteLine("Message: {0}", commit.MessageShort);
            }
          }
      }
    }