namespace Shared.Domain;

public sealed class Church
{
    internal Church()
    {
    }

    public Guid Id { get; internal init; }

    required public string CanonicalName { get; init; }

    required public string Slug { get; init; }

    public double Latitude { get; internal init; }

    public double Longitude { get; internal init; }

    public string? Street { get; internal init; }

    required public string City { get; init; }

    required public StateCode State { get; init; }

    required public string Zip { get; init; }

    public string? PhoneNumber { get; internal init; }

    public string? Website { get; internal init; }

    public string? EmailAddress { get; internal init; }

    public Guid? DenominationId { get; internal init; }

    public int WorshipStyle { get; internal init; }

    required public string PrimaryLanguage { get; init; }

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
