using TaskFlow.Api.DTOs.Tasks;
using TaskFlow.Api.Models;
using TaskFlow.Api.Repositories;

namespace TaskFlow.Api.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskResponseDto> CreateAsync(Guid projectId, CreateTaskDto dto)
    {
        TaskItem task = new()
        {
            Title = dto.Title,
            DueDate = dto.DueDate,
            Status = dto.Status,
            ProjectId = projectId
        };

        await _repository.AddAsync(task);

        return Map(task);
    }

    public async Task<IEnumerable<TaskResponseDto>> GetByProjectAsync(
        Guid projectId,
        int page,
        int pageSize,
        string? sortBy)
    {
        IEnumerable<TaskItem> tasks = await _repository.GetByProjectAsync(projectId, page, pageSize, sortBy);
        return tasks.Select(Map);
    }

    private static TaskResponseDto Map(TaskItem task)
    {
        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Status = task.Status,
            DueDate = task.DueDate
        };
    }
}