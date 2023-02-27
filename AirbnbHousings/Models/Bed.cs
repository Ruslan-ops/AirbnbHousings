using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Bed
{
    public int BedId { get; set; }

    public int BedroomId { get; set; }

    public int OrderNumber { get; set; }

    public int BedTypeId { get; set; }

    public virtual BedType BedType { get; set; } = null!;

    public virtual Bedroom Bedroom { get; set; } = null!;
}
