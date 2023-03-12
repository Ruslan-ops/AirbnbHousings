using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string EnglishName { get; set; } = null!;

    public virtual ICollection<RoleTranslation> RoleTranslations { get; } = new List<RoleTranslation>();

    public virtual ICollection<UsersRole> UsersRoles { get; } = new List<UsersRole>();
}
