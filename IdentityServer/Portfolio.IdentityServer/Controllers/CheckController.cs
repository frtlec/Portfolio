using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using static IdentityServer4.IdentityServerConstants;

namespace Portfolio.IdentityServer.Controllers
{
  [Route("api/[controller]")]
  [EnableCors()]
  [ApiController]
  public class CheckController : ControllerBase
  {
    private readonly IConfiguration _config;

    public CheckController(IConfiguration config)
    {
      _config = config;
    }

    [HttpGet]
    public IActionResult Check(string token,string clientId)
    {
      return Ok();
    }

  }
}
