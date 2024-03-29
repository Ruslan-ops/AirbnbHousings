﻿using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class BedroomType
{
    public int BedroomTypeId { get; set; }

    public string EnglishName { get; set; } = null!;

    public virtual ICollection<Bedroom> Bedrooms { get; } = new List<Bedroom>();
}
