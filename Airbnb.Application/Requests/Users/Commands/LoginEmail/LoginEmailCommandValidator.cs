using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.LoginEmail
{
    public class LoginEmailCommandValidator : AbstractValidator<LoginEmailCommand>
    {
        public LoginEmailCommandValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(command => command.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.Password).NotEmpty();
        }
    }
}
