using FluentValidation;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.General.Validators
{
    public static class ValidatorExtentions
    {
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var minLen = 8;
            var maxLen = 16;

            return ruleBuilder.NotEmpty().WithMessage("Your password cannot be empty.")
                    .MinimumLength(minLen).WithMessage($"Your password length must be at least {minLen}.")
                    .MaximumLength(maxLen).WithMessage($"Your password length must not exceed {maxLen}.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\#\.\&\%\-]+").WithMessage("Your password must contain at least one ( ! ? * # . & % - ).");
        }

    }
}
