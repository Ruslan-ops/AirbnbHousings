using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class BedTypeTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int BedTypeId { get; set; }

    public int LanguageId { get; set; }

    public virtual BedType BedType { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;
}
