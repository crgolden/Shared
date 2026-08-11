namespace Shared.Domain;

public sealed class Campus
{
    internal Campus()
    {
    }

    public Guid Id { get; internal init; }

    public Guid ChurchId { get; internal init; }

    public string Name { get; internal init; } = string.Empty;

    public string? Street { get; internal init; }

    public string City { get; internal init; } = string.Empty;

    public string State { get; internal init; } = string.Empty;

    public string Zip { get; internal init; } = string.Empty;

    public double Latitude { get; internal init; }

    public double Longitude { get; internal init; }

    public DateTime CreatedAt { get; internal init; }

    public DateTime UpdatedAt { get; internal init; }
}
