namespace Shared.Tests;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class MinistryTests
{
    [Fact]
    public void Build_AllValidInput_ReturnsMinistry()
    {
        var ministry = Build();

        Assert.Equal("Youth Group", ministry.Name);
    }

    [Fact]
    public void WithId_Empty_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new MinistryBuilder().WithId(Guid.Empty));
        Assert.Equal("id", ex.ParamName);
    }

    [Fact]
    public void WithChurchId_Empty_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new MinistryBuilder().WithChurchId(Guid.Empty));
        Assert.Equal("churchId", ex.ParamName);
    }

    [Fact]
    public void WithName_Blank_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new MinistryBuilder().WithName(string.Empty));
        Assert.Equal("name", ex.ParamName);
    }

    [Fact]
    public void WithDescription_Null_IsAllowed()
    {
        var ministry = Build(description: null);

        Assert.Null(ministry.Description);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new MinistryBuilder().WithCreatedAt(default));
        Assert.Equal("createdAt", ex.ParamName);
    }

    [Fact]
    public void WithUpdatedAt_Default_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new MinistryBuilder().WithUpdatedAt(default));
        Assert.Equal("updatedAt", ex.ParamName);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        var builder = new MinistryBuilder()
            .WithId(Guid.NewGuid())
            .WithChurchId(Guid.NewGuid())
            .WithCreatedAt(DateTime.UtcNow)
            .WithUpdatedAt(DateTime.UtcNow);

        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build());
        Assert.Contains("WithName", ex.Message, StringComparison.Ordinal);
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
        return new MinistryBuilder()
            .WithId(id ?? Guid.NewGuid())
            .WithChurchId(churchId ?? Guid.NewGuid())
            .WithName(name)
            .WithDescription(description)
            .WithCreatedAt(createdAt ?? now)
            .WithUpdatedAt(updatedAt ?? now)
            .Build();
    }
}
