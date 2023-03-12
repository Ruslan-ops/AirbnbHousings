using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class HousingRatingType
{
    public int HousingRatingTypeId { get; set; }

    public string EnglishName { get; set; } = null!;

    public virtual ICollection<HousingRating> HousingRatings { get; } = new List<HousingRating>();
}
