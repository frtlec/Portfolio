using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Services.WorkItems.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
    [HttpGet("Test")]
    public IActionResult Test()
    {
      return Ok("SUCCESS");
    }
    [HttpGet("ClientTokenValidator")]
    [Authorize(Policy = "ReadAndWrite")]
    public IActionResult ClientTokenValidator()
    {
      return Ok("OK");
    }
   
  }
}
