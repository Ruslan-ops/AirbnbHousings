using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class HousingTypeTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int HousingTypeId { get; set; }

    public int LanguageId { get; set; }

    public virtual HousingType HousingType { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;
}
