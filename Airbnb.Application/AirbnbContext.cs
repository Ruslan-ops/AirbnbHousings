using System;
using System.Collections.Generic;
using Airbnb.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Application;

public partial class AirbnbContext : DbContext
{
    public AirbnbContext()
    {
    }

    public AirbnbContext(DbContextOptions<AirbnbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bed> Beds { get; set; }

    public virtual DbSet<BedType> BedTypes { get; set; }

    public virtual DbSet<BedTypeTranslation> BedTypeTranslations { get; set; }

    public virtual DbSet<Bedroom> Bedrooms { get; set; }

    public virtual DbSet<BedroomType> BedroomTypes { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<CalendarDay> CalendarDays { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Condition> Conditions { get; set; }

    public virtual DbSet<ConditionType> ConditionTypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CountryTranslation> CountryTranslations { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<DescriptionTopic> DescriptionTopics { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<GuestRating> GuestRatings { get; set; }

    public virtual DbSet<GuestRatingType> GuestRatingTypes { get; set; }

    public virtual DbSet<GuestReview> GuestReviews { get; set; }

    public virtual DbSet<GuestsRequirement> GuestsRequirements { get; set; }

    public virtual DbSet<Housing> Housings { get; set; }

    public virtual DbSet<HousingAdditionalDescription> HousingAdditionalDescriptions { get; set; }

    public virtual DbSet<HousingCondition> HousingConditions { get; set; }

    public virtual DbSet<HousingFeature> HousingFeatures { get; set; }

    public virtual DbSet<HousingGuestsRequirement> HousingGuestsRequirements { get; set; }

    public virtual DbSet<HousingPart> HousingParts { get; set; }

    public virtual DbSet<HousingPhoto> HousingPhotos { get; set; }

    public virtual DbSet<HousingRating> HousingRatings { get; set; }

    public virtual DbSet<HousingRatingType> HousingRatingTypes { get; set; }

    public virtual DbSet<HousingReview> HousingReviews { get; set; }

    public virtual DbSet<HousingReviewBlock> HousingReviewBlocks { get; set; }

    public virtual DbSet<HousingSubtype> HousingSubtypes { get; set; }

    public virtual DbSet<HousingSubtypeTranslation> HousingSubtypeTranslations { get; set; }

    public virtual DbSet<HousingType> HousingTypes { get; set; }

    public virtual DbSet<HousingTypeTranslation> HousingTypeTranslations { get; set; }

    public virtual DbSet<IdentifyDocument> IdentifyDocuments { get; set; }

    public virtual DbSet<IdentifyDocumentPhoto> IdentifyDocumentPhotos { get; set; }

    public virtual DbSet<IdentifyDocumentType> IdentifyDocumentTypes { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<LocalArrivalTime> LocalArrivalTimes { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<RegionTranslation> RegionTranslations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleTranslation> RoleTranslations { get; set; }

    public virtual DbSet<Sex> Sexes { get; set; }

    public virtual DbSet<Street> Streets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersLanguage> UsersLanguages { get; set; }

    public virtual DbSet<UsersPhoto> UsersPhotos { get; set; }

    public virtual DbSet<UsersRole> UsersRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5477;Database=airbnb;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bed>(entity =>
        {
            entity.HasKey(e => e.BedId).HasName("bed_pkey");

            entity.ToTable("bed");

            entity.HasIndex(e => new { e.BedroomId, e.OrderNumber }, "uq_bed").IsUnique();

            entity.Property(e => e.BedId).HasColumnName("bed_id");
            entity.Property(e => e.BedTypeId).HasColumnName("bed_type_id");
            entity.Property(e => e.BedroomId).HasColumnName("bedroom_id");
            entity.Property(e => e.OrderNumber).HasColumnName("order_number");

            entity.HasOne(d => d.BedType).WithMany(p => p.Beds)
                .HasForeignKey(d => d.BedTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bed_bed_type_id_fkey");

            entity.HasOne(d => d.Bedroom).WithMany(p => p.Beds)
                .HasForeignKey(d => d.BedroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bed_bedroom_id_fkey");
        });

        modelBuilder.Entity<BedType>(entity =>
        {
            entity.HasKey(e => e.BedTypeId).HasName("bed_type_pkey");

            entity.ToTable("bed_type");

            entity.Property(e => e.BedTypeId).HasColumnName("bed_type_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(30)
                .HasColumnName("english_name");
        });

        modelBuilder.Entity<BedTypeTranslation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bed_type_translations_pkey");

            entity.ToTable("bed_type_translations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BedTypeId).HasColumnName("bed_type_id");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");

            entity.HasOne(d => d.BedType).WithMany(p => p.BedTypeTranslations)
                .HasForeignKey(d => d.BedTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bed_type_translations_bed_type_id_fkey");

            entity.HasOne(d => d.Language).WithMany(p => p.BedTypeTranslations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bed_type_translations_language_id_fkey");
        });

        modelBuilder.Entity<Bedroom>(entity =>
        {
            entity.HasKey(e => e.BedroomId).HasName("bedroom_pkey");

            entity.ToTable("bedroom");

            entity.HasIndex(e => new { e.HousingId, e.OrderNumber }, "uq_bedroom").IsUnique();

            entity.Property(e => e.BedroomId).HasColumnName("bedroom_id");
            entity.Property(e => e.BedroomTypeId).HasColumnName("bedroom_type_id");
            entity.Property(e => e.HousingId).HasColumnName("housing_id");
            entity.Property(e => e.OrderNumber).HasColumnName("order_number");

            entity.HasOne(d => d.BedroomType).WithMany(p => p.Bedrooms)
                .HasForeignKey(d => d.BedroomTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bedroom_bedroom_type_id_fkey");

            entity.HasOne(d => d.Housing).WithMany(p => p.Bedrooms)
                .HasForeignKey(d => d.HousingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bedroom_housing_id_fkey");
        });

        modelBuilder.Entity<BedroomType>(entity =>
        {
            entity.HasKey(e => e.BedroomTypeId).HasName("bedroom_type_pkey");

            entity.ToTable("bedroom_type");

            entity.Property(e => e.BedroomTypeId).HasColumnName("bedroom_type_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(30)
                .HasColumnName("english_name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("book_pkey");

            entity.ToTable("book");

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.AirbnbServiceCost).HasColumnName("airbnb_service_cost");
            entity.Property(e => e.ArrivalDate).HasColumnName("arrival_date");
            entity.Property(e => e.CanceledDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("canceled_date");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.DepartureDate).HasColumnName("departure_date");
            entity.Property(e => e.Deposit).HasColumnName("deposit");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.GuestHasPaid).HasColumnName("guest_has_paid");
            entity.Property(e => e.GuestId).HasColumnName("guest_id");
            entity.Property(e => e.GuestReturnedMoney).HasColumnName("guest_returned_money");
            entity.Property(e => e.GuestsAmount).HasColumnName("guests_amount");
            entity.Property(e => e.HousingId).HasColumnName("housing_id");
            entity.Property(e => e.IsApproved).HasColumnName("is_approved");
            entity.Property(e => e.LandlordGotMoney).HasColumnName("landlord_got_money");
            entity.Property(e => e.MaxFreeCancelationDate).HasColumnName("max_free_cancelation_date");
            entity.Property(e => e.NightPrice).HasColumnName("night_price");

            entity.HasOne(d => d.Currency).WithMany(p => p.Books)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("book_currency_id_fkey");

            entity.HasOne(d => d.Guest).WithMany(p => p.Books)
                .HasForeignKey(d => d.GuestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("book_guest_id_fkey");

            entity.HasOne(d => d.Housing).WithMany(p => p.Books)
                .HasForeignKey(d => d.HousingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("book_housing_id_fkey");
        });

        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(e => e.CalendarId).HasName("calendar_pkey");

            entity.ToTable("calendar");

            entity.Property(e => e.CalendarId).HasColumnName("calendar_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CalendarDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("calendar_days_pkey");

            entity.ToTable("calendar_days");

            entity.HasIndex(e => new { e.CalendarId, e.Day }, "uq_calendar_days").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CalendarId).HasColumnName("calendar_id");
            entity.Property(e => e.Day).HasColumnName("day");

            entity.HasOne(d => d.Calendar).WithMany(p => p.CalendarDays)
                .HasForeignKey(d => d.CalendarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calendar_days_calendar_id_fkey");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("city_pkey");

            entity.ToTable("city");

            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.RegionId).HasColumnName("region_id");

            entity.HasOne(d => d.Region).WithMany(p => p.Cities)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("city_region_id_fkey");
        });

        modelBuilder.Entity<Condition>(entity =>
        {
            entity.HasKey(e => e.ConditionId).HasName("condition_item_pkey");

            entity.ToTable("conditions");

            entity.Property(e => e.ConditionId)
                .HasDefaultValueSql("nextval('condition_item_condition_item_id_seq'::regclass)")
                .HasColumnName("condition_id");
            entity.Property(e => e.ConditionTypeId).HasColumnName("condition_type_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .HasColumnName("english_name");
            entity.Property(e => e.Explanation)
                .HasMaxLength(150)
                .HasColumnName("explanation");

            entity.HasOne(d => d.ConditionType).WithMany(p => p.Conditions)
                .HasForeignKey(d => d.ConditionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("condition_item_condition_id_fkey");
        });

        modelBuilder.Entity<ConditionType>(entity =>
        {
            entity.HasKey(e => e.ConditionTypeId).HasName("conditions_pkey");

            entity.ToTable("condition_types");

            entity.Property(e => e.ConditionTypeId)
                .HasDefaultValueSql("nextval('conditions_condition_id_seq'::regclass)")
                .HasColumnName("condition_type_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .HasColumnName("english_name");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("country_pkey");

            entity.ToTable("country");

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(70)
                .HasColumnName("english_name");
        });

        modelBuilder.Entity<CountryTranslation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("country_translations_pkey");

            entity.ToTable("country_translations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");

            entity.HasOne(d => d.Country).WithMany(p => p.CountryTranslations)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("country_translations_country_id_fkey");

            entity.HasOne(d => d.Language).WithMany(p => p.CountryTranslations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("country_translations_language_id_fkey");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyId).HasName("currency_pkey");

            entity.ToTable("currency");

            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.ShortName)
                .HasMaxLength(10)
                .HasColumnName("short_name");
        });

        modelBuilder.Entity<DescriptionTopic>(entity =>
        {
            entity.HasKey(e => e.DescriptionTopicId).HasName("description_topic_pkey");

            entity.ToTable("description_topic");

            entity.Property(e => e.DescriptionTopicId).HasColumnName("description_topic_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(30)
                .HasColumnName("english_name");
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.FeatureId).HasName("feature_pkey");

            entity.ToTable("feature");

            entity.Property(e => e.FeatureId).HasColumnName("feature_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .HasColumnName("english_name");
        });

        modelBuilder.Entity<GuestRating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("guest_rating_pkey");

            entity.ToTable("guest_rating");

            entity.HasIndex(e => new { e.GuestRatingTypeId, e.GuestId, e.AuthorId }, "uq_landlord_id_guest_id_guest_rating_type_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.GuestId).HasColumnName("guest_id");
            entity.Property(e => e.GuestRatingTypeId).HasColumnName("guest_rating_type_id");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Author).WithMany(p => p.GuestRatingAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("guest_rating_landlord_id_fkey");

            entity.HasOne(d => d.Guest).WithMany(p => p.GuestRatingGuests)
                .HasForeignKey(d => d.GuestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("guest_rating_guest_id_fkey");

            entity.HasOne(d => d.GuestRatingType).WithMany(p => p.GuestRatings)
                .HasForeignKey(d => d.GuestRatingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("guest_rating_guest_rating_type_id_fkey");
        });

        modelBuilder.Entity<GuestRatingType>(entity =>
        {
            entity.HasKey(e => e.GuestRatingTypeId).HasName("guest_rating_type_pkey");

            entity.ToTable("guest_rating_type");

            entity.Property(e => e.GuestRatingTypeId).HasColumnName("guest_rating_type_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .HasColumnName("english_name");
        });

        modelBuilder.Entity<GuestReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("guest_review_pkey");

            entity.ToTable("guest_review");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.GuestId).HasColumnName("guest_id");
            entity.Property(e => e.Text)
                .HasMaxLength(1000)
                .HasColumnName("text");

            entity.HasOne(d => d.Author).WithMany(p => p.GuestReviewAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("guest_review_author_id_fkey");

            entity.HasOne(d => d.Guest).WithMany(p => p.GuestReviewGuests)
                .HasForeignKey(d => d.GuestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("guest_review_guest_id_fkey");
        });

        modelBuilder.Entity<GuestsRequirement>(entity =>
        {
            entity.HasKey(e => e.GuestsRequirementId).HasName("guests_requirement_pkey");

            entity.ToTable("guests_requirement");

            entity.Property(e => e.GuestsRequirementId).HasColumnName("guests_requirement_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .HasColumnName("english_name");
        });

        modelBuilder.Entity<Housing>(entity =>
        {
            entity.HasKey(e => e.HousingId).HasName("housing_pkey");

            entity.ToTable("housing");

            entity.Property(e => e.HousingId).HasColumnName("housing_id");
            entity.Property(e => e.AdditionalRules)
                .HasMaxLength(500)
                .HasColumnName("additional_rules");
            entity.Property(e => e.BathsAmount).HasColumnName("baths_amount");
            entity.Property(e => e.CalendarId).HasColumnName("calendar_id");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(100)
                .HasColumnName("contact_email");
            entity.Property(e => e.ContactExtensionPhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("contact_extension_phone_number");
            entity.Property(e => e.ContactPhoneNumber)
                .HasMaxLength(40)
                .HasColumnName("contact_phone_number");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.CurrentNightPrice).HasColumnName("current_night_price");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_date");
            entity.Property(e => e.Deposit).HasColumnName("deposit");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.FlatNumber)
                .HasMaxLength(10)
                .HasColumnName("flat_number");
            entity.Property(e => e.FloorNumber).HasColumnName("floor_number");
            entity.Property(e => e.FullAddress)
                .HasMaxLength(200)
                .HasColumnName("full_address");
            entity.Property(e => e.HasSeparateBath).HasColumnName("has_separate_bath");
            entity.Property(e => e.HousingPartId).HasColumnName("housing_part_id");
            entity.Property(e => e.HousingSubtypeId).HasColumnName("housing_subtype_id");
            entity.Property(e => e.IsCompletelyForGuests)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("is_completely_for_guests");
            entity.Property(e => e.IsHidden)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("is_hidden");
            entity.Property(e => e.LandlordId).HasColumnName("landlord_id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.LocalArrivalMaxTimeId)
                .HasDefaultValueSql("1")
                .HasColumnName("local_arrival_max_time_id");
            entity.Property(e => e.LocalArrivalMinTimeId).HasColumnName("local_arrival_min_time_id");
            entity.Property(e => e.LocalDepartureTimeId).HasColumnName("local_departure_time_id");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.MaxBookingDays).HasColumnName("max_booking_days");
            entity.Property(e => e.MaxGuestsAmount).HasColumnName("max_guests_amount");
            entity.Property(e => e.MinBookingDays)
                .HasDefaultValueSql("1")
                .HasColumnName("min_booking_days");
            entity.Property(e => e.MonthDiscount)
                .HasDefaultValueSql("30")
                .HasColumnName("month_discount");
            entity.Property(e => e.NightBasePrice).HasColumnName("night_base_price");
            entity.Property(e => e.NightMaxPrice).HasColumnName("night_max_price");
            entity.Property(e => e.NightMinPrice).HasColumnName("night_min_price");
            entity.Property(e => e.NormContactPhoneNumber)
                .HasMaxLength(40)
                .HasColumnName("norm_contact_phone_number");
            entity.Property(e => e.PostIndex).HasColumnName("post_index");
            entity.Property(e => e.ReceiveReceptionNotifications).HasColumnName("receive_reception_notifications");
            entity.Property(e => e.Square).HasColumnName("square");
            entity.Property(e => e.StreetId).HasColumnName("street_id");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.UseInstantBooking)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("use_instant_booking");
            entity.Property(e => e.UseSmartPrices)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("use_smart_prices");
            entity.Property(e => e.WeekDiscount)
                .HasDefaultValueSql("15")
                .HasColumnName("week_discount");

            entity.HasOne(d => d.Calendar).WithMany(p => p.Housings)
                .HasForeignKey(d => d.CalendarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_calendar_id_fkey");

            entity.HasOne(d => d.Currency).WithMany(p => p.Housings)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_currency_id_fkey");

            entity.HasOne(d => d.HousingPart).WithMany(p => p.Housings)
                .HasForeignKey(d => d.HousingPartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_housing_part_id_fkey");

            entity.HasOne(d => d.HousingSubtype).WithMany(p => p.Housings)
                .HasForeignKey(d => d.HousingSubtypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_housing_subtype_id_fkey");

            entity.HasOne(d => d.Landlord).WithMany(p => p.Housings)
                .HasForeignKey(d => d.LandlordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_landlord_id_fkey");

            entity.HasOne(d => d.LocalArrivalMaxTime).WithMany(p => p.HousingLocalArrivalMaxTimes)
                .HasForeignKey(d => d.LocalArrivalMaxTimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_local_arrival_max_time_id_fkey");

            entity.HasOne(d => d.LocalArrivalMinTime).WithMany(p => p.HousingLocalArrivalMinTimes)
                .HasForeignKey(d => d.LocalArrivalMinTimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_local_arrival_time_id_fkey");

            entity.HasOne(d => d.LocalDepartureTime).WithMany(p => p.HousingLocalDepartureTimes)
                .HasForeignKey(d => d.LocalDepartureTimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_local_departure_time_id_fkey");

            entity.HasOne(d => d.Street).WithMany(p => p.Housings)
                .HasForeignKey(d => d.StreetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_street_id_fkey");
        });

        modelBuilder.Entity<HousingAdditionalDescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("housing_additional_description_pkey");

            entity.ToTable("housing_additional_description");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DescriptionTopicId).HasColumnName("description_topic_id");
            entity.Property(e => e.HousingId).HasColumnName("housing_id");
            entity.Property(e => e.Text)
                .HasMaxLength(200)
                .HasColumnName("text");

            entity.HasOne(d => d.DescriptionTopic).WithMany(p => p.HousingAdditionalDescriptions)
                .HasForeignKey(d => d.DescriptionTopicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_additional_description_description_topic_id_fkey");

            entity.HasOne(d => d.Housing).WithMany(p => p.HousingAdditionalDescriptions)
                .HasForeignKey(d => d.HousingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_additional_description_housing_id_fkey");
        });

        modelBuilder.Entity<HousingCondition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("housing_condition_item_pkey");

            entity.ToTable("housing_conditions");

            entity.HasIndex(e => new { e.HousingId, e.ConditionId }, "uq_housing_id_condition_item_id").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('housing_condition_item_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.ConditionId).HasColumnName("condition_id");
            entity.Property(e => e.HousingId).HasColumnName("housing_id");

            entity.HasOne(d => d.Condition).WithMany(p => p.HousingConditions)
                .HasForeignKey(d => d.ConditionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_condition_item_condition_item_id_fkey");

            entity.HasOne(d => d.Housing).WithMany(p => p.HousingConditions)
                .HasForeignKey(d => d.HousingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_condition_item_housing_id_fkey");
        });

        modelBuilder.Entity<HousingFeature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("housing_feature_pkey");

            entity.ToTable("housing_feature");

            entity.HasIndex(e => new { e.HousingId, e.FeatureId }, "uq_housing_id_feature_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FeatureId).HasColumnName("feature_id");
            entity.Property(e => e.HousingId).HasColumnName("housing_id");

            entity.HasOne(d => d.Feature).WithMany(p => p.HousingFeatures)
                .HasForeignKey(d => d.FeatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_feature_feature_id_fkey");

            entity.HasOne(d => d.Housing).WithMany(p => p.HousingFeatures)
                .HasForeignKey(d => d.HousingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_feature_housing_id_fkey");
        });

        modelBuilder.Entity<HousingGuestsRequirement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("housing_guests_requirement_pkey");

            entity.ToTable("housing_guests_requirement");

            entity.HasIndex(e => new { e.HousingId, e.GuestsRequirementId }, "uq_housing_id_guests_requirement_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GuestsRequirementId).HasColumnName("guests_requirement_id");
            entity.Property(e => e.HousingId).HasColumnName("housing_id");

            entity.HasOne(d => d.GuestsRequirement).WithMany(p => p.HousingGuestsRequirements)
                .HasForeignKey(d => d.GuestsRequirementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_guests_requirement_guests_requirement_id_fkey");

            entity.HasOne(d => d.Housing).WithMany(p => p.HousingGuestsRequirements)
                .HasForeignKey(d => d.HousingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_guests_requirement_housing_id_fkey");
        });

        modelBuilder.Entity<HousingPart>(entity =>
        {
            entity.HasKey(e => e.HousingPartId).HasName("housing_part_pkey");

            entity.ToTable("housing_part");

            entity.Property(e => e.HousingPartId).HasColumnName("housing_part_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .HasColumnName("english_name");
        });

        modelBuilder.Entity<HousingPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("housing_photo_pkey");

            entity.ToTable("housing_photo");

            entity.HasIndex(e => new { e.HousingId, e.PhotoId }, "uq_housing_id_photo_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HousingId).HasColumnName("housing_id");
            entity.Property(e => e.OrderNumber).HasColumnName("order_number");
            entity.Property(e => e.PhotoId).HasColumnName("photo_id");

            entity.HasOne(d => d.Housing).WithMany(p => p.HousingPhotos)
                .HasForeignKey(d => d.HousingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_photo_housing_id_fkey");

            entity.HasOne(d => d.Photo).WithMany(p => p.HousingPhotos)
                .HasForeignKey(d => d.PhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_photo_photo_id_fkey");
        });

        modelBuilder.Entity<HousingRating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("housing_rating_pkey");

            entity.ToTable("housing_rating");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GuestId).HasColumnName("guest_id");
            entity.Property(e => e.HousingId).HasColumnName("housing_id");
            entity.Property(e => e.HousingRatingTypeId).HasColumnName("housing_rating_type_id");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Guest).WithMany(p => p.HousingRatings)
                .HasForeignKey(d => d.GuestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_rating_guest_id_fkey");

            entity.HasOne(d => d.Housing).WithMany(p => p.HousingRatings)
                .HasForeignKey(d => d.HousingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_rating_housing_id_fkey");

            entity.HasOne(d => d.HousingRatingType).WithMany(p => p.HousingRatings)
                .HasForeignKey(d => d.HousingRatingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_rating_housing_rating_type_id_fkey");
        });

        modelBuilder.Entity<HousingRatingType>(entity =>
        {
            entity.HasKey(e => e.HousingRatingTypeId).HasName("housing_rating_type_pkey");

            entity.ToTable("housing_rating_type");

            entity.Property(e => e.HousingRatingTypeId).HasColumnName("housing_rating_type_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .HasColumnName("english_name");
        });

        modelBuilder.Entity<HousingReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("housing_review_pkey");

            entity.ToTable("housing_review");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.HousingReviewBlockId).HasColumnName("housing_review_block_id");
            entity.Property(e => e.Text)
                .HasMaxLength(1000)
                .HasColumnName("text");

            entity.HasOne(d => d.Author).WithMany(p => p.HousingReviews)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_review_author_id_fkey");

            entity.HasOne(d => d.HousingReviewBlock).WithMany(p => p.HousingReviews)
                .HasForeignKey(d => d.HousingReviewBlockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_review_housing_review_block_id_fkey");
        });

        modelBuilder.Entity<HousingReviewBlock>(entity =>
        {
            entity.HasKey(e => e.HousingReviewBlockId).HasName("housing_review_block_pkey");

            entity.ToTable("housing_review_block");

            entity.Property(e => e.HousingReviewBlockId).HasColumnName("housing_review_block_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.HousingId).HasColumnName("housing_id");

            entity.HasOne(d => d.Housing).WithMany(p => p.HousingReviewBlocks)
                .HasForeignKey(d => d.HousingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_review_block_housing_id_fkey");
        });

        modelBuilder.Entity<HousingSubtype>(entity =>
        {
            entity.HasKey(e => e.HousingSubtypeId).HasName("housing_subtype_pkey");

            entity.ToTable("housing_subtype");

            entity.Property(e => e.HousingSubtypeId).HasColumnName("housing_subtype_id");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .HasColumnName("english_name");
            entity.Property(e => e.HousingTypeId).HasColumnName("housing_type_id");

            entity.HasOne(d => d.HousingType).WithMany(p => p.HousingSubtypes)
                .HasForeignKey(d => d.HousingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_subtype_housing_type_id_fkey");
        });

        modelBuilder.Entity<HousingSubtypeTranslation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("housing_subtype_translations_pkey");

            entity.ToTable("housing_subtype_translations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.HousingSubtypeId).HasColumnName("housing_subtype_id");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.HousingSubtype).WithMany(p => p.HousingSubtypeTranslations)
                .HasForeignKey(d => d.HousingSubtypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_subtype_translations_housing_subtype_id_fkey");

            entity.HasOne(d => d.Language).WithMany(p => p.HousingSubtypeTranslations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_subtype_translations_language_id_fkey");
        });

        modelBuilder.Entity<HousingType>(entity =>
        {
            entity.HasKey(e => e.HousingTypeId).HasName("housing_type_pkey");

            entity.ToTable("housing_type");

            entity.Property(e => e.HousingTypeId).HasColumnName("housing_type_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .HasColumnName("english_name");
        });

        modelBuilder.Entity<HousingTypeTranslation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("housing_type_translations_pkey");

            entity.ToTable("housing_type_translations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HousingTypeId).HasColumnName("housing_type_id");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.HousingType).WithMany(p => p.HousingTypeTranslations)
                .HasForeignKey(d => d.HousingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_type_translations_housing_type_id_fkey");

            entity.HasOne(d => d.Language).WithMany(p => p.HousingTypeTranslations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_type_translations_language_id_fkey");
        });

        modelBuilder.Entity<IdentifyDocument>(entity =>
        {
            entity.HasKey(e => e.IdentifyDocumentId).HasName("identify_document_pkey");

            entity.ToTable("identify_document");

            entity.Property(e => e.IdentifyDocumentId)
                .HasDefaultValueSql("nextval('test_id_seq'::regclass)")
                .HasColumnName("identify_document_id");
            entity.Property(e => e.CountryIssueId).HasColumnName("country_issue_id");
            entity.Property(e => e.EncryptedDocumentNumber)
                .HasMaxLength(50)
                .HasColumnName("encrypted_document_number");
            entity.Property(e => e.IdentifyDocumentTypeId).HasColumnName("identify_document_type_id");
            entity.Property(e => e.IsConfirmed).HasColumnName("is_confirmed");

            entity.HasOne(d => d.CountryIssue).WithMany(p => p.IdentifyDocuments)
                .HasForeignKey(d => d.CountryIssueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("identify_document_country_issue_id_fkey");

            entity.HasOne(d => d.IdentifyDocumentType).WithMany(p => p.IdentifyDocuments)
                .HasForeignKey(d => d.IdentifyDocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("identify_document_identify_document_type_id_fkey");
        });

        modelBuilder.Entity<IdentifyDocumentPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("identify_document_photo_pkey");

            entity.ToTable("identify_document_photo");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('test_id_seq2'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.IdentifyDocumentId).HasColumnName("identify_document_id");
            entity.Property(e => e.Page).HasColumnName("page");
            entity.Property(e => e.PhotoId).HasColumnName("photo_id");

            entity.HasOne(d => d.IdentifyDocument).WithMany(p => p.IdentifyDocumentPhotos)
                .HasForeignKey(d => d.IdentifyDocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("identify_document_photo_identify_document_id_fkey");

            entity.HasOne(d => d.Photo).WithMany(p => p.IdentifyDocumentPhotos)
                .HasForeignKey(d => d.PhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("identify_document_photo_photo_id_fkey");
        });

        modelBuilder.Entity<IdentifyDocumentType>(entity =>
        {
            entity.HasKey(e => e.IdentifyDocumentTypeId).HasName("identify_document_type_pkey");

            entity.ToTable("identify_document_type");

            entity.Property(e => e.IdentifyDocumentTypeId).HasColumnName("identify_document_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.RequiredPhotosAmount).HasColumnName("required_photos_amount");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageId).HasName("language_pkey");

            entity.ToTable("language");

            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.Code)
                .HasMaxLength(5)
                .HasColumnName("code");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(30)
                .HasColumnName("english_name");
            entity.Property(e => e.LocalName)
                .HasMaxLength(30)
                .HasColumnName("local_name");
        });

        modelBuilder.Entity<LocalArrivalTime>(entity =>
        {
            entity.HasKey(e => e.LocalArrivalTimeId).HasName("local_arrival_time_pkey");

            entity.ToTable("local_arrival_time");

            entity.Property(e => e.LocalArrivalTimeId).HasColumnName("local_arrival_time_id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
            entity.Property(e => e.LocalTime).HasColumnName("local_time");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("photo_pkey");

            entity.ToTable("photo");

            entity.Property(e => e.PhotoId).HasColumnName("photo_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Name)
                .HasMaxLength(300)
                .HasColumnName("name");
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("region_pkey");

            entity.ToTable("region");

            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(70)
                .HasColumnName("english_name");

            entity.HasOne(d => d.Country).WithMany(p => p.Regions)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("region_country_id_fkey");
        });

        modelBuilder.Entity<RegionTranslation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("region_translations_pkey");

            entity.ToTable("region_translations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
            entity.Property(e => e.RegionId).HasColumnName("region_id");

            entity.HasOne(d => d.Language).WithMany(p => p.RegionTranslations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("region_translations_language_id_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.RegionTranslations)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("region_translations_region_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("nextval('test_id_seq1'::regclass)")
                .HasColumnName("role_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(100)
                .HasColumnName("english_name");
        });

        modelBuilder.Entity<RoleTranslation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_translations_pkey");

            entity.ToTable("role_translations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Language).WithMany(p => p.RoleTranslations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_translations_language_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleTranslations)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_translations_role_id_fkey");
        });

        modelBuilder.Entity<Sex>(entity =>
        {
            entity.HasKey(e => e.SexId).HasName("sex_pkey");

            entity.ToTable("sex");

            entity.Property(e => e.SexId).HasColumnName("sex_id");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(30)
                .HasColumnName("english_name");
        });

        modelBuilder.Entity<Street>(entity =>
        {
            entity.HasKey(e => e.StreetId).HasName("street_pkey");

            entity.ToTable("street");

            entity.Property(e => e.StreetId).HasColumnName("street_id");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.City).WithMany(p => p.Streets)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("street_city_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => new { e.PhoneNumber, e.PhoneVerificationCode }, "uq_phone_number_phone_verification_code").IsUnique();

            entity.HasIndex(e => e.EmailVerificationToken, "users_email_verification_token_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.BornDate).HasColumnName("born_date");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(3000)
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.EmailVerificationToken)
                .HasMaxLength(100)
                .HasColumnName("email_verification_token");
            entity.Property(e => e.ExtensionPhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("extension_phone_number");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.HashedPassword)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("hashed_password");
            entity.Property(e => e.IdentifyDocumentId).HasColumnName("identify_document_id");
            entity.Property(e => e.IsConfirmed).HasColumnName("is_confirmed");
            entity.Property(e => e.IsEmailConfirmed).HasColumnName("is_email_confirmed");
            entity.Property(e => e.IsPhoneConfirmed).HasColumnName("is_phone_confirmed");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasColumnName("middle_name");
            entity.Property(e => e.NormEmail)
                .HasMaxLength(100)
                .HasColumnName("norm_email");
            entity.Property(e => e.PasswordChanged)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("password_changed");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(40)
                .HasColumnName("phone_number");
            entity.Property(e => e.PhoneVerificationCode)
                .HasMaxLength(10)
                .HasColumnName("phone_verification_code");
            entity.Property(e => e.ReceiveNews).HasColumnName("receive_news");
            entity.Property(e => e.RefreshPasswordToken)
                .HasMaxLength(100)
                .HasColumnName("refresh_password_token");
            entity.Property(e => e.SecondName)
                .HasMaxLength(50)
                .HasColumnName("second_name");
            entity.Property(e => e.SexId).HasColumnName("sex_id");
            entity.Property(e => e.StreetId).HasColumnName("street_id");

            entity.HasOne(d => d.IdentifyDocument).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdentifyDocumentId)
                .HasConstraintName("users_identify_document_id_fkey");

            entity.HasOne(d => d.Sex).WithMany(p => p.Users)
                .HasForeignKey(d => d.SexId)
                .HasConstraintName("users_sex_id_fkey");

            entity.HasOne(d => d.Street).WithMany(p => p.Users)
                .HasForeignKey(d => d.StreetId)
                .HasConstraintName("users_street_id_fkey");
        });

        modelBuilder.Entity<UsersLanguage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_language_pkey");

            entity.ToTable("users_language");

            entity.HasIndex(e => new { e.UserId, e.LanguageId }, "uq_users_language").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Language).WithMany(p => p.UsersLanguages)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_language_language_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UsersLanguages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_language_user_id_fkey");
        });

        modelBuilder.Entity<UsersPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_photo_pkey");

            entity.ToTable("users_photo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderNum).HasColumnName("order_num");
            entity.Property(e => e.PhotoId).HasColumnName("photo_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Photo).WithMany(p => p.UsersPhotos)
                .HasForeignKey(d => d.PhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_photo_photo_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UsersPhotos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_photo_user_id_fkey");
        });

        modelBuilder.Entity<UsersRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_role_pkey");

            entity.ToTable("users_role");

            entity.HasIndex(e => new { e.UserId, e.RoleId }, "uq_users_role").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UsersRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_role_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UsersRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
