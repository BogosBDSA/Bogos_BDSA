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

        GitRepoRepository _repository = new GitRepoRepository(db);
       
        build.Services.AddEndpointsApiExplorer();
        build.Services.AddSwaggerGen();

	    var app = build.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

	    app.MapGet("/", () => "To run Author and Frequency mode: Type /frequency or /author your localHost:Port, and add the users name and which repository you want to display. \n For example : http://localhost:5243/frequency/<User>/<Repository>");


        app.MapGet("/frequency/{author}/{repo}", (string author, string repo) => {
                var path = $"https://github.com/{author}/{repo}.git";

                
                var convertThis = GitInsight.FrequencyMode(HandleRepo(path, _repository));
                var returnJson = Newtonsoft.Json.JsonConvert.SerializeObject(convertThis);
                
                return returnJson;
        });


        app.MapGet("/author/{author}/{repo}", (string author, string repo) => {
                var path = $"https://github.com/{author}/{repo}.git";
                
               
                var convertThis = GitInsight.AuthorMode(HandleRepo(path, _repository));
                var returnJson = Newtonsoft.Json.JsonConvert.SerializeObject(convertThis);
                
                
                return returnJson;
        });

        app.Run();

    }
    private static GitRepo HandleRepo(string path, GitRepoRepository _repository){

        var result = _repository.ReadRepoByUri(path);
                if(result == null){
                result = _repository.CreateRepo(new GitRepo(path)).Item2;
                } else {
                    _repository.UpdateRepo(result);
        }
        return result;
    }
}