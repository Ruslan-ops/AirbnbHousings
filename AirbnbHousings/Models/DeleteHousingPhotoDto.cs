using Airbnb.Application.Common.Mappings;
using Airbnb.Application.Requests.HousingPhotos.Commands.CreateHousingPhoto;
using Airbnb.Application.Requests.HousingPhotos.Commands.DeleteHousingPhoto;
using AutoMapper;

namespace AirbnbHousings.Models
{
    public class DeleteHousingPhotoDto : IMapWith<DeleteHousingPhotoCommand>
    {
        public int? HousingId { get; set; }
        public int? PhotoId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteHousingPhotoDto, DeleteHousingPhotoCommand>()
                .ForMember(dto => dto.PhotoId,
                    opt => opt.MapFrom(command => command.PhotoId))
                .ForMember(dto => dto.HousingId,
                    opt => opt.MapFrom(command => command.HousingId));
        }
    }
}
