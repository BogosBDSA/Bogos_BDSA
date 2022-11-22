namespace Bogos.entities;

public class GitCommit
{
    public int Id { get; set; }
    /*NOTE: THIS IS NOT A 1 TO 1 copy of libgit2sharp commit, but a reduced version only keeping essential data*/

    public GitRepo Repo { get; set; }
    public GitSignature Committer { get; set; }
    public string Sha { get; set; }
    public DateTime Date { get; set; }
    public string Message { get; set; }
    public string Encoding { get; set; }

    public GitCommit(GitRepo repo, Commit commit)
    {
        Repo = repo;
        Committer = new GitSignature(commit.Committer);
        Sha = commit.Sha;
        Date = commit.Committer.When.UtcDateTime;
        Message = commit.Message;
        Encoding = commit.Encoding;
    }
    public GitCommit(GitRepo repo, GitSignature committer, DateTime date, String message = "", string sha = "", String encoding = "0")
    {
        Repo = repo;
        Committer = committer;
        Sha = sha;
        Date = date.ToUniversalTime();
        Message = message;
        Encoding = encoding;
    }
    private GitCommit() { } // Required for entity framework

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        var other = (GitCommit)obj;

        return other.Sha == this.Sha;
    }
}