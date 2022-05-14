using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Porfolio.Services.Setting.API.Models.Dtos;
using Porfolio.Services.Setting.API.Services.Interfaces;
using Portfolio.Shared.ControllerBases;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Porfolio.Services.Setting.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AboutSettingController : CustomBaseController
  {
    private readonly IAboutPageSettingService _aboutPageSettingService;

    public AboutSettingController( IAboutPageSettingService aboutPageSettingService)
    {
      _aboutPageSettingService = aboutPageSettingService;
    }

    [HttpGet]
    [Authorize(Policy = "ReadAndWrite")]
    public async Task<IActionResult> GetAllByActive(bool? isActive)
    {
      return CreateActionResultInstance(await _aboutPageSettingService.GetAllByActive(isActive));
    }
    [HttpPut]
    [Authorize(Policy = "WriteEditWork")]
    public async Task<IActionResult> MultiUpdate(AboutPageDto aboutPageDto)
    {
      return CreateActionResultInstance(await _aboutPageSettingService.MultiAddOrUpdate(aboutPageDto));
    }
  }
}
