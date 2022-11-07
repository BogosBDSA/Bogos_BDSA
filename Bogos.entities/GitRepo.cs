namespace Bogos.entities;
public class GitRepo
{
    public int id { get; set; }
    public GitCommit latestCommit { get; set; }
    public string Path { get; set; }
    public virtual ICollection<GitCommit> commits { get; set; }
    public GitRepo()
    {
        commits = new List<GitCommit>();
    }
    public GitRepo(Repository repo, string path, string branchName = "")
    {

        commits = new List<GitCommit>();
        if (branchName != "" && repo.Branches[branchName] != null) { Commands.Checkout(repo, branchName); }
        commits = repo.Commits.Select(commit => new GitCommit(commit)).ToList();
        this.commits.ToList().Sort((a, b) => a.CommitterWhen.CompareTo(b.CommitterWhen));
        //latestCommit = commits.ElementAt(0);
        Path = path;
    }
}