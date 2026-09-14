namespace Shared.Tests.Unit;

using System.Globalization;
using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class StateCodesTests
{
    [Fact]
    public void TryParse_ReadsAUspsCode()
    {
        var expected = TestValues.NewStateCode();

        var parsed = StateCodes.TryParse(expected.ToString(), out var state);

        Assert.True(parsed);
        Assert.Equal(expected, state);
    }

    [Fact]
    public void TryParse_IsCaseInsensitive()
    {
        var expected = TestValues.NewStateCode();

        var parsed = StateCodes.TryParse(expected.ToString().ToLowerInvariant(), out var state);

        Assert.True(parsed);
        Assert.Equal(expected, state);
    }

    [Fact]
    public void TryParse_IgnoresSurroundingWhitespace()
    {
        var expected = TestValues.NewStateCode();

        var parsed = StateCodes.TryParse($"{TestValues.NewBlank()}{expected}{TestValues.NewBlank()}", out var state);

        Assert.True(parsed);
        Assert.Equal(expected, state);
    }

    [Fact]
    public void TryParse_RejectsTheNumericFormEnumTryParseWouldAccept()
    {
        var underlyingValue = ((int)TestValues.NewStateCode()).ToString(CultureInfo.InvariantCulture);

        var parsed = StateCodes.TryParse(underlyingValue, out _);

        Assert.False(parsed);
    }

    [Fact]
    public void TryParse_RejectsATwoLetterStringThatIsNotAUspsCode()
    {
        var parsed = StateCodes.TryParse(TestValues.NewNonUspsLetterPair(), out _);

        Assert.False(parsed);
    }

    [Fact]
    public void TryParse_RejectsAStringOfTheWrongLength()
    {
        var parsed = StateCodes.TryParse(TestValues.NewUnparseableStateCode(), out _);

        Assert.False(parsed);
    }

    [Fact]
    public void TryParse_RejectsBlank()
    {
        var parsed = StateCodes.TryParse(TestValues.NewBlank(), out _);

        Assert.False(parsed);
    }

    [Fact]
    public void TryParse_RejectsNull()
    {
        var parsed = StateCodes.TryParse(null, out _);

        Assert.False(parsed);
    }
}
