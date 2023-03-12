using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class BedType
{
    public int BedTypeId { get; set; }

    public string EnglishName { get; set; } = null!;

    public virtual ICollection<BedTypeTranslation> BedTypeTranslations { get; } = new List<BedTypeTranslation>();

    public virtual ICollection<Bed> Beds { get; } = new List<Bed>();
}
