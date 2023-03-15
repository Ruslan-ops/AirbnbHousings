using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.DeleteUserPhoto
{
    public class DeleteUserPhotoCommand : IRequest<Unit>
    {
        public int? UserId { get; set; }
        public int? PhotoId { get; set; }
    }
}
