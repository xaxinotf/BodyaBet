using BodyaBet.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BodyaBet.Contexts
{
    public class VolleyballContext : DbContext
    {
        public VolleyballContext(DbContextOptions<VolleyballContext> options)  : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Matches>()
                .HasOne<Team>(m => m.HomeTeam)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Matches>()
               .HasOne<Team>(m => m.GuestTeam)
               .WithMany()
               .OnDelete(DeleteBehavior.ClientCascade);
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Matches> Matches { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }


    }
}
