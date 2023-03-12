using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class GuestsRequirement
{
    public int GuestsRequirementId { get; set; }

    public string EnglishName { get; set; } = null!;

    public virtual ICollection<HousingGuestsRequirement> HousingGuestsRequirements { get; } = new List<HousingGuestsRequirement>();
}
