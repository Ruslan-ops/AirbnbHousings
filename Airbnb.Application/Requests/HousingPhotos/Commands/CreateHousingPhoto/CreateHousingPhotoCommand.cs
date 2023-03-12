using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.HousingPhotos.Commands.CreateHousingPhoto
{
    public class CreateHousingPhotoCommand : IRequest
    {
        public int? UserId { get; set; }
        public IFormFile Photo { get; set; }
        public int? HousingId { get; set; }
    }
}
