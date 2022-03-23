using FluentValidation.Results;
using MediatR;
using Portfolio.Services.WorkItems.Application.Commands;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Services.WorkItems.Application.Validations.FluentValidation;
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
    public class CreateWorkCommandHandler : IRequestHandler<CreateWorkCommand, Response<CreatedWorkDto>>
    {
        private readonly WorkItemsDbContext _context;

        public CreateWorkCommandHandler(WorkItemsDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CreatedWorkDto>> Handle(CreateWorkCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ValidationResult validationResult = new CreateWorkCommandValidator().Validate(request);
                if (validationResult.IsValid==false)
                    return Response<CreatedWorkDto>.Fail(JsonSerializer.Serialize(validationResult.Errors), 400);

                if(_context.Works.Any(f=>f.Title==request.Title))
                    return Response<CreatedWorkDto>.Fail($"{request.Title} is already exits", 400);

                Work newWork = new Work(request.MainPicture, request.Title, request.Description, 1,request.IsActive,request.CategoryId);

                CreatedWorkDto createdWorkDto = new CreatedWorkDto();

                foreach (var workItem in request.WorkItems)
                {
                    (bool, string) isAdded = newWork.AddWorkItem(workItem.Pictures, workItem.TemplateType, workItem.Title, workItem.Description, 1, newWork.Id);
                    if (createdWorkDto.HasIsBeenAdded == null)
                        createdWorkDto.HasIsBeenAdded = new Dictionary<string, string>();
                    createdWorkDto.HasIsBeenAdded.Add(workItem.Title==null?"":workItem.Title, $"{isAdded.Item1}:{isAdded.Item2}");
                }
                await _context.Works.AddAsync(newWork);

                await _context.SaveChangesAsync();
              
                createdWorkDto.WorkId = newWork.Id;
                return Response<CreatedWorkDto>.Success(createdWorkDto, 200);
            }
            catch (Exception ex)
            {
                return Response<CreatedWorkDto>.Fail("Error", 500);
            }
        }
    }
}
