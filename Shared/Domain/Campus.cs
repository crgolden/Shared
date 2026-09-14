namespace Shared.Domain;

public sealed class Campus
{
    internal Campus()
    {
    }

    public Guid Id { get; internal init; }

    public Guid ChurchId { get; internal init; }

    required public string Name { get; init; }

    public string? Street { get; internal init; }

    required public string City { get; init; }

    required public StateCode State { get; init; }

    required public string Zip { get; init; }

    public double Latitude { get; internal init; }

    public double Longitude { get; internal init; }

    public DateTimeOffset CreatedAt { get; internal init; }

    public DateTimeOffset UpdatedAt { get; internal init; }
}
