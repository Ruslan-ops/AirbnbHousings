using Airbnb.Application.Common.Mappings;
using Airbnb.Application.Requests.HousingPhotos.Commands.CreateHousingPhoto;
using Airbnb.Application.Requests.Users.Commands.RegisterEmail;
using AutoMapper;

namespace AirbnbHousings.Models
{
    public class RegisterEmailDto : IMapWith<RegisterEmailCommand>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string? MiddleName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RecieveNews { get; set; }
        public DateTime BornDate { get; set; }
        public string Sex { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterEmailDto, RegisterEmailCommand>()
                .ForMember(command => command.BornDate, opts =>
                    opts.MapFrom(dto => DateOnly.FromDateTime(dto.BornDate)));
        }

        //private void bar(IMemberConfigurationExpression<RegisterEmailDto, RegisterEmailCommand, DateOnly> opts)
        //{
            
        //    opts.MapFrom()
        //}
    }
}
