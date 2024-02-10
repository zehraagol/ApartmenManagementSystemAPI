using AparmentSystemAPI.Apartment;
using AparmentSystemAPI.Payments;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AparmentSystemAPI.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
    {
        //flats table
        public DbSet<Flat> Flats { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;

        //OnModelCreating method    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
      

            //modelBuilder.Entity<Flat>()
            //    .HasMany(f => f.Payments)
            //    .WithOne(p => p.Flat)
            //    .HasForeignKey(p => p.FlatId)
            //    .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            //modelBuilder.Entity<Flat>()

           


        }

        }
    }
