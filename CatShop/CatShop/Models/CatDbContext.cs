namespace CatShop.Models
{
    using Microsoft.EntityFrameworkCore;

    public class CatDbContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        public CatDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected const string connectionString = @"Server=DESKTOP-3SHTERE\SQLEXPRESS02;Database=CatDbContext;Integrated Security=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
