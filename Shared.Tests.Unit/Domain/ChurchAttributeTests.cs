namespace Shared.Tests.Unit.Domain;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class ChurchAttributeTests
{
    [Fact]
    public void Build_AllValidInput_ReturnsChurchAttribute()
    {
        // Arrange
        var attributeKey = Generated.NewAttributeKey();
        var attributeValue = Generated.NewName();

        // Act
        var attribute = PopulatedBuilder().WithKey(attributeKey).WithValue(attributeValue).Build();

        // Assert
        Assert.Equal(attributeKey, attribute.Key);
        Assert.Equal(attributeValue, attribute.Value);
    }

    [Fact]
    public void WithId_Empty_Throws()
    {
        // Arrange
        var id = Guid.Empty;

        // Act
        var exception = Record.Exception(() => new ChurchAttributeBuilder().WithId(id));

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
        var exception = Record.Exception(() => new ChurchAttributeBuilder().WithChurchId(churchId));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(churchId), ex.ParamName);
    }

    [Fact]
    public void WithKey_Blank_Throws()
    {
        // Arrange
        var key = Generated.NewBlank();

        // Act
        var exception = Record.Exception(() => new ChurchAttributeBuilder().WithKey(key));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(key), ex.ParamName);
    }

    [Fact]
    public void WithValue_Blank_Throws()
    {
        // Arrange
        var value = Generated.NewBlank();

        // Act
        var exception = Record.Exception(() => new ChurchAttributeBuilder().WithValue(value));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(value), ex.ParamName);
    }

    [Fact]
    public void WithSource_Blank_Throws()
    {
        // Arrange
        var source = Generated.NewBlank();

        // Act
        var exception = Record.Exception(() => new ChurchAttributeBuilder().WithSource(source));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(source), ex.ParamName);
    }

    [Fact]
    public void WithConfidence_BelowMinimum_Throws()
    {
        // Arrange
        var confidence = ChurchAttributeBuilder.MinConfidence - Generated.NewOutOfRangeOffset();

        // Act
        var exception = Record.Exception(() => new ChurchAttributeBuilder().WithConfidence(confidence));

        // Assert
        var ex = Assert.IsType<ArgumentOutOfRangeException>(exception);
        Assert.Equal(nameof(confidence), ex.ParamName);
    }

    [Fact]
    public void WithConfidence_AboveMaximum_Throws()
    {
        // Arrange
        var confidence = ChurchAttributeBuilder.MaxConfidence + Generated.NewOutOfRangeOffset();

        // Act
        var exception = Record.Exception(() => new ChurchAttributeBuilder().WithConfidence(confidence));

        // Assert
        var ex = Assert.IsType<ArgumentOutOfRangeException>(exception);
        Assert.Equal(nameof(confidence), ex.ParamName);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        // Arrange
        var createdAt = default(DateTimeOffset);

        // Act
        var exception = Record.Exception(() => new ChurchAttributeBuilder().WithCreatedAt(createdAt));

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
        var exception = Record.Exception(() => new ChurchAttributeBuilder().WithUpdatedAt(updatedAt));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(updatedAt), ex.ParamName);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        // Arrange
        var attributeId = Guid.NewGuid();
        var churchId = Guid.NewGuid();
        var attributeKey = Generated.NewAttributeKey();
        var attributeValue = Generated.NewName();
        var confidence = Generated.NewRoundedFraction(ChurchAttributeBuilder.ConfidenceScale);
        var createdAt = Generated.NewUtcTimestamp();
        var updatedAt = Generated.NewUtcTimestamp();
        var builder = new ChurchAttributeBuilder()
            .WithId(attributeId)
            .WithChurchId(churchId)
            .WithKey(attributeKey)
            .WithValue(attributeValue)
            .WithConfidence(confidence)
            .WithCreatedAt(createdAt)
            .WithUpdatedAt(updatedAt);

        // Act
        var exception = Record.Exception(() => builder.Build());

        // Assert
        var ex = Assert.IsType<InvalidOperationException>(exception);
        Assert.Contains(nameof(ChurchAttributeBuilder.WithSource), ex.Message, StringComparison.Ordinal);
    }

    private static ChurchAttributeBuilder PopulatedBuilder()
    {
        var generatedAttributeId = Guid.NewGuid();
        var generatedChurchId = Guid.NewGuid();

        return new ChurchAttributeBuilder()
            .WithId(generatedAttributeId)
            .WithChurchId(generatedChurchId)
            .WithKey(Generated.NewAttributeKey())
            .WithValue(Generated.NewName())
            .WithSource(Generated.NewAttributeSource())
            .WithConfidence(Generated.NewRoundedFraction(ChurchAttributeBuilder.ConfidenceScale))
            .WithCreatedAt(Generated.NewUtcTimestamp())
            .WithUpdatedAt(Generated.NewUtcTimestamp());
    }
}
