using Airbnb.Application.Common.Validators;
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
            RuleFor(command => command.FirstName).NotEmpty();
            RuleFor(command => command.SecondName).NotEmpty();
            RuleFor(command => command.MiddleName).NotEmpty().When(c => c.MiddleName != null);
            RuleFor(command => command.Sex).Custom(CheckSex);

            DateOnly minDate = DateOnly.FromDateTime(new DateTime(1900, 1, 1));
            RuleFor(command => command.BornDate).NotEmpty().InclusiveBetween(minDate, DateOnly.FromDateTime(DateTime.Today)).WithMessage("Invalid borndate.");

            RuleFor(command => command.Password).Password();
        }


        private void CheckSex(int? sex, ValidationContext<RegisterEmailCommand> context)
        {
            if (sex == null)
                return;

            if (sex >= 0 &&  sex <= 1)
                return;
            context.AddFailure("sex must be in { null, 0, 1 }");
        }
    }
}
