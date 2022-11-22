namespace Bogos.entities;
public class GitRepo
{
    public int Id { get; set; }
    public string? Uri { get; set; }
    public virtual ICollection<GitCommit> Commits { get; set; } = new List<GitCommit>();
    public GitRepo(string url, string branchName = "")
    {
        Repository? tempRepo = null;
        var tempPath = Environment.CurrentDirectory + "/TempRepoFolder";
        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        try
        {
            var clonedPath = Repository.Clone(url, tempPath);
            tempRepo = new Repository(clonedPath);
            
            if (branchName != "" && tempRepo.Branches[branchName] != null) 
            { 
                Commands.Checkout(tempRepo, branchName); 
            }
            
            Commits = tempRepo.Commits.Select(commit => new GitCommit(this, commit)).ToList();
        }
        catch 
        {
            Console.WriteLine("Remote repo not found");
        }
        finally
        {  
            Uri = url;
            if (tempRepo != null) 
            {
                tempRepo.Dispose();
            }
            DeleteReadOnlyDirectory(tempPath);   
        }
    }
    private GitRepo() { } // Required for entity framework

    // This function is borrowed from https://stackoverflow.com/questions/2316308/remove-readonly-attribute-from-directory
    private static void DeleteReadOnlyDirectory(string directory)
    {
        if (!Directory.Exists(directory)) 
            return;
            
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

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        var other = (GitRepo)obj;

        if (other.Uri != this.Uri) {
            return false;
        }

        if (!other.Commits.SequenceEqual(this.Commits)) {
            return false;
        }

        return true;
    }
}