using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.HousingPhotos.Commands.DeleteHousingPhoto
{
    public class DeleteHousingPhotoCommand : IRequest<Unit>
    {
        public int? UserId { get; set; }
        public int? HousingId { get; set; }
        public int? PhotoId { get; set; }
    }
}
