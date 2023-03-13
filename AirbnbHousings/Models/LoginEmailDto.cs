using Airbnb.Application.Common.Mappings;
using Airbnb.Application.Requests.Users.Commands.LoginEmail;
using Airbnb.Application.Requests.Users.Commands.RegisterEmail;
using AutoMapper;

namespace AirbnbHousings.Models
{
    public class LoginEmailDto : IMapWith<LoginEmailCommand>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginEmailDto, LoginEmailCommand>();
        }
    }
}
