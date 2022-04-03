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
        [Authorize(Policy = "ReadAndWrite")]
        public async Task<IActionResult> GetCategories(bool? isActive)
        {
            var response = await _mediator.Send(new GetCategoriesByFilterQuery { IsActive = isActive });

            return CreateActionResultInstance(response);
        }
      
        [HttpGet("get/{categoryId}")]
        [Produces(typeof(Response<List<CategoryDto>>))]
        [Authorize(Policy = "ReadAndWrite")]
        public async Task<IActionResult> GetCategory(short categoryId)
        {
            var response = await _mediator.Send(new GetCategoryByIdQuery { CategoryId = categoryId });

            return CreateActionResultInstance(response);
        }
   
        [HttpDelete("delete/{categoryId}")]
        [Authorize(Policy = "WriteEditWork")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var response = await _mediator.Send(new DeleteCategoryCommand { CategoryId=categoryId});

            return CreateActionResultInstance(response);
        }
     
        [HttpPost("addcategory")]
        [Authorize(Policy = "WriteEditWork")]
        public async Task<IActionResult> addCategory([FromBody]CreateCategoryCommand createCategory)
        {
            var response = await _mediator.Send(createCategory);

            return CreateActionResultInstance(response);
        }
       
        [HttpPost("updatecategory")]
        [Authorize(Policy = "WriteEditWork")]
        public async Task<IActionResult> updateCategory([FromBody] UpdateCategoryCommand updateCategory)
        {
            var response = await _mediator.Send(updateCategory);

            return CreateActionResultInstance(response);
        }
    }
}
