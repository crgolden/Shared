namespace Shared.Domain;

public sealed class CampusBuilder
{
    private Guid? _id;
    private Guid? _churchId;
    private string? _name;
    private string? _street;
    private string? _city;
    private string? _state;
    private string? _zip;
    private double? _latitude;
    private double? _longitude;
    private DateTimeOffset? _createdAt;
    private DateTimeOffset? _updatedAt;

    public CampusBuilder WithId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id is required.", nameof(id));
        }

        _id = id;
        return this;
    }

    public CampusBuilder WithChurchId(Guid churchId)
    {
        if (churchId == Guid.Empty)
        {
            throw new ArgumentException("ChurchId is required.", nameof(churchId));
        }

        _churchId = churchId;
        return this;
    }

    public CampusBuilder WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        _name = name;
        return this;
    }

    public CampusBuilder WithStreet(string? street)
    {
        _street = street;
        return this;
    }

    public CampusBuilder WithCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ArgumentException("City is required.", nameof(city));
        }

        _city = city;
        return this;
    }

    public CampusBuilder WithState(string state)
    {
        if (state is not { Length: 2 })
        {
            throw new ArgumentException("State must be a 2-letter code.", nameof(state));
        }

        _state = state;
        return this;
    }

    public CampusBuilder WithZip(string zip)
    {
        if (string.IsNullOrWhiteSpace(zip))
        {
            throw new ArgumentException("Zip is required.", nameof(zip));
        }

        _zip = zip;
        return this;
    }

    public CampusBuilder WithLatitude(double latitude)
    {
        if (latitude is < -90 or > 90)
        {
            throw new ArgumentOutOfRangeException(nameof(latitude), latitude, "Latitude must be between -90 and 90.");
        }

        _latitude = latitude;
        return this;
    }

    public CampusBuilder WithLongitude(double longitude)
    {
        if (longitude is < -180 or > 180)
        {
            throw new ArgumentOutOfRangeException(nameof(longitude), longitude, "Longitude must be between -180 and 180.");
        }

        _longitude = longitude;
        return this;
    }

    public CampusBuilder WithCreatedAt(DateTimeOffset createdAt)
    {
        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        _createdAt = createdAt;
        return this;
    }

    public CampusBuilder WithUpdatedAt(DateTimeOffset updatedAt)
    {
        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        _updatedAt = updatedAt;
        return this;
    }

    public Campus Build() =>
        new Campus
        {
            Id = _id ?? throw NotCalled(nameof(WithId)),
            ChurchId = _churchId ?? throw NotCalled(nameof(WithChurchId)),
            Name = _name ?? throw NotCalled(nameof(WithName)),
            Street = _street,
            City = _city ?? throw NotCalled(nameof(WithCity)),
            State = _state ?? throw NotCalled(nameof(WithState)),
            Zip = _zip ?? throw NotCalled(nameof(WithZip)),
            Latitude = _latitude ?? throw NotCalled(nameof(WithLatitude)),
            Longitude = _longitude ?? throw NotCalled(nameof(WithLongitude)),
            CreatedAt = _createdAt ?? throw NotCalled(nameof(WithCreatedAt)),
            UpdatedAt = _updatedAt ?? throw NotCalled(nameof(WithUpdatedAt)),
        };

    private static InvalidOperationException NotCalled(string setterName) =>
        new InvalidOperationException($"{setterName} must be called before {nameof(Build)}.");
}
