namespace Shared.Domain;

/// <summary>
/// Builds a <see cref="ServiceSchedule"/> one field at a time. Each <c>With*</c> call validates that
/// field immediately and returns <see langword="this"/> for chaining; <see cref="Build"/> only checks
/// that every required field was set.
/// </summary>
public sealed class ServiceScheduleBuilder
{
    private Guid? _id;
    private Guid? _churchId;
    private Guid? _campusId;
    private byte? _dayOfWeek;
    private TimeOnly? _startTime;
    private string? _description;
    private DateTime? _createdAt;
    private DateTime? _updatedAt;

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
        if (dayOfWeek > 6)
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

    public ServiceScheduleBuilder WithCreatedAt(DateTime createdAt)
    {
        if (createdAt == default)
        {
            throw new ArgumentException("CreatedAt is required.", nameof(createdAt));
        }

        _createdAt = createdAt;
        return this;
    }

    public ServiceScheduleBuilder WithUpdatedAt(DateTime updatedAt)
    {
        if (updatedAt == default)
        {
            throw new ArgumentException("UpdatedAt is required.", nameof(updatedAt));
        }

        _updatedAt = updatedAt;
        return this;
    }

    public ServiceSchedule Build()
    {
        EnsureRequiredFieldsSet();
        return new ServiceSchedule
        {
            Id = _id!.Value,
            ChurchId = _churchId!.Value,
            CampusId = _campusId,
            DayOfWeek = _dayOfWeek!.Value,
            StartTime = _startTime!.Value,
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

        if (_dayOfWeek is null)
        {
            throw new InvalidOperationException($"{nameof(WithDayOfWeek)} must be called before {nameof(Build)}.");
        }

        if (_startTime is null)
        {
            throw new InvalidOperationException($"{nameof(WithStartTime)} must be called before {nameof(Build)}.");
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
