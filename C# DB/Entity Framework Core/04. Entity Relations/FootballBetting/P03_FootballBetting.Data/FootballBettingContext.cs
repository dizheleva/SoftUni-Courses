using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Models;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {
                
        }

        public FootballBettingContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=FootballBetting;Integrated Security=True");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>(x =>
            {
                x.HasOne(y => y.PrimaryKitColor)
                    .WithMany(y => y.PrimaryKitTeams)
                    .HasForeignKey(y => y.PrimaryKitColorId)
                    .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(y => y.SecondaryKitColor)
                    .WithMany(y => y.SecondaryKitTeams)
                    .HasForeignKey(y => y.SecondaryKitColorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Game>(x =>
            {
                x.HasOne(y => y.HomeTeam)
                    .WithMany(y => y.HomeGames)
                    .HasForeignKey(y => y.HomeTeamId)
                    .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(y => y.AwayTeam)
                    .WithMany(y => y.AwayGames)
                    .HasForeignKey(y => y.AwayTeamId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PlayerStatistic>(x =>
            {
                x.HasKey(y => new {y.GameId, y.PlayerId});
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
