namespace Bogos;
public class AuthorMode
{
    private IConsole Console { get; }
    private Repository repo;
    public AuthorMode(IConsole console, Repository repo)
    {
        this.Console = console;
        this.repo = repo;
        run();
    }
    public AuthorMode(Repository repo)
    {
        this.Console = new RunningConsole();
        this.repo = repo;
        run();
    }

    void run()
    {
        foreach (var Committer in repo.Commits.Select(c => c.Committer).DistinctBy(c => c.Name))
        {
            Console.WriteLine(Committer.ToString());
            var Commits = repo.Commits
                                .Where(c => c.Committer.Name == Committer.Name)
                                .GroupBy(k => new DateTime(
                                    k.Committer.When.Year,
                                    k.Committer.When.Month,
                                    k.Committer.When.Day
                                    ))
                                .Select(g => $@"{g.Count()} {g.Key.ToString("dd/MM/yyyy")}");

            foreach (var commit in Commits)
            {
                Console.WriteLine($"\t{commit}");
            }

        }
    }
}