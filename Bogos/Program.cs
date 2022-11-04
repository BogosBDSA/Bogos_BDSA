
namespace Bogos;

public class Program
{
    public static void Main(string[] args)
    {
        Repository repo;
        using var db = new GitContext();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();



        while (true)
        {
            Console.WriteLine("Please enter a valid repository path.");
            var repoPath = Console.ReadLine();
            try
            {
                repo = new Repository(repoPath);

                if (repo != null) break;
            }
            catch (RepositoryNotFoundException)
            {

                Console.WriteLine("Not a valid path! Try again!\n");
            }

        }

        while (true)
        {
            Console.WriteLine("\nPlease choose which mode you wish to use.");
            Console.WriteLine("Type 1 for Frequency Mode");
            Console.WriteLine("Type 2 for Author Mode");
            

            var mod = Console.ReadLine();
            switch (mod)
            {
                case "1":
                    Console.WriteLine("\nFrequency Mode:");
                    var rep =new frequency(repo);
                    var res =rep.gitInsightfrequency();
                    int Id = 0;
                    foreach (var commit in res)
                    {
                    Id++;
                    
                    db.FD.Add(new() {Id=Id, Date = commit.date, Commit = commit.commit});
                    db.SaveChanges();
                    }
                    foreach (var FrequencyData in db.FD)
                    {
                        
                        Console.Write(FrequencyData.Commit);
                        Console.WriteLine(FrequencyData.Date);
                    }
                    
                    break;
                case "2":
                    Console.WriteLine("\nAuthor Mode:");
                    _ = new AuthorMode(repo);
                    break;
                default:
                    Console.WriteLine("Unable to understand input. Try again!\n");
                    continue;
            }
            break;

        }


    }
}