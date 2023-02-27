using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class Calendar
{
    public int CalendarId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CalendarDay> CalendarDays { get; } = new List<CalendarDay>();

    public virtual ICollection<Housing> Housings { get; } = new List<Housing>();
}
