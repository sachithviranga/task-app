using Microsoft.EntityFrameworkCore;
using TaskApp.Infrastructure.Entities;

namespace TaskApp.Infrastructure.Data
{
    public class TaskAppDbContext(DbContextOptions<TaskAppDbContext> options) : DbContext(options)
    {
        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
        public DbSet<StatusEntity> Statuses => Set<StatusEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<TaskEntity>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TaskEntity>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<StatusEntity>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<StatusEntity>()
                .Property(c => c.Id)
                .ValueGeneratedNever();

        }
    }
}
