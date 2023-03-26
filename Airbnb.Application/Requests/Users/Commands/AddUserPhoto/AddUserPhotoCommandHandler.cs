using Airbnb.Application.Interfaces;
using Airbnb.Application.Services;
using Airbnb.Domain.Models;
using MediatR;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.AddUserPhoto
{
    public class AddUserPhotoCommandHandler : IRequestHandler<AddUserPhotoCommand, Unit>
    {
        private readonly IS3Storage _s3Storage;
        private readonly DatabaseService _databaseService;

        public AddUserPhotoCommandHandler(IS3Storage s3Storage, DatabaseService databaseService)
        {
            _s3Storage = s3Storage;
            _databaseService = databaseService;
        }

        public async Task<Unit> Handle(AddUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var uploadResult = await _s3Storage.UploadPhotoAsync(request.Photo, S3PhotoDir.User);
            var photo = new Photo { Name = uploadResult.ObjectName, Url = uploadResult.Url };
            await _databaseService.AddUserPhotoAsync(request.UserId!.Value, photo, cancellationToken);
            return Unit.Value;
        }
    }
}
