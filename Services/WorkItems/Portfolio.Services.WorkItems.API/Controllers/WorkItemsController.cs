
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Services.WorkItems.Application.Queries;
using Portfolio.Shared.ControllerBases;
using Portfolio.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemsController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public WorkItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //[Authorize(Policy = "ReadWork")]
        [HttpGet("getworkitems/{workId:int}")]
        [Produces(typeof(Response<List<WorkItemDto>>))]
        public async Task<IActionResult> GetWorkItems(int workId)
        {
            var response = await _mediator.Send(new GetWorkItemsByWorkIdQuery { WorkId = workId });

            return CreateActionResultInstance(response);
        }
    }
}
