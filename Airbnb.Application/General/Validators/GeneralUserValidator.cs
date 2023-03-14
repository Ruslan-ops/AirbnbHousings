using Airbnb.Application.General.Models;
using Airbnb.Application.Requests.Auth.Commands.RegisterEmail;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.General.Validators
{
    public class GeneralUserValidator : AbstractValidator<GeneralUser>
    {
        public GeneralUserValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.FirstName).NotEmpty().MaximumLength(30);
            RuleFor(command => command.SecondName).NotEmpty().MaximumLength(30);
            RuleFor(command => command.MiddleName).MaximumLength(30);
            RuleFor(command => command.Sex).Custom(CheckSex);

            DateOnly minDate = DateOnly.FromDateTime(new DateTime(1900, 1, 1));
            RuleFor(command => command.BornDate).NotEmpty().InclusiveBetween(minDate, DateOnly.FromDateTime(DateTime.Today)).WithMessage("Invalid borndate.");
        }

        private void CheckSex(int? sex, ValidationContext<GeneralUser> context)
        {
            if (sex == null)
                return;

            if (sex >= 0 && sex <= 1)
                return;
            context.AddFailure("sex must be in { null, 0, 1 }");
        }
    }
}
