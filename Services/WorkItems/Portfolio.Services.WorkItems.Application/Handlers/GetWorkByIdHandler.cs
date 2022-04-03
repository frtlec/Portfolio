using AutoMapper.Internal.Mappers;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Services.WorkItems.Application.Mapping;
using Portfolio.Services.WorkItems.Application.Queries;
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
    public class GetWorkByIdHandler : IRequestHandler<GetWorkByIdQuery, Response<WorkAndWorkItemsDto>>
    {
        private readonly WorkItemsDbContext _context;
        public GetWorkByIdHandler(WorkItemsDbContext context)
        {
            _context = context;
        }

        public async Task<Response<WorkAndWorkItemsDto>> Handle(GetWorkByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                ValidationResult validationResult= new GetWorkByIdQueryValidator().Validate(request);
                if (!validationResult.IsValid)
                    return Response<WorkAndWorkItemsDto>.Fail(validationResult.Errors.FluentValidationErrorToListString(), 400);

                Work work = _context.Works.Include(f=>f.WorkItems).FirstOrDefault(f=>f.Id==request.WorkId);

                WorkAndWorkItemsDto workDto = ObjectMapper.Mapper.Map<WorkAndWorkItemsDto>(work);
                return Response<WorkAndWorkItemsDto>.Success(workDto, 200);

            }
            catch (Exception)
            {

                return Response<WorkAndWorkItemsDto>.Fail("error", 500);
            }
        }
    }
}
