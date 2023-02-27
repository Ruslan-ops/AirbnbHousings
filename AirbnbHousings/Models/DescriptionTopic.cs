﻿using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class DescriptionTopic
{
    public int DescriptionTopicId { get; set; }

    public string EnglishName { get; set; } = null!;

    public virtual ICollection<HousingAdditionalDescription> HousingAdditionalDescriptions { get; } = new List<HousingAdditionalDescription>();
}
