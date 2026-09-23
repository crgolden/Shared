namespace Shared.Domain;

using JetBrains.Annotations;

[PublicAPI]
public sealed class ChurchAttribute
{
    internal ChurchAttribute()
    {
    }

    public Guid Id { get; internal init; }

    public Guid ChurchId { get; internal init; }

    public required string Key { get; init; }

    public required string Value { get; init; }

    public required string Source { get; init; }

    public decimal Confidence { get; internal init; }

    public DateTimeOffset CreatedAt { get; internal init; }

    public DateTimeOffset UpdatedAt { get; internal init; }
}
