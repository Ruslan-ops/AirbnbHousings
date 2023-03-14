using Airbnb.Application.General.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : GeneralUser, IRequest<Unit>
    {
        public int? UserId { get; set; }
        public string? Description { get; set; }

    }
}
