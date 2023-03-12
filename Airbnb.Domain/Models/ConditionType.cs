using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class ConditionType
{
    public int ConditionTypeId { get; set; }

    public string EnglishName { get; set; } = null!;

    public virtual ICollection<Condition> Conditions { get; } = new List<Condition>();
}
