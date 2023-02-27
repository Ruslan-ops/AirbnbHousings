using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class GuestReview
{
    public int ReviewId { get; set; }

    public string Text { get; set; } = null!;

    public int AuthorId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int GuestId { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual User Guest { get; set; } = null!;
}
