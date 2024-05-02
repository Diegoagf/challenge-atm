using Challenge.Atm.Application.Handlers.Commnads;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Challenge.Atm.Application.Validations
{
    public class LoginCommandValidator: AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Request)
            .NotNull().WithMessage(ServiceConstans.RequestNullMessage);

            RuleFor(x => x.Request)
            .SetValidator(new CardValidatorBase());
        }
        
    }
    
}
