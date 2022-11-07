namespace Bogos.entities;

public class GitSignature
{
    public int id { get; set; }
    public String Name { get; set; }
    public String Email { get; set; }
    public DateTimeOffset When { get; set; }

    public GitSignature()
    {

    }
    public GitSignature(Signature sig)
    {
        Name = sig.Name;
        Email = sig.Email;
        When = sig.When;
    }
    public GitSignature(string name, string email, DateTimeOffset when)
    {
        Name = name;
        Email = email;
        When = when;
    }
}