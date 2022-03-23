using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Services.WorkItems.Application.Commands;
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
    public class CategoriesController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getcategories")]
        [Produces(typeof(Response<List<CategoryDto>>))]
        public async Task<IActionResult> GetCategories(bool? isActive)
        {
            var response = await _mediator.Send(new GetCategoriesByFilterQuery { IsActive = isActive });

            return CreateActionResultInstance(response);
        }

        [HttpPost("SaveCategory")]
        public async Task<IActionResult> SaveCategory([FromBody]CreateCategoryCommand createCategory)
        {
            var response = await _mediator.Send(createCategory);

            return CreateActionResultInstance(response);
        }
    }
}
