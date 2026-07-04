namespace Shared.Domain;

/// <summary>
/// Builds a <see cref="Church"/> one field at a time. Each <c>With*</c> call validates that field
/// immediately and returns <see langword="this"/> for chaining; <see cref="Build"/> only checks that
/// every required field was set, since each one was already validated at the moment it was supplied.
/// </summary>
public sealed class ChurchBuilder
{
    private Guid? _id;
    private string? _canonicalName;
    private string? _slug;
    private double? _latitude;
    private double? _longitude;
    private string? _street;
    private string? _city;
    private string? _state;
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
    private DateTime? _lastVerifiedAt;
    private DateTime? _createdAt;
    private DateTime? _updatedAt;
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
        if (latitude is < -90 or > 90)
        {
            throw new ArgumentOutOfRangeException(nameof(latitude), latitude, "Latitude must be between -90 and 90.");
        }

        _latitude = latitude;
        return this;
    }

    public ChurchBuilder WithLongitude(double longitude)
    {
        if (longitude is < -180 or > 180)
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

    public ChurchBuilder WithCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ArgumentException("City is required.", nameof(city));
        }

        _city = city;
        return this;
    }

    public ChurchBuilder WithState(string state)
    {
        if (state is not { Length: 2 })
        {
            throw new ArgumentException("State must be a 2-letter code.", nameof(state));
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
        if (worshipStyle is < 0 or > 5)
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
        if (confidenceScore is < 0m or > 1m)
        {
            throw new ArgumentOutOfRangeException(nameof(confidenceScore), confidenceScore, "ConfidenceScore must be between 0 and 1.");
        }

        _confidenceScore = confidenceScore;
        return this;
    }

    public ChurchBuilder WithLastVerifiedAt(DateTime? lastVerifiedAt)
    {
        _lastVerifiedAt = lastVerifiedAt;
        return this;
    }

    public ChurchBuilder WithCreatedAt(DateTime createdAt)
    {
        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        _createdAt = createdAt;
        return this;
    }

    public ChurchBuilder WithUpdatedAt(DateTime updatedAt)
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

    public Church Build()
    {
        EnsureRequiredFieldsSet();
        return new Church
        {
            Id = _id!.Value,
            CanonicalName = _canonicalName!,
            Slug = _slug!,
            Latitude = _latitude!.Value,
            Longitude = _longitude!.Value,
            Street = _street,
            City = _city!,
            State = _state!,
            Zip = _zip!,
            PhoneNumber = _phoneNumber,
            Website = _website,
            EmailAddress = _emailAddress,
            DenominationId = _denominationId,
            WorshipStyle = _worshipStyle!.Value,
            PrimaryLanguage = _primaryLanguage!,
            AcceptsLGBTQ = _acceptsLgbtq,
            WheelchairAccessible = _wheelchairAccessible,
            HasNursery = _hasNursery,
            HasYouthProgram = _hasYouthProgram,
            ConfidenceScore = _confidenceScore!.Value,
            LastVerifiedAt = _lastVerifiedAt,
            CreatedAt = _createdAt!.Value,
            UpdatedAt = _updatedAt!.Value,
            IsActive = _isActive,
        };
    }

    private void EnsureRequiredFieldsSet()
    {
        if (_id is null)
        {
            throw new InvalidOperationException($"{nameof(WithId)} must be called before {nameof(Build)}.");
        }

        if (_canonicalName is null)
        {
            throw new InvalidOperationException($"{nameof(WithCanonicalName)} must be called before {nameof(Build)}.");
        }

        if (_slug is null)
        {
            throw new InvalidOperationException($"{nameof(WithSlug)} must be called before {nameof(Build)}.");
        }

        if (_latitude is null)
        {
            throw new InvalidOperationException($"{nameof(WithLatitude)} must be called before {nameof(Build)}.");
        }

        if (_longitude is null)
        {
            throw new InvalidOperationException($"{nameof(WithLongitude)} must be called before {nameof(Build)}.");
        }

        if (_city is null)
        {
            throw new InvalidOperationException($"{nameof(WithCity)} must be called before {nameof(Build)}.");
        }

        if (_state is null)
        {
            throw new InvalidOperationException($"{nameof(WithState)} must be called before {nameof(Build)}.");
        }

        if (_zip is null)
        {
            throw new InvalidOperationException($"{nameof(WithZip)} must be called before {nameof(Build)}.");
        }

        if (_worshipStyle is null)
        {
            throw new InvalidOperationException($"{nameof(WithWorshipStyle)} must be called before {nameof(Build)}.");
        }

        if (_primaryLanguage is null)
        {
            throw new InvalidOperationException($"{nameof(WithPrimaryLanguage)} must be called before {nameof(Build)}.");
        }

        if (_confidenceScore is null)
        {
            throw new InvalidOperationException($"{nameof(WithConfidenceScore)} must be called before {nameof(Build)}.");
        }

        if (_createdAt is null)
        {
            throw new InvalidOperationException($"{nameof(WithCreatedAt)} must be called before {nameof(Build)}.");
        }

        if (_updatedAt is null)
        {
            throw new InvalidOperationException($"{nameof(WithUpdatedAt)} must be called before {nameof(Build)}.");
        }
    }
}
