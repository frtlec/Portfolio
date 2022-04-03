using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Services.MailSender.Dtos;
using Portfolio.Services.MailSender.Services;
using Portfolio.Shared.ControllerBases;
using System.Threading.Tasks;

namespace Portfolio.Services.MailSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : CustomBaseController
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("getall")]
        [Authorize(Policy = "WriteEditWork")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _contactService.GetAll());
        }
        [HttpPost("create")]
        [Authorize(Policy = "ReadAndWrite")]
        public async Task<IActionResult> Create([FromBody]AddContactDto addContactDto)
        {
            return CreateActionResultInstance(await _contactService.AddContact(addContactDto));
        }


    }
}
