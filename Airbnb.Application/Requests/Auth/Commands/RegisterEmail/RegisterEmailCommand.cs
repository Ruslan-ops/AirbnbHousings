using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Auth.Commands.RegisterEmail
{
    public class RegisterEmailCommand : IRequest<string>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set;}
        public string? MiddleName { get; set; } 
        public string Email { get; set;}
        public string Password { get; set;}
        public bool RecieveNews { get; set; }
        public DateOnly BornDate { get; set; }
        public string Sex { get; set; }

    }
}
