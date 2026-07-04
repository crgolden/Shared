namespace Shared.Tests;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class ChurchAttributeTests
{
    [Fact]
    public void Build_AllValidInput_ReturnsChurchAttribute()
    {
        var attribute = Build();

        Assert.Equal("denomination", attribute.Key);
        Assert.Equal("Baptist", attribute.Value);
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
        var ex = Assert.Throws<ArgumentException>(() => new ChurchAttributeBuilder().WithValue(" "));
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
        var builder = new ChurchAttributeBuilder()
            .WithId(Guid.NewGuid())
            .WithChurchId(Guid.NewGuid())
            .WithKey("denomination")
            .WithValue("Baptist")
            .WithConfidence(0.6m)
            .WithCreatedAt(DateTime.UtcNow)
            .WithUpdatedAt(DateTime.UtcNow);

        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build());
        Assert.Contains("WithSource", ex.Message, StringComparison.Ordinal);
    }

    private static ChurchAttribute Build(
        Guid? id = null,
        Guid? churchId = null,
        string key = "denomination",
        string value = "Baptist",
        string source = "enrichment",
        decimal confidence = 0.6m,
        DateTime? createdAt = null,
        DateTime? updatedAt = null)
    {
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return new ChurchAttributeBuilder()
            .WithId(id ?? Guid.NewGuid())
            .WithChurchId(churchId ?? Guid.NewGuid())
            .WithKey(key)
            .WithValue(value)
            .WithSource(source)
            .WithConfidence(confidence)
            .WithCreatedAt(createdAt ?? now)
            .WithUpdatedAt(updatedAt ?? now)
            .Build();
    }
}
