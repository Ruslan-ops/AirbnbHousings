using Airbnb.Application.Common.Mappings;
using Airbnb.Application.Requests.Users.Commands.ChangeEmail;
using AutoMapper;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace AirbnbHousings.Models
{
    public class ChangeEmailDto : IMapWith<ChangeEmailCommand>
    {
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeEmailDto, ChangeEmailCommand>();
        }
    }
}
