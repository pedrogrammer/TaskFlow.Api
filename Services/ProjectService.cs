using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.DTOs.Projects;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Services;

public class ProjectService : IProjectService
{
  private readonly TaskFlowDbContext _context;

  public ProjectService(TaskFlowDbContext context)
  {
    _context = context;
  }

  public async Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto)
  {
    var project = new Project
    {
      Name = dto.Name,
      Description = dto.Description,
      OwnerId = dto.OwnerId
    };

    _context.Projects.Add(project);
    await _context.SaveChangesAsync();

    return MapToResponse(project);
  }

  public async Task<ProjectResponseDto?> GetByIdAsync(Guid id)
  {
    var project = await _context.Projects
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id == id);

    return project is null ? null : MapToResponse(project);
  }

  public async Task<bool> UpdateAsync(Guid id, UpdateProjectDto dto)
  {
    var project = await _context.Projects.FindAsync(id);

    if (project is null)
      return false;

    project.Name = dto.Name;
    project.Description = dto.Description;

    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> DeleteAsync(Guid id)
  {
    var project = await _context.Projects.FindAsync(id);

    if (project is null)
      return false;

    _context.Projects.Remove(project);
    await _context.SaveChangesAsync();
    return true;
  }

  private static ProjectResponseDto MapToResponse(Project project)
  {
    return new ProjectResponseDto
    {
      Id = project.Id,
      Name = project.Name,
      Description = project.Description,
      OwnerId = project.OwnerId,
      CreatedAt = project.CreatedAt
    };
  }
}
