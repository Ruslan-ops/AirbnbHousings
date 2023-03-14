using Airbnb.Application.General.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Auth.Commands.RegisterEmail
{
    public class RegisterEmailCommand : GeneralUser, IRequest<string>
    {
        public string Email { get; set;}
        public string Password { get; set;}
    }
}
