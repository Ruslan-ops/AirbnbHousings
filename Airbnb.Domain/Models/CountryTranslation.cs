using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class CountryTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    public int LanguageId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;
}
