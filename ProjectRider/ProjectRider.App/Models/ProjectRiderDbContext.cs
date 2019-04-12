namespace ProjectRider.App.Models
{
    using Microsoft.EntityFrameworkCore;

    public class ProjectRiderDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        //public ProjectRiderDbContext(DbContextOptions<ProjectRiderDbContext> options) : base(options)
        //{
        //    this.InitializeDatabase();
        //}

        //private void InitializeDatabase()
        //{
        //    this.Database.EnsureCreated();
        //}

        private const string ConnectionString = @"Server=DESKTOP-3SHTERE\SQLEXPRESS02;Database=ProjectRiderDb;Integrated Security=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}