using Airbnb.Application.General.Models;
using Airbnb.Application.General.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() 
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.Description).MaximumLength(3000);
            RuleFor(command => command as GeneralUser).SetValidator(new GeneralUserValidator());
        }
    }
}
