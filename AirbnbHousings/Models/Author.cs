using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Author
{
    public int AId { get; set; }

    public string AName { get; set; } = null!;

    public virtual ICollection<Book1> BIds { get; } = new List<Book1>();
}
