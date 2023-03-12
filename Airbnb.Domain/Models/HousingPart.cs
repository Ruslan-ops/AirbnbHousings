using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class HousingPart
{
    public int HousingPartId { get; set; }

    public string EnglishName { get; set; } = null!;

    public virtual ICollection<Housing> Housings { get; } = new List<Housing>();
}
