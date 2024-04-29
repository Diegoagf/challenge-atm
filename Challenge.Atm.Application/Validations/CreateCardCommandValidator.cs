using Challenge.Atm.Application.Handlers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Validations
{   
    public class CreateCardCommandValdator: AbstractValidator<CreateCardCommand>
    {
        public CreateCardCommandValdator()
        {
            RuleFor(x => x.Request)
            .NotNull().WithMessage("The request cannot be null");

            RuleFor(x => x.Request)
                .SetValidator(new CardValidatorBase());
        }
    }
}
