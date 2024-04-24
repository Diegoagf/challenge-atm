using Challenge.Atm.Application.Handlers;
using FluentValidation;

namespace Challenge.Atm.Application.Validations
{
    public class LoginCommandValidator: AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator() 
        {
            RuleFor(x => x.Request.CardNumber)
                .NotEmpty()
                .WithMessage("the card number cannot be empty");

            RuleFor(x => x.Request.Pin)
                .LessThan(1000)
                  .WithMessage("the PIN IS INVALID");
        }
    }
    
}
