namespace Bogos.entities;

public class GitCommit
{
    public int Id { get; set; }
    /*NOTE: THIS IS NOT A 1 TO 1 copy of libgit2sharp commit, but a reduced version only keeping essential data*/

    [Required]
    public GitSignature Committer { get; set; }

    public string Message { get; set; }
    public string Encoding { get; set; }
    public string Sha { get; set; }
    public DateTimeOffset CommitterWhen { get; set; }
    public GitRepo belongsTo { get; set; }

    public GitCommit() { }
    public GitCommit(GitRepo repo, Commit commit)
    {
        CommitterWhen = commit.Committer.When;
        Committer = new GitSignature(commit.Committer);
        Encoding = commit.Encoding;
        Message = commit.Message;
        Sha = commit.Sha;
    }
    public GitCommit(GitRepo repo, GitSignature committer, String message = "", string sha = "", String encoding = "0")
    {
        CommitterWhen = committer.When;
        Committer = committer;
        belongsTo = repo;
        Encoding = encoding;
        Message = message;
        Sha = sha;

    }
}