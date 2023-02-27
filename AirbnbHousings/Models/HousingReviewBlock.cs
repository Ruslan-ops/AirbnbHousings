using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class HousingReviewBlock
{
    public int HousingReviewBlockId { get; set; }

    public int HousingId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Housing Housing { get; set; } = null!;

    public virtual ICollection<HousingReview> HousingReviews { get; } = new List<HousingReview>();
}
