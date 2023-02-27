using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class HousingType
{
    public int HousingTypeId { get; set; }

    public string EnglishName { get; set; } = null!;

    public virtual ICollection<HousingSubtype> HousingSubtypes { get; } = new List<HousingSubtype>();

    public virtual ICollection<HousingTypeTranslation> HousingTypeTranslations { get; } = new List<HousingTypeTranslation>();
}
