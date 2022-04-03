using MediatR;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Services.WorkItems.Application.Mapping;
using Portfolio.Services.WorkItems.Application.Queries;
using Portfolio.Services.WorkItems.Domain.WorkAggregate;
using Portfolio.Services.WorkItems.Infrastructure;
using Portfolio.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Handlers
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Response<CategoryDto>>
    {
        private readonly WorkItemsDbContext _context;
        public GetCategoryByIdHandler(WorkItemsDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                Category category = _context.Categories.FirstOrDefault(f => f.Id == request.CategoryId);

                CategoryDto categoryDto = ObjectMapper.Mapper.Map<CategoryDto>(category);
                return Response<CategoryDto>.Success(categoryDto, 200);

            }
            catch (Exception)
            {

                return Response<CategoryDto>.Fail("error", 500);
            }
        }
    }
}
