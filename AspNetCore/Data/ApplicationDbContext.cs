﻿using AspNetCore.Data.Models;
using Microsoft.EntityFrameworkCore;
using StoppedFishing.Data.Models;

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
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>().HasMany(meet => meet.Users).WithMany(user => user.Meetings);
            modelBuilder.Entity<User>().OwnsMany(user => user.TimeBlocks);
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TimeBlock> TimeBlocks { get; set; }
    }
}

