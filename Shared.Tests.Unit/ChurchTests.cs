namespace Shared.Tests.Unit;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class ChurchTests
{
    private const double OutOfRangeCoordinateOffset = 1;

    private const int OutOfRangeWorshipStyleOffset = 1;

    public static TheoryData<decimal> OutOfRangeConfidenceScores() => new TheoryData<decimal>
    {
        ChurchBuilder.MinConfidenceScore - TestValues.NewOutOfRangeOffset(),
        ChurchBuilder.MaxConfidenceScore + TestValues.NewOutOfRangeOffset(),
    };

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
        var canonicalName = TestValues.NewBlank();

        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithCanonicalName(canonicalName));
        Assert.Equal(nameof(canonicalName), ex.ParamName);
    }

    [Fact]
    public void WithSlug_Blank_Throws()
    {
        var slug = TestValues.NewBlank();

        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithSlug(slug));
        Assert.Equal(nameof(slug), ex.ParamName);
    }

    [Fact]
    public void WithCity_Null_Throws()
    {
        string? city = null;

        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithCity(city));
        Assert.Equal(nameof(city), ex.ParamName);
    }

    [Fact]
    public void WithCity_Blank_Throws()
    {
        var city = TestValues.NewBlank();

        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithCity(city));
        Assert.Equal(nameof(city), ex.ParamName);
    }

    [Fact]
    public void WithState_Null_Throws()
    {
        string? state = null;

        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithState(state));
        Assert.Equal(nameof(state), ex.ParamName);
    }

    [Fact]
    public void WithState_WrongLength_Throws()
    {
        var state = TestValues.NewWrongLengthStateCode();

        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithState(state));
        Assert.Equal(nameof(state), ex.ParamName);
    }

    [Fact]
    public void WithZip_Blank_Throws()
    {
        var zip = TestValues.NewBlank();

        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithZip(zip));
        Assert.Equal(nameof(zip), ex.ParamName);
    }

    [Fact]
    public void WithPrimaryLanguage_Blank_Throws()
    {
        var primaryLanguage = TestValues.NewBlank();

        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithPrimaryLanguage(primaryLanguage));
        Assert.Equal(nameof(primaryLanguage), ex.ParamName);
    }

    [Theory]
    [InlineData(ChurchBuilder.MinLatitude - OutOfRangeCoordinateOffset)]
    [InlineData(ChurchBuilder.MaxLatitude + OutOfRangeCoordinateOffset)]
    public void WithLatitude_OutOfRange_Throws(double latitude)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new ChurchBuilder().WithLatitude(latitude));
        Assert.Equal(nameof(latitude), ex.ParamName);
    }

    [Theory]
    [InlineData(ChurchBuilder.MinLongitude - OutOfRangeCoordinateOffset)]
    [InlineData(ChurchBuilder.MaxLongitude + OutOfRangeCoordinateOffset)]
    public void WithLongitude_OutOfRange_Throws(double longitude)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new ChurchBuilder().WithLongitude(longitude));
        Assert.Equal(nameof(longitude), ex.ParamName);
    }

    [Fact]
    public void Build_ZeroZeroCoordinates_IsAllowed()
    {
        var church = Build(latitude: 0, longitude: 0);

        Assert.Equal(0, church.Latitude);
        Assert.Equal(0, church.Longitude);
    }

    [Theory]
    [InlineData(ChurchBuilder.MinWorshipStyle - OutOfRangeWorshipStyleOffset)]
    [InlineData(ChurchBuilder.MaxWorshipStyle + OutOfRangeWorshipStyleOffset)]
    public void WithWorshipStyle_OutOfRange_Throws(int worshipStyle)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new ChurchBuilder().WithWorshipStyle(worshipStyle));
        Assert.Equal(nameof(worshipStyle), ex.ParamName);
    }

    [Theory]
    [MemberData(nameof(OutOfRangeConfidenceScores))]
    public void WithConfidenceScore_OutOfRange_Throws(decimal confidenceScore)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new ChurchBuilder().WithConfidenceScore(confidenceScore));
        Assert.Equal(nameof(confidenceScore), ex.ParamName);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        var createdAt = default(DateTimeOffset);

        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithCreatedAt(createdAt));
        Assert.Equal(nameof(createdAt), ex.ParamName);
    }

    [Fact]
    public void WithUpdatedAt_Default_Throws()
    {
        var updatedAt = default(DateTimeOffset);

        var ex = Assert.Throws<ArgumentException>(() => new ChurchBuilder().WithUpdatedAt(updatedAt));
        Assert.Equal(nameof(updatedAt), ex.ParamName);
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
        var churchId = Guid.NewGuid();

        var church = new ChurchBuilder()
            .WithId(churchId)
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
        Assert.Contains(nameof(ChurchBuilder.WithCity), ex.Message, StringComparison.Ordinal);
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
        DateTimeOffset? updatedAt = null)
    {
        var generatedChurchId = Guid.NewGuid();

        return new ChurchBuilder()
            .WithId(id ?? generatedChurchId)
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
}
