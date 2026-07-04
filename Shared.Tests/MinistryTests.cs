namespace Shared.Tests;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class MinistryTests
{
    [Fact]
    public void Create_AllValidInput_ReturnsMinistry()
    {
        var ministry = Build();

        Assert.Equal("Youth Group", ministry.Name);
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
    public void Create_BlankName_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(name: string.Empty));
        Assert.Equal("name", ex.ParamName);
    }

    [Fact]
    public void Create_NullDescription_IsAllowed()
    {
        var ministry = Build(description: null);

        Assert.Null(ministry.Description);
    }

    [Fact]
    public void Create_DefaultCreatedAt_Throws()
    {
        // Build()'s DateTime? "unset" sentinel is itself null, so default(DateTime) (a real, non-null
        // value) can only be exercised by calling Create directly rather than through the helper.
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var ex = Assert.Throws<ArgumentException>(() => Ministry.Create(Guid.NewGuid(), Guid.NewGuid(), "Youth Group", "Teens", default, now));
        Assert.Equal("createdAt", ex.ParamName);
    }

    [Fact]
    public void Create_DefaultUpdatedAt_Throws()
    {
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var ex = Assert.Throws<ArgumentException>(() => Ministry.Create(Guid.NewGuid(), Guid.NewGuid(), "Youth Group", "Teens", now, default));
        Assert.Equal("updatedAt", ex.ParamName);
    }

    private static Ministry Build(
        Guid? id = null,
        Guid? churchId = null,
        string name = "Youth Group",
        string? description = "Teens",
        DateTime? createdAt = null,
        DateTime? updatedAt = null)
    {
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Ministry.Create(id ?? Guid.NewGuid(), churchId ?? Guid.NewGuid(), name, description, createdAt ?? now, updatedAt ?? now);
    }
}
