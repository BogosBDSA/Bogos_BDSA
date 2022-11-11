namespace Bogos.entities;

public class GitSignature
{
    public int Id { get; set; }
    public String Name { get; set; }
    public String Email { get; set; }
    public DateTimeOffset When { get; set; }
    public ICollection<GitCommit> commits { get; set; }

    public GitSignature()
    {

    }
    public GitSignature(Signature sig)
    {
        Name = sig.Name;
        Email = sig.Email;
        When = sig.When.UtcDateTime;
        commits = new List<GitCommit>();
    }
    public GitSignature(string name, string email, DateTimeOffset when)
    {
        Name = name;
        Email = email;
        When = when.UtcDateTime;
        commits = new List<GitCommit>();
    }

    public GitSignature(string name, string email, DateTime when)
    {
        Name = name;
        Email = email;
        When = when.ToUniversalTime();
        commits = new List<GitCommit>();

    }

    public override string ToString()
    {
        return $"{Name} {Email}";
    }
}