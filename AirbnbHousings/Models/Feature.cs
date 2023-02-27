using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Feature
{
    public int FeatureId { get; set; }

    public string EnglishName { get; set; } = null!;

    public virtual ICollection<HousingFeature> HousingFeatures { get; } = new List<HousingFeature>();
}
