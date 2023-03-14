using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.AddUserPhoto
{
    public class AddUserPhotoCommand : IRequest<Unit>
    {
        public int? UserId { get; set; }
        public List<IFormFile> Photos { get; set; }
        
        public AddUserPhotoCommand()
        {
            Photos = new List<IFormFile>();
            Photos.First();
        }
    }
}
