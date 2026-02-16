using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.DTOs.Projects;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
  private readonly TaskFlowDbContext _context;

  public ProjectsController(TaskFlowDbContext context)
  {
    _context = context;
  }

  [HttpPost]
  public async Task<ActionResult<ProjectResponseDto>> Create(CreateProjectDto dto)
  {
    var project = new Project
    {
      Name = dto.Name,
      Description = dto.Description,
      OwnerId = dto.OwnerId
    };

    _context.Projects.Add(project);
    await _context.SaveChangesAsync();

    var response = new ProjectResponseDto
    {
      Id = project.Id,
      Name = project.Name,
      Description = project.Description,
      OwnerId = project.OwnerId,
      CreatedAt = project.CreatedAt
    };

    return CreatedAtAction(nameof(GetById), new { id = project.Id }, response);
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult<ProjectResponseDto>> GetById(Guid id)
  {
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
