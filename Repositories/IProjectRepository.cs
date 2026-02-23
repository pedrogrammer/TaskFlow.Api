using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories;

public interface IProjectRepository
{
    Task<Project> AddAsync(Project project);
    Task<Project?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task UpdateAsync(Project project);
    Task DeleteAsync(Project project);
}