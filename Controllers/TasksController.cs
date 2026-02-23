using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.DTOs.Tasks;
using TaskFlow.Api.Services;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponseDto>> Create(
        Guid projectId,
        CreateTaskDto dto)
    {
        TaskResponseDto result = await _service.CreateAsync(projectId, dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResponseDto>>> Get(
        Guid projectId,
        int page = 1,
        int pageSize = 10,
        string? sortBy = null)
    {
        IEnumerable<TaskResponseDto> result = await _service.GetByProjectAsync(projectId, page, pageSize, sortBy);
        return Ok(result);
    }
}