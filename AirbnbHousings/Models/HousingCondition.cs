using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class HousingCondition
{
    public int Id { get; set; }

    public int HousingId { get; set; }

    public int ConditionId { get; set; }

    public virtual Condition Condition { get; set; } = null!;

    public virtual Housing Housing { get; set; } = null!;
}
