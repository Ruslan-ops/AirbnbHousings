using Airbnb.Application.Common.Consts;
using Airbnb.Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.HousingPhotos.Commands.DeleteHousingPhoto
{
    public class DeleteHousingPhotoCommandValidator : AbstractValidator<DeleteHousingPhotoCommand>
    {
        private readonly DatabaseService _databaseService;
        public DeleteHousingPhotoCommandValidator(DatabaseService databaseService) 
        {
            _databaseService = databaseService;
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.HousingId).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.PhotoId).NotEmpty();
            RuleFor(command => command).CustomAsync(CheckUserOwnsHousing);
        }

        private async Task CheckUserOwnsHousing(DeleteHousingPhotoCommand command, ValidationContext<DeleteHousingPhotoCommand> context, CancellationToken cancellationToken)
        {
            if (! await _databaseService.UserOwnsHousing(command.UserId!.Value, command.HousingId!.Value, cancellationToken))
            {
                context.AddFailure(ErrorMessages.UserNotOwnHousing);
                return;
            }
            if (! await _databaseService.HousingHasPhoto(command.HousingId!.Value, command.PhotoId!.Value, cancellationToken))
            {
                context.AddFailure(ErrorMessages.HousingNotHasPhoto(command.PhotoId!.Value));
                return;
            }
        }
    }
}
