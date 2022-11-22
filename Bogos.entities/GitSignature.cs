namespace Bogos.entities;

public class GitSignature
{
    public int Id { get; set; }
    public String Name { get; set; }
    public String Email { get; set; }
    public ICollection<GitCommit> commits { get; set; }
    public GitSignature(Signature sig)
    {
        Name = sig.Name;
        Email = sig.Email;
        commits = new List<GitCommit>();
    }
    public GitSignature(string name, string email)
    {
        Name = name;
        Email = email;
        commits = new List<GitCommit>();
    }
    private GitSignature() { } // Required for entity framework 

    public override string ToString()
    {
        return $"{Name} {Email}";
    }
}