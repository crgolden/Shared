namespace Shared.Domain;

/// <summary>
/// A ministry/program belonging to a <see cref="Church"/>, matching the <c>Ministries</c> table schema exactly.
/// </summary>
public sealed class Ministry
{
    private Ministry(Guid id, Guid churchId, string name, string? description, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        ChurchId = churchId;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; }

    public Guid ChurchId { get; }

    public string Name { get; }

    public string? Description { get; }

    public DateTime CreatedAt { get; }

    public DateTime UpdatedAt { get; }

    public static Ministry Create(Guid id, Guid churchId, string name, string? description, DateTime createdAt, DateTime updatedAt)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id is required.", nameof(id));
        }

        if (churchId == Guid.Empty)
        {
            throw new ArgumentException("ChurchId is required.", nameof(churchId));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        return new Ministry(id, churchId, name, description, createdAt, updatedAt);
    }
}
