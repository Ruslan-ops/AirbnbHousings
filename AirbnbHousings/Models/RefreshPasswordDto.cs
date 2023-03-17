using Airbnb.Application.Common.Mappings;
using Airbnb.Application.Requests.Auth.Commands.RefreshPassword;
using Airbnb.Application.Requests.Auth.Commands.RegisterEmail;
using AutoMapper;

namespace Web.Models
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
