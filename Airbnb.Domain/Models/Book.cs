using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class Book
{
    public int BookId { get; set; }

    public DateTime CreatedDate { get; set; }

    public decimal Cost { get; set; }

    public decimal AirbnbServiceCost { get; set; }

    public int Discount { get; set; }

    public decimal NightPrice { get; set; }

    public DateOnly ArrivalDate { get; set; }

    public DateOnly DepartureDate { get; set; }

    public decimal? Deposit { get; set; }

    public DateTime? CanceledDate { get; set; }

    public bool GuestHasPaid { get; set; }

    public bool LandlordGotMoney { get; set; }

    public int HousingId { get; set; }

    public int GuestId { get; set; }

    public int CurrencyId { get; set; }

    public int GuestsAmount { get; set; }

    public DateOnly? MaxFreeCancelationDate { get; set; }

    public bool GuestReturnedMoney { get; set; }

    public bool IsApproved { get; set; }

    public virtual Currency Currency { get; set; } = null!;

    public virtual User Guest { get; set; } = null!;

    public virtual Housing Housing { get; set; } = null!;
}
