using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.RefreshPassword
{
    public class RefreshPasswordCommand : IRequest<Unit>
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
