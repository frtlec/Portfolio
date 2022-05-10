using FluentValidation.Results;
using MediatR;
using Portfolio.Services.WorkItems.Application.Commands;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Services.WorkItems.Application.Validations.FluentValidation;
using Portfolio.Services.WorkItems.Domain.WorkAggregate;
using Portfolio.Services.WorkItems.Infrastructure;
using Portfolio.Shared.Dtos;
using Portfolio.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Response<UpdateCategoryDto>>
    {
        private readonly WorkItemsDbContext _context;

        public UpdateCategoryHandler(WorkItemsDbContext context)
        {
            _context = context;
        }
        public async Task<Response<UpdateCategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ValidationResult validationResult = new UpdateCategoryCommandValidator().Validate(request);
                if (validationResult.IsValid == false)
                    return Response<UpdateCategoryDto>.Fail(validationResult.Errors.FluentValidationErrorToListString(), 400);

                Category category = _context.Categories.FirstOrDefault(f => f.Id == request.CategoryId);


                category.UpdateCategory(request.Title,request.Description,request.IsActive,request.Sort,request.IsShowMainPage);

                 _context.Update(category);

                await _context.SaveChangesAsync();

                UpdateCategoryDto updateCategoryDto = new UpdateCategoryDto();
                updateCategoryDto.CategoryId = request.CategoryId;
                return Response<UpdateCategoryDto>.Success(updateCategoryDto, 200);
            }
            catch (Exception ex)
            {
                return Response<UpdateCategoryDto>.Fail("Error", 500);
            }
        }
    }
}
