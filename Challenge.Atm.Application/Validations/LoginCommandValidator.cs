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

            When(x => x.Request != null, () =>
            {
                RuleFor(x => x.Request.CardNumber)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("The card number is required")
                    .Length(16).WithMessage("The card number must be 16 digits")
                    .Must(BeValidCardNumber).WithMessage("The card number is not valid");

                RuleFor(x => x.Request.Pin)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("The PIN is required")
                    .Must(BeValidPin).WithMessage("The PIN is not valid");
            });
        }

        private bool BeValidCardNumber(string number) =>
                 Regex.IsMatch(number, @"^\d+$");
       

        private bool BeValidPin(int pin) =>
            pin.ToString().Length == 4;
        
    }
    
}
