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

    modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();

    modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(200);

    modelBuilder.Entity<Project>()
        .HasOne(p => p.Owner)
        .WithMany(u => u.Projects)
        .HasForeignKey(p => p.OwnerId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Project>().HasIndex(p => p.Name);

    modelBuilder.Entity<Project>().Property(p => p.Name).IsRequired().HasMaxLength(100);

    modelBuilder.Entity<Project>().HasIndex(p => new { p.OwnerId, p.Name }).IsUnique();

    modelBuilder.Entity<TaskItem>()
        .HasOne(t => t.Project)
        .WithMany(p => p.Tasks)
        .HasForeignKey(t => t.ProjectId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<TaskItem>()
        .Property(t => t.Status)
        .HasConversion<string>()
        .HasMaxLength(50);

    modelBuilder.Entity<TaskItem>().HasIndex(t => new { t.ProjectId, t.Status });
  }
}
