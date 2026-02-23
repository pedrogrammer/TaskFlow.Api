using TaskFlow.Api.DTOs.Tasks;

namespace TaskFlow.Api.Services;

public interface ITaskService
{
    Task<TaskResponseDto> CreateAsync(Guid projectId, CreateTaskDto dto);
    Task<IEnumerable<TaskResponseDto>> GetByProjectAsync(
        Guid projectId,
        int page,
        int pageSize,
        string? sortBy);
}