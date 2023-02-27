using System;
using System.Collections.Generic;

namespace AirbnbHousings.Models;

public partial class CalendarDay
{
    public int Id { get; set; }

    public int CalendarId { get; set; }

    public DateOnly Day { get; set; }

    public virtual Calendar Calendar { get; set; } = null!;
}
