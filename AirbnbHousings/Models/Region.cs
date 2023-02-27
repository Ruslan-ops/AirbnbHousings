using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Region
{
    public int RegionId { get; set; }

    public string EnglishName { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual ICollection<City> Cities { get; } = new List<City>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<RegionTranslation> RegionTranslations { get; } = new List<RegionTranslation>();
}
