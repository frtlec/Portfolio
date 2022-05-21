using FluentValidation;
using Portfolio.Services.MailSender.Dtos;

namespace Portfolio.Services.MailSender.Validations.FluentValidation
{
  public class AddContactDtoValidator : AbstractValidator<AddContactDto>
  {
    public AddContactDtoValidator()
    {
      RuleFor(f => f.Content)
          .NotEmpty().WithMessage($"Content {ValidatorResponseMessage.CANNOT_EMPTY_OR_NULL}")
          .MaximumLength(3000).WithMessage($"Content maximum limit 1000");
      RuleFor(f => f.Subject)
          .NotEmpty().WithMessage($"Subject {ValidatorResponseMessage.CANNOT_EMPTY_OR_NULL}")
          .MaximumLength(50).WithMessage($"Content maximum limit 50");

      RuleFor(f => f.FromMail)
                .NotEmpty().WithMessage($"FromMail {ValidatorResponseMessage.CANNOT_EMPTY_OR_NULL}")
                .MaximumLength(100).WithMessage($"Content maximum limit 100"); 
    }
  }
}
