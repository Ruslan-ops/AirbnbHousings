using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Book1
{
    public int BId { get; set; }

    public string BName { get; set; } = null!;

    public short BYear { get; set; }

    public short BQuantity { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();

    public virtual ICollection<Author> AIds { get; } = new List<Author>();

    public virtual ICollection<Genre> GIds { get; } = new List<Genre>();
}
