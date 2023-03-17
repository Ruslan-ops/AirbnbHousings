using Airbnb.Application.Common.Mappings;
using Airbnb.Application.Requests.Auth.Commands.ForgotPassword;
using Airbnb.Application.Requests.Auth.Commands.LoginEmail;
using AutoMapper;

namespace Web.Models
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
