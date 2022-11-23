using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore;
using Bogos.core;
using Microsoft.AspNetCore.Hosting;
using Bogos.entities;
using Microsoft.Data.Sqlite;

namespace Bogos.integration.tests;

/*Helper class to inject the Sqlite inmemory database into the API at runtime. 
We can then use the WebApplicationFactory, to test as we normally would.*/
public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {


        builder.ConfigureServices(services =>
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<GitContext>));

            services.Remove(descriptor);
            services.AddDbContext<GitContext>(options =>
            {
                options.UseSqlite(connection);
            });
        });

        builder.UseEnvironment("Development");
    }
}
