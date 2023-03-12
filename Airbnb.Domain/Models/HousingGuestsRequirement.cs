using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class HousingGuestsRequirement
{
    public int Id { get; set; }

    public int HousingId { get; set; }

    public int GuestsRequirementId { get; set; }

    public virtual GuestsRequirement GuestsRequirement { get; set; } = null!;

    public virtual Housing Housing { get; set; } = null!;
}
