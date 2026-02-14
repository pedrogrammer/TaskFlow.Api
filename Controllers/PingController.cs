using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/ping")]
public class PingController : ControllerBase
{
  [HttpGet]
  public IActionResult Get()
  {
    return Ok(new
    {
      message = "TaskFlow API is running",
      utcTime = DateTime.UtcNow
    });
  }
}
