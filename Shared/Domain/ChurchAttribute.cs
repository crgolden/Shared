namespace Shared.Domain;

/// <summary>
/// A key/value attribute attached to a <see cref="Church"/> (e.g. denomination, worship style),
/// matching the <c>ChurchAttributes</c> table schema exactly. The internal parameterless constructor
/// plus <c>internal init</c> properties mean only <see cref="ChurchAttributeBuilder"/> (in this
/// assembly) can ever populate a <see cref="ChurchAttribute"/>.
/// </summary>
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
