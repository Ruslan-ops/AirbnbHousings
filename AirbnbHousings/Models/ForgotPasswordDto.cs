using Airbnb.Application.Common.Mappings;
using Airbnb.Application.Requests.Users.Commands.ForgotPassword;
using Airbnb.Application.Requests.Users.Commands.LoginEmail;
using AutoMapper;

namespace AirbnbHousings.Models
{
    public class ForgotPasswordDto : IMapWith<ForgotPasswordCommand>
    {
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ForgotPasswordDto, ForgotPasswordCommand>();
        }
    }
}
