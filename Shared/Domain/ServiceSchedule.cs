namespace Shared.Domain;

/// <summary>
/// A recurring service time for a <see cref="Church"/> (optionally scoped to a <see cref="Campus"/>),
/// matching the <c>ServiceSchedules</c> table schema exactly. The internal parameterless constructor
/// plus <c>internal init</c> properties mean only <see cref="ServiceScheduleBuilder"/> (in this
/// assembly) can ever populate a <see cref="ServiceSchedule"/>.
/// </summary>
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

    public DateTime CreatedAt { get; internal init; }

    public DateTime UpdatedAt { get; internal init; }
}
