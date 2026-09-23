namespace Shared.Domain;

using JetBrains.Annotations;

[PublicAPI]
public sealed class Campus
{
    internal Campus()
    {
    }

    public Guid Id { get; internal init; }

    public Guid ChurchId { get; internal init; }

    public required string Name { get; init; }

    public string? Street { get; internal init; }

    public required string City { get; init; }

    public required StateCode State { get; init; }

    public required string Zip { get; init; }

    public double Latitude { get; internal init; }

    public double Longitude { get; internal init; }

    public DateTimeOffset CreatedAt { get; internal init; }

    public DateTimeOffset UpdatedAt { get; internal init; }
}
