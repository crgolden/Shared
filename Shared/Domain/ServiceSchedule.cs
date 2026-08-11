namespace Shared.Domain;

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
