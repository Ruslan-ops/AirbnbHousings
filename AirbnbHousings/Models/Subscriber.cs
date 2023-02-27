using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Subscriber
{
    public int SId { get; set; }

    public string SName { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();
}
