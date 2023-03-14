using Airbnb.Application.General.Models;
using Airbnb.Application.General.Validators;
using Airbnb.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Auth.Commands.RegisterEmail
{
    public class RegisterEmailCommandValidator : AbstractValidator<RegisterEmailCommand>
    {
        public RegisterEmailCommandValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(command => command.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.Password).Password();

            RuleFor(command => command as GeneralUser).SetValidator(new GeneralUserValidator());
        }
    }
}
