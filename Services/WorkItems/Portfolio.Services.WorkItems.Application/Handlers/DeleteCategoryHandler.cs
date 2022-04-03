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
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Response<DeleteCategoryDto>>
    {
        private readonly WorkItemsDbContext _context;

        public DeleteCategoryHandler(WorkItemsDbContext context)
        {
            _context = context;
        }

        public async Task<Response<DeleteCategoryDto>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Category category = _context.Categories.FirstOrDefault(f => f.Id == request.CategoryId);
                if (category == null)
                    return Response<DeleteCategoryDto>.Fail("Silinecek kategori bulunamadı",400);
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return Response<DeleteCategoryDto>.Success(200);
            }
            catch (Exception ex)
            {
                return Response<DeleteCategoryDto>.Success(200);
            }
        }
    }
}
