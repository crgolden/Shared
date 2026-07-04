namespace Shared.Domain;

/// <summary>
/// A ministry/program belonging to a <see cref="Church"/>, matching the <c>Ministries</c> table schema exactly.
/// The internal parameterless constructor plus <c>internal init</c> properties mean only
/// <see cref="MinistryBuilder"/> (in this assembly) can ever populate a <see cref="Ministry"/>.
/// </summary>
public sealed class Ministry
{
    internal Ministry()
    {
    }

    public Guid Id { get; internal init; }

    public Guid ChurchId { get; internal init; }

    public string Name { get; internal init; } = string.Empty;

    public string? Description { get; internal init; }

    public DateTime CreatedAt { get; internal init; }

    public DateTime UpdatedAt { get; internal init; }
}
