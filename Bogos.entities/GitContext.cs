namespace Bogos.entities;
public class GitContext : DbContext
{
    public DbSet<GitRepo> repos => Set<GitRepo>();
    public DbSet<GitCommit> commits => Set<GitCommit>();
    public DbSet<GitSignature> signatures => Set<GitSignature>();

    public GitContext(DbContextOptions<GitContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GitRepo>().HasMany(c => c.Commits).WithOne(c => c.Repo);
        modelBuilder.Entity<GitCommit>().HasOne(c => c.Committer).WithMany(k => k.commits);
        //optionsBuilder.UseNpgsql(@"Server=127.0.0.1;Port=5430;Database=bogosdb;User Id=postgres;Password=mypassword;");
    }
}
