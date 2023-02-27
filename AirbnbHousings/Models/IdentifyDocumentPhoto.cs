using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class IdentifyDocumentPhoto
{
    public int Id { get; set; }

    public int IdentifyDocumentId { get; set; }

    public int PhotoId { get; set; }

    public short Page { get; set; }

    public virtual IdentifyDocument IdentifyDocument { get; set; } = null!;

    public virtual Photo Photo { get; set; } = null!;
}
