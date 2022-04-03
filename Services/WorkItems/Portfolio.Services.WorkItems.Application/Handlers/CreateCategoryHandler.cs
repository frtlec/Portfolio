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
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Response<CreateCategoryDto>>
    {
        private readonly WorkItemsDbContext _context;

        public CreateCategoryHandler(WorkItemsDbContext context)
        {
            _context = context;
        }
        public async Task<Response<CreateCategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ValidationResult validationResult = new CreateCategoryCommandValidator().Validate(request);
                if (validationResult.IsValid == false)
                    return Response<CreateCategoryDto>.Fail(validationResult.Errors.FluentValidationErrorToListString(), 400);

                Category category = new Category(request.Title, request.Description, request.IsActive, 1, request.Sort);

                await _context.AddAsync(category);

                await _context.SaveChangesAsync();

                CreateCategoryDto createCategoryDto = new CreateCategoryDto();
                return Response<CreateCategoryDto>.Success(createCategoryDto, 200);
            }
            catch (Exception ex)
            {
                return Response<CreateCategoryDto>.Fail("Error", 500);
            }
        }
    }
}
