namespace Bogos.entities;
public class GitRepo
{
    public int Id { get; set; }

    // skal måske ændres til url, når vi arbejder med vores API, dette giver dog ingen mening
    // hvis man bruger programmet lokalt, da man vil give det en system path.

    // Måske bruge første commit til at samligne.
    public Uri? Uri { get; set; }
    public virtual ICollection<GitCommit> commits { get; set; } = new List<GitCommit>();
    public GitRepo()
    {
    }
    public GitRepo(Repository repo, string uri, string branchName = "")
    {

        commits = new List<GitCommit>();
        if (branchName != "" && repo.Branches[branchName] != null) { Commands.Checkout(repo, branchName); }

        commits = repo.Commits.Select(commit => new GitCommit(this, commit)).ToList();

        this.commits.ToList().Sort((a, b) => a.CommitterWhen.CompareTo(b.CommitterWhen));

        Uri = new Uri(uri);
    }

}