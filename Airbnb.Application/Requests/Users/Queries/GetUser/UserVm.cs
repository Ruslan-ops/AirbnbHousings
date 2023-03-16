using Airbnb.Application.Common.Mappings;
using Airbnb.Application.General.ViewModels;
using Airbnb.Domain.Models;
using AutoMapper;

namespace Airbnb.Application.Requests.Users.Queries.GetUser
{
    public class UserVm : IMapWith<User>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string? MiddleName { get; set; }
        public string? Description { get; set; }
        public IEnumerable<PhotoVm> Photos { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public int Sex { get; set; }
        public bool IsConfirmed { get; set; }

        public void Mapping(Profile profile)
        {
            

            profile.CreateMap<User, UserVm>()
                .ForMember(vm => vm.IsConfirmed, ops =>
                    ops.MapFrom(u => u.IsEmailConfirmed))
                .ForMember(vm => vm.RegistrationDate, ops =>
                    ops.MapFrom(u => DateOnly.FromDateTime(u.CreatedDate)))
                .ForMember(vm => vm.Photos, ops =>
                    ops.MapFrom(u => u.UsersPhotos
                        .OrderBy(up => up.OrderNum)));
        }
    }
}