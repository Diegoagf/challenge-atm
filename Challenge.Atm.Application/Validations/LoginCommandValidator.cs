using Challenge.Atm.Application.Handlers;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Challenge.Atm.Application.Validations
{
    public class LoginCommandValidator: AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Request)
            .NotNull().WithMessage("The login request cannot be null");

            RuleFor(x => x.Request)
            .SetValidator(new CardValidatorBase());
        }
        
    }
    
}
