namespace Shared.Tests;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class ChurchTests
{
    [Fact]
    public void Build_AllValidInput_ReturnsChurch()
    {
        var church = ValidChurch();

        Assert.Equal("Grace Church", church.CanonicalName);
        Assert.Equal("Phoenix", church.City);
        Assert.Equal("AZ", church.State);
        Assert.True(church.IsActive);
    }

    [Fact]
    public void WithId_Empty_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithId(Guid.Empty));
        Assert.Equal("id", ex.ParamName);
    }

    [Fact]
    public void WithCanonicalName_Blank_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithCanonicalName("  "));
        Assert.Equal("canonicalName", ex.ParamName);
    }

    [Fact]
    public void WithSlug_Blank_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithSlug(string.Empty));
        Assert.Equal("slug", ex.ParamName);
    }

    [Fact]
    public void WithCity_Null_Throws()
    {
        // The exact bug this domain model exists to prevent: a null City can never reach SQL.
        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithCity(null!));
        Assert.Equal("city", ex.ParamName);
    }

    [Fact]
    public void WithCity_Blank_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithCity("   "));
        Assert.Equal("city", ex.ParamName);
    }

    [Fact]
    public void WithState_Null_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithState(null!));
        Assert.Equal("state", ex.ParamName);
    }

    [Fact]
    public void WithState_WrongLength_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithState("Arizona"));
        Assert.Equal("state", ex.ParamName);
    }

    [Fact]
    public void WithZip_Blank_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithZip(string.Empty));
        Assert.Equal("zip", ex.ParamName);
    }

    [Fact]
    public void WithPrimaryLanguage_Blank_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithPrimaryLanguage(string.Empty));
        Assert.Equal("primaryLanguage", ex.ParamName);
    }

    [Theory]
    [InlineData(-91.0)]
    [InlineData(91.0)]
    public void WithLatitude_OutOfRange_Throws(double latitude)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new ChurchBuilder().WithLatitude(latitude));
        Assert.Equal("latitude", ex.ParamName);
    }

    [Theory]
    [InlineData(-181.0)]
    [InlineData(181.0)]
    public void WithLongitude_OutOfRange_Throws(double longitude)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new ChurchBuilder().WithLongitude(longitude));
        Assert.Equal("longitude", ex.ParamName);
    }

    [Fact]
    public void Build_ZeroZeroCoordinates_IsAllowed()
    {
        // (0,0) is a deliberate "not yet geocoded" fallback used elsewhere in the pipeline, not an
        // error — the domain model must not reject it, only genuinely out-of-range values.
        var church = Build(latitude: 0, longitude: 0);

        Assert.Equal(0, church.Latitude);
        Assert.Equal(0, church.Longitude);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    public void WithWorshipStyle_OutOfRange_Throws(int worshipStyle)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new ChurchBuilder().WithWorshipStyle(worshipStyle));
        Assert.Equal("worshipStyle", ex.ParamName);
    }

    [Theory]
    [InlineData(-0.0001)]
    [InlineData(1.0001)]
    public void WithConfidenceScore_OutOfRange_Throws(decimal confidenceScore)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new ChurchBuilder().WithConfidenceScore(confidenceScore));
        Assert.Equal("confidenceScore", ex.ParamName);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithCreatedAt(default));
        Assert.Equal("createdAt", ex.ParamName);
    }

    [Fact]
    public void WithUpdatedAt_Default_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithUpdatedAt(default));
        Assert.Equal("updatedAt", ex.ParamName);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        // Build() itself only checks presence — each field's own validity was already enforced the
        // instant its With* method was called, so this covers the "forgot to call WithCity" case.
        var builder = new ChurchBuilder()
            .WithId(Guid.NewGuid())
            .WithCanonicalName("Grace Church")
            .WithSlug("grace-church")
            .WithLatitude(0)
            .WithLongitude(0)
            .WithState("AZ")
            .WithZip("85001")
            .WithWorshipStyle(0)
            .WithPrimaryLanguage("English")
            .WithConfidenceScore(0.5m)
            .WithCreatedAt(DateTime.UtcNow)
            .WithUpdatedAt(DateTime.UtcNow);

        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build());
        Assert.Contains("WithCity", ex.Message, StringComparison.Ordinal);
    }

    private static Church ValidChurch() => Build();

    private static Church Build(
        Guid? id = null,
        string canonicalName = "Grace Church",
        string slug = "grace-church-phoenix-az",
        double latitude = 33.4,
        double longitude = -112.0,
        string city = "Phoenix",
        string state = "AZ",
        string zip = "85001",
        int worshipStyle = 0,
        string primaryLanguage = "English",
        decimal confidenceScore = 0.5m,
        DateTime? createdAt = null,
        DateTime? updatedAt = null)
    {
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return new ChurchBuilder()
            .WithId(id ?? Guid.NewGuid())
            .WithCanonicalName(canonicalName)
            .WithSlug(slug)
            .WithLatitude(latitude)
            .WithLongitude(longitude)
            .WithStreet("123 Main St")
            .WithCity(city)
            .WithState(state)
            .WithZip(zip)
            .WithWorshipStyle(worshipStyle)
            .WithPrimaryLanguage(primaryLanguage)
            .WithConfidenceScore(confidenceScore)
            .WithCreatedAt(createdAt ?? now)
            .WithUpdatedAt(updatedAt ?? now)
            .Build();
    }
}
