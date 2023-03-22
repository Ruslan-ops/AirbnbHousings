using Airbnb.Application.Common.Mappings;
using Airbnb.Application.Requests.Auth.Commands.RefreshPassword;
using Airbnb.Application.Requests.Users.Commands.UpdateUser;
using AutoMapper;

namespace AirbnbHousings.Models
{
    public class UpdateUserDto : IMapWith<UpdateUserCommand>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string? MiddleName { get; set; }
        public bool RecieveNews { get; set; }
        public DateOnly BornDate { get; set; }
        public int? Sex { get; set; }
        public string? Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserDto, UpdateUserCommand>();
        }
    }
}
