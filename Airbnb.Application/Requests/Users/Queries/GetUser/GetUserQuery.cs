using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<UserVm>
    {
        public int? Userid { get; set; }
    }
}
