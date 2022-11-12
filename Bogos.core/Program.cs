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

        var build = WebApplication.CreateBuilder(args);

        build.Services.AddDbContext<GitContext>(opt => opt = builder);

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

        //opt.UseNpgsql(@"Server=127.0.0.1;Port=5430;Database=bogosdb;User Id=postgres;Password=mypassword;"));

        build.Services.AddEndpointsApiExplorer();
        build.Services.AddSwaggerGen();

	    var app = build.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

	    app.MapGet("/", () => "Hello World!");

    //Get function to Get data from database
        //app.MapGet("/data", (GitContext data) => data.Repos.ToListAsync());

        app.MapGet("/gitinsight/frequency/{repo}", (string repo) => {
                var path = $"https://github.com/{repo}.git";
                

                var result = repository.ReadRepoByUri(hardcodepath);
               
                var converthis = GitInsight.FrequencyMode(result);
                var returnthis = Newtonsoft.Json.JsonConvert.SerializeObject(converthis);
                
                
                return returnthis;
        });


        app.MapGet("/gitinsight/author/{repo}", (string repo) => {
                var path = $"https://github.com/{repo}.git";

                var result = repository.ReadRepoByUri(hardcodepath);
               
                var converthis = GitInsight.AuthorMode(result);
                var returnthis = Newtonsoft.Json.JsonConvert.SerializeObject(converthis);
                
                
                return returnthis;
        });
        // API

        // -> URI("")
        // repo = GITREPO(URI) 


        // AUTHOR MODE <- repo

        // FREQUENCY MODE <- repo

        app.Run();







    }
}