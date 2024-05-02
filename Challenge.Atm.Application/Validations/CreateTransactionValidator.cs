using Challenge.Atm.Application.Handlers.Commnads;
using Challenge.Atm.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Validations
{
    public class CreateTransactionValidator: AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionValidator()
        {
            RuleFor(x => x.Request)
                .NotNull().WithMessage(ServiceConstans.RequestNullMessage);

            RuleFor(x => x.Request.Amount)
                    .NotEmpty().WithMessage("The amount is required")
                     .GreaterThan(0).WithMessage("The amount must be greater than zero");

            RuleFor(x => x.Request.Type)
                    .NotEmpty().WithMessage("The type is required")
                                .Must(x => Enum.TryParse<TransactionType>(x, out _)).WithMessage("The value provided for 'type' is not valid. Please provide one of the following values: Extraction, Deposit");


        }
    }
}
