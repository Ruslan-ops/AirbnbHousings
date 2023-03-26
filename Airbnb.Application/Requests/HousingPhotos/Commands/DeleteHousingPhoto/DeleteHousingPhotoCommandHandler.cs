using Airbnb.Application.Interfaces;
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
        private readonly IS3Storage _s3Storage;
        private readonly DatabaseService _databaseService;

        public DeleteHousingPhotoCommandHandler(IS3Storage s3Storage, DatabaseService databaseService)
        {
            _s3Storage = s3Storage;
            _databaseService = databaseService;
        }

        public async Task<Unit> Handle(DeleteHousingPhotoCommand request, CancellationToken cancellationToken)
        {
            var deletedPhoto = await _databaseService.DeleteHousingPhotoAsync(request.PhotoId!.Value, request.HousingId!.Value, cancellationToken);
            if (deletedPhoto != null)
            {
                await _s3Storage.DeletePhotoAsync(deletedPhoto.Name);
            }
            return Unit.Value;
        }
    }
}
