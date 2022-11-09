namespace Bogos.entities;
public class GitContext : DbContext
{
    public DbSet<GitRepo> Repos => Set<GitRepo>();
    public DbSet<GitCommit> Commits => Set<GitCommit>();
    public DbSet<GitSignature> Signatures => Set<GitSignature>();


    public GitContext(DbContextOptions<GitContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GitRepo>().HasMany(c => c.Commits).WithOne(k => k.belongsTo);
        modelBuilder.Entity<GitCommit>().HasOne(c => c.Committer).WithMany(k => k.commits);
        //optionsBuilder.UseNpgsql(@"Server=127.0.0.1;Port=5430;Database=bogosdb;User Id=postgres;Password=mypassword;");

    }
}
