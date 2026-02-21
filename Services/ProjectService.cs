using TaskFlow.Api.DTOs.Projects;
using TaskFlow.Api.Models;
using TaskFlow.Api.Repositories;

namespace TaskFlow.Api.Services;

public class ProjectService : IProjectService
{
  private readonly IProjectRepository _repository;

  public ProjectService(IProjectRepository repository)
  {
    _repository = repository;
  }

  public async Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto)
  {
    var project = new Project
    {
      Name = dto.Name,
      Description = dto.Description,
      OwnerId = dto.OwnerId
    };

    await _repository.AddAsync(project);

    return MapToResponse(project);
  }

  public async Task<ProjectResponseDto?> GetByIdAsync(Guid id)
  {
    var project = await _repository.GetByIdAsync(id);
    return project is null ? null : MapToResponse(project);
  }

  public async Task<bool> UpdateAsync(Guid id, UpdateProjectDto dto)
  {
    var project = await _repository.GetByIdAsync(id);

    if (project is null)
      return false;

    project.Name = dto.Name;
    project.Description = dto.Description;

    await _repository.UpdateAsync(project);
    return true;
  }

  public async Task<bool> DeleteAsync(Guid id)
  {
    var project = await _repository.GetByIdAsync(id);

    if (project is null)
      return false;

    await _repository.DeleteAsync(project);
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