using Airbnb.Application.Common.Consts;
using Airbnb.Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.HousingPhotos.Commands.CreateHousingPhoto
{
    public class CreateHousingPhotoCommandValidator : AbstractValidator<CreateHousingPhotoCommand>
    {
        private readonly DatabaseService _databaseService;
        public CreateHousingPhotoCommandValidator(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(command => command.Photo).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.HousingId).NotEmpty();
            RuleFor(command => command).CustomAsync(CheckUserOwnsHousing);
        }

        private async Task CheckUserOwnsHousing(CreateHousingPhotoCommand command, ValidationContext<CreateHousingPhotoCommand> context, CancellationToken cancellationToken)
        {
            if (await _databaseService.UserOwnsHousing(command.UserId!.Value, command.HousingId!.Value, cancellationToken))
                return;
            context.AddFailure(ErrorMessages.UserNotOwnHousing);
        }
    }
}
