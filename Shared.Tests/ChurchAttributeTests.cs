namespace Shared.Tests;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class ChurchAttributeTests
{
    [Fact]
    public void Create_AllValidInput_ReturnsChurchAttribute()
    {
        var attribute = Build();

        Assert.Equal("denomination", attribute.Key);
        Assert.Equal("Baptist", attribute.Value);
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
    public void Create_BlankKey_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(key: string.Empty));
        Assert.Equal("key", ex.ParamName);
    }

    [Fact]
    public void Create_BlankValue_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(value: " "));
        Assert.Equal("value", ex.ParamName);
    }

    [Fact]
    public void Create_BlankSource_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(source: string.Empty));
        Assert.Equal("source", ex.ParamName);
    }

    [Theory]
    [InlineData(-0.0001)]
    [InlineData(1.0001)]
    public void Create_ConfidenceOutOfRange_Throws(decimal confidence)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Build(confidence: confidence));
        Assert.Equal("confidence", ex.ParamName);
    }

    [Fact]
    public void Create_DefaultCreatedAt_Throws()
    {
        // Build()'s DateTime? "unset" sentinel is itself null, so default(DateTime) (a real, non-null
        // value) can only be exercised by calling Create directly rather than through the helper.
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var ex = Assert.Throws<ArgumentException>(() => ChurchAttribute.Create(
            Guid.NewGuid(), Guid.NewGuid(), "denomination", "Baptist", "enrichment", 0.6m, default, now));
        Assert.Equal("createdAt", ex.ParamName);
    }

    [Fact]
    public void Create_DefaultUpdatedAt_Throws()
    {
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var ex = Assert.Throws<ArgumentException>(() => ChurchAttribute.Create(
            Guid.NewGuid(), Guid.NewGuid(), "denomination", "Baptist", "enrichment", 0.6m, now, default));
        Assert.Equal("updatedAt", ex.ParamName);
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
        return ChurchAttribute.Create(id ?? Guid.NewGuid(), churchId ?? Guid.NewGuid(), key, value, source, confidence, createdAt ?? now, updatedAt ?? now);
    }
}
