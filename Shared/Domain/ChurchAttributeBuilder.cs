namespace Shared.Domain;

/// <summary>
/// Builds a <see cref="ChurchAttribute"/> one field at a time. Each <c>With*</c> call validates that
/// field immediately and returns <see langword="this"/> for chaining; <see cref="Build"/> only checks
/// that every required field was set.
/// </summary>
public sealed class ChurchAttributeBuilder
{
    private Guid? _id;
    private Guid? _churchId;
    private string? _key;
    private string? _value;
    private string? _source;
    private decimal? _confidence;
    private DateTime? _createdAt;
    private DateTime? _updatedAt;

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
        if (confidence is < 0m or > 1m)
        {
            throw new ArgumentOutOfRangeException(nameof(confidence), confidence, "Confidence must be between 0 and 1.");
        }

        _confidence = confidence;
        return this;
    }

    public ChurchAttributeBuilder WithCreatedAt(DateTime createdAt)
    {
        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        _createdAt = createdAt;
        return this;
    }

    public ChurchAttributeBuilder WithUpdatedAt(DateTime updatedAt)
    {
        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        _updatedAt = updatedAt;
        return this;
    }

    public ChurchAttribute Build()
    {
        EnsureRequiredFieldsSet();
        return new ChurchAttribute
        {
            Id = _id!.Value,
            ChurchId = _churchId!.Value,
            Key = _key!,
            Value = _value!,
            Source = _source!,
            Confidence = _confidence!.Value,
            CreatedAt = _createdAt!.Value,
            UpdatedAt = _updatedAt!.Value,
        };
    }

    private void EnsureRequiredFieldsSet()
    {
        if (_id is null)
        {
            throw new InvalidOperationException($"{nameof(WithId)} must be called before {nameof(Build)}.");
        }

        if (_churchId is null)
        {
            throw new InvalidOperationException($"{nameof(WithChurchId)} must be called before {nameof(Build)}.");
        }

        if (_key is null)
        {
            throw new InvalidOperationException($"{nameof(WithKey)} must be called before {nameof(Build)}.");
        }

        if (_value is null)
        {
            throw new InvalidOperationException($"{nameof(WithValue)} must be called before {nameof(Build)}.");
        }

        if (_source is null)
        {
            throw new InvalidOperationException($"{nameof(WithSource)} must be called before {nameof(Build)}.");
        }

        if (_confidence is null)
        {
            throw new InvalidOperationException($"{nameof(WithConfidence)} must be called before {nameof(Build)}.");
        }

        if (_createdAt is null)
        {
            throw new InvalidOperationException($"{nameof(WithCreatedAt)} must be called before {nameof(Build)}.");
        }

        if (_updatedAt is null)
        {
            throw new InvalidOperationException($"{nameof(WithUpdatedAt)} must be called before {nameof(Build)}.");
        }
    }
}
