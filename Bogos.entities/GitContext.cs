namespace Bogos.entities;
public class GitContext : DbContext
{
    public DbSet<GitRepo> repos => Set<GitRepo>();

    //public DbSet<AuthorData> AD { get; set; }
    //public DbSet<FrequencyData> FD { get; set; }

    public GitContext(DbContextOptions<GitContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GitRepo>().HasMany(c => c.commits);

        //optionsBuilder.UseNpgsql(@"Server=127.0.0.1;Port=5430;Database=bogosdb;User Id=postgres;Password=mypassword;");

    }
}
