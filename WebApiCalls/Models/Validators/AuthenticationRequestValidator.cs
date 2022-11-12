using WebApiCalls.Models.Requests;
using FluentValidation;

namespace WebApiCalls.Models.Validators
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator()
        {
            RuleFor(x => x.Phone)
                 .NotNull()
                 .NotEmpty()
                 .Length(16, 16)
                 .Must(RulesValidators.RulePhone)
                 .WithMessage("The phone number should look like +Х-ХХХ-ХХХ-ХХ-ХХ ");
        }
    }
}
