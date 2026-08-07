namespace Shared.Domain;

public sealed class ChurchAttribute
{
    internal ChurchAttribute()
    {
    }

    public Guid Id { get; internal init; }

    public Guid ChurchId { get; internal init; }

    required public string Key { get; init; }

    required public string Value { get; init; }

    required public string Source { get; init; }

    public decimal Confidence { get; internal init; }

    public DateTime CreatedAt { get; internal init; }

    public DateTime UpdatedAt { get; internal init; }
}
