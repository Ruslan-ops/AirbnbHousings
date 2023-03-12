using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class HousingRating
{
    public int Id { get; set; }

    public int HousingRatingTypeId { get; set; }

    public int GuestId { get; set; }

    public int HousingId { get; set; }

    public int Rating { get; set; }

    public virtual User Guest { get; set; } = null!;

    public virtual Housing Housing { get; set; } = null!;

    public virtual HousingRatingType HousingRatingType { get; set; } = null!;
}
