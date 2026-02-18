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
    var result = await _projectService.CreateAsync(dto);

    return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult<ProjectResponseDto>> GetById(Guid id)
  {
    var result = await _projectService.GetByIdAsync(id);

    if(result is null)
    {
      return NotFound();
    }

    var project = await _context.Projects
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id == id);

    if (project is null)
      return NotFound();

    return new ProjectResponseDto
    {
      Id = project.Id,
      Name = project.Name,
      Description = project.Description,
      OwnerId = project.OwnerId,
      CreatedAt = project.CreatedAt
    };
  }

  [HttpPut("{id:guid}")]
  public async Task<IActionResult> Update(Guid id, UpdateProjectDto dto)
  {
    var project = await _context.Projects.FindAsync(id);

    if (project is null)
      return NotFound();

    project.Name = dto.Name;
    project.Description = dto.Description;

    await _context.SaveChangesAsync();

    return NoContent();
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    var project = await _context.Projects.FindAsync(id);

    if (project is null)
      return NotFound();

    _context.Projects.Remove(project);
    await _context.SaveChangesAsync();

    return NoContent();
  }
}
