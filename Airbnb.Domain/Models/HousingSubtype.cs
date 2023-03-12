using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class HousingSubtype
{
    public int HousingSubtypeId { get; set; }

    public string EnglishName { get; set; } = null!;

    public int HousingTypeId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<HousingSubtypeTranslation> HousingSubtypeTranslations { get; } = new List<HousingSubtypeTranslation>();

    public virtual HousingType HousingType { get; set; } = null!;

    public virtual ICollection<Housing> Housings { get; } = new List<Housing>();
}
