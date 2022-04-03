using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        [HttpGet("testworks")]
        [Authorize(Policy = "ReadAndWrite")]
        public async Task<IActionResult> TestWorks()
        {

            return CreateActionResultInstance(Response<string>.Success("test",200));
        }


        [HttpPost("getworks")]
        [Authorize(Policy = "ReadAndWrite")]
        [Produces(typeof(Response<List<WorkDto>>))]
        public async Task<IActionResult> GetWorks([FromBody]GetAllWorkByFilterQuery query)
        {
            var response = await _mediator.Send(query);

            return CreateActionResultInstance(response);
        }
        [HttpGet("get/{workId}")]
        [Authorize(Policy = "ReadAndWrite")]
        [Produces(typeof(Response<List<WorkDto>>))]
        public async Task<IActionResult> GetWorkById(int workId)
        {
            var response = await _mediator.Send(new GetWorkByIdQuery { WorkId=workId});

            return CreateActionResultInstance(response);
        }

        //[Authorize(Policy = "WriteEditWork",Roles ="admin")]
        [HttpPost("SaveWork")]
        [Authorize(Policy = "WriteEditWork")]
        public async Task<IActionResult> SaveWork(CreateWorkCommand createWork)
        {
            var response = await _mediator.Send(createWork);

            return CreateActionResultInstance(response);
        }
        [HttpPost("UpdateWork")]
        [Authorize(Policy = "WriteEditWork")]
        public async Task<IActionResult> UpdateWork(UpdateWorkCommand updateWork)
        {
            var response = await _mediator.Send(updateWork);

            return CreateActionResultInstance(response);
        }
        [HttpDelete("delete/{workId}")]
        [Authorize(Policy = "WriteEditWork")]
        public async Task<IActionResult> DeleteWork(int workId)
        {
            var response = await _mediator.Send(new DeleteWorkCommand { WorkId = workId });

            return CreateActionResultInstance(response);
        }
    }
}
