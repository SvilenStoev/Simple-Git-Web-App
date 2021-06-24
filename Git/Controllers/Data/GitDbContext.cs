﻿using Microsoft.EntityFrameworkCore;

namespace Git.Data
{
    public class GitDbContext : DbContext
    {
        public DbSet<Commit> Commites { get; init; }
        public DbSet<Repository> Repositories { get; init; }
        public DbSet<User> Users { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Git;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Repository>()
                .HasOne(r => r.Owner)
                .WithMany(o => o.Repositories)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
              .Entity<Commit>()
              .HasOne(c => c.Creator)
              .WithMany(u => u.Commits)
              .HasForeignKey(c => c.CreatorId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
             .Entity<Commit>()
             .HasOne(c => c.Repository)
             .WithMany(r => r.Commits)
             .HasForeignKey(c => c.RepositoryId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
