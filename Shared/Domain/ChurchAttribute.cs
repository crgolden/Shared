namespace Shared.Domain;

public sealed class ChurchAttribute
{
    internal ChurchAttribute()
    {
    }

    public Guid Id { get; internal init; }

    public Guid ChurchId { get; internal init; }

    public string Key { get; internal init; } = string.Empty;

    public string Value { get; internal init; } = string.Empty;

    public string Source { get; internal init; } = string.Empty;

    public decimal Confidence { get; internal init; }

    public DateTime CreatedAt { get; internal init; }

    public DateTime UpdatedAt { get; internal init; }
}
