using Airbnb.Application.Common.Mappings;
using Airbnb.Application.Requests.Users.Commands.AddUserPhoto;
using Airbnb.Application.Requests.Users.Commands.UpdateUser;
using AutoMapper;

namespace AirbnbHousings.Models
{
    public class AddUserPhotoDto : IMapWith<AddUserPhotoCommand>
    {
        public IFormFile Photo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddUserPhotoDto, AddUserPhotoCommand>();
        }

    }
}
