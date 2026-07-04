namespace Shared.Domain;

/// <summary>
/// A church record, matching the <c>Churches</c> table schema exactly. The private constructor plus
/// <see cref="Create"/> factory make it impossible to hold a <see cref="Church"/> instance in memory
/// that would violate a NOT NULL/range constraint at write time — callers get an immediate,
/// specific <see cref="ArgumentException"/> instead of a raw SQL constraint violation three layers
/// away from the bad input.
/// </summary>
public sealed class Church
{
    private Church(
        Guid id,
        string canonicalName,
        string slug,
        double latitude,
        double longitude,
        string? street,
        string city,
        string state,
        string zip,
        string? phoneNumber,
        string? website,
        string? emailAddress,
        Guid? denominationId,
        int worshipStyle,
        string primaryLanguage,
        bool? acceptsLgbtq,
        bool? wheelchairAccessible,
        bool? hasNursery,
        bool? hasYouthProgram,
        decimal confidenceScore,
        DateTime? lastVerifiedAt,
        DateTime createdAt,
        DateTime updatedAt,
        bool isActive)
    {
        Id = id;
        CanonicalName = canonicalName;
        Slug = slug;
        Latitude = latitude;
        Longitude = longitude;
        Street = street;
        City = city;
        State = state;
        Zip = zip;
        PhoneNumber = phoneNumber;
        Website = website;
        EmailAddress = emailAddress;
        DenominationId = denominationId;
        WorshipStyle = worshipStyle;
        PrimaryLanguage = primaryLanguage;
        AcceptsLGBTQ = acceptsLgbtq;
        WheelchairAccessible = wheelchairAccessible;
        HasNursery = hasNursery;
        HasYouthProgram = hasYouthProgram;
        ConfidenceScore = confidenceScore;
        LastVerifiedAt = lastVerifiedAt;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        IsActive = isActive;
    }

    public Guid Id { get; }

    public string CanonicalName { get; }

    public string Slug { get; }

    public double Latitude { get; }

    public double Longitude { get; }

    public string? Street { get; }

    public string City { get; }

    public string State { get; }

    public string Zip { get; }

    public string? PhoneNumber { get; }

    public string? Website { get; }

    public string? EmailAddress { get; }

    public Guid? DenominationId { get; }

    public int WorshipStyle { get; }

    public string PrimaryLanguage { get; }

    public bool? AcceptsLGBTQ { get; }

    public bool? WheelchairAccessible { get; }

    public bool? HasNursery { get; }

    public bool? HasYouthProgram { get; }

    public decimal ConfidenceScore { get; }

    public DateTime? LastVerifiedAt { get; }

    public DateTime CreatedAt { get; }

    public DateTime UpdatedAt { get; }

    public bool IsActive { get; }

    public static Church Create(
        Guid id,
        string canonicalName,
        string slug,
        double latitude,
        double longitude,
        string? street,
        string city,
        string state,
        string zip,
        string? phoneNumber,
        string? website,
        string? emailAddress,
        Guid? denominationId,
        int worshipStyle,
        string primaryLanguage,
        bool? acceptsLgbtq,
        bool? wheelchairAccessible,
        bool? hasNursery,
        bool? hasYouthProgram,
        decimal confidenceScore,
        DateTime? lastVerifiedAt,
        DateTime createdAt,
        DateTime updatedAt,
        bool isActive = true)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id is required.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(canonicalName))
        {
            throw new ArgumentException("CanonicalName is required.", nameof(canonicalName));
        }

        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new ArgumentException("Slug is required.", nameof(slug));
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ArgumentException("City is required.", nameof(city));
        }

        if (state is not { Length: 2 } || string.IsNullOrWhiteSpace(state))
        {
            throw new ArgumentException("State must be a 2-letter code.", nameof(state));
        }

        if (string.IsNullOrWhiteSpace(zip))
        {
            throw new ArgumentException("Zip is required.", nameof(zip));
        }

        if (string.IsNullOrWhiteSpace(primaryLanguage))
        {
            throw new ArgumentException("PrimaryLanguage is required.", nameof(primaryLanguage));
        }

        if (latitude is < -90 or > 90)
        {
            throw new ArgumentOutOfRangeException(nameof(latitude), latitude, "Latitude must be between -90 and 90.");
        }

        if (longitude is < -180 or > 180)
        {
            throw new ArgumentOutOfRangeException(nameof(longitude), longitude, "Longitude must be between -180 and 180.");
        }

        if (worshipStyle is < 0 or > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(worshipStyle), worshipStyle, "WorshipStyle must be 0-5 (Unknown..Liturgical).");
        }

        if (confidenceScore is < 0m or > 1m)
        {
            throw new ArgumentOutOfRangeException(nameof(confidenceScore), confidenceScore, "ConfidenceScore must be between 0 and 1.");
        }

        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        return new Church(
            id,
            canonicalName,
            slug,
            latitude,
            longitude,
            street,
            city,
            state,
            zip,
            phoneNumber,
            website,
            emailAddress,
            denominationId,
            worshipStyle,
            primaryLanguage,
            acceptsLgbtq,
            wheelchairAccessible,
            hasNursery,
            hasYouthProgram,
            confidenceScore,
            lastVerifiedAt,
            createdAt,
            updatedAt,
            isActive);
    }
}
