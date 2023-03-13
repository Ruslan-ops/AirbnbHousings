using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string? MiddleName { get; set; }
        public DateOnly BornDate { get; set; }
        public int? Sex { get; set; }
        public bool RecieveNews { get; set; }
        public string? Description { get; set; }
    }
}
