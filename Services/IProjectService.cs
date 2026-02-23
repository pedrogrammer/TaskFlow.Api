using TaskFlow.Api.DTOs.Projects;

namespace TaskFlow.Api.Services;

public interface IProjectService
{
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto);
    Task<ProjectResponseDto?> GetByIdAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, UpdateProjectDto dto);
    Task<bool> DeleteAsync(Guid id);
}
