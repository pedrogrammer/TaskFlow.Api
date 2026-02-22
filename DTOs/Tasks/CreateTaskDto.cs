using System.ComponentModel.DataAnnotations;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.DTOs.Tasks;

public class CreateTaskDto
{
  [Required]
  [MaxLength(200)]
  public string Title { get; set; } = default!;

  public DateTime? DueDate { get; set; }

  public CurrentTaskStatus Status { get; set; } = CurrentTaskStatus.Todo;
}