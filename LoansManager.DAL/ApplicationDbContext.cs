using System;
using LoansManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoansManager.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<LoanEntity> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException($"{nameof(modelBuilder)} can not be null");
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasKey(x => x.UserName);
        }
    }
}
