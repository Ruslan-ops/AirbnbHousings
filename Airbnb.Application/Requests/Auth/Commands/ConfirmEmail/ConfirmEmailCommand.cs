using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Auth.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<Unit>
    {
        public string Token { get; set; }

    }
}
