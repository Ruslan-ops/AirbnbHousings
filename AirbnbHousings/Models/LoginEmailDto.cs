using Airbnb.Application.Common.Mappings;
using Airbnb.Application.Requests.Auth.Commands.LoginEmail;
using Airbnb.Application.Requests.Auth.Commands.RegisterEmail;
using AutoMapper;

namespace Web.Models
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
