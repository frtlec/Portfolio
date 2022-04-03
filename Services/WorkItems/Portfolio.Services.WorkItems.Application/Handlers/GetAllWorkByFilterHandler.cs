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
    public class GetAllWorkByFilterHandler : IRequestHandler<GetAllWorkByFilterQuery, Response<List<WorkDto>>>
    {
        private readonly WorkItemsDbContext _context;

        public GetAllWorkByFilterHandler(WorkItemsDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<WorkDto>>> Handle(GetAllWorkByFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<Work> query = _context.Works.AsQueryable().AsNoTracking();
                if (request.IsActive!=null)
                {
                    query = query.Where(f => f.IsActive == request.IsActive);
                }
               
                if (string.IsNullOrEmpty(request.Search)==false)
                {
                    query = query.Where(f => f.Title.ToLower().Contains(request.Search.ToLower()) || f.Description.ToLower().Contains(request.Search.ToLower()));
                }
                if (request.CategoryId!=0)
                {
                    query = query.Where(f=>f.CategoryId==request.CategoryId);
                }
                query = query.Take(request.Limit);


                List<Work> works = await query.OrderByDescending(f=>f.Id).ToListAsync();
                if (!works.Any())
                {
                    return Response<List<WorkDto>>.Success(new List<WorkDto>(), 200);
                }

                var worksDto = ObjectMapper.Mapper.Map<List<WorkDto>>(works);
                return Response<List<WorkDto>>.Success(worksDto, 200);
            }
            catch (Exception ex)
            {

                return Response<List<WorkDto>>.Fail("error", 500);
            }
        }
    }
}
