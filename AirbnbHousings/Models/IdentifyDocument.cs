using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class IdentifyDocument
{
    public int IdentifyDocumentId { get; set; }

    public bool IsConfirmed { get; set; }

    public int IdentifyDocumentTypeId { get; set; }

    public int CountryIssueId { get; set; }

    public string EncryptedDocumentNumber { get; set; } = null!;

    public virtual Country CountryIssue { get; set; } = null!;

    public virtual ICollection<IdentifyDocumentPhoto> IdentifyDocumentPhotos { get; } = new List<IdentifyDocumentPhoto>();

    public virtual IdentifyDocumentType IdentifyDocumentType { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
