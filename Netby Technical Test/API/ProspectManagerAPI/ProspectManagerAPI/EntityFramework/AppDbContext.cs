using Microsoft.EntityFrameworkCore;
using ProspectManagerAPI.Models;

namespace ProspectManagerAPI.EntityFramework
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Prospect> Prospects { get; set; }
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Prospect)
                .WithMany(p => p.Activities)
                .HasForeignKey(a => a.ProspectId);

            modelBuilder.Entity<Activity>()
                .ToTable(t => t.HasCheckConstraint("CHK_Activities_Type", "Type IN ('llamada', 'mensaje', 'correo')"))
                .Property(a => a.Rating)
                .IsRequired();

            modelBuilder.Entity<Activity>()
                .ToTable(t => t.HasCheckConstraint("CHK_Activities_Rating", "Rating BETWEEN 1 AND 5"))
                .Property(a => a.Rating)
                .IsRequired();
        }
    }
}
