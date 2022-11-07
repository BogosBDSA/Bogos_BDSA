namespace Bogos.entities;

public class GitCommit
{
    public int id { get; set; }
    /*NOTE: THIS IS NOT A 1 TO 1 copy of libgit2sharp commit, but a reduced version only keeping essential data*/
    public GitSignature Author { get; set; }
    public GitSignature Committer { get; set; }

    public string Message { get; set; }
    public string Encoding { get; set; }
    public string Sha { get; set; }
    public DateTimeOffset CommitterWhen { get; set; }
    public DateTimeOffset AuthorWhen { get; set; }

    public GitCommit() { }
    public GitCommit(Commit commit)
    {
        CommitterWhen = commit.Committer.When;
        AuthorWhen = commit.Author.When;
        Committer = new GitSignature(commit.Committer);
        Encoding = commit.Encoding;
        Message = commit.Message;
        Author = new GitSignature(commit.Author);
        Sha = commit.Sha;
    }
    public GitCommit(GitSignature author, GitSignature committer, String message = "", string sha = "", String encoding = "0")
    {
        CommitterWhen = committer.When;
        AuthorWhen = author.When;
        Committer = committer;
        Encoding = encoding;
        Message = message;
        Author = author;
        Sha = sha;

    }
}