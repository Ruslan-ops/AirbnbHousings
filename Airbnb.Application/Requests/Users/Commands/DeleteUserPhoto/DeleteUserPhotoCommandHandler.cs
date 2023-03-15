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
        private readonly MinioService _minioService;
        private readonly DatabaseService _databaseService;

        public DeleteUserPhotoCommandHandler(MinioService minioService, DatabaseService databaseService)
        {
            _minioService = minioService;
            _databaseService = databaseService;
        }

        public async Task<Unit> Handle(DeleteUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var deletedPhoto = await _databaseService.DeleteUserPhotoAsync(request.PhotoId!.Value, cancellationToken);
            await _minioService.DeletePhotoAsync(deletedPhoto.Name);
            return Unit.Value;
        }
    }
}
