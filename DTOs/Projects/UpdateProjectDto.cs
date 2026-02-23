using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.DTOs.Projects;

public class UpdateProjectDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = default!;

    [MaxLength(500)]
    public string? Description { get; set; }
}
