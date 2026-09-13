namespace Shared.Tests.Unit;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class ServiceScheduleTests
{
    private const byte OutOfRangeDayOfWeekOffset = 1;

    [Fact]
    public void Build_AllValidInput_ReturnsServiceSchedule()
    {
        var scheduledDayOfWeek = TestValues.NewDayOfWeek();
        var scheduledStartTime = TestValues.NewTimeOfDay();

        var schedule = Build(dayOfWeek: scheduledDayOfWeek, startTime: scheduledStartTime);

        Assert.Equal(scheduledDayOfWeek, schedule.DayOfWeek);
        Assert.Equal(scheduledStartTime, schedule.StartTime);
    }

    [Fact]
    public void WithId_Empty_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ServiceScheduleBuilder().WithId(Guid.Empty));
        Assert.Equal("id", ex.ParamName);
    }

    [Fact]
    public void WithChurchId_Empty_Throws()
    {
        var churchId = Guid.Empty;

        var ex = Assert.Throws<ArgumentException>(() => new ServiceScheduleBuilder().WithChurchId(churchId));
        Assert.Equal(nameof(churchId), ex.ParamName);
    }

    [Fact]
    public void WithCampusId_Null_IsAllowed()
    {
        var schedule = Build(campusId: null);

        Assert.Null(schedule.CampusId);
    }

    [Fact]
    public void WithDayOfWeek_AboveSix_Throws()
    {
        const byte dayOfWeek = ServiceScheduleBuilder.MaxDayOfWeek + OutOfRangeDayOfWeekOffset;

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new ServiceScheduleBuilder().WithDayOfWeek(dayOfWeek));
        Assert.Equal(nameof(dayOfWeek), ex.ParamName);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        var createdAt = default(DateTimeOffset);

        var ex = Assert.Throws<ArgumentException>(() => new ServiceScheduleBuilder().WithCreatedAt(createdAt));
        Assert.Equal(nameof(createdAt), ex.ParamName);
    }

    [Fact]
    public void WithUpdatedAt_Default_Throws()
    {
        var updatedAt = default(DateTimeOffset);

        var ex = Assert.Throws<ArgumentException>(() => new ServiceScheduleBuilder().WithUpdatedAt(updatedAt));
        Assert.Equal(nameof(updatedAt), ex.ParamName);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        var scheduleId = Guid.NewGuid();
        var churchId = Guid.NewGuid();
        var scheduledDayOfWeek = TestValues.NewDayOfWeek();
        var createdAt = TestValues.NewUtcTimestamp();
        var updatedAt = TestValues.NewUtcTimestamp();
        var builder = new ServiceScheduleBuilder()
            .WithId(scheduleId)
            .WithChurchId(churchId)
            .WithDayOfWeek(scheduledDayOfWeek)
            .WithCreatedAt(createdAt)
            .WithUpdatedAt(updatedAt);

        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build());
        Assert.Contains(nameof(ServiceScheduleBuilder.WithStartTime), ex.Message, StringComparison.Ordinal);
    }

    private static ServiceSchedule Build(
        Guid? id = null,
        Guid? churchId = null,
        Guid? campusId = null,
        byte? dayOfWeek = null,
        TimeOnly? startTime = null,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null)
    {
        var generatedScheduleId = Guid.NewGuid();
        var generatedChurchId = Guid.NewGuid();

        return new ServiceScheduleBuilder()
            .WithId(id ?? generatedScheduleId)
            .WithChurchId(churchId ?? generatedChurchId)
            .WithCampusId(campusId)
            .WithDayOfWeek(dayOfWeek ?? TestValues.NewDayOfWeek())
            .WithStartTime(startTime ?? TestValues.NewTimeOfDay())
            .WithDescription(TestValues.NewDescription())
            .WithCreatedAt(createdAt ?? TestValues.NewUtcTimestamp())
            .WithUpdatedAt(updatedAt ?? TestValues.NewUtcTimestamp())
            .Build();
    }
}
