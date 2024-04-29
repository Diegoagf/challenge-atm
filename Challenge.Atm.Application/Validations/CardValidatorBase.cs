using Challenge.Atm.Application.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Validations
{
    public class  CardValidatorBase: AbstractValidator<CardBaseRequest>
    {
        public CardValidatorBase()
        {

                RuleFor(x => x.CardNumber)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("The card number is required")
                    .Length(16).WithMessage("The card number must be 16 digits")
                    .Must(BeValidCardNumber).WithMessage("The card number is not valid");

                RuleFor(x => x.Pin)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("The PIN is required")
                    .Must(BeValidPin).WithMessage("The PIN is not valid");
           
        }

        private bool BeValidCardNumber(string number) =>
                 Regex.IsMatch(number, @"^\d+$");


        private bool BeValidPin(int pin) =>
            pin.ToString().Length == 4;
 }
}
