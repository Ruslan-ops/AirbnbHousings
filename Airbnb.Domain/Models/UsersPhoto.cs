using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class UsersPhoto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PhotoId { get; set; }

    public int OrderNum { get; set; }

    public virtual Photo Photo { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
