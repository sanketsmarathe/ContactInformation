using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EvolentTest.Database.Entities;

namespace EvolentTest.Database
{
    public class EvolentTestDBContext : IdentityDbContext<ApplicationUser>
    {
        public EvolentTestDBContext(DbContextOptions<EvolentTestDBContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<ApplicationUser> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Contact>().ToTable("Contact");
        }
    }
}
