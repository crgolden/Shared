namespace Shared.Tests.Unit;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class CampusTests
{
    private const double OutOfRangeCoordinateOffset = 1;

    [Fact]
    public void Build_AllValidInput_ReturnsCampus()
    {
        var campusName = TestValues.NewName();
        var campusCity = TestValues.NewCity();
        var campusState = TestValues.NewStateCode();

        var campus = Build(name: campusName, city: campusCity, state: campusState);

        Assert.Equal(campusName, campus.Name);
        Assert.Equal(campusCity, campus.City);
        Assert.Equal(campusState, campus.State);
    }

    [Fact]
    public void WithId_Empty_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithId(Guid.Empty));
        Assert.Equal("id", ex.ParamName);
    }

    [Fact]
    public void WithChurchId_Empty_Throws()
    {
        var churchId = Guid.Empty;

        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithChurchId(churchId));
        Assert.Equal(nameof(churchId), ex.ParamName);
    }

    [Fact]
    public void WithName_Blank_Throws()
    {
        var name = TestValues.NewBlank();

        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithName(name));
        Assert.Equal(nameof(name), ex.ParamName);
    }

    [Fact]
    public void WithCity_Null_Throws()
    {
        string? city = null;

        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithCity(city));
        Assert.Equal(nameof(city), ex.ParamName);
    }

    [Fact]
    public void WithState_UndefinedCode_Throws()
    {
        var state = TestValues.NewUndefinedStateCode();

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new CampusBuilder().WithState(state));
        Assert.Equal(nameof(state), ex.ParamName);
    }

    [Fact]
    public void WithZip_Blank_Throws()
    {
        var zip = TestValues.NewBlank();

        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithZip(zip));
        Assert.Equal(nameof(zip), ex.ParamName);
    }

    [Theory]
    [InlineData(CampusBuilder.MinLatitude - OutOfRangeCoordinateOffset)]
    [InlineData(CampusBuilder.MaxLatitude + OutOfRangeCoordinateOffset)]
    public void WithLatitude_OutOfRange_Throws(double latitude)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new CampusBuilder().WithLatitude(latitude));
        Assert.Equal(nameof(latitude), ex.ParamName);
    }

    [Theory]
    [InlineData(CampusBuilder.MinLongitude - OutOfRangeCoordinateOffset)]
    [InlineData(CampusBuilder.MaxLongitude + OutOfRangeCoordinateOffset)]
    public void WithLongitude_OutOfRange_Throws(double longitude)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new CampusBuilder().WithLongitude(longitude));
        Assert.Equal(nameof(longitude), ex.ParamName);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        var createdAt = default(DateTimeOffset);

        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithCreatedAt(createdAt));
        Assert.Equal(nameof(createdAt), ex.ParamName);
    }

    [Fact]
    public void WithUpdatedAt_Default_Throws()
    {
        var updatedAt = default(DateTimeOffset);

        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithUpdatedAt(updatedAt));
        Assert.Equal(nameof(updatedAt), ex.ParamName);
    }

    [Fact]
    public void Build_RequiredFieldNeverSet_Throws()
    {
        var campusId = Guid.NewGuid();
        var churchId = Guid.NewGuid();
        var campusName = TestValues.NewName();
        var state = TestValues.NewStateCode();
        var zip = TestValues.NewZip();
        var latitude = TestValues.NewLatitude();
        var longitude = TestValues.NewLongitude();
        var createdAt = TestValues.NewUtcTimestamp();
        var updatedAt = TestValues.NewUtcTimestamp();
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

        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build());
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
            .WithName(name ?? TestValues.NewName())
            .WithStreet(TestValues.NewStreet())
            .WithCity(city ?? TestValues.NewCity())
            .WithState(state ?? TestValues.NewStateCode())
            .WithZip(zip ?? TestValues.NewZip())
            .WithLatitude(latitude ?? TestValues.NewLatitude())
            .WithLongitude(longitude ?? TestValues.NewLongitude())
            .WithCreatedAt(createdAt ?? TestValues.NewUtcTimestamp())
            .WithUpdatedAt(updatedAt ?? TestValues.NewUtcTimestamp())
            .Build();
    }
}
