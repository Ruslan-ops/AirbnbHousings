using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class HousingReview
{
    public int ReviewId { get; set; }

    public string Text { get; set; } = null!;

    public int HousingReviewBlockId { get; set; }

    public int AuthorId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual HousingReviewBlock HousingReviewBlock { get; set; } = null!;
}
