namespace Bogos{

    public class GitContext : DbContext
    {
        public DbSet<AuthorData> AD { get; set; }
        public DbSet<FrequencyData> FD { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(@"Server=127.0.0.1;Port=5430;Database=bogosdb;User Id=postgres;Password=mypassword;");

    }

}