using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Services.WorkItems.Application.Mapping;
using Portfolio.Services.WorkItems.Application.Queries;
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
    public class GetWorkItemsByWorkIdHandler : IRequestHandler<GetWorkItemsByWorkIdQuery, Response<List<WorkItemDto>>>
    {
        private readonly WorkItemsDbContext _context;

        public GetWorkItemsByWorkIdHandler(WorkItemsDbContext context)
        {
            _context = context;
        }
        public async Task<Response<List<WorkItemDto>>> Handle(GetWorkItemsByWorkIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var workItems = await _context.WorkItems.Where(f => f.WorkId == request.WorkId).Where(f=>f.Pictures.Any()==true).Take(1).ToListAsync();
                if (!workItems.Any())
                {
                    return Response<List<WorkItemDto>>.Success(new List<WorkItemDto>(), 200);
                }

                var worksItemsDto = ObjectMapper.Mapper.Map<List<WorkItemDto>>(workItems);
                return Response<List<WorkItemDto>>.Success(worksItemsDto, 200);

            }
            catch (Exception ex)
            {

                return Response<List<WorkItemDto>>.Fail("error", 500);
            }

        }
    }
}
