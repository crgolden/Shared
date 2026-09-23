namespace Shared.Tests.Unit;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class CampusTests
{
    private const double OutOfRangeCoordinateOffset = 1;

    [Fact]
    public void Build_AllValidInput_ReturnsCampus()
    {
        // Arrange
        var campusName = Generated.NewName();
        var campusCity = Generated.NewCity();
        var campusState = TestValues.NewStateCode();

        // Act
        var campus = Build(name: campusName, city: campusCity, state: campusState);

        // Assert
        Assert.Equal(campusName, campus.Name);
        Assert.Equal(campusCity, campus.City);
        Assert.Equal(campusState, campus.State);
    }

    [Fact]
    public void WithId_Empty_Throws()
    {
        // Arrange
        var id = Guid.Empty;

        // Act
        var exception = Record.Exception(() => new CampusBuilder().WithId(id));

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
        var exception = Record.Exception(() => new CampusBuilder().WithChurchId(churchId));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(churchId), ex.ParamName);
    }

    [Fact]
    public void WithName_Blank_Throws()
    {
        // Arrange
        var name = Generated.NewBlank();

        // Act
        var exception = Record.Exception(() => new CampusBuilder().WithName(name));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(name), ex.ParamName);
    }

    [Fact]
    public void WithCity_Null_Throws()
    {
        // Arrange
        string? city = null;

        // Act
        var exception = Record.Exception(() => new CampusBuilder().WithCity(city));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(city), ex.ParamName);
    }

    [Fact]
    public void WithState_UndefinedCode_Throws()
    {
        // Arrange
        var state = TestValues.NewUndefinedStateCode();

        // Act
        var exception = Record.Exception(() => new CampusBuilder().WithState(state));

        // Assert
        var ex = Assert.IsType<ArgumentOutOfRangeException>(exception);
        Assert.Equal(nameof(state), ex.ParamName);
    }

    [Fact]
    public void WithZip_Blank_Throws()
    {
        // Arrange
        var zip = Generated.NewBlank();

        // Act
        var exception = Record.Exception(() => new CampusBuilder().WithZip(zip));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(zip), ex.ParamName);
    }

    [Theory]
    [InlineData(CampusBuilder.MinLatitude - OutOfRangeCoordinateOffset)]
    [InlineData(CampusBuilder.MaxLatitude + OutOfRangeCoordinateOffset)]
    public void WithLatitude_OutOfRange_Throws(double latitude)
    {
        // Act
        var exception = Record.Exception(() => new CampusBuilder().WithLatitude(latitude));

        // Assert
        var ex = Assert.IsType<ArgumentOutOfRangeException>(exception);
        Assert.Equal(nameof(latitude), ex.ParamName);
    }

    [Theory]
    [InlineData(CampusBuilder.MinLongitude - OutOfRangeCoordinateOffset)]
    [InlineData(CampusBuilder.MaxLongitude + OutOfRangeCoordinateOffset)]
    public void WithLongitude_OutOfRange_Throws(double longitude)
    {
        // Act
        var exception = Record.Exception(() => new CampusBuilder().WithLongitude(longitude));

        // Assert
        var ex = Assert.IsType<ArgumentOutOfRangeException>(exception);
        Assert.Equal(nameof(longitude), ex.ParamName);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        // Arrange
        var createdAt = default(DateTimeOffset);

        // Act
        var exception = Record.Exception(() => new CampusBuilder().WithCreatedAt(createdAt));

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
        var exception = Record.Exception(() => new CampusBuilder().WithUpdatedAt(updatedAt));

        // Assert
        var ex = Assert.IsType<ArgumentException>(exception);
        Assert.Equal(nameof(updatedAt), ex.ParamName);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        // Arrange
        var campusId = Guid.NewGuid();
        var churchId = Guid.NewGuid();
        var campusName = Generated.NewName();
        var state = TestValues.NewStateCode();
        var zip = Generated.NewZip();
        var latitude = Generated.NewLatitude();
        var longitude = Generated.NewLongitude();
        var createdAt = Generated.NewUtcTimestamp();
        var updatedAt = Generated.NewUtcTimestamp();
        var builder = new CampusBuilder()
            .WithId(campusId)
            .WithChurchId(churchId)
            .WithName(campusName)
            .WithState(state)
            .WithZip(zip)
            .WithLatitude(latitude)
            .WithLongitude(longitude)
            .WithCreatedAt(createdAt)
            .WithUpdatedAt(updatedAt);

        // Act
        var exception = Record.Exception(() => builder.Build());

        // Assert
        var ex = Assert.IsType<InvalidOperationException>(exception);
        Assert.Contains(nameof(CampusBuilder.WithCity), ex.Message, StringComparison.Ordinal);
    }

    private static Campus Build(
        Guid? id = null,
        Guid? churchId = null,
        string? name = null,
        string? city = null,
        StateCode? state = null,
        string? zip = null,
        double? latitude = null,
        double? longitude = null,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null)
    {
        var generatedCampusId = Guid.NewGuid();
        var generatedChurchId = Guid.NewGuid();

        return new CampusBuilder()
            .WithId(id ?? generatedCampusId)
            .WithChurchId(churchId ?? generatedChurchId)
            .WithName(name ?? Generated.NewName())
            .WithStreet(Generated.NewStreet())
            .WithCity(city ?? Generated.NewCity())
            .WithState(state ?? TestValues.NewStateCode())
            .WithZip(zip ?? Generated.NewZip())
            .WithLatitude(latitude ?? Generated.NewLatitude())
            .WithLongitude(longitude ?? Generated.NewLongitude())
            .WithCreatedAt(createdAt ?? Generated.NewUtcTimestamp())
            .WithUpdatedAt(updatedAt ?? Generated.NewUtcTimestamp())
            .Build();
    }
}
