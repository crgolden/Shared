namespace Shared.Domain;

/// <summary>
/// Builds a <see cref="Campus"/> one field at a time. Each <c>With*</c> call validates that field
/// immediately and returns <see langword="this"/> for chaining; <see cref="Build"/> only checks that
/// every required field was set.
/// </summary>
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
    private DateTime? _createdAt;
    private DateTime? _updatedAt;

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

    public CampusBuilder WithCreatedAt(DateTime createdAt)
    {
        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        _createdAt = createdAt;
        return this;
    }

    public CampusBuilder WithUpdatedAt(DateTime updatedAt)
    {
        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        _updatedAt = updatedAt;
        return this;
    }

    public Campus Build()
    {
        EnsureRequiredFieldsSet();
        return new Campus
        {
            Id = _id!.Value,
            ChurchId = _churchId!.Value,
            Name = _name!,
            Street = _street,
            City = _city!,
            State = _state!,
            Zip = _zip!,
            Latitude = _latitude!.Value,
            Longitude = _longitude!.Value,
            CreatedAt = _createdAt!.Value,
            UpdatedAt = _updatedAt!.Value,
        };
    }

    private void EnsureRequiredFieldsSet()
    {
        if (_id is null)
        {
            throw new InvalidOperationException($"{nameof(WithId)} must be called before {nameof(Build)}.");
        }

        if (_churchId is null)
        {
            throw new InvalidOperationException($"{nameof(WithChurchId)} must be called before {nameof(Build)}.");
        }

        if (_name is null)
        {
            throw new InvalidOperationException($"{nameof(WithName)} must be called before {nameof(Build)}.");
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

        if (_latitude is null)
        {
            throw new InvalidOperationException($"{nameof(WithLatitude)} must be called before {nameof(Build)}.");
        }

        if (_longitude is null)
        {
            throw new InvalidOperationException($"{nameof(WithLongitude)} must be called before {nameof(Build)}.");
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
