using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class LocalArrivalTime
{
    public int LocalArrivalTimeId { get; set; }

    public TimeOnly LocalTime { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Housing> HousingLocalArrivalMaxTimes { get; } = new List<Housing>();

    public virtual ICollection<Housing> HousingLocalArrivalMinTimes { get; } = new List<Housing>();

    public virtual ICollection<Housing> HousingLocalDepartureTimes { get; } = new List<Housing>();
}
