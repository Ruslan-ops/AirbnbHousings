using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Language
{
    public int LanguageId { get; set; }

    public string LocalName { get; set; } = null!;

    public string EnglishName { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<BedTypeTranslation> BedTypeTranslations { get; } = new List<BedTypeTranslation>();

    public virtual ICollection<CountryTranslation> CountryTranslations { get; } = new List<CountryTranslation>();

    public virtual ICollection<HousingSubtypeTranslation> HousingSubtypeTranslations { get; } = new List<HousingSubtypeTranslation>();

    public virtual ICollection<HousingTypeTranslation> HousingTypeTranslations { get; } = new List<HousingTypeTranslation>();

    public virtual ICollection<RegionTranslation> RegionTranslations { get; } = new List<RegionTranslation>();

    public virtual ICollection<RoleTranslation> RoleTranslations { get; } = new List<RoleTranslation>();

    public virtual ICollection<UsersLanguage> UsersLanguages { get; } = new List<UsersLanguage>();
}
