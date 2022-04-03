using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Services.WorkItems.Application.Commands;
using Portfolio.Services.WorkItems.Application.Queries;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralSettingsController : Shared.ControllerBases.CustomBaseController
    {
        private readonly IMediator _mediator;

        public GeneralSettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getall")]
        [Authorize(Policy = "ReadAndWrite")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _mediator.Send(new GetAllSettingsQuery()));
        }
        [HttpPost("save")]
        [Authorize(Policy = "WriteEditWork")]
        public async Task<IActionResult> Save([FromBody]SaveGeneralSettingCommand Command)
        {
            return CreateActionResultInstance(await _mediator.Send(Command));
        }
    }
}
