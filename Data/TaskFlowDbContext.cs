using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Data;

public class TaskFlowDbContext : DbContext
{
    public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // User
        // =========================
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Email)
          .IsRequired()
          .HasMaxLength(200);
        });

        // =========================
        // Project
        // =========================
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasOne(p => p.Owner)
          .WithMany(u => u.Projects)
          .HasForeignKey(p => p.OwnerId)
          .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.Name)
          .IsRequired()
          .HasMaxLength(100);

            entity.HasIndex(p => p.Name);
            entity.HasIndex(p => new { p.OwnerId, p.Name })
          .IsUnique();
        });

        // =========================
        // TaskItem
        // =========================
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasOne(t => t.Project)
          .WithMany(p => p.Tasks)
          .HasForeignKey(t => t.ProjectId)
          .OnDelete(DeleteBehavior.Cascade);

            entity.Property(t => t.Status)
          .HasConversion<string>()
          .HasMaxLength(50);

            entity.HasIndex(t => new { t.ProjectId, t.Status });
        });
    }

}
