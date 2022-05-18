using FluentValidation;
using Porfolio.Services.Setting.API.Models.Dtos;

namespace Porfolio.Services.Setting.API.ModelValidator
{
  public class LocalizationAddDtoValidator:AbstractValidator<LocalizationAddDto>
  {
    public LocalizationAddDtoValidator()
    {
      RuleFor(f => f.Value).NotEmpty().WithMessage("Türkçesi boş bıraklamaz");
      RuleFor(f => f.Key).NotEmpty().WithMessage("Karşılığı boş bıraklamaz");
      RuleFor(f => f.LocalizationType).Must(value=>value>0).WithMessage("Hangi dil boş bıraklamaz");
    }
  }
}
