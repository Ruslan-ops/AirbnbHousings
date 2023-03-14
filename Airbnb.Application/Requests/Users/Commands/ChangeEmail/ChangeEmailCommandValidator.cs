using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.ChangeEmail
{
    public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
    {
        public ChangeEmailCommandValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(command => command.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}
