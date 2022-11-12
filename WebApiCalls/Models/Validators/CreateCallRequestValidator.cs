using FluentValidation;
using WebApiCalls.Models.Requests;

namespace WebApiCalls.Models.Validators
{
    public class CreateCallRequestValidator : AbstractValidator<CreateCallRequest>
    {
        public CreateCallRequestValidator()
        {
            RuleFor(x => x.ToPhone)
                 .NotNull()
                 .NotEmpty()
                 .Length(16, 16)
                 .Must(RulesValidators.RulePhone)
                 .WithMessage("The phone number should look like +Х-ХХХ-ХХХ-ХХ-ХХ ");

            RuleFor(x => x.TimeStart)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.TimeEnd)
               .NotNull()
               .NotEmpty();
        }
    }
}
