namespace Shared.Tests;

using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class CampusTests
{
    [Fact]
    public void Create_AllValidInput_ReturnsCampus()
    {
        var campus = Build();

        Assert.Equal("North Campus", campus.Name);
        Assert.Equal("Denver", campus.City);
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
    public void Create_BlankName_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(name: string.Empty));
        Assert.Equal("name", ex.ParamName);
    }

    [Fact]
    public void Create_NullCity_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(city: null!));
        Assert.Equal("city", ex.ParamName);
    }

    [Fact]
    public void Create_StateWrongLength_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(state: "CO-North"));
        Assert.Equal("state", ex.ParamName);
    }

    [Fact]
    public void Create_BlankZip_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => Build(zip: " "));
        Assert.Equal("zip", ex.ParamName);
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
    public void Create_DefaultCreatedAt_Throws()
    {
        // Build()'s DateTime? "unset" sentinel is itself null, so default(DateTime) (a real, non-null
        // value) can only be exercised by calling Create directly rather than through the helper.
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var ex = Assert.Throws<ArgumentException>(() => Campus.Create(
            Guid.NewGuid(), Guid.NewGuid(), "North Campus", "1 N St", "Denver", "CO", "80201", 0, 0, default, now));
        Assert.Equal("createdAt", ex.ParamName);
    }

    [Fact]
    public void Create_DefaultUpdatedAt_Throws()
    {
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var ex = Assert.Throws<ArgumentException>(() => Campus.Create(
            Guid.NewGuid(), Guid.NewGuid(), "North Campus", "1 N St", "Denver", "CO", "80201", 0, 0, now, default));
        Assert.Equal("updatedAt", ex.ParamName);
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
        return Campus.Create(
            id ?? Guid.NewGuid(),
            churchId ?? Guid.NewGuid(),
            name,
            street: "1 N St",
            city,
            state,
            zip,
            latitude,
            longitude,
            createdAt ?? now,
            updatedAt ?? now);
    }
}
