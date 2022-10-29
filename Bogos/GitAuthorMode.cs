namespace Bogos;
public class GitAuthorMode
{
    private IConsole Console { get; }
    private Repository repo;
    public GitAuthorMode(IConsole console, Repository repo)
    {
        this.Console = console;
        this.repo = repo;
        run();
    }
    public GitAuthorMode(Repository repo)
    {
        this.Console = new RunningConsole();
        this.repo = repo;
        run();
    }

    void run()
    {
        foreach (var Committer in repo.Commits.Select(c => c.Committer).Distinct())
        {
            Console.WriteLine(Committer.ToString());

            foreach (var commit in repo.Commits.Where(c => c.Committer == Committer).GroupBy(k => k.Committer.When).Select(g => $"{g.Count()} {g.Key.ToString()}"))
            {
                Console.WriteLine($"\t{commit}");
            }

        }
    }
}