namespace Shared.Tests;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class ChurchTests
{
    [Fact]
    public void Create_AllValidInput_ReturnsChurch()
    {
        var church = ValidChurch();

        Assert.Equal("Grace Church", church.CanonicalName);
        Assert.Equal("Phoenix", church.City);
        Assert.Equal("AZ", church.State);
        Assert.True(church.IsActive);
    }

    [Fact]
    public void Create_EmptyId_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(id: Guid.Empty));
        Assert.Equal("id", ex.ParamName);
    }

    [Fact]
    public void Create_BlankCanonicalName_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(canonicalName: "  "));
        Assert.Equal("canonicalName", ex.ParamName);
    }

    [Fact]
    public void Create_BlankSlug_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(slug: string.Empty));
        Assert.Equal("slug", ex.ParamName);
    }

    [Fact]
    public void Create_NullCity_Throws()
    {
        // The exact bug this domain model exists to prevent: a null City can never reach SQL.
        var ex = Assert.Throws<ArgumentException>(() => Build(city: null!));
        Assert.Equal("city", ex.ParamName);
    }

    [Fact]
    public void Create_BlankCity_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(city: "   "));
        Assert.Equal("city", ex.ParamName);
    }

    [Fact]
    public void Create_NullState_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(state: null!));
        Assert.Equal("state", ex.ParamName);
    }

    [Fact]
    public void Create_StateWrongLength_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(state: "Arizona"));
        Assert.Equal("state", ex.ParamName);
    }

    [Fact]
    public void Create_BlankZip_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(zip: string.Empty));
        Assert.Equal("zip", ex.ParamName);
    }

    [Fact]
    public void Create_BlankPrimaryLanguage_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(primaryLanguage: string.Empty));
        Assert.Equal("primaryLanguage", ex.ParamName);
    }

    [Theory]
    [InlineData(-91.0)]
    [InlineData(91.0)]
    public void Create_LatitudeOutOfRange_Throws(double latitude)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Build(latitude: latitude));
        Assert.Equal("latitude", ex.ParamName);
    }

    [Theory]
    [InlineData(-181.0)]
    [InlineData(181.0)]
    public void Create_LongitudeOutOfRange_Throws(double longitude)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Build(longitude: longitude));
        Assert.Equal("longitude", ex.ParamName);
    }

    [Fact]
    public void Create_ZeroZeroCoordinates_IsAllowed()
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
    public void Create_WorshipStyleOutOfRange_Throws(int worshipStyle)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Build(worshipStyle: worshipStyle));
        Assert.Equal("worshipStyle", ex.ParamName);
    }

    [Theory]
    [InlineData(-0.0001)]
    [InlineData(1.0001)]
    public void Create_ConfidenceScoreOutOfRange_Throws(decimal confidenceScore)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Build(confidenceScore: confidenceScore));
        Assert.Equal("confidenceScore", ex.ParamName);
    }

    [Fact]
    public void Create_DefaultCreatedAt_Throws()
    {
        // Build()'s DateTime? "unset" sentinel is itself null, so default(DateTime) (a real, non-null
        // value) can only be exercised by calling Create directly rather than through the helper.
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var ex = Assert.Throws<ArgumentException>(() => Church.Create(
            Guid.NewGuid(),
            "Grace Church",
            "grace-church",
            0,
            0,
            null,
            "Phoenix",
            "AZ",
            "85001",
            null,
            null,
            null,
            null,
            0,
            "English",
            null,
            null,
            null,
            null,
            0.5m,
            null,
            default,
            now));
        Assert.Equal("createdAt", ex.ParamName);
    }

    [Fact]
    public void Create_DefaultUpdatedAt_Throws()
    {
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var ex = Assert.Throws<ArgumentException>(() => Church.Create(
            Guid.NewGuid(),
            "Grace Church",
            "grace-church",
            0,
            0,
            null,
            "Phoenix",
            "AZ",
            "85001",
            null,
            null,
            null,
            null,
            0,
            "English",
            null,
            null,
            null,
            null,
            0.5m,
            null,
            now,
            default));
        Assert.Equal("updatedAt", ex.ParamName);
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
        return Church.Create(
            id ?? Guid.NewGuid(),
            canonicalName,
            slug,
            latitude,
            longitude,
            street: "123 Main St",
            city,
            state,
            zip,
            phoneNumber: null,
            website: null,
            emailAddress: null,
            denominationId: null,
            worshipStyle,
            primaryLanguage,
            acceptsLgbtq: null,
            wheelchairAccessible: null,
            hasNursery: null,
            hasYouthProgram: null,
            confidenceScore,
            lastVerifiedAt: null,
            createdAt ?? now,
            updatedAt ?? now);
    }
}
