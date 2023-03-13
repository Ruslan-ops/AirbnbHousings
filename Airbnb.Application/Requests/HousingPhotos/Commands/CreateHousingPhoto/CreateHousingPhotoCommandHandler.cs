using Airbnb.Application.Services;
using Airbnb.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.HousingPhotos.Commands.CreateHousingPhoto
{
    public class CreateHousingPhotoCommandHandler : IRequestHandler<CreateHousingPhotoCommand, Unit>
    {
        private readonly MinioService _minioService;
        private readonly DatabaseService _databaseService;

        public CreateHousingPhotoCommandHandler(MinioService minioService, DatabaseService databaseService)
        {
            _minioService = minioService;
            _databaseService = databaseService;
        }

        public async Task<Unit> Handle(CreateHousingPhotoCommand request, CancellationToken cancellationToken)
        {
            var uploadResult = await _minioService.UploadPhotoAsync(request.Photo, MinioPhotoDir.Housing);
            var photo = new Photo { Name = uploadResult.ObjectName, Url = uploadResult.Url };
            await _databaseService.AddHousingPhotoAsync(request.HousingId!.Value, photo);
            return Unit.Value;
        }
    }
}
