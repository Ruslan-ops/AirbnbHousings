using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class IdentifyDocumentType
{
    public int IdentifyDocumentTypeId { get; set; }

    public string Name { get; set; } = null!;

    public short RequiredPhotosAmount { get; set; }

    public virtual ICollection<IdentifyDocument> IdentifyDocuments { get; } = new List<IdentifyDocument>();
}
