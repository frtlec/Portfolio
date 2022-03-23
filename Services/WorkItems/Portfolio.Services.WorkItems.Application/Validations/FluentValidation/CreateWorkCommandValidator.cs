using FluentValidation;
using Portfolio.Services.WorkItems.Application.Commands;
using Portfolio.Services.WorkItems.Application.Constants.ResponseMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Validations.FluentValidation
{
    public class CreateWorkCommandValidator:AbstractValidator<CreateWorkCommand>
    {
        public CreateWorkCommandValidator()
        {

            RuleFor(f => f.Title)
                .NotEmpty().WithMessage($"Title {ValidatorResponseMessage.CANNOT_EMPTY_OR_NULL}")
                .MaximumLength(150).WithMessage($"Title {ValidatorResponseMessage.MAX_LENGTH_EXCEEDED} 150");
            RuleFor(f => f.Description)
                .NotEmpty().WithMessage($"Description {ValidatorResponseMessage.CANNOT_EMPTY_OR_NULL}")
                 .MaximumLength(200).WithMessage($"Description {ValidatorResponseMessage.MAX_LENGTH_EXCEEDED} 200");
            RuleFor(f => f.MainPicture)
                .NotEmpty().WithMessage($"MainPicture {ValidatorResponseMessage.CANNOT_EMPTY_OR_NULL}")
                .MaximumLength(200).WithMessage($"Description {ValidatorResponseMessage.MAX_LENGTH_EXCEEDED} 200");
            RuleFor(f => f.CategoryId)
                .NotNull().WithMessage($"CategoryId {ValidatorResponseMessage.CANNOT_EMPTY_OR_NULL}");
        }


    }
}
