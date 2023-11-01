using ChessGame.Models;
using Microsoft.EntityFrameworkCore;
using ChessGame.Data;
namespace ChessGame.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Khai báo các DbSet và cấu hình DbContext tại đây
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MapEntity> maps { get; set; }
        public DbSet<Account> Account1 { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MapEntity>().HasKey(e => new {e.Idmaps, e.X, e.Y });
            modelBuilder.Entity<Account>().HasKey(e => new { e.username });
            modelBuilder.Entity<MapEntity>().ToTable("maps");
            modelBuilder.Entity<Account>().ToTable("Account");
        }
    }
}

