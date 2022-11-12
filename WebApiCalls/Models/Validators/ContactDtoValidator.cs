using FluentValidation;

namespace WebApiCalls.Models.Validators
{
    public class ContactDtoValidator : AbstractValidator<ContactDto>
    {
        public ContactDtoValidator()
        {
            RuleFor(x => x.ContactId)
                .NotNull()
                .NotEmpty();


            RuleFor(x => x.Phone)
                 .NotNull()
                 .NotEmpty()
                 .Length(16, 16)
                 .Must(RulesValidators.RulePhone)
                 .WithMessage("The phone number should look like +Х-ХХХ-ХХХ-ХХ-ХХ ");

            RuleFor(x => x.FIO)
                .Length(1, 127);

            RuleFor(x => x.Company)
                .Length(0, 127);

            RuleFor(x => x.Description)
                .Length(0, 383);
        }

        
    }
}
