namespace Shared.Tests;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class ServiceScheduleTests
{
    [Fact]
    public void Create_AllValidInput_ReturnsServiceSchedule()
    {
        var schedule = Build();

        Assert.Equal((byte)0, schedule.DayOfWeek);
        Assert.Equal(new TimeOnly(10, 30), schedule.StartTime);
    }

    [Fact]
    public void Create_EmptyId_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(id: Guid.Empty));
        Assert.Equal("id", ex.ParamName);
    }

    [Fact]
    public void Create_EmptyChurchId_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(churchId: Guid.Empty));
        Assert.Equal("churchId", ex.ParamName);
    }

    [Fact]
    public void Create_NullCampusId_IsAllowed()
    {
        var schedule = Build(campusId: null);

        Assert.Null(schedule.CampusId);
    }

    [Fact]
    public void Create_DayOfWeekAboveSix_Throws()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Build(dayOfWeek: 7));
        Assert.Equal("dayOfWeek", ex.ParamName);
    }

    [Fact]
    public void Create_DefaultCreatedAt_Throws()
    {
        // Build()'s DateTime? "unset" sentinel is itself null, so default(DateTime) (a real, non-null
        // value) can only be exercised by calling Create directly rather than through the helper.
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var ex = Assert.Throws<ArgumentException>(() => ServiceSchedule.Create(
            Guid.NewGuid(), Guid.NewGuid(), null, 0, new TimeOnly(10, 30), "Sunday Worship", default, now));
        Assert.Equal("createdAt", ex.ParamName);
    }

    [Fact]
    public void Create_DefaultUpdatedAt_Throws()
    {
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var ex = Assert.Throws<ArgumentException>(() => ServiceSchedule.Create(
            Guid.NewGuid(), Guid.NewGuid(), null, 0, new TimeOnly(10, 30), "Sunday Worship", now, default));
        Assert.Equal("updatedAt", ex.ParamName);
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
        return ServiceSchedule.Create(
            id ?? Guid.NewGuid(),
            churchId ?? Guid.NewGuid(),
            campusId,
            dayOfWeek,
            new TimeOnly(10, 30),
            description: "Sunday Worship",
            createdAt ?? now,
            updatedAt ?? now);
    }
}
