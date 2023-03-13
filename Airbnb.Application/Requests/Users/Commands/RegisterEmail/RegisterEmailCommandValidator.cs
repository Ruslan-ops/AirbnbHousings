using Airbnb.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.RegisterEmail
{
    public class RegisterEmailCommandValidator : AbstractValidator<RegisterEmailCommand>
    {
        public RegisterEmailCommandValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(command => command.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.FirstName).NotEmpty();
            RuleFor(command => command.SecondName).NotEmpty();
            RuleFor(command => command.MiddleName).NotEmpty().When(c => c.MiddleName != null);
            RuleFor(command => command.Sex).Custom(CheckSex);

            DateOnly minDate = DateOnly.FromDateTime(new DateTime(1900, 1, 1));
            RuleFor(command => command.BornDate).NotEmpty().InclusiveBetween(minDate, DateOnly.FromDateTime(DateTime.Today)).WithMessage("Invalid borndate.");

            var minLen = 8;
            var maxLen = 16;

            RuleFor(command => command.Password).NotEmpty().WithMessage("Your password cannot be empty.")
                    .MinimumLength(minLen).WithMessage($"Your password length must be at least {minLen}.")
                    .MaximumLength(maxLen).WithMessage($"Your password length must not exceed {maxLen}.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\#\.\&\%\-]+").WithMessage("Your password must contain at least one ( ! ? * # . & % - ).");
        }


        private void CheckSex(string sex, ValidationContext<RegisterEmailCommand> context)
        {
            if (sex == null)
                return;

            if (sex == "m" || sex == "f")
                return;
            context.AddFailure("sex must be in { null, 'f', 'm' }");
        }
    }
}
