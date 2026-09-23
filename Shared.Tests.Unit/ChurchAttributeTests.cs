namespace Shared.Tests.Unit;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class ChurchAttributeTests
{
    public static TheoryData<decimal> OutOfRangeConfidences() => new TheoryData<decimal>
    {
        ChurchAttributeBuilder.MinConfidence - TestValues.NewOutOfRangeOffset(),
        ChurchAttributeBuilder.MaxConfidence + TestValues.NewOutOfRangeOffset(),
    };

    [Fact]
    public void Build_AllValidInput_ReturnsChurchAttribute()
    {
        // Arrange
        var attributeKey = TestValues.NewAttributeKey();
        var attributeValue = Generated.NewName();

        // Act
        var attribute = Build(key: attributeKey, value: attributeValue);

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

    [Theory]
    [MemberData(nameof(OutOfRangeConfidences))]
    public void WithConfidence_OutOfRange_Throws(decimal confidence)
    {
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
        var attributeKey = TestValues.NewAttributeKey();
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

    private static ChurchAttribute Build(
        Guid? id = null,
        Guid? churchId = null,
        string? key = null,
        string? value = null,
        string? source = null,
        decimal? confidence = null,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null)
    {
        var generatedAttributeId = Guid.NewGuid();
        var generatedChurchId = Guid.NewGuid();

        return new ChurchAttributeBuilder()
            .WithId(id ?? generatedAttributeId)
            .WithChurchId(churchId ?? generatedChurchId)
            .WithKey(key ?? TestValues.NewAttributeKey())
            .WithValue(value ?? Generated.NewName())
            .WithSource(source ?? TestValues.NewAttributeSource())
            .WithConfidence(confidence ?? Generated.NewRoundedFraction(ChurchAttributeBuilder.ConfidenceScale))
            .WithCreatedAt(createdAt ?? Generated.NewUtcTimestamp())
            .WithUpdatedAt(updatedAt ?? Generated.NewUtcTimestamp())
            .Build();
    }
}
