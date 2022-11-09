namespace Bogos.entities;
public class GitRepo
{
    public int Id { get; set; }

    // skal måske ændres til url, når vi arbejder med vores API, dette giver dog ingen mening
    // hvis man bruger programmet lokalt, da man vil give det en system path.

    // Måske bruge første commit til at samligne.
    public Uri? Uri { get; set; }
    public virtual ICollection<GitCommit> Commits { get; set; } = new List<GitCommit>();
    public GitRepo()
    {
    }
    public GitRepo(Repository repo, string uri, string branchName = "")
    {

        Commits = new List<GitCommit>();
        if (branchName != "" && repo.Branches[branchName] != null) { Commands.Checkout(repo, branchName); }

        Commits = repo.Commits.Select(commit => new GitCommit(this, commit)).ToList();

        this.Commits.ToList().Sort((a, b) => a.CommitterWhen.CompareTo(b.CommitterWhen));

        Uri = new Uri(uri);
    }

    public override bool Equals(object? obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
        {
            return false;
        } 
        var other = (GitRepo) obj;
        return this.Uri == other.Uri;
    }

}