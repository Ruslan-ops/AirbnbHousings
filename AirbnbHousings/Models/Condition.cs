using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Condition
{
    public int ConditionId { get; set; }

    public int ConditionTypeId { get; set; }

    public string EnglishName { get; set; } = null!;

    public string? Explanation { get; set; }

    public virtual ConditionType ConditionType { get; set; } = null!;

    public virtual ICollection<HousingCondition> HousingConditions { get; } = new List<HousingCondition>();
}
