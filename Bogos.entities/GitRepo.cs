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
    public GitRepo(string url, string branchName = "")
    {
        var tempPath = "./TempRepoFolder";
        try
        {
            Directory.CreateDirectory(tempPath);
            var opt = new CloneOptions();
            var clonedPath = Repository.Clone(url, tempPath, opt);
            var repo = new Repository(clonedPath);
            if (branchName != "" && repo.Branches[branchName] != null) { Commands.Checkout(repo, branchName); }
            Commits = repo.Commits.Select(commit => new GitCommit(this, commit)).ToList();
            // Kan muligvis fjernes, kommer an på hvordan det sorteres.
            this.Commits.ToList().Sort((a, b) => a.CommitterWhen.CompareTo(b.CommitterWhen));
        }
        finally
        {

            DeleteReadOnlyDirectory(tempPath);
            Directory.Delete("TempRepoFolder");

        }


    }

    private static void DeleteReadOnlyDirectory(string directory)
    {

        foreach (var subdirectory in Directory.EnumerateDirectories(directory))
        {
            DeleteReadOnlyDirectory(subdirectory);
        }

        foreach (var fileName in Directory.EnumerateFiles(directory))
        {
            var fileInfo = new FileInfo(fileName);
            fileInfo.Attributes = FileAttributes.Normal;
            fileInfo.Delete();
        }
        Directory.Delete(directory);
    }
}