namespace Shared.Tests.Unit;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class ServiceScheduleTests
{
    private const byte OutOfRangeDayOfWeekOffset = 1;

    [Fact]
    public void Build_AllValidInput_ReturnsServiceSchedule()
    {
        // Arrange
        var scheduledDayOfWeek = TestValues.NewDayOfWeek();
        var scheduledStartTime = TestValues.NewTimeOfDay();

        // Act
        var schedule = Build(dayOfWeek: scheduledDayOfWeek, startTime: scheduledStartTime);

        // Assert
        Assert.Equal(scheduledDayOfWeek, schedule.DayOfWeek);
        Assert.Equal(scheduledStartTime, schedule.StartTime);
    }

    [Fact]
    public void WithId_Empty_Throws()
    {
        // Arrange
        var id = Guid.Empty;

        // Act
        var exception = Record.Exception(() => new ServiceScheduleBuilder().WithId(id));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(id), ex.ParamName);
    }

    [Fact]
    public void WithChurchId_Empty_Throws()
    {
        // Arrange
        var churchId = Guid.Empty;

        // Act
        var exception = Record.Exception(() => new ServiceScheduleBuilder().WithChurchId(churchId));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(churchId), ex.ParamName);
    }

    [Fact]
    public void WithCampusId_Null_IsAllowed()
    {
        // Act
        var schedule = Build(campusId: null);

        // Assert
        Assert.Null(schedule.CampusId);
    }

    [Fact]
    public void WithDayOfWeek_AboveSix_Throws()
    {
        // Arrange
        const byte dayOfWeek = ServiceScheduleBuilder.MaxDayOfWeek + OutOfRangeDayOfWeekOffset;

        // Act
        var exception = Record.Exception(() => new ServiceScheduleBuilder().WithDayOfWeek(dayOfWeek));

        // Assert
        var ex = Assert.IsType<ArgumentOutOfRangeException>(exception);
        Assert.Equal(nameof(dayOfWeek), ex.ParamName);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        // Arrange
        var createdAt = default(DateTimeOffset);

        // Act
        var exception = Record.Exception(() => new ServiceScheduleBuilder().WithCreatedAt(createdAt));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(createdAt), ex.ParamName);
    }

    [Fact]
    public void WithUpdatedAt_Default_Throws()
    {
        // Arrange
        var updatedAt = default(DateTimeOffset);

        // Act
        var exception = Record.Exception(() => new ServiceScheduleBuilder().WithUpdatedAt(updatedAt));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(updatedAt), ex.ParamName);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        // Arrange
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

        // Act
        var exception = Record.Exception(() => builder.Build());

        // Assert
        var ex = Assert.IsType<InvalidOperationException>(exception);
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
