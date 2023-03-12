using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class UsersLanguage
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int LanguageId { get; set; }

    public virtual Language Language { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
