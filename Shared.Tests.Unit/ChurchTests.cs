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
        // Arrange
        var canonicalName = TestValues.NewName();
        var city = TestValues.NewCity();
        var state = TestValues.NewStateCode();

        // Act
        var church = Build(canonicalName: canonicalName, city: city, state: state);

        // Assert
        Assert.Equal(canonicalName, church.CanonicalName);
        Assert.Equal(city, church.City);
        Assert.Equal(state, church.State);
        Assert.True(church.IsActive);
    }

    [Fact]
    public void WithId_Empty_Throws()
    {
        // Arrange
        var id = Guid.Empty;

        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithId(id));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(id), ex.ParamName);
    }

    [Fact]
    public void WithCanonicalName_Blank_Throws()
    {
        var canonicalName = TestValues.NewBlank();

        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithCanonicalName(canonicalName));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(canonicalName), ex.ParamName);
    }

    [Fact]
    public void WithSlug_Blank_Throws()
    {
        var slug = TestValues.NewBlank();

        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithSlug(slug));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(slug), ex.ParamName);
    }

    [Fact]
    public void WithCity_Null_Throws()
    {
        string? city = null;

        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithCity(city));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(city), ex.ParamName);
    }

    [Fact]
    public void WithCity_Blank_Throws()
    {
        var city = TestValues.NewBlank();

        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithCity(city));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(city), ex.ParamName);
    }

    [Fact]
    public void WithState_UndefinedCode_Throws()
    {
        var state = TestValues.NewUndefinedStateCode();

        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithState(state));

        // Assert
        var ex = Assert.IsType<ArgumentOutOfRangeException>(exception);
        Assert.Equal(nameof(state), ex.ParamName);
    }

    [Fact]
    public void WithZip_Blank_Throws()
    {
        var zip = TestValues.NewBlank();

        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithZip(zip));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(zip), ex.ParamName);
    }

    [Fact]
    public void WithPrimaryLanguage_Blank_Throws()
    {
        var primaryLanguage = TestValues.NewBlank();

        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithPrimaryLanguage(primaryLanguage));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(primaryLanguage), ex.ParamName);
    }

    [Theory]
    [InlineData(ChurchBuilder.MinLatitude - OutOfRangeCoordinateOffset)]
    [InlineData(ChurchBuilder.MaxLatitude + OutOfRangeCoordinateOffset)]
    public void WithLatitude_OutOfRange_Throws(double latitude)
    {
        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithLatitude(latitude));

        // Assert
        var ex = Assert.IsType<ArgumentOutOfRangeException>(exception);
        Assert.Equal(nameof(latitude), ex.ParamName);
    }

    [Theory]
    [InlineData(ChurchBuilder.MinLongitude - OutOfRangeCoordinateOffset)]
    [InlineData(ChurchBuilder.MaxLongitude + OutOfRangeCoordinateOffset)]
    public void WithLongitude_OutOfRange_Throws(double longitude)
    {
        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithLongitude(longitude));

        // Assert
        var ex = Assert.IsType<ArgumentOutOfRangeException>(exception);
        Assert.Equal(nameof(longitude), ex.ParamName);
    }

    [Fact]
    public void Build_ZeroZeroCoordinates_IsAllowed()
    {
        // Act
        var church = Build(latitude: 0, longitude: 0);

        // Assert
        Assert.Equal(0, church.Latitude);
        Assert.Equal(0, church.Longitude);
    }

    [Theory]
    [InlineData(ChurchBuilder.MinWorshipStyle - OutOfRangeWorshipStyleOffset)]
    [InlineData(ChurchBuilder.MaxWorshipStyle + OutOfRangeWorshipStyleOffset)]
    public void WithWorshipStyle_OutOfRange_Throws(int worshipStyle)
    {
        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithWorshipStyle(worshipStyle));

        // Assert
        var ex = Assert.IsType<ArgumentOutOfRangeException>(exception);
        Assert.Equal(nameof(worshipStyle), ex.ParamName);
    }

    [Theory]
    [MemberData(nameof(OutOfRangeConfidenceScores))]
    public void WithConfidenceScore_OutOfRange_Throws(decimal confidenceScore)
    {
        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithConfidenceScore(confidenceScore));

        // Assert
        var ex = Assert.IsType<ArgumentOutOfRangeException>(exception);
        Assert.Equal(nameof(confidenceScore), ex.ParamName);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        var createdAt = default(DateTimeOffset);

        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithCreatedAt(createdAt));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(createdAt), ex.ParamName);
    }

    [Fact]
    public void WithUpdatedAt_Default_Throws()
    {
        var updatedAt = default(DateTimeOffset);

        // Act
        var exception = Record.Exception(() => new ChurchBuilder().WithUpdatedAt(updatedAt));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(updatedAt), ex.ParamName);
    }

    [Fact]
    public void Build_CreatedAtCarriesNonZeroOffset_PreservesTheInstant()
    {
        // Arrange
        var createdAtInSourceOffset = TestValues.NewTimestampWithNonZeroOffset();

        // Act
        var church = Build(createdAt: createdAtInSourceOffset);

        // Assert
        Assert.Equal(createdAtInSourceOffset.UtcDateTime, church.CreatedAt.UtcDateTime);
    }

    [Fact]
    public void Build_CreatedAtCarriesNonZeroOffset_PreservesTheOffset()
    {
        // Arrange
        var createdAtInSourceOffset = TestValues.NewTimestampWithNonZeroOffset();

        // Act
        var church = Build(createdAt: createdAtInSourceOffset);

        // Assert
        Assert.Equal(createdAtInSourceOffset.Offset, church.CreatedAt.Offset);
    }

    [Fact]
    public void Build_LastVerifiedAtCarriesNonZeroOffset_PreservesTheInstant()
    {
        // Arrange
        var lastVerifiedAtInSourceOffset = TestValues.NewTimestampWithNonZeroOffset();
        var churchId = Guid.NewGuid();

        // Act
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

        // Assert
        Assert.Equal(lastVerifiedAtInSourceOffset.UtcDateTime, church.LastVerifiedAt?.UtcDateTime);
    }

    [Fact]
    public void Build_UtcTimestamp_CarriesZeroOffset()
    {
        // Arrange
        var createdAtInUtc = TestValues.NewUtcTimestamp();

        // Act
        var church = Build(createdAt: createdAtInUtc);

        // Assert
        Assert.Equal(TimeSpan.Zero, church.CreatedAt.Offset);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        // Arrange
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

        // Act
        var exception = Record.Exception(() => builder.Build());

        // Assert
        var ex = Assert.IsType<InvalidOperationException>(exception);
        Assert.Contains(nameof(ChurchBuilder.WithCity), ex.Message, StringComparison.Ordinal);
    }

    private static Church Build(
        Guid? id = null,
        string? canonicalName = null,
        string? slug = null,
        double? latitude = null,
        double? longitude = null,
        string? city = null,
        StateCode? state = null,
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
