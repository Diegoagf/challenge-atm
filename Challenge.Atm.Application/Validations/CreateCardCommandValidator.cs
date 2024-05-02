using Challenge.Atm.Application.Handlers.Commnads;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Validations
{
    public class CreateCardCommandValidator: AbstractValidator<CreateCardCommand>
    {
        public CreateCardCommandValidator()
        {
            RuleFor(x => x.Request)
            .NotNull().WithMessage(ServiceConstans.RequestNullMessage);

            RuleFor(x => x.Request)
                .SetValidator(new CardValidatorBase());
        }
    }
}
