using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Settings.Models.Dtos;
using Portfolio.Api.Settings.Services.Interfaces;
using Portfolio.Shared.ControllerBases;
using System.Threading.Tasks;

namespace Portfolio.Api.Settings.Controllers
{
  [Route("services/Settings/[controller]")]
  [ApiController]
  public class LocalizationController : CustomBaseController
  {
    private readonly ILocalizationService _localizationService;

    public LocalizationController(ILocalizationService localizationService)
    {
      _localizationService = localizationService;
    }

    [HttpGet("GetAll")]
    [Authorize(Policy = "ReadAndWrite")]
    public async Task<IActionResult> GetAll()
    {
      return CreateActionResultInstance(await _localizationService.GetAll());
    }
    [HttpPut("Add")]
    [Authorize(Policy = "WriteEditWork")]
    public async Task<IActionResult> Add([FromBody]LocalizationAddDto localizationAddDto)
    {
      return CreateActionResultInstance(await _localizationService.Add(localizationAddDto));
    }
    [HttpDelete]
    [Authorize(Policy = "WriteEditWork")]
    public async Task<IActionResult> Delete([FromQuery]string id)
    {
      return CreateActionResultInstance(await _localizationService.Delete(id));
    }
    [HttpPost("GetByCulture")]
    [Authorize(Policy = "ReadAndWrite")]
    public async Task<IActionResult> GetByCulture([FromBody] LocalizationGetByCultureDto localizationGetByCultureDto)
    {
      return CreateActionResultInstance(await _localizationService.GetByCulture(localizationGetByCultureDto));
    }
  }
}
