using AparmentSystemAPI.Models.Flats;
using AparmentSystemAPI.Models.Identities;
using AparmentSystemAPI.Models.Payments;
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

        }

    }
}
