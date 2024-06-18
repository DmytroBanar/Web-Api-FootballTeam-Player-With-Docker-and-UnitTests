using Microsoft.EntityFrameworkCore;

namespace WebApi.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<FootballTeam> FootballTeams { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Налаштування моделей тут, якщо потрібно
            modelBuilder.Entity<FootballTeam>().HasKey(ft => ft.TeamId);
            modelBuilder.Entity<Player>().HasKey(p => p.PlayerId);

            // Встановлення зв'язків між сутностями
            modelBuilder.Entity<Player>()
                .HasOne<FootballTeam>()
                .WithMany()
                .HasForeignKey(p => p.TeamNameId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
