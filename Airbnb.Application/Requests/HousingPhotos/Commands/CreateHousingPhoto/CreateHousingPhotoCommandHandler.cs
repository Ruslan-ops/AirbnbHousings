using Airbnb.Application.Services;
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
    public class CreateHousingPhotoCommandHandler : IRequestHandler<CreateHousingPhotoCommand>
    {
        private readonly MinioService _minioService;
        private readonly DatabaseService _databaseService;
        private readonly CreateHousingPhotoCommandValidator _validator;

        public CreateHousingPhotoCommandHandler(MinioService minioService, DatabaseService databaseService, CreateHousingPhotoCommandValidator validator)
        {
            _minioService = minioService;
            _databaseService = databaseService;
            _validator = validator;
        }

        public async Task Handle(CreateHousingPhotoCommand request, CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync($"********Handle: {JsonSerializer.Serialize(request)}");
            var valresult = await _validator.ValidateAsync( request );
            if (!valresult.IsValid)
            {
                await Console.Out.WriteLineAsync("$$$$ FAILERS");
                foreach (var failure in valresult.Errors)
                {
                    await Console.Out.WriteLineAsync("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }
            var uploadResult = await _minioService.UploadPhotoAsync(request.Photo, MinioPhotoDir.Housing);
            await Console.Out.WriteLineAsync($"Upload result: {JsonSerializer.Serialize(uploadResult)}");
            await _databaseService.AddHousingPhotoAsync(request.HousingId!.Value, uploadResult.ObjectName, uploadResult.Url);
        }
    }
}
