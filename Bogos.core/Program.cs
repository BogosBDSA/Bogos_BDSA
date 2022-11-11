namespace Bogos.core;
using Bogos.entities;
public class Program
{
    public static void Main(string[] args)
    {
        /* var builder = new DbContextOptionsBuilder<GitContext>().UseNpgsql(@"Server=127.0.0.1;Port=5430;Database=bogosdb;User Id=postgres;Password=mypassword;");
        using var db = new GitContext(builder.Options);
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated(); */

        var printable = new GitRepo("https://github.com/nselpriv/Bogos_BDSA.git");

        // API

        // -> URI("")
        // repo = GITREPO(URI) 


        // AUTHOR MODE <- repo

        // FREQUENCY MODE <- repo









    }
}