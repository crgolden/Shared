namespace Shared.Tests.Unit;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class ChurchAttributeTests
{
    [Fact]
    public void Build_AllValidInput_ReturnsChurchAttribute()
    {
        var attributeKey = TestValues.LowercaseToken(11);
        var attributeValue = TestValues.NewName();

        var attribute = Build(key: attributeKey, value: attributeValue);

        Assert.Equal(attributeKey, attribute.Key);
        Assert.Equal(attributeValue, attribute.Value);
    }

    [Fact]
    public void WithId_Empty_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithId(Guid.Empty));
        Assert.Equal("id", ex.ParamName);
    }

    [Fact]
    public void WithChurchId_Empty_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithChurchId(Guid.Empty));
        Assert.Equal("churchId", ex.ParamName);
    }

    [Fact]
    public void WithKey_Blank_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithKey(string.Empty));
        Assert.Equal("key", ex.ParamName);
    }

    [Fact]
    public void WithValue_Blank_Throws()
    {
        var blankValue = new string(' ', Random.Shared.Next(1, 4));

        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithValue(blankValue));
        Assert.Equal("value", ex.ParamName);
    }

    [Fact]
    public void WithSource_Blank_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithSource(string.Empty));
        Assert.Equal("source", ex.ParamName);
    }

    [Theory]
    [InlineData(-0.0001)]
    [InlineData(1.0001)]
    public void WithConfidence_OutOfRange_Throws(decimal confidence)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new ChurchAttributeBuilder().WithConfidence(confidence));
        Assert.Equal("confidence", ex.ParamName);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithCreatedAt(default));
        Assert.Equal("createdAt", ex.ParamName);
    }

    [Fact]
    public void WithUpdatedAt_Default_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithUpdatedAt(default));
        Assert.Equal("updatedAt", ex.ParamName);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        var attributeId = Guid.NewGuid();
        var churchId = Guid.NewGuid();
        var attributeKey = TestValues.LowercaseToken(11);
        var attributeValue = TestValues.NewName();
        var confidence = TestValues.NewConfidenceScore();
        var createdAt = TestValues.NewUtcTimestamp();
        var updatedAt = TestValues.NewUtcTimestamp();
        var builder = new ChurchAttributeBuilder()
            .WithId(attributeId)
            .WithChurchId(churchId)
            .WithKey(attributeKey)
            .WithValue(attributeValue)
            .WithConfidence(confidence)
            .WithCreatedAt(createdAt)
            .WithUpdatedAt(updatedAt);

        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build());
        Assert.Contains("WithSource", ex.Message, StringComparison.Ordinal);
    }

    private static ChurchAttribute Build(
        Guid? id = null,
        Guid? churchId = null,
        string? key = null,
        string? value = null,
        string? source = null,
        decimal? confidence = null,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null) =>
        new ChurchAttributeBuilder()
            .WithId(id ?? Guid.NewGuid())
            .WithChurchId(churchId ?? Guid.NewGuid())
            .WithKey(key ?? TestValues.LowercaseToken(11))
            .WithValue(value ?? TestValues.NewName())
            .WithSource(source ?? TestValues.LowercaseToken(10))
            .WithConfidence(confidence ?? TestValues.NewConfidenceScore())
            .WithCreatedAt(createdAt ?? TestValues.NewUtcTimestamp())
            .WithUpdatedAt(updatedAt ?? TestValues.NewUtcTimestamp())
            .Build();
}
