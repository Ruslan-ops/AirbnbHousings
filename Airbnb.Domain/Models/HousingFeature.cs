using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class HousingFeature
{
    public int Id { get; set; }

    public int HousingId { get; set; }

    public int FeatureId { get; set; }

    public virtual Feature Feature { get; set; } = null!;

    public virtual Housing Housing { get; set; } = null!;
}
