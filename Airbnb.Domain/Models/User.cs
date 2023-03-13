using System;
using System.Collections.Generic;

namespace Airbnb.Domain.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public int? StreetId { get; set; }

    public string SecondName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string HashedPassword { get; set; } = null!;

    public DateOnly BornDate { get; set; }

    public string? Email { get; set; }

    public bool IsEmailConfirmed { get; set; }

    public string? PhoneNumber { get; set; }

    public string? ExtensionPhoneNumber { get; set; }

    public bool IsPhoneConfirmed { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool ReceiveNews { get; set; }

    public string? Description { get; set; }

    public int? IdentifyDocumentId { get; set; }

    public bool IsConfirmed { get; set; }

    public int? SexId { get; set; }

    public string? EmailVerificationToken { get; set; }

    public string? PhoneVerificationCode { get; set; }

    public string? RefreshPasswordToken { get; set; }

    public DateTime? PasswordChanged { get; set; }

    public string? NormEmail { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();

    public virtual ICollection<GuestRating> GuestRatingAuthors { get; } = new List<GuestRating>();

    public virtual ICollection<GuestRating> GuestRatingGuests { get; } = new List<GuestRating>();

    public virtual ICollection<GuestReview> GuestReviewAuthors { get; } = new List<GuestReview>();

    public virtual ICollection<GuestReview> GuestReviewGuests { get; } = new List<GuestReview>();

    public virtual ICollection<HousingRating> HousingRatings { get; } = new List<HousingRating>();

    public virtual ICollection<HousingReview> HousingReviews { get; } = new List<HousingReview>();

    public virtual ICollection<Housing> Housings { get; } = new List<Housing>();

    public virtual IdentifyDocument? IdentifyDocument { get; set; }

    public virtual Sex? Sex { get; set; }

    public virtual Street? Street { get; set; }

    public virtual ICollection<UsersLanguage> UsersLanguages { get; } = new List<UsersLanguage>();

    public virtual ICollection<UsersPhoto> UsersPhotos { get; } = new List<UsersPhoto>();

    public virtual ICollection<UsersRole> UsersRoles { get; } = new List<UsersRole>();
}
