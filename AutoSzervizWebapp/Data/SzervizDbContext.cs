using AutoSzervizWebapp.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoSzervizWebapp.Data
{
    public class SzervizDbContext : DbContext
    {
        public SzervizDbContext(DbContextOptions<SzervizDbContext> options) : base(options)
        {
        }
        public DbSet<Ugyfel> Ugyfelek { get; set; }
        public DbSet<Javitas> Javitasok { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Javitas>()
                .HasOne(j => j.Ugyfel)
                .WithMany(u => u.Javitasok)
                .HasForeignKey(j => j.UgyfelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
