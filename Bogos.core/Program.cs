namespace Bogos.core;
using Bogos.entities;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = new DbContextOptionsBuilder<GitContext>().UseNpgsql(@"Server=127.0.0.1;Port=5430;Database=bogosdb;User Id=postgres;Password=mypassword;");
        using var db = new GitContext(builder.Options);
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated(); 

        var hardcodepath = "https://github.com/nselpriv/Bogos_BDSA.git"; 
        GitRepoRepository repository = new GitRepoRepository(db);
        var printable = repository.ReadRepoByUri(hardcodepath);
        GitRepo printable2 = null;
        if(printable == null)
        {
            printable2 = new GitRepo(hardcodepath);
            repository.CreateRepo(printable2);
            
        } else {
             printable2 = new GitRepo(hardcodepath);
                if(!printable.Equals(printable2))
                {
                    repository.UpdateRepo(printable2);
                    Console.WriteLine("it updated");
                    return;
                }
                Console.WriteLine("is the same");

        }

        /*
        if(repository.ReadRepoByUri(hardcodepath))

        var freq = GitInsight.FrequencyMode(printable);
        var auth = GitInsight.AuthorMode(printable); 
        */


        // API

        // -> URI("")
        // repo = GITREPO(URI) 


        // AUTHOR MODE <- repo

        // FREQUENCY MODE <- repo









    }
}