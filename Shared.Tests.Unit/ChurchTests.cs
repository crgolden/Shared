namespace Shared.Tests.Unit;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class ChurchTests
{
    [Fact]
    public void Build_AllValidInput_ReturnsChurch()
    {
        var canonicalName = TestValues.NewName();
        var city = TestValues.NewCity();
        var state = TestValues.NewStateCode();

        var church = Build(canonicalName: canonicalName, city: city, state: state);

        Assert.Equal(canonicalName, church.CanonicalName);
        Assert.Equal(city, church.City);
        Assert.Equal(state, church.State);
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
        var blankCanonicalName = new string(' ', Random.Shared.Next(1, 4));

        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithCanonicalName(blankCanonicalName));
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
        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithCity(null!));
        Assert.Equal("city", ex.ParamName);
    }

    [Fact]
    public void WithCity_Blank_Throws()
    {
        var blankCity = new string(' ', Random.Shared.Next(1, 4));

        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithCity(blankCity));
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
        var wrongLengthStateCode = TestValues.LowercaseToken(Random.Shared.Next(3, 10));

        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithState(wrongLengthStateCode));
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
    public void Build_CreatedAtCarriesNonZeroOffset_PreservesTheInstant()
    {
        var createdAtInSourceOffset = TestValues.NewTimestampWithNonZeroOffset();

        var church = Build(createdAt: createdAtInSourceOffset);

        Assert.Equal(createdAtInSourceOffset.UtcDateTime, church.CreatedAt.UtcDateTime);
    }

    [Fact]
    public void Build_CreatedAtCarriesNonZeroOffset_PreservesTheOffset()
    {
        var createdAtInSourceOffset = TestValues.NewTimestampWithNonZeroOffset();

        var church = Build(createdAt: createdAtInSourceOffset);

        Assert.Equal(createdAtInSourceOffset.Offset, church.CreatedAt.Offset);
    }

    [Fact]
    public void Build_LastVerifiedAtCarriesNonZeroOffset_PreservesTheInstant()
    {
        var lastVerifiedAtInSourceOffset = TestValues.NewTimestampWithNonZeroOffset();

        var church = new ChurchBuilder()
            .WithId(Guid.NewGuid())
            .WithCanonicalName(TestValues.NewName())
            .WithSlug(TestValues.NewSlug())
            .WithLatitude(TestValues.NewLatitude())
            .WithLongitude(TestValues.NewLongitude())
            .WithCity(TestValues.NewCity())
            .WithState(TestValues.NewStateCode())
            .WithZip(TestValues.NewZip())
            .WithWorshipStyle(TestValues.NewWorshipStyleCode())
            .WithPrimaryLanguage(TestValues.NewLanguage())
            .WithConfidenceScore(TestValues.NewConfidenceScore())
            .WithLastVerifiedAt(lastVerifiedAtInSourceOffset)
            .WithCreatedAt(TestValues.NewUtcTimestamp())
            .WithUpdatedAt(TestValues.NewUtcTimestamp())
            .Build();

        Assert.Equal(lastVerifiedAtInSourceOffset.UtcDateTime, church.LastVerifiedAt?.UtcDateTime);
    }

    [Fact]
    public void Build_UtcTimestamp_CarriesZeroOffset()
    {
        var createdAtInUtc = TestValues.NewUtcTimestamp();

        var church = Build(createdAt: createdAtInUtc);

        Assert.Equal(TimeSpan.Zero, church.CreatedAt.Offset);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        var churchId = Guid.NewGuid();
        var canonicalName = TestValues.NewName();
        var slug = TestValues.NewSlug();
        var latitude = TestValues.NewLatitude();
        var longitude = TestValues.NewLongitude();
        var state = TestValues.NewStateCode();
        var zip = TestValues.NewZip();
        var worshipStyle = TestValues.NewWorshipStyleCode();
        var primaryLanguage = TestValues.NewLanguage();
        var confidenceScore = TestValues.NewConfidenceScore();
        var createdAt = TestValues.NewUtcTimestamp();
        var updatedAt = TestValues.NewUtcTimestamp();
        var builder = new ChurchBuilder()
            .WithId(churchId)
            .WithCanonicalName(canonicalName)
            .WithSlug(slug)
            .WithLatitude(latitude)
            .WithLongitude(longitude)
            .WithState(state)
            .WithZip(zip)
            .WithWorshipStyle(worshipStyle)
            .WithPrimaryLanguage(primaryLanguage)
            .WithConfidenceScore(confidenceScore)
            .WithCreatedAt(createdAt)
            .WithUpdatedAt(updatedAt);

        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build());
        Assert.Contains("WithCity", ex.Message, StringComparison.Ordinal);
    }

    private static Church Build(
        Guid? id = null,
        string? canonicalName = null,
        string? slug = null,
        double? latitude = null,
        double? longitude = null,
        string? city = null,
        string? state = null,
        string? zip = null,
        int? worshipStyle = null,
        string? primaryLanguage = null,
        decimal? confidenceScore = null,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null) =>
        new ChurchBuilder()
            .WithId(id ?? Guid.NewGuid())
            .WithCanonicalName(canonicalName ?? TestValues.NewName())
            .WithSlug(slug ?? TestValues.NewSlug())
            .WithLatitude(latitude ?? TestValues.NewLatitude())
            .WithLongitude(longitude ?? TestValues.NewLongitude())
            .WithStreet(TestValues.NewStreet())
            .WithCity(city ?? TestValues.NewCity())
            .WithState(state ?? TestValues.NewStateCode())
            .WithZip(zip ?? TestValues.NewZip())
            .WithWorshipStyle(worshipStyle ?? TestValues.NewWorshipStyleCode())
            .WithPrimaryLanguage(primaryLanguage ?? TestValues.NewLanguage())
            .WithConfidenceScore(confidenceScore ?? TestValues.NewConfidenceScore())
            .WithCreatedAt(createdAt ?? TestValues.NewUtcTimestamp())
            .WithUpdatedAt(updatedAt ?? TestValues.NewUtcTimestamp())
            .Build();
}
