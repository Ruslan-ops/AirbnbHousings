using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class HousingPhoto
{
    public int Id { get; set; }

    public int HousingId { get; set; }

    public int PhotoId { get; set; }

    public int OrderNumber { get; set; }

    public virtual Housing Housing { get; set; } = null!;

    public virtual Photo Photo { get; set; } = null!;
}
