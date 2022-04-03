using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class UpdateWorkHandler : IRequestHandler<UpdateWorkCommand, Response<UpdateWorkDto>>
    {
        private readonly WorkItemsDbContext _context;

        public UpdateWorkHandler(WorkItemsDbContext context)
        {
            _context = context;
        }
        public async Task<Response<UpdateWorkDto>> Handle(UpdateWorkCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ValidationResult validationResult = new UpdateWorkCommandValidator().Validate(request);
                if (validationResult.IsValid == false)
                    return Response<UpdateWorkDto>.Fail(validationResult.Errors.FluentValidationErrorToListString(), 400);

                Work work = _context.Works.Include(x=>x.WorkItems).FirstOrDefault(f => f.Id == request.WorkId);
                if (work==null)
                    return Response<UpdateWorkDto>.Fail($"not found", 400);

                work.UpdateWork(request.MainPicture,request.Title,request.Description,request.IsActive,request.CategoryId);

                UpdateWorkDto updateWorkDto = new UpdateWorkDto();


            
                foreach (var item in request.WorkItems)
                {


                    work.WorkItems.ForEach(f =>
                    {
                        if (f.Id==item.Id)
                        {
                            f.UpdateWorkItem(item.Pictures, item.TemplateType, item.Title, item.Description);
                            if (updateWorkDto.HasIsBeenUpdate == null)
                                updateWorkDto.HasIsBeenUpdate = new Dictionary<int, string>();
                            updateWorkDto.HasIsBeenUpdate.Add(f.Id, "updated successful");
                            return;
                        }
                    });

                }
                _context.Works.Update(work);

                await _context.SaveChangesAsync();

                return Response<UpdateWorkDto>.Success(updateWorkDto, 200);
            }
            catch (Exception ex)
            {
                return Response<UpdateWorkDto>.Fail("Error", 500);
            }
        }
    }
}
