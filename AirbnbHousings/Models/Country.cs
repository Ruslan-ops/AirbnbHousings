using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string EnglishName { get; set; } = null!;

    public virtual ICollection<CountryTranslation> CountryTranslations { get; } = new List<CountryTranslation>();

    public virtual ICollection<IdentifyDocument> IdentifyDocuments { get; } = new List<IdentifyDocument>();

    public virtual ICollection<Region> Regions { get; } = new List<Region>();
}
