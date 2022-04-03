using FluentValidation;
using Portfolio.Services.WorkItems.Application.Commands;
using Portfolio.Services.WorkItems.Application.Constants.ResponseMessages;
using Portfolio.Services.WorkItems.Domain.WorkAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Validations.FluentValidation
{
    public class SaveGeneralSettingCommandValidator:AbstractValidator<GeneralSetting>
    {
        public SaveGeneralSettingCommandValidator()
        {
            RuleFor(f => f.IsActive).NotNull().WithMessage($"IsActive {ValidatorResponseMessage.CANNOT_EMPTY_OR_NULL}");
            RuleFor(f => f.SettingType).NotNull().WithMessage($"Value {ValidatorResponseMessage.CANNOT_EMPTY_OR_NULL}");
        }
    }
}
