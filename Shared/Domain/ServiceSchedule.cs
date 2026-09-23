namespace Shared.Domain;

using JetBrains.Annotations;

[PublicAPI]
public sealed class ServiceSchedule
{
    internal ServiceSchedule()
    {
    }

    public Guid Id { get; internal init; }

    public Guid ChurchId { get; internal init; }

    public Guid? CampusId { get; internal init; }

    public byte DayOfWeek { get; internal init; }

    public TimeOnly StartTime { get; internal init; }

    public string? Description { get; internal init; }

    public DateTimeOffset CreatedAt { get; internal init; }

    public DateTimeOffset UpdatedAt { get; internal init; }
}
