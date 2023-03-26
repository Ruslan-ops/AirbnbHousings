﻿using Airbnb.Application.Interfaces;
using Airbnb.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.DeleteUserPhoto
{
    public class DeleteUserPhotoCommandHandler : IRequestHandler<DeleteUserPhotoCommand, Unit>
    {
        private readonly IS3Storage _s3Storage;
        private readonly DatabaseService _databaseService;

        public DeleteUserPhotoCommandHandler(IS3Storage s3Storage, DatabaseService databaseService)
        {
            _s3Storage = s3Storage;
            _databaseService = databaseService;
        }

        public async Task<Unit> Handle(DeleteUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var deletedPhoto = await _databaseService.DeleteUserPhotoAsync(request.PhotoId!.Value, cancellationToken);
            if (deletedPhoto != null)
            {
                await _s3Storage.DeletePhotoAsync(deletedPhoto.Name);
            }
            return Unit.Value;
        }
    }
}
