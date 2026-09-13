namespace Shared.Tests.Unit;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class MinistryTests
{
    [Fact]
    public void Build_AllValidInput_ReturnsMinistry()
    {
        var ministryName = TestValues.NewName();

        var ministry = Build(name: ministryName);

        Assert.Equal(ministryName, ministry.Name);
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
        var churchId = Guid.Empty;

        var ex = Assert.Throws<ArgumentException>(() => new MinistryBuilder().WithChurchId(churchId));
        Assert.Equal(nameof(churchId), ex.ParamName);
    }

    [Fact]
    public void WithName_Blank_Throws()
    {
        var name = TestValues.NewBlank();

        var ex = Assert.Throws<ArgumentException>(() => new MinistryBuilder().WithName(name));
        Assert.Equal(nameof(name), ex.ParamName);
    }

    [Fact]
    public void WithDescription_Null_IsAllowed()
    {
        var ministryId = Guid.NewGuid();
        var churchId = Guid.NewGuid();
        var ministryName = TestValues.NewName();
        var createdAt = TestValues.NewUtcTimestamp();
        var updatedAt = TestValues.NewUtcTimestamp();

        var ministry = new MinistryBuilder()
            .WithId(ministryId)
            .WithChurchId(churchId)
            .WithName(ministryName)
            .WithDescription(null)
            .WithCreatedAt(createdAt)
            .WithUpdatedAt(updatedAt)
            .Build();

        Assert.Null(ministry.Description);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        var createdAt = default(DateTimeOffset);

        var ex = Assert.Throws<ArgumentException>(() => new MinistryBuilder().WithCreatedAt(createdAt));
        Assert.Equal(nameof(createdAt), ex.ParamName);
    }

    [Fact]
    public void WithUpdatedAt_Default_Throws()
    {
        var updatedAt = default(DateTimeOffset);

        var ex = Assert.Throws<ArgumentException>(() => new MinistryBuilder().WithUpdatedAt(updatedAt));
        Assert.Equal(nameof(updatedAt), ex.ParamName);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        var ministryId = Guid.NewGuid();
        var churchId = Guid.NewGuid();
        var createdAt = TestValues.NewUtcTimestamp();
        var updatedAt = TestValues.NewUtcTimestamp();
        var builder = new MinistryBuilder()
            .WithId(ministryId)
            .WithChurchId(churchId)
            .WithCreatedAt(createdAt)
            .WithUpdatedAt(updatedAt);

        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build());
        Assert.Contains(nameof(MinistryBuilder.WithName), ex.Message, StringComparison.Ordinal);
    }

    private static Ministry Build(
        Guid? id = null,
        Guid? churchId = null,
        string? name = null,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null)
    {
        var generatedMinistryId = Guid.NewGuid();
        var generatedChurchId = Guid.NewGuid();

        return new MinistryBuilder()
            .WithId(id ?? generatedMinistryId)
            .WithChurchId(churchId ?? generatedChurchId)
            .WithName(name ?? TestValues.NewName())
            .WithDescription(TestValues.NewDescription())
            .WithCreatedAt(createdAt ?? TestValues.NewUtcTimestamp())
            .WithUpdatedAt(updatedAt ?? TestValues.NewUtcTimestamp())
            .Build();
    }
}
