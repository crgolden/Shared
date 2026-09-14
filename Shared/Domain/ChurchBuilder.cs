namespace Shared.Domain;

public sealed class ChurchBuilder
{
    public const double MinLatitude = -90;

    public const double MaxLatitude = 90;

    public const double MinLongitude = -180;

    public const double MaxLongitude = 180;

    public const int MinWorshipStyle = 0;

    public const int MaxWorshipStyle = 5;

    public const decimal MinConfidenceScore = 0m;

    public const decimal MaxConfidenceScore = 1m;

    private Guid? _id;
    private string? _canonicalName;
    private string? _slug;
    private double? _latitude;
    private double? _longitude;
    private string? _street;
    private string? _city;
    private StateCode? _state;
    private string? _zip;
    private string? _phoneNumber;
    private string? _website;
    private string? _emailAddress;
    private Guid? _denominationId;
    private int? _worshipStyle;
    private string? _primaryLanguage;
    private bool? _acceptsLgbtq;
    private bool? _wheelchairAccessible;
    private bool? _hasNursery;
    private bool? _hasYouthProgram;
    private decimal? _confidenceScore;
    private DateTimeOffset? _lastVerifiedAt;
    private DateTimeOffset? _createdAt;
    private DateTimeOffset? _updatedAt;
    private bool _isActive = true;

