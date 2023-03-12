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
        private readonly AirbnbContext _dbContext;
        public DeleteHousingPhotoCommandValidator(AirbnbContext dbContext) 
        {
            _dbContext = dbContext;
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.HousingId).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.PhotoId).NotEmpty();

            RuleFor(command => command).CustomAsync(CheckThatUserOwnsHousingPhoto);
        }

        private async Task CheckThatUserOwnsHousingPhoto(DeleteHousingPhotoCommand command, ValidationContext<DeleteHousingPhotoCommand> context, CancellationToken cancellationToken)
        {
            var isUserOwnsHousing = await _dbContext.Housings.AsNoTracking().AnyAsync(h => (h.HousingId == command.HousingId!.Value) && (h.LandlordId == command.UserId!.Value), cancellationToken);
            if (isUserOwnsHousing)
            {
                var isHousingHasPhoto = await _dbContext.HousingPhotos.AsNoTracking().AnyAsync(h => (h.HousingId == command.HousingId!.Value) && (h.PhotoId == command.PhotoId!.Value));
                if(isHousingHasPhoto)
                {
                    return;
                }
                else
                {
                    await Console.Out.WriteLineAsync("^^^^^^HousingNotHasPhoto");
                    context.AddFailure(ErrorMessages.HousingNotHasPhoto(command.PhotoId!.Value));
                }
            }
            else
            {
                context.AddFailure(ErrorMessages.UserNotOwnHousing);
            }
        }
    }
}
