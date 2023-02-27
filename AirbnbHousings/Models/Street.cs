using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Street
{
    public int StreetId { get; set; }

    public string Name { get; set; } = null!;

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Housing> Housings { get; } = new List<Housing>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
