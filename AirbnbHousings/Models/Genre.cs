using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Genre
{
    public int GId { get; set; }

    public string GName { get; set; } = null!;

    public virtual ICollection<Book1> BIds { get; } = new List<Book1>();
}
