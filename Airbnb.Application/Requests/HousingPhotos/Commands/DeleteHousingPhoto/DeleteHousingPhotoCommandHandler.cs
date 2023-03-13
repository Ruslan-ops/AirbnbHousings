using Airbnb.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.HousingPhotos.Commands.DeleteHousingPhoto
{
    public class DeleteHousingPhotoCommandHandler : IRequestHandler<DeleteHousingPhotoCommand, Unit>
    {
        private readonly MinioService _minioService;
        private readonly DatabaseService _databaseService;

        public DeleteHousingPhotoCommandHandler(MinioService minioService, DatabaseService databaseService)
        {
            _minioService = minioService;
            _databaseService = databaseService;
        }

        public async Task<Unit> Handle(DeleteHousingPhotoCommand request, CancellationToken cancellationToken)
        {
            var deletedPhoto = await _databaseService.DeleteHousingPhotoAsync(request.PhotoId!.Value);
            await _minioService.DeletePhotoAsync(deletedPhoto.Name);
            return Unit.Value;
        }
    }
}
