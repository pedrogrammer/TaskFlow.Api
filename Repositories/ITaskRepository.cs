using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories;

public interface ITaskRepository
{
    Task<TaskItem> AddAsync(TaskItem task);
    Task<IEnumerable<TaskItem>> GetByProjectAsync(
        Guid projectId,
        int page,
        int pageSize,
        string? sortBy);
}