    public ChurchBuilder WithId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id is required.", nameof(id));
        }

        _id = id;
        return this;
    }

    public ChurchBuilder WithCanonicalName(string canonicalName)
    {
        if (string.IsNullOrWhiteSpace(canonicalName))
        {
            throw new ArgumentException("CanonicalName is required.", nameof(canonicalName));
        }

        _canonicalName = canonicalName;
        return this;
    }

    public ChurchBuilder WithSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new ArgumentException("Slug is required.", nameof(slug));
        }

        _slug = slug;
        return this;
    }

    public ChurchBuilder WithLatitude(double latitude)
    {
        if (latitude is < MinLatitude or > MaxLatitude)
        {
            throw new ArgumentOutOfRangeException(nameof(latitude), latitude, "Latitude must be between -90 and 90.");
        }

        _latitude = latitude;
        return this;
    }

    public ChurchBuilder WithLongitude(double longitude)
    {
        if (longitude is < MinLongitude or > MaxLongitude)
        {
            throw new ArgumentOutOfRangeException(nameof(longitude), longitude, "Longitude must be between -180 and 180.");
        }

        _longitude = longitude;
        return this;
    }

    public ChurchBuilder WithStreet(string? street)
    {
        _street = street;
        return this;
    }

    public ChurchBuilder WithCity(string? city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ArgumentException("City is required.", nameof(city));
        }

        _city = city;
        return this;
    }

    public ChurchBuilder WithState(StateCode state)
    {
        if (!Enum.IsDefined(state))
        {
            throw new ArgumentOutOfRangeException(nameof(state), state, "State must be a USPS state code.");
        }

        _state = state;
        return this;
    }

    public ChurchBuilder WithZip(string zip)
    {
        if (string.IsNullOrWhiteSpace(zip))
        {
            throw new ArgumentException("Zip is required.", nameof(zip));
        }

        _zip = zip;
        return this;
    }

    public ChurchBuilder WithPhoneNumber(string? phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public ChurchBuilder WithWebsite(string? website)
    {
        _website = website;
        return this;
    }

    public ChurchBuilder WithEmailAddress(string? emailAddress)
    {
        _emailAddress = emailAddress;
        return this;
    }

    public ChurchBuilder WithDenominationId(Guid? denominationId)
    {
        _denominationId = denominationId;
        return this;
    }

    public ChurchBuilder WithWorshipStyle(int worshipStyle)
    {
        if (worshipStyle is < MinWorshipStyle or > MaxWorshipStyle)
        {
            throw new ArgumentOutOfRangeException(nameof(worshipStyle), worshipStyle, "WorshipStyle must be 0-5 (Unknown..Liturgical).");
        }

        _worshipStyle = worshipStyle;
        return this;
    }

    public ChurchBuilder WithPrimaryLanguage(string primaryLanguage)
    {
        if (string.IsNullOrWhiteSpace(primaryLanguage))
        {
            throw new ArgumentException("PrimaryLanguage is required.", nameof(primaryLanguage));
        }

        _primaryLanguage = primaryLanguage;
        return this;
    }

    public ChurchBuilder WithAcceptsLGBTQ(bool? acceptsLgbtq)
    {
        _acceptsLgbtq = acceptsLgbtq;
        return this;
    }

    public ChurchBuilder WithWheelchairAccessible(bool? wheelchairAccessible)
    {
        _wheelchairAccessible = wheelchairAccessible;
        return this;
    }

    public ChurchBuilder WithHasNursery(bool? hasNursery)
    {
        _hasNursery = hasNursery;
        return this;
    }

    public ChurchBuilder WithHasYouthProgram(bool? hasYouthProgram)
    {
        _hasYouthProgram = hasYouthProgram;
        return this;
    }

    public ChurchBuilder WithConfidenceScore(decimal confidenceScore)
    {
        if (confidenceScore is < MinConfidenceScore or > MaxConfidenceScore)
        {
            throw new ArgumentOutOfRangeException(nameof(confidenceScore), confidenceScore, "ConfidenceScore must be between 0 and 1.");
        }

        _confidenceScore = confidenceScore;
        return this;
    }

    public ChurchBuilder WithLastVerifiedAt(DateTimeOffset? lastVerifiedAt)
    {
        _lastVerifiedAt = lastVerifiedAt;
        return this;
    }

    public ChurchBuilder WithCreatedAt(DateTimeOffset createdAt)
    {
        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        _createdAt = createdAt;
        return this;
    }

    public ChurchBuilder WithUpdatedAt(DateTimeOffset updatedAt)
    {
        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        _updatedAt = updatedAt;
        return this;
    }

    public ChurchBuilder WithIsActive(bool isActive)
    {
        _isActive = isActive;
        return this;
    }

    public Church Build() =>
        new Church
        {
            Id = _id ?? throw NotCalled(nameof(WithId)),
            CanonicalName = _canonicalName ?? throw NotCalled(nameof(WithCanonicalName)),
            Slug = _slug ?? throw NotCalled(nameof(WithSlug)),
            Latitude = _latitude ?? throw NotCalled(nameof(WithLatitude)),
            Longitude = _longitude ?? throw NotCalled(nameof(WithLongitude)),
            Street = _street,
            City = _city ?? throw NotCalled(nameof(WithCity)),
            State = _state ?? throw NotCalled(nameof(WithState)),
            Zip = _zip ?? throw NotCalled(nameof(WithZip)),
            PhoneNumber = _phoneNumber,
            Website = _website,
            EmailAddress = _emailAddress,
            DenominationId = _denominationId,
            WorshipStyle = _worshipStyle ?? throw NotCalled(nameof(WithWorshipStyle)),
            PrimaryLanguage = _primaryLanguage ?? throw NotCalled(nameof(WithPrimaryLanguage)),
            AcceptsLGBTQ = _acceptsLgbtq,
            WheelchairAccessible = _wheelchairAccessible,
            HasNursery = _hasNursery,
            HasYouthProgram = _hasYouthProgram,
            ConfidenceScore = _confidenceScore ?? throw NotCalled(nameof(WithConfidenceScore)),
            LastVerifiedAt = _lastVerifiedAt,
            CreatedAt = _createdAt ?? throw NotCalled(nameof(WithCreatedAt)),
            UpdatedAt = _updatedAt ?? throw NotCalled(nameof(WithUpdatedAt)),
            IsActive = _isActive,
        };

    private static InvalidOperationException NotCalled(string setterName) =>
        new InvalidOperationException($"{setterName} must be called before {nameof(Build)}.");
}
