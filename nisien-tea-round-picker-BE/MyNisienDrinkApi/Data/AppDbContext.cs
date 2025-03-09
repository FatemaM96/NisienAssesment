using Microsoft.EntityFrameworkCore;
using MyNisienDrinkApi.Models;

namespace MyNisienDrinkApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<DrinkOrder> DrinkOrders { get; set; }
        public DbSet<DrinkRun> DrinkRuns { get; set; }
        public DbSet<DrinkRunParticipant> DrinkRunParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<DrinkRunParticipant>()
                .HasKey(drp => drp.Id);

            
            modelBuilder.Entity<DrinkRunParticipant>()
                .HasOne(drp => drp.User)
                .WithMany(u => u.DrinkRunParticipants)
                .HasForeignKey(drp => drp.UserId);

       
            modelBuilder.Entity<DrinkRunParticipant>()
                .HasOne(drp => drp.DrinkOrder)
                .WithMany(doi => doi.DrinkRunParticipants)
                .HasForeignKey(drp => drp.DrinkOrderId);

            modelBuilder.Entity<DrinkOrder>()
                .OwnsOne(d => d.AdditionalSpecification);
        }
    }
}
