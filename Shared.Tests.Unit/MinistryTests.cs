namespace Shared.Tests.Unit;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class MinistryTests
{
    [Fact]
    public void Build_AllValidInput_ReturnsMinistry()
    {
        // Arrange
        var ministryName = TestValues.NewName();

        // Act
        var ministry = Build(name: ministryName);

        // Assert
        Assert.Equal(ministryName, ministry.Name);
    }

    [Fact]
    public void WithId_Empty_Throws()
    {
        // Arrange
        var id = Guid.Empty;

        // Act
        var exception = Record.Exception(() => new MinistryBuilder().WithId(id));

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
        var exception = Record.Exception(() => new MinistryBuilder().WithChurchId(churchId));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(churchId), ex.ParamName);
    }

    [Fact]
    public void WithName_Blank_Throws()
    {
        // Arrange
        var name = TestValues.NewBlank();

        // Act
        var exception = Record.Exception(() => new MinistryBuilder().WithName(name));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(name), ex.ParamName);
    }

    [Fact]
    public void WithDescription_Null_IsAllowed()
    {
        // Arrange
        var ministryId = Guid.NewGuid();
        var churchId = Guid.NewGuid();
        var ministryName = TestValues.NewName();
        var createdAt = TestValues.NewUtcTimestamp();
        var updatedAt = TestValues.NewUtcTimestamp();

        // Act
        var ministry = new MinistryBuilder()
            .WithId(ministryId)
            .WithChurchId(churchId)
            .WithName(ministryName)
            .WithDescription(null)
            .WithCreatedAt(createdAt)
            .WithUpdatedAt(updatedAt)
            .Build();

        // Assert
        Assert.Null(ministry.Description);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        // Arrange
        var createdAt = default(DateTimeOffset);

        // Act
        var exception = Record.Exception(() => new MinistryBuilder().WithCreatedAt(createdAt));

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
        var exception = Record.Exception(() => new MinistryBuilder().WithUpdatedAt(updatedAt));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(updatedAt), ex.ParamName);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        // Arrange
        var ministryId = Guid.NewGuid();
        var churchId = Guid.NewGuid();
        var createdAt = TestValues.NewUtcTimestamp();
        var updatedAt = TestValues.NewUtcTimestamp();
        var builder = new MinistryBuilder()
            .WithId(ministryId)
            .WithChurchId(churchId)
            .WithCreatedAt(createdAt)
            .WithUpdatedAt(updatedAt);

        // Act
        var exception = Record.Exception(() => builder.Build());

        // Assert
        var ex = Assert.IsType<InvalidOperationException>(exception);
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
