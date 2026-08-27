namespace Shared.Domain;

public sealed class Ministry
{
    internal Ministry()
    {
    }

    public Guid Id { get; internal init; }

    public Guid ChurchId { get; internal init; }

    required public string Name { get; init; }

    public string? Description { get; internal init; }

    public DateTimeOffset CreatedAt { get; internal init; }

    public DateTimeOffset UpdatedAt { get; internal init; }
}
