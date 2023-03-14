using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Common.Exceptions
{
    public class CustomValidationException : ValidationException
    {
        public CustomValidationException(string message)
            : base(new ValidationFailure[] { new ValidationFailure { ErrorMessage = message } })
        { }

    }
}
