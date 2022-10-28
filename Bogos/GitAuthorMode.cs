namespace Bogos;
public class GitAuthorMode
{
    private IConsole Console { get; }
    private Repository repo;
    public GitAuthorMode(IConsole console, Repository repo)
    {
        this.Console = console;
        this.repo = repo;
    }
    public GitAuthorMode(Repository repo)
    {
        this.Console = new RunningConsole();
        this.repo = repo;

    }
}