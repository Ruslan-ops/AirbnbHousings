﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.ChangeEmail
{
    public class ChangeEmailCommand : IRequest<Unit>
    {
        public int? UserId { get; set; }
        public string Email { get; set; }
    }
}
