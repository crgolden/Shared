namespace Shared.Tests.Unit;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class CampusTests
{
    [Fact]
    public void Build_AllValidInput_ReturnsCampus()
    {
        var campusName = TestValues.NewName();
        var campusCity = TestValues.NewCity();

        var campus = Build(name: campusName, city: campusCity);

        Assert.Equal(campusName, campus.Name);
        Assert.Equal(campusCity, campus.City);
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
        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithChurchId(Guid.Empty));
        Assert.Equal("churchId", ex.ParamName);
    }

    [Fact]
    public void WithName_Blank_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithName(string.Empty));
        Assert.Equal("name", ex.ParamName);
    }

    [Fact]
    public void WithCity_Null_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithCity(null!));
        Assert.Equal("city", ex.ParamName);
    }

    [Fact]
    public void WithState_WrongLength_Throws()
    {
        var wrongLengthStateCode = TestValues.LowercaseToken(Random.Shared.Next(3, 10));

        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithState(wrongLengthStateCode));
        Assert.Equal("state", ex.ParamName);
    }

    [Fact]
    public void WithZip_Blank_Throws()
    {
        var blankZip = new string(' ', Random.Shared.Next(1, 4));

        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithZip(blankZip));
        Assert.Equal("zip", ex.ParamName);
    }

    [Theory]
    [InlineData(-91.0)]
    [InlineData(91.0)]
    public void WithLatitude_OutOfRange_Throws(double latitude)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new CampusBuilder().WithLatitude(latitude));
        Assert.Equal("latitude", ex.ParamName);
    }

    [Theory]
    [InlineData(-181.0)]
    [InlineData(181.0)]
    public void WithLongitude_OutOfRange_Throws(double longitude)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new CampusBuilder().WithLongitude(longitude));
        Assert.Equal("longitude", ex.ParamName);
    }

    [Fact]
    public void WithCreatedAt_Default_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithCreatedAt(default));
        Assert.Equal("createdAt", ex.ParamName);
    }

    [Fact]
    public void WithUpdatedAt_Default_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithUpdatedAt(default));
        Assert.Equal("updatedAt", ex.ParamName);
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
        Assert.Contains("WithCity", ex.Message, StringComparison.Ordinal);
    }

    private static Campus Build(
        Guid? id = null,
        Guid? churchId = null,
        string? name = null,
        string? city = null,
        string? state = null,
        string? zip = null,
        double? latitude = null,
        double? longitude = null,
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null) =>
        new CampusBuilder()
            .WithId(id ?? Guid.NewGuid())
            .WithChurchId(churchId ?? Guid.NewGuid())
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
