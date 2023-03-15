using Airbnb.Application.Common.Consts;
using Airbnb.Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.DeleteUserPhoto
{
    public class DeleteUserPhotoCommandValidator : AbstractValidator<DeleteUserPhotoCommand>
    {
        private readonly DatabaseService _databaseService;

        public DeleteUserPhotoCommandValidator(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.PhotoId).NotEmpty();
            RuleFor(command => command).CustomAsync(CheckUserHasPhoto);
        }

        private async Task CheckUserHasPhoto(DeleteUserPhotoCommand command, ValidationContext<DeleteUserPhotoCommand> context, CancellationToken cancellationToken)
        {
            if (await _databaseService.UserHasPhoto(command.UserId!.Value, command.PhotoId!.Value, cancellationToken))
                return;
            context.AddFailure(ErrorMessages.UserNotHasPhoto(command.PhotoId!.Value));
        }
    }
}
