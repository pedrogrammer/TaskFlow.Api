using Microsoft.OpenApi;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;

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

var app = builder.Build();

// ======================
// Middleware Pipeline
// ======================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
