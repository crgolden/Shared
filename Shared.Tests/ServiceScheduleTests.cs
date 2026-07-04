namespace Shared.Tests;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class ServiceScheduleTests
{
    [Fact]
    public void Build_AllValidInput_ReturnsServiceSchedule()
    {
        var schedule = Build();

        Assert.Equal((byte)0, schedule.DayOfWeek);
        Assert.Equal(new TimeOnly(10, 30), schedule.StartTime);
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
        var ex = Assert.Throws<ArgumentException>(() => new ServiceScheduleBuilder().WithChurchId(Guid.Empty));
        Assert.Equal("churchId", ex.ParamName);
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
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new ServiceScheduleBuilder().WithDayOfWeek(7));
        Assert.Equal("dayOfWeek", ex.ParamName);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ServiceScheduleBuilder().WithCreatedAt(default));
        Assert.Equal("createdAt", ex.ParamName);
    }

    [Fact]
    public void WithUpdatedAt_Default_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ServiceScheduleBuilder().WithUpdatedAt(default));
        Assert.Equal("updatedAt", ex.ParamName);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        var builder = new ServiceScheduleBuilder()
            .WithId(Guid.NewGuid())
            .WithChurchId(Guid.NewGuid())
            .WithDayOfWeek(0)
            .WithCreatedAt(DateTime.UtcNow)
            .WithUpdatedAt(DateTime.UtcNow);

        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build());
        Assert.Contains("WithStartTime", ex.Message, StringComparison.Ordinal);
    }

    private static ServiceSchedule Build(
        Guid? id = null,
        Guid? churchId = null,
        Guid? campusId = null,
        byte dayOfWeek = 0,
        DateTime? createdAt = null,
        DateTime? updatedAt = null)
    {
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return new ServiceScheduleBuilder()
            .WithId(id ?? Guid.NewGuid())
            .WithChurchId(churchId ?? Guid.NewGuid())
            .WithCampusId(campusId)
            .WithDayOfWeek(dayOfWeek)
            .WithStartTime(new TimeOnly(10, 30))
            .WithDescription("Sunday Worship")
            .WithCreatedAt(createdAt ?? now)
            .WithUpdatedAt(updatedAt ?? now)
            .Build();
    }
}
