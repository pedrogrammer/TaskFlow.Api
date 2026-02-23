using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.DTOs.Projects;
using TaskFlow.Api.Services;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectResponseDto>> Create(CreateProjectDto dto)
    {
        ProjectResponseDto result = await _projectService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectResponseDto>> GetById(Guid id)
    {
        ProjectResponseDto? result = await _projectService.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProjectDto dto)
    {
        bool updated = await _projectService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _projectService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}