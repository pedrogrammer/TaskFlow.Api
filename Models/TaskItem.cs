using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.Models;

public class TaskItem
{
  public Guid Id { get; set; } = Guid.NewGuid();

  [Required]
  [MaxLength(200)]
  public string Title { get; set; } = default!;

  public TaskStatus Status { get; set; } = TaskStatus.Todo;

  [Required]
  public Guid ProjectId { get; set; }

  public DateTime? DueDate { get; set; }

  // Navigation
  public Project Project { get; set; } = default!;
}
