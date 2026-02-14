using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.Models;

public class User
{
  public Guid Id { get; set; } = Guid.NewGuid();

  [Required]
  [MaxLength(150)]
  public string FullName { get; set; } = default!;

  [Required]
  [EmailAddress]
  [MaxLength(200)]
  public string Email { get; set; } = default!;

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  // Navigation
  public ICollection<Project> Projects { get; set; } = new List<Project>();
}
