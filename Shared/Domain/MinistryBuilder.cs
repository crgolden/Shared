namespace Shared.Domain;

public sealed class MinistryBuilder
{
    private Guid? _id;
    private Guid? _churchId;
    private string? _name;
    private string? _description;
    private DateTimeOffset? _createdAt;
    private DateTimeOffset? _updatedAt;

    public MinistryBuilder WithId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id is required.", nameof(id));
        }

        _id = id;
        return this;
    }

    public MinistryBuilder WithChurchId(Guid churchId)
    {
        if (churchId == Guid.Empty)
        {
            throw new ArgumentException("ChurchId is required.", nameof(churchId));
        }

        _churchId = churchId;
        return this;
    }

    public MinistryBuilder WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        _name = name;
        return this;
    }

    public MinistryBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public MinistryBuilder WithCreatedAt(DateTimeOffset createdAt)
    {
        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        _createdAt = createdAt;
        return this;
    }

    public MinistryBuilder WithUpdatedAt(DateTimeOffset updatedAt)
    {
        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        _updatedAt = updatedAt;
        return this;
    }

    public Ministry Build() =>
        new Ministry
        {
            Id = _id ?? throw NotCalled(nameof(WithId)),
            ChurchId = _churchId ?? throw NotCalled(nameof(WithChurchId)),
            Name = _name ?? throw NotCalled(nameof(WithName)),
            Description = _description,
            CreatedAt = _createdAt ?? throw NotCalled(nameof(WithCreatedAt)),
            UpdatedAt = _updatedAt ?? throw NotCalled(nameof(WithUpdatedAt)),
        };

    private static InvalidOperationException NotCalled(string setterName) =>
        new InvalidOperationException($"{setterName} must be called before {nameof(Build)}.");
}
