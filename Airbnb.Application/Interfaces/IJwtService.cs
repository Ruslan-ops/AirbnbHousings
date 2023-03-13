using Airbnb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Interfaces
{
    public interface IJwtService
    {
        string Generate(User user, IEnumerable<Role> userRoles);

        string GenerateRandomToken();
    }
}
