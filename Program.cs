using Microsoft.OpenApi;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.Services;
using TaskFlow.Api.Repositories;
using TaskFlow.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// ======================
// Services
// ======================

builder.Services.AddControllers();

string connectionString = builder.Configuration["DefaultConnection"];

builder.Services.AddDbContext<TaskFlowDbContext>(options =>
{
  options.UseSqlServer(connectionString);
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "TaskFlow API",
    Version = "v1",
    Description = "Task and Project Management API built with ASP.NET Core (.NET 10)"
  });
});

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// ======================
// Middleware Pipeline
// ======================

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var diagnostics = app.MapGroup("/api/diagnostics");

diagnostics.MapGet("/health", () =>
{
  return Results.Ok(new
  {
    status = "Healthy",
    utcTime = DateTime.UtcNow
  });
})
.WithName("HealthCheck")
.WithTags("Diagnostics")
.WithOpenApi();

diagnostics.MapGet("/stats",
    async (TaskFlowDbContext context) =>
    {
      var projectCount = await context.Projects.CountAsync();
      var taskCount = await context.Tasks.CountAsync();

      return Results.Ok(new
      {
        totalProjects = projectCount,
        totalTasks = taskCount
      });
    })
.WithName("GetStatistics")
.WithTags("Diagnostics")
.WithOpenApi();

diagnostics.MapPost("/echo",
    (object payload) =>
    {
      return Results.Ok(new
      {
        received = payload,
        timestamp = DateTime.UtcNow
      });
    })
.WithTags("Diagnostics")
.WithOpenApi();

app.Run();
