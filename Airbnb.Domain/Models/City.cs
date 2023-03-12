using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class City
{
    public int CityId { get; set; }

    public string Name { get; set; } = null!;

    public int RegionId { get; set; }

    public virtual Region Region { get; set; } = null!;

    public virtual ICollection<Street> Streets { get; } = new List<Street>();
}
