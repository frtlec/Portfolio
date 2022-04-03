using FluentValidation;
using Portfolio.Services.WorkItems.Application.Constants.ResponseMessages;
using Portfolio.Services.WorkItems.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Validations.FluentValidation
{
    public class GetWorkByIdQueryValidator:AbstractValidator<GetWorkByIdQuery>
    {

        public GetWorkByIdQueryValidator()
        {
            RuleFor(f => f.WorkId).NotEmpty().WithMessage($"WorkId {ValidatorResponseMessage.CANNOT_EMPTY_OR_NULL}");
        }
    }
}
