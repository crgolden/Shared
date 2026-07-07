namespace Shared.Tests.Unit;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class CampusTests
{
    [Fact]
    public void Build_AllValidInput_ReturnsCampus()
    {
        var campus = Build();

        Assert.Equal("North Campus", campus.Name);
        Assert.Equal("Denver", campus.City);
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
        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithState("CO-North"));
        Assert.Equal("state", ex.ParamName);
    }

    [Fact]
    public void WithZip_Blank_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => new CampusBuilder().WithZip(" "));
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
        var builder = new CampusBuilder()
            .WithId(Guid.NewGuid())
            .WithChurchId(Guid.NewGuid())
            .WithName("North Campus")
            .WithState("CO")
            .WithZip("80201")
            .WithLatitude(0)
            .WithLongitude(0)
            .WithCreatedAt(DateTime.UtcNow)
            .WithUpdatedAt(DateTime.UtcNow);

        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build());
        Assert.Contains("WithCity", ex.Message, StringComparison.Ordinal);
    }

    private static Campus Build(
        Guid? id = null,
        Guid? churchId = null,
        string name = "North Campus",
        string city = "Denver",
        string state = "CO",
        string zip = "80201",
        double latitude = 39.7,
        double longitude = -104.9,
        DateTime? createdAt = null,
        DateTime? updatedAt = null)
    {
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return new CampusBuilder()
            .WithId(id ?? Guid.NewGuid())
            .WithChurchId(churchId ?? Guid.NewGuid())
            .WithName(name)
            .WithStreet("1 N St")
            .WithCity(city)
            .WithState(state)
            .WithZip(zip)
            .WithLatitude(latitude)
            .WithLongitude(longitude)
            .WithCreatedAt(createdAt ?? now)
            .WithUpdatedAt(updatedAt ?? now)
            .Build();
    }
}
