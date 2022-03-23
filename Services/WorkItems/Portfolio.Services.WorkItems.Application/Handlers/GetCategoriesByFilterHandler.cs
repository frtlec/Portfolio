using MediatR;
using Microsoft.EntityFrameworkCore;
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
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Handlers
{
    public class GetCategoriesByFilterHandler : IRequestHandler<GetCategoriesByFilterQuery, Response<List<CategoryDto>>>
    {
        private readonly WorkItemsDbContext _context;

        public GetCategoriesByFilterHandler(WorkItemsDbContext context)
        {
            _context = context;
        }
        public async Task<Response<List<CategoryDto>>> Handle(GetCategoriesByFilterQuery request, CancellationToken cancellationToken)
        {

            try
            {
                IQueryable<Category> query = _context.Categories.AsQueryable().AsNoTracking();
                if (request.IsActive!=null)
                {
                    query = query.Where(f => f.IsActive == request.IsActive);
                }
              
               
                query = query.OrderBy(f=>f.Sort);


                List<Category> categories = await query.ToListAsync();
                if (!categories.Any())
                {
                    return Response<List<CategoryDto>>.Success(new List<CategoryDto>(), 200);
                }

                var mappedData = ObjectMapper.Mapper.Map<List<CategoryDto>>(categories);
                return Response<List<CategoryDto>>.Success(mappedData, 200);
            }
            catch (Exception ex)
            {

                return Response<List<CategoryDto>>.Fail("error", 500);
            }
        }
    }
}
