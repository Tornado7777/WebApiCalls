using FluentValidation;

namespace WebApiCalls.Models.Validators
{
    public class CallDtoValidator : AbstractValidator<CallDto>
    {
        public CallDtoValidator()
        {
            RuleFor(x => x.FromPhone)
                 .NotNull()
                 .NotEmpty()
                 .Length(16, 16)
                 .Must(RulesValidators.RulePhone)
                 .WithMessage("The phone number should look like +Х-ХХХ-ХХХ-ХХ-ХХ ");

            RuleFor(x => x.ToPhone)
                .NotNull()
                .NotEmpty()
                .Length(16, 16)
                .Must(RulesValidators.RulePhone)
                .WithMessage("The phone number should look like +Х-ХХХ-ХХХ-ХХ-ХХ ");

            RuleFor(x => x.TimeStart)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.TimeEnd)
                .NotEmpty()
                .NotNull();
        }
    }
}
