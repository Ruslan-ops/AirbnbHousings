using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<string>
    {
        public string Email { get; set; }
    }
}
