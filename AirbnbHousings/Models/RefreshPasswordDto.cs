using Airbnb.Application.Common.Mappings;
using Airbnb.Application.Requests.Users.Commands.RefreshPassword;
using Airbnb.Application.Requests.Users.Commands.RegisterEmail;
using AutoMapper;

namespace AirbnbHousings.Models
{
    public class RefreshPasswordDto : IMapWith<RefreshPasswordCommand>
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RefreshPasswordDto, RefreshPasswordCommand>();
        }
    }
}
