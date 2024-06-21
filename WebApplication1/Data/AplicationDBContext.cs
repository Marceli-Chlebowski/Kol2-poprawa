using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Object_Owner> OwnerObjects { get; set; }
        public DbSet<OwnedObject> OwnedObjects { get; set; } // Updated DbSet name

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Owner>()
                .HasMany(o => o.Object_Owners)
                .WithOne(oo => oo.Owner)
                .HasForeignKey(oo => oo.OwnerId);

            modelBuilder.Entity<Owner>().HasData(new Owner
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                PhoneNumber = "123456789"
            });

            modelBuilder.Entity<Object_Owner>().HasData(new Object_Owner
            {
                ObjectId = 1,
                Width = 1.5,
                Height = 2.0,
                Type = "exampleType",
                OwnerId = 1
            });
        }
    }
}