using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Bedroom
{
    public int BedroomId { get; set; }

    public int BedroomTypeId { get; set; }

    public int HousingId { get; set; }

    public int OrderNumber { get; set; }

    public virtual BedroomType BedroomType { get; set; } = null!;

    public virtual ICollection<Bed> Beds { get; } = new List<Bed>();

    public virtual Housing Housing { get; set; } = null!;
}
