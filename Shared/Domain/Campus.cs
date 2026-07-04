namespace Shared.Domain;

/// <summary>
/// A satellite location for a <see cref="Church"/>, matching the <c>Campuses</c> table schema exactly.
/// </summary>
public sealed class Campus
{
    private Campus(
        Guid id,
        Guid churchId,
        string name,
        string? street,
        string city,
        string state,
        string zip,
        double latitude,
        double longitude,
        DateTime createdAt,
        DateTime updatedAt)
    {
        Id = id;
        ChurchId = churchId;
        Name = name;
        Street = street;
        City = city;
        State = state;
        Zip = zip;
        Latitude = latitude;
        Longitude = longitude;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; }

    public Guid ChurchId { get; }

    public string Name { get; }

    public string? Street { get; }

    public string City { get; }

    public string State { get; }

    public string Zip { get; }

    public double Latitude { get; }

    public double Longitude { get; }

    public DateTime CreatedAt { get; }

    public DateTime UpdatedAt { get; }

    public static Campus Create(
        Guid id,
        Guid churchId,
        string name,
        string? street,
        string city,
        string state,
        string zip,
        double latitude,
        double longitude,
        DateTime createdAt,
        DateTime updatedAt)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id is required.", nameof(id));
        }

        if (churchId == Guid.Empty)
        {
            throw new ArgumentException("ChurchId is required.", nameof(churchId));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ArgumentException("City is required.", nameof(city));
        }

        if (state is not { Length: 2 })
        {
            throw new ArgumentException("State must be a 2-letter code.", nameof(state));
        }

        if (string.IsNullOrWhiteSpace(zip))
        {
            throw new ArgumentException("Zip is required.", nameof(zip));
        }

        if (latitude is < -90 or > 90)
        {
            throw new ArgumentOutOfRangeException(nameof(latitude), latitude, "Latitude must be between -90 and 90.");
        }

        if (longitude is < -180 or > 180)
        {
            throw new ArgumentOutOfRangeException(nameof(longitude), longitude, "Longitude must be between -180 and 180.");
        }

        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        return new Campus(id, churchId, name, street, city, state, zip, latitude, longitude, createdAt, updatedAt);
    }
}
