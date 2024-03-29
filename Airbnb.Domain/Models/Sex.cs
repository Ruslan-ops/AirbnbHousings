﻿using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class Sex
{
    public int SexId { get; set; }

    public string EnglishName { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
