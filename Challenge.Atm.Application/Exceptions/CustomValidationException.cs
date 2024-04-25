using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Atm.Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public List<string> Errors { get;}
        public CustomValidationException(): base("One or more validation errors occurred") 
        {
            Errors = new List<string>();
        }
        public CustomValidationException(IEnumerable<ValidationFailure> failures) :this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}
