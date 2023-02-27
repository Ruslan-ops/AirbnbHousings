using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class HousingSubtypeTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int HousingSubtypeId { get; set; }

    public int LanguageId { get; set; }

    public string? Description { get; set; }

    public virtual HousingSubtype HousingSubtype { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;
}
