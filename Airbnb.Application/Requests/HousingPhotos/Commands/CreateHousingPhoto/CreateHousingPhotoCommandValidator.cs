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
        private readonly AirbnbContext _dbContext;
        public CreateHousingPhotoCommandValidator(AirbnbContext dbContext)
        {
            _dbContext = dbContext;
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(command => command.Photo).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.HousingId).NotEmpty();
            RuleFor(command => command).CustomAsync(CheckThatUserOwnsHousing);
        }

        private async Task CheckThatUserOwnsHousing(CreateHousingPhotoCommand command, ValidationContext<CreateHousingPhotoCommand> context, CancellationToken cancellationToken)
        {
            var instance = context.InstanceToValidate;
            var isUserOwnsHousing = await _dbContext.Housings.AsNoTracking().AnyAsync(h => (h.HousingId == command.HousingId!.Value) && (h.LandlordId == command.UserId!.Value), cancellationToken);
            if (!isUserOwnsHousing)
            {
                context.AddFailure(ErrorMessages.UserNotOwnHousing);
            }
        }
    }
}
