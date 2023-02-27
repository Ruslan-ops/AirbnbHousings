using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class HousingAdditionalDescription
{
    public int Id { get; set; }

    public int HousingId { get; set; }

    public int DescriptionTopicId { get; set; }

    public string Text { get; set; } = null!;

    public virtual DescriptionTopic DescriptionTopic { get; set; } = null!;

    public virtual Housing Housing { get; set; } = null!;
}
