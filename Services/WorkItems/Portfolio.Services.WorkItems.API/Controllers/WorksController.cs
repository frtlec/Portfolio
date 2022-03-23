using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Services.WorkItems.Application.Commands;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Services.WorkItems.Application.Queries;
using Portfolio.Shared.ControllerBases;
using Portfolio.Shared.Dtos;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.API.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class WorksController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public WorksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //[Authorize(Policy = "ReadWork")]
        [HttpPost("getworks")]
        [Produces(typeof(Response<List<WorkDto>>))]
        public async Task<IActionResult> GetWorks([FromBody]GetAllWorkByFilterQuery query)
        {
            var response = await _mediator.Send(query);

            return CreateActionResultInstance(response);
        }
     
        //[Authorize(Policy = "WriteEditWork",Roles ="admin")]
        [HttpPost("SaveWork")]
        public async Task<IActionResult> SaveWork(CreateWorkCommand createWork)
        {
            var response = await _mediator.Send(createWork);

            return CreateActionResultInstance(response);
        }
    }
}
