using MediatR;
using Portfolio.Services.WorkItems.Application.Commands;
using Portfolio.Services.WorkItems.Application.Dtos;
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
    public class DeleteWorkHandler : IRequestHandler<DeleteWorkCommand, Response<DeleteWorkDto>>
    {
        private readonly WorkItemsDbContext _context;

        public DeleteWorkHandler(WorkItemsDbContext context)
        {
            _context = context;
        }
        public async Task<Response<DeleteWorkDto>> Handle(DeleteWorkCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Work work = _context.Works.FirstOrDefault(f => f.Id == request.WorkId);
                if (work == null)
                    return Response<DeleteWorkDto>.Fail("Silinecek iş bulunamadı", 400);
                _context.Works.Remove(work);
                await _context.SaveChangesAsync();

                return Response<DeleteWorkDto>.Success(200);
            }
            catch (Exception ex)
            {
                return Response<DeleteWorkDto>.Success(200);
            }
        }
    }
}
