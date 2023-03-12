using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class GuestRatingType
{
    public int GuestRatingTypeId { get; set; }

    public string EnglishName { get; set; } = null!;

    public virtual ICollection<GuestRating> GuestRatings { get; } = new List<GuestRating>();
}
