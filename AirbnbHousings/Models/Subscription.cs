using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Subscription
{
    public int SbId { get; set; }

    public int SbSubscriber { get; set; }

    public int SbBook { get; set; }

    public DateOnly? SbStart { get; set; }

    public DateOnly SbFinish { get; set; }

    public char SbIsActive { get; set; }

    public virtual Book1 SbBookNavigation { get; set; } = null!;

    public virtual Subscriber SbSubscriberNavigation { get; set; } = null!;
}
