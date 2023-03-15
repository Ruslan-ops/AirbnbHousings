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
        private readonly MinioService _minioService;
        private readonly DatabaseService _databaseService;

        public AddUserPhotoCommandHandler(MinioService minioService, DatabaseService databaseService)
        {
            _minioService = minioService;
            _databaseService = databaseService;
        }

        public async Task<Unit> Handle(AddUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var uploadResult = await _minioService.UploadPhotoAsync(request.Photo, MinioPhotoDir.User);
            var photo = new Photo { Name = uploadResult.ObjectName, Url = uploadResult.Url };
            await _databaseService.AddUserPhotoAsync(request.UserId!.Value, photo, cancellationToken);
            return Unit.Value;
        }
    }
}
