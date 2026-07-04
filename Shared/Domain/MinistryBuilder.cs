namespace Shared.Domain;

/// <summary>
/// Builds a <see cref="Ministry"/> one field at a time. Each <c>With*</c> call validates that field
/// immediately and returns <see langword="this"/> for chaining; <see cref="Build"/> only checks that
/// every required field was set.
/// </summary>
public sealed class MinistryBuilder
{
    private Guid? _id;
    private Guid? _churchId;
    private string? _name;
    private string? _description;
    private DateTime? _createdAt;
    private DateTime? _updatedAt;

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

    public MinistryBuilder WithCreatedAt(DateTime createdAt)
    {
        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        _createdAt = createdAt;
        return this;
    }

    public MinistryBuilder WithUpdatedAt(DateTime updatedAt)
    {
        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        _updatedAt = updatedAt;
        return this;
    }

    public Ministry Build()
    {
        EnsureRequiredFieldsSet();
        return new Ministry
        {
            Id = _id!.Value,
            ChurchId = _churchId!.Value,
            Name = _name!,
            Description = _description,
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

        if (_name is null)
        {
            throw new InvalidOperationException($"{nameof(WithName)} must be called before {nameof(Build)}.");
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
