﻿using Airbnb.Application.General.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Auth.Commands.RefreshPassword
{
    public class RefreshPasswordCommandValidator : AbstractValidator<RefreshPasswordCommand>
    {
        public RefreshPasswordCommandValidator() 
        {
            RuleFor(command => command.Token).NotEmpty();
            RuleFor(command => command.NewPassword).Password();
        }
    }
}
