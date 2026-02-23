using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.DTOs.Projects;

public class CreateProjectDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = default!;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public Guid OwnerId { get; set; }
}
