using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class UsersRole
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public int Id { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
