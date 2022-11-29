namespace Bogos.core;
using Bogos.entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;
using Microsoft.Identity.Web;
using Newtonsoft.Json;

public class Program
{
    public static void Main(string[] args)
    {
        // Setup database
        var dbBuilder = new DbContextOptionsBuilder<GitContext>().UseNpgsql(@"Server=127.0.0.1;Port=5430;Database=bogosdb;User Id=postgres;Password=mypassword;");
        using var db = new GitContext(dbBuilder.Options);
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated(); 

        // Setup repository
        GitRepoRepository _repository = new GitRepoRepository(db);

        // Setup web app
        var webAppBuilder = WebApplication.CreateBuilder(args);
        webAppBuilder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(webAppBuilder.Configuration.GetSection("AzureAdB2C"));
        webAppBuilder.Services.AddAuthorization();
        webAppBuilder.Services.AddDbContext<GitContext>(builder => builder = dbBuilder);
        webAppBuilder.Services.AddEndpointsApiExplorer();
        webAppBuilder.Services.AddSwaggerGen();
        webAppBuilder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyOrigin();
                });
        });

	    var webApp = webAppBuilder.Build();
        
        webApp.UseCors();
        webApp.UseAuthorization();
        webApp.UseSwagger();
        webApp.UseSwaggerUI();
        webApp.UseHttpsRedirection();

        // Setup endpoints
	    webApp.MapGet("/", () => 
            @"To run Author and Frequency mode: 
            Type /frequency or /author your localHost:Port, 
            and add the users name and which repository you want to display. \n 
            For example : http://localhost:5243/frequency/<User>/<Repository>"
        );
        webApp.MapGet("/frequency/{author}/{repo}", [Authorize][RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")] (string author, string repo) => {
                
                var (_, gitRepo) = _repository.HandleUri($"https://github.com/{author}/{repo}.git");
                if (gitRepo == null) 
                {
                    return "Error"; // TODO: Handle error
                }
                var frequencyModeDto = GitInsight.FrequencyMode(gitRepo);
                return JsonConvert.SerializeObject(frequencyModeDto, Formatting.Indented);
        });
        webApp.MapGet("/author/{author}/{repo}", [Authorize][RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes")] (string author, string repo) => {
                var (_, gitRepo) = _repository.HandleUri($"https://github.com/{author}/{repo}.git");
                if (gitRepo == null) 
                {
                    return "Error"; // TODO: Handle error
                }
                var authorModeDto = GitInsight.AuthorMode(gitRepo);
                return JsonConvert.SerializeObject(authorModeDto, Formatting.Indented);
        });
        

        webApp.Run();
    }
}