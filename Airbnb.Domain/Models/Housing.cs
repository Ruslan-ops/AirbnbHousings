using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class Housing
{
    public int HousingId { get; set; }

    public int MaxGuestsAmount { get; set; }

    public int BathsAmount { get; set; }

    public bool HasSeparateBath { get; set; }

    public bool ReceiveReceptionNotifications { get; set; }

    public bool? IsHidden { get; set; }

    public bool? IsCompletelyForGuests { get; set; }

    public int MinBookingDays { get; set; }

    public int MaxBookingDays { get; set; }

    public bool? UseInstantBooking { get; set; }

    public string? ContactPhoneNumber { get; set; }

    public string? ContactExtensionPhoneNumber { get; set; }

    public string? ContactEmail { get; set; }

    public int? Square { get; set; }

    public string? AdditionalRules { get; set; }

    public int PostIndex { get; set; }

    public string? FlatNumber { get; set; }

    public int? FloorNumber { get; set; }

    public string Description { get; set; } = null!;

    public string Title { get; set; } = null!;

    public decimal NightBasePrice { get; set; }

    public decimal NightMinPrice { get; set; }

    public decimal NightMaxPrice { get; set; }

    public decimal CurrentNightPrice { get; set; }

    public bool? UseSmartPrices { get; set; }

    public int? WeekDiscount { get; set; }

    public int? MonthDiscount { get; set; }

    public decimal? Deposit { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int StreetId { get; set; }

    public int HousingSubtypeId { get; set; }

    public int HousingPartId { get; set; }

    public int CalendarId { get; set; }

    public int CurrencyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int LandlordId { get; set; }

    public int LocalArrivalMinTimeId { get; set; }

    public int LocalDepartureTimeId { get; set; }

    public int LocalArrivalMaxTimeId { get; set; }

    public string? NormContactPhoneNumber { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string FullAddress { get; set; } = null!;

    public virtual ICollection<Bedroom> Bedrooms { get; } = new List<Bedroom>();

    public virtual ICollection<Book> Books { get; } = new List<Book>();

    public virtual Calendar Calendar { get; set; } = null!;

    public virtual Currency Currency { get; set; } = null!;

    public virtual ICollection<HousingAdditionalDescription> HousingAdditionalDescriptions { get; } = new List<HousingAdditionalDescription>();

    public virtual ICollection<HousingCondition> HousingConditions { get; } = new List<HousingCondition>();

    public virtual ICollection<HousingFeature> HousingFeatures { get; } = new List<HousingFeature>();

    public virtual ICollection<HousingGuestsRequirement> HousingGuestsRequirements { get; } = new List<HousingGuestsRequirement>();

    public virtual HousingPart HousingPart { get; set; } = null!;

    public virtual ICollection<HousingPhoto> HousingPhotos { get; } = new List<HousingPhoto>();

    public virtual ICollection<HousingRating> HousingRatings { get; } = new List<HousingRating>();

    public virtual ICollection<HousingReviewBlock> HousingReviewBlocks { get; } = new List<HousingReviewBlock>();

    public virtual HousingSubtype HousingSubtype { get; set; } = null!;

    public virtual User Landlord { get; set; } = null!;

    public virtual LocalArrivalTime LocalArrivalMaxTime { get; set; } = null!;

    public virtual LocalArrivalTime LocalArrivalMinTime { get; set; } = null!;

    public virtual LocalArrivalTime LocalDepartureTime { get; set; } = null!;

    public virtual Street Street { get; set; } = null!;
}
