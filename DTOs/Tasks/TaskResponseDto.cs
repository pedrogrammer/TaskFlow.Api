using TaskFlow.Api.Models;

namespace TaskFlow.Api.DTOs.Tasks;

public class TaskResponseDto
{
  public Guid Id { get; set; }
  public string Title { get; set; } = default!;
  public CurrentTaskStatus Status { get; set; }
  public DateTime? DueDate { get; set; }
}