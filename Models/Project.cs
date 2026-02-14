using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.Models;

public class Project
{
  public Guid Id { get; set; } = Guid.NewGuid();

  [Required]
  [MaxLength(100)]
  public string Name { get; set; } = default!;

  [MaxLength(500)]
  public string? Description { get; set; }

  [Required]
  public Guid OwnerId { get; set; }

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  // Navigation
  public User Owner { get; set; } = default!;
  public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
