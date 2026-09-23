namespace Shared.Domain;

using JetBrains.Annotations;

[PublicAPI]
public sealed class Church
{
    internal Church()
    {
    }

    public Guid Id { get; internal init; }

    public required string CanonicalName { get; init; }

    public required string Slug { get; init; }

    public double Latitude { get; internal init; }

    public double Longitude { get; internal init; }

    public string? Street { get; internal init; }

    public required string City { get; init; }

    public required StateCode State { get; init; }

    public required string Zip { get; init; }

    public string? PhoneNumber { get; internal init; }

    public string? Website { get; internal init; }

    public string? EmailAddress { get; internal init; }

    public Guid? DenominationId { get; internal init; }

    public int WorshipStyle { get; internal init; }

    public required string PrimaryLanguage { get; init; }

    public bool? AcceptsLGBTQ { get; internal init; }

    public bool? WheelchairAccessible { get; internal init; }

    public bool? HasNursery { get; internal init; }

    public bool? HasYouthProgram { get; internal init; }

    public decimal ConfidenceScore { get; internal init; }

    public DateTimeOffset? LastVerifiedAt { get; internal init; }

    public DateTimeOffset CreatedAt { get; internal init; }

    public DateTimeOffset UpdatedAt { get; internal init; }

    public bool IsActive { get; internal init; } = true;
}
