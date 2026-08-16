using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Mail.Dtos;
using Portfolio.Api.Mail.Services;
using Portfolio.Shared.ControllerBases;
using System.Threading.Tasks;

namespace Portfolio.Api.Mail.Controllers
{
    [Route("services/MailSender/[controller]")]
    [ApiController]
    public class MailSettingsController : CustomBaseController
    {
        private readonly IMailSettingService _mailSettingService;

        public MailSettingsController(IMailSettingService mailSettingService)
        {
            _mailSettingService = mailSettingService;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "WriteEditWork")]
        public async Task<IActionResult> GetById(string id)
        {
            return CreateActionResultInstance(await _mailSettingService.GetById(id));
        }
        [HttpPost]
        [Authorize(Policy = "WriteEditWork")]
        public async Task<IActionResult> Create([FromBody] CreateMailSettingDto createMailSettingDto)
        {
            return CreateActionResultInstance(await _mailSettingService.Create(createMailSettingDto));
        }
    }
}
