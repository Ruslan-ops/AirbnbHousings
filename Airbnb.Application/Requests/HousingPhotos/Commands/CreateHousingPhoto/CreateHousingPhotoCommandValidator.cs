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
            Console.WriteLine("&&&&&& CREATED VALIDATOR");
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(command => command.Photo).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.HousingId).NotEmpty().CustomAsync(CheckThatUserOwnsHousing);
        }

        private async Task CheckThatUserOwnsHousing(int? housingId, ValidationContext<CreateHousingPhotoCommand> context, CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync("&&&&&& VALIDATOIN"); 

            var instance = context.InstanceToValidate;
            await Console.Out.WriteLineAsync($"check is started: housingId = {housingId!.Value}");
            var isUserOwnsHousing = await _dbContext.Housings.AsNoTracking().AnyAsync(h => (h.HousingId == housingId!.Value) && (h.LandlordId == instance.UserId!.Value), cancellationToken);
            if (!isUserOwnsHousing)
            {
                context.AddFailure("The user doesn't own the housing");
            }
        }
    }
}
