using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Api.WorkItems.Controllers
{
  [Route("services/workitems/[controller]")]
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
