using Airbnb.Application.Common.Mappings;
using Airbnb.Application.Requests.Users.Commands.DeleteUserPhoto;
using AutoMapper;

namespace Web.Models
{
    public class DeleteUserPhotoDto : IMapWith<DeleteUserPhotoCommand>
    {
        public int? PhotoId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteUserPhotoDto, DeleteUserPhotoCommand>();
        }
    }
}
