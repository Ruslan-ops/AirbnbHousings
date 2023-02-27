using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class RegionTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RegionId { get; set; }

    public int LanguageId { get; set; }

    public virtual Language Language { get; set; } = null!;

    public virtual Region Region { get; set; } = null!;
}
