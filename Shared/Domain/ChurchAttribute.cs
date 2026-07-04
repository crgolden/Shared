namespace Shared.Domain;

/// <summary>
/// A key/value attribute attached to a <see cref="Church"/> (e.g. denomination, worship style),
/// matching the <c>ChurchAttributes</c> table schema exactly.
/// </summary>
public sealed class ChurchAttribute
{
    private ChurchAttribute(
        Guid id,
        Guid churchId,
        string key,
        string value,
        string source,
        decimal confidence,
        DateTime createdAt,
        DateTime updatedAt)
    {
        Id = id;
        ChurchId = churchId;
        Key = key;
        Value = value;
        Source = source;
        Confidence = confidence;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; }

    public Guid ChurchId { get; }

    public string Key { get; }

    public string Value { get; }

    public string Source { get; }

    public decimal Confidence { get; }

    public DateTime CreatedAt { get; }

    public DateTime UpdatedAt { get; }

    public static ChurchAttribute Create(
        Guid id,
        Guid churchId,
        string key,
        string value,
        string source,
        decimal confidence,
        DateTime createdAt,
        DateTime updatedAt)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id is required.", nameof(id));
        }

        if (churchId == Guid.Empty)
        {
            throw new ArgumentException("ChurchId is required.", nameof(churchId));
        }

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key is required.", nameof(key));
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value is required.", nameof(value));
        }

        if (string.IsNullOrWhiteSpace(source))
        {
            throw new ArgumentException("Source is required.", nameof(source));
        }

        if (confidence is < 0m or > 1m)
        {
            throw new ArgumentOutOfRangeException(nameof(confidence), confidence, "Confidence must be between 0 and 1.");
        }

        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        return new ChurchAttribute(id, churchId, key, value, source, confidence, createdAt, updatedAt);
    }
}
