using Airbnb.Application.General.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.AddUserPhoto
{
    public class AddUserPhotoCommandValidator : AbstractValidator<AddUserPhotoCommand>
    {
        public AddUserPhotoCommandValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.Photo).NotEmpty().Custom(CheckFileIsImage);
        }

        private void CheckFileIsImage(IFormFile photo, ValidationContext<AddUserPhotoCommand> context)
        {
            if (photo.IsImage())
                return;
            context.AddFailure("invalid image file");
        }
    }
}
