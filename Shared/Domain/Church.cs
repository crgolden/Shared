namespace Shared.Domain;

/// <summary>
/// A church record, matching the <c>Churches</c> table schema exactly. The internal parameterless
/// constructor plus <c>internal init</c> properties mean only <see cref="ChurchBuilder"/> (in this
/// assembly) can ever populate a <see cref="Church"/> — external assemblies can't construct one via
/// object initializer and bypass validation. Each <c>With*</c> call on the builder validates its own
/// field immediately, and <see cref="ChurchBuilder.Build"/> refuses to construct until every required
/// field has been set, so callers get an immediate, specific <see cref="ArgumentException"/> instead of
/// a raw SQL constraint violation three layers away from the bad input.
/// </summary>
public sealed class Church
{
    internal Church()
    {
    }

    public Guid Id { get; internal init; }

    public string CanonicalName { get; internal init; } = string.Empty;

    public string Slug { get; internal init; } = string.Empty;

    public double Latitude { get; internal init; }

    public double Longitude { get; internal init; }

    public string? Street { get; internal init; }

    public string City { get; internal init; } = string.Empty;

    public string State { get; internal init; } = string.Empty;

    public string Zip { get; internal init; } = string.Empty;

    public string? PhoneNumber { get; internal init; }

    public string? Website { get; internal init; }

    public string? EmailAddress { get; internal init; }

    public Guid? DenominationId { get; internal init; }

    public int WorshipStyle { get; internal init; }

    public string PrimaryLanguage { get; internal init; } = string.Empty;

    public bool? AcceptsLGBTQ { get; internal init; }

    public bool? WheelchairAccessible { get; internal init; }

    public bool? HasNursery { get; internal init; }

    public bool? HasYouthProgram { get; internal init; }

    public decimal ConfidenceScore { get; internal init; }

    public DateTime? LastVerifiedAt { get; internal init; }

    public DateTime CreatedAt { get; internal init; }

    public DateTime UpdatedAt { get; internal init; }

    public bool IsActive { get; internal init; } = true;
}
