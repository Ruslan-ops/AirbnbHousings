using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Currency
{
    public int CurrencyId { get; set; }

    public string Name { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; } = new List<Book>();

    public virtual ICollection<Housing> Housings { get; } = new List<Housing>();
}
