using AspNetCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=DESKTOP-710T6CK\\MSSQLSERVER01;Database=TEST-Website;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=true;Integrated Security=SSPI");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>().HasMany(meet => meet.Users);
            modelBuilder.Entity<User>().OwnsMany(user => user.SimpleBlocks);
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SimpleTimeBlock> SimpleTimeBlocks { get; set; }
    }
}

