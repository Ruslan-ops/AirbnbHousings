using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class GuestRating
{
    public int Id { get; set; }

    public int GuestRatingTypeId { get; set; }

    public int GuestId { get; set; }

    public int AuthorId { get; set; }

    public int Rating { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual User Guest { get; set; } = null!;

    public virtual GuestRatingType GuestRatingType { get; set; } = null!;
}
