using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class RoleTranslation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RoleId { get; set; }

    public int LanguageId { get; set; }

    public virtual Language Language { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
