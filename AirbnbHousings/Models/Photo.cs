using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Photo
{
    public int PhotoId { get; set; }

    public string Url { get; set; } = null!;

    public string? Name { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<HousingPhoto> HousingPhotos { get; } = new List<HousingPhoto>();

    public virtual ICollection<IdentifyDocumentPhoto> IdentifyDocumentPhotos { get; } = new List<IdentifyDocumentPhoto>();

    public virtual ICollection<UsersPhoto> UsersPhotos { get; } = new List<UsersPhoto>();
}
