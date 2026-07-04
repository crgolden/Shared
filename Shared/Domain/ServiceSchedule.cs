namespace Shared.Domain;

/// <summary>
/// A recurring service time for a <see cref="Church"/> (optionally scoped to a <see cref="Campus"/>),
/// matching the <c>ServiceSchedules</c> table schema exactly.
/// </summary>
public sealed class ServiceSchedule
{
    private ServiceSchedule(
        Guid id,
        Guid churchId,
        Guid? campusId,
        byte dayOfWeek,
        TimeOnly startTime,
        string? description,
        DateTime createdAt,
        DateTime updatedAt)
    {
        Id = id;
        ChurchId = churchId;
        CampusId = campusId;
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        Description = description;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; }

    public Guid ChurchId { get; }

    public Guid? CampusId { get; }

    public byte DayOfWeek { get; }

    public TimeOnly StartTime { get; }

    public string? Description { get; }

    public DateTime CreatedAt { get; }

    public DateTime UpdatedAt { get; }

    public static ServiceSchedule Create(
        Guid id,
        Guid churchId,
        Guid? campusId,
        byte dayOfWeek,
        TimeOnly startTime,
        string? description,
        DateTime createdAt,
        DateTime updatedAt)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id is required.", nameof(id));
        }

        if (churchId == Guid.Empty)
        {
            throw new ArgumentException("ChurchId is required.", nameof(churchId));
        }

        if (dayOfWeek > 6)
        {
            throw new ArgumentOutOfRangeException(nameof(dayOfWeek), dayOfWeek, "DayOfWeek must be 0 (Sunday) through 6 (Saturday).");
        }

        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        return new ServiceSchedule(id, churchId, campusId, dayOfWeek, startTime, description, createdAt, updatedAt);
    }
}
