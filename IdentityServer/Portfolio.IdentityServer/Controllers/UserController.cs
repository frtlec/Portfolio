using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.IdentityServer.Dtos;
using Portfolio.IdentityServer.Models;
using Portfolio.Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Portfolio.IdentityServer.Controllers
{

  [Route("api/[controller]/[action]")]
  [EnableCors()]
  [ApiController]

  [Authorize(LocalApi.PolicyName)]
  public class UserController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      _userManager = userManager;
      _roleManager = roleManager;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpDto signUpDto)
    {
      var user = new ApplicationUser
      {
        UserName = signUpDto.UserName,
        Email = signUpDto.Email,
        City = signUpDto.City
      };

      var result = await _userManager.CreateAsync(user, signUpDto.Password);
      await _userManager.AddToRoleAsync(user, signUpDto.RoleName);
      if (!result.Succeeded)
      {
        return BadRequest(Response<Shared.Dtos.NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
      }

      return NoContent();

    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateRoleDto model)
    {
      var result = await _roleManager.CreateAsync(new IdentityRole(model.RoleName));

      if (!result.Succeeded)
      {
        return BadRequest(Response<Shared.Dtos.NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
      }

      return NoContent();
    }
    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
      var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
      if (userIdClaim == null) return BadRequest();

      var user = await _userManager.FindByIdAsync(userIdClaim.Value);
      if (user == null) return BadRequest();

      return Ok(new { Id = user.Id, UserName = user.UserName, Email = user.Email, City = user.City });
    }
  }
}
