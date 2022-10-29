
namespace Bogos;

public class Program
{
    public static void Main(string[] args)
    {
        Repository repo;
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
                    _ = new frequency(repo);
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