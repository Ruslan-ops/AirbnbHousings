using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.LoginEmail
{
    public class LoginEmailCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
