namespace Bogos.entities;
public class GitRepo
{
    public int Id { get; set; }

    public Uri? Uri { get; set; }
    public virtual ICollection<GitCommit> Commits { get; set; } = new List<GitCommit>();
    public GitRepo()
    {
    }
    public GitRepo(string url, string branchName = "")
    {
        Repository repo = null;
        var tempPath = "./TempRepoFolder";
        try
        {
            var clonedPath = Repository.Clone(url, tempPath);
            repo = new Repository(clonedPath);
            if (branchName != "" && repo.Branches[branchName] != null) { Commands.Checkout(repo, branchName); }
            Commits = repo.Commits.Select(commit => new GitCommit(this, commit)).ToList();
        }
        finally
        {  
            if(repo != null) {repo.Dispose();}
            DeleteReadOnlyDirectory(tempPath);   
        }
    }


//This function is borrowed from https://stackoverflow.com/questions/2316308/remove-readonly-attribute-from-directory
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