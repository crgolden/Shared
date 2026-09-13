namespace Shared.Domain;

public sealed class ServiceScheduleBuilder
{
    public const byte MaxDayOfWeek = 6;

    private Guid? _id;
    private Guid? _churchId;
    private Guid? _campusId;
    private byte? _dayOfWeek;
    private TimeOnly? _startTime;
    private string? _description;
    private DateTimeOffset? _createdAt;
    private DateTimeOffset? _updatedAt;

    public ServiceScheduleBuilder WithId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id is required.", nameof(id));
        }

        _id = id;
        return this;
    }

    public ServiceScheduleBuilder WithChurchId(Guid churchId)
    {
        if (churchId == Guid.Empty)
        {
            throw new ArgumentException("ChurchId is required.", nameof(churchId));
        }

        _churchId = churchId;
        return this;
    }

    public ServiceScheduleBuilder WithCampusId(Guid? campusId)
    {
        _campusId = campusId;
        return this;
    }

    public ServiceScheduleBuilder WithDayOfWeek(byte dayOfWeek)
    {
        if (dayOfWeek > MaxDayOfWeek)
        {
            throw new ArgumentOutOfRangeException(nameof(dayOfWeek), dayOfWeek, "DayOfWeek must be 0 (Sunday) through 6 (Saturday).");
        }

        _dayOfWeek = dayOfWeek;
        return this;
    }

    public ServiceScheduleBuilder WithStartTime(TimeOnly startTime)
    {
        _startTime = startTime;
        return this;
    }

    public ServiceScheduleBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public ServiceScheduleBuilder WithCreatedAt(DateTimeOffset createdAt)
    {
        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        _createdAt = createdAt;
        return this;
    }

    public ServiceScheduleBuilder WithUpdatedAt(DateTimeOffset updatedAt)
    {
        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        _updatedAt = updatedAt;
        return this;
    }

    public ServiceSchedule Build() =>
        new ServiceSchedule
        {
            Id = _id ?? throw NotCalled(nameof(WithId)),
            ChurchId = _churchId ?? throw NotCalled(nameof(WithChurchId)),
            CampusId = _campusId,
            DayOfWeek = _dayOfWeek ?? throw NotCalled(nameof(WithDayOfWeek)),
            StartTime = _startTime ?? throw NotCalled(nameof(WithStartTime)),
            Description = _description,
            CreatedAt = _createdAt ?? throw NotCalled(nameof(WithCreatedAt)),
            UpdatedAt = _updatedAt ?? throw NotCalled(nameof(WithUpdatedAt)),
        };

    private static InvalidOperationException NotCalled(string setterName) =>
        new InvalidOperationException($"{setterName} must be called before {nameof(Build)}.");
}
