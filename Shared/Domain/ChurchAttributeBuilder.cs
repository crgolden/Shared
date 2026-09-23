namespace Shared.Domain;

public sealed class ChurchAttributeBuilder
{
    public const decimal MinConfidence = 0m;

    public const decimal MaxConfidence = 1m;

    public const int ConfidencePrecision = 5;

    public const int ConfidenceScale = 4;

    private Guid? _id;
    private Guid? _churchId;
    private string? _key;
    private string? _value;
    private string? _source;
    private decimal? _confidence;
    private DateTimeOffset? _createdAt;
    private DateTimeOffset? _updatedAt;

    public ChurchAttributeBuilder WithId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id is required.", nameof(id));
        }

        _id = id;
        return this;
    }

    public ChurchAttributeBuilder WithChurchId(Guid churchId)
    {
        if (churchId == Guid.Empty)
        {
            throw new ArgumentException("ChurchId is required.", nameof(churchId));
        }

        _churchId = churchId;
        return this;
    }

    public ChurchAttributeBuilder WithKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key is required.", nameof(key));
        }

        _key = key;
        return this;
    }

    public ChurchAttributeBuilder WithValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value is required.", nameof(value));
        }

        _value = value;
        return this;
    }

    public ChurchAttributeBuilder WithSource(string source)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            throw new ArgumentException("Source is required.", nameof(source));
        }

        _source = source;
        return this;
    }

    public ChurchAttributeBuilder WithConfidence(decimal confidence)
    {
        if (confidence is < MinConfidence or > MaxConfidence)
        {
            throw new ArgumentOutOfRangeException(nameof(confidence), confidence, "Confidence must be between 0 and 1.");
        }

        _confidence = confidence;
        return this;
    }

    public ChurchAttributeBuilder WithCreatedAt(DateTimeOffset createdAt)
    {
        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        _createdAt = createdAt;
        return this;
    }

    public ChurchAttributeBuilder WithUpdatedAt(DateTimeOffset updatedAt)
    {
        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        _updatedAt = updatedAt;
        return this;
    }

    public ChurchAttribute Build() =>
        new ChurchAttribute
        {
            Id = _id ?? throw NotCalled(nameof(WithId)),
            ChurchId = _churchId ?? throw NotCalled(nameof(WithChurchId)),
            Key = _key ?? throw NotCalled(nameof(WithKey)),
            Value = _value ?? throw NotCalled(nameof(WithValue)),
            Source = _source ?? throw NotCalled(nameof(WithSource)),
            Confidence = _confidence ?? throw NotCalled(nameof(WithConfidence)),
            CreatedAt = _createdAt ?? throw NotCalled(nameof(WithCreatedAt)),
            UpdatedAt = _updatedAt ?? throw NotCalled(nameof(WithUpdatedAt)),
        };

    private static InvalidOperationException NotCalled(string setterName) =>
        new InvalidOperationException($"{setterName} must be called before {nameof(Build)}.");
}
