using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories;

public class TaskRepository : ITaskRepository
{
  private readonly TaskFlowDbContext _context;

  public TaskRepository(TaskFlowDbContext context)
  {
    _context = context;
  }

  public async Task<TaskItem> AddAsync(TaskItem task)
  {
    _context.Tasks.Add(task);
    await _context.SaveChangesAsync();
    return task;
  }

  public async Task<IEnumerable<TaskItem>> GetByProjectAsync(
      Guid projectId,
      int page,
      int pageSize,
      string? sortBy)
  {
    var query = _context.Tasks
        .AsNoTracking()
        .Where(t => t.ProjectId == projectId);

    query = sortBy?.ToLower() switch
    {
      "duedate" => query.OrderBy(t => t.DueDate),
      "status" => query.OrderBy(t => t.Status),
      _ => query.OrderBy(t => t.Title)
    };

    return await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
  }
}