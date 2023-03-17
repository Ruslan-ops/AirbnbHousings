using Airbnb.Application.Common.Mappings;
using Airbnb.Application.Requests.HousingPhotos.Commands.CreateHousingPhoto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CreateHousingPhotoDto : IMapWith<CreateHousingPhotoCommand>
    {
        public IFormFile Photo { get; set; }
        public int? HousingId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateHousingPhotoDto, CreateHousingPhotoCommand>()
                .ForMember(dto => dto.Photo,
                    opt => opt.MapFrom(command => command.Photo))
                .ForMember(dto => dto.HousingId,
                    opt => opt.MapFrom(command => command.HousingId));
        }
    }
}
