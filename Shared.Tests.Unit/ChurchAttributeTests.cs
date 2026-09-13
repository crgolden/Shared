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
        var attributeKey = TestValues.NewAttributeKey();
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
        var churchId = Guid.Empty;

        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithChurchId(churchId));
        Assert.Equal(nameof(churchId), ex.ParamName);
    }

    [Fact]
    public void WithKey_Blank_Throws()
    {
        var key = TestValues.NewBlank();

        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithKey(key));
        Assert.Equal(nameof(key), ex.ParamName);
    }

    [Fact]
    public void WithValue_Blank_Throws()
    {
        var blankValue = TestValues.NewBlank();

        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithValue(blankValue));
        Assert.Equal("value", ex.ParamName);
    }

    [Fact]
    public void WithSource_Blank_Throws()
    {
        var source = TestValues.NewBlank();

        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithSource(source));
        Assert.Equal(nameof(source), ex.ParamName);
    }

    [Theory]
    [MemberData(nameof(OutOfRangeConfidences))]
    public void WithConfidence_OutOfRange_Throws(decimal confidence)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new ChurchAttributeBuilder().WithConfidence(confidence));
        Assert.Equal(nameof(confidence), ex.ParamName);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        var createdAt = default(DateTimeOffset);

        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithCreatedAt(createdAt));
        Assert.Equal(nameof(createdAt), ex.ParamName);
    }

    [Fact]
    public void WithUpdatedAt_Default_Throws()
    {
        var updatedAt = default(DateTimeOffset);

        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithUpdatedAt(updatedAt));
        Assert.Equal(nameof(updatedAt), ex.ParamName);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        var attributeId = Guid.NewGuid();
        var churchId = Guid.NewGuid();
        var attributeKey = TestValues.NewAttributeKey();
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
            .WithValue(value ?? TestValues.NewName())
            .WithSource(source ?? TestValues.NewAttributeSource())
            .WithConfidence(confidence ?? TestValues.NewConfidenceScore())
            .WithCreatedAt(createdAt ?? TestValues.NewUtcTimestamp())
            .WithUpdatedAt(updatedAt ?? TestValues.NewUtcTimestamp())
            .Build();
    }
}
