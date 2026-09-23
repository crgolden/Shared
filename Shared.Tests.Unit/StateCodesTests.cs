namespace Shared.Tests.Unit;

using System.Globalization;
using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class StateCodesTests
{
    [Fact]
    public void TryParse_ReadsAUspsCode()
    {
        // Arrange
        var expected = TestValues.NewStateCode();

        // Act
        var parsed = StateCodes.TryParse(expected.ToString(), out var state);

        // Assert
        Assert.True(parsed);
        Assert.Equal(expected, state);
    }

    [Fact]
    public void TryParse_IsCaseInsensitive()
    {
        // Arrange
        var expected = TestValues.NewStateCode();

        // Act
        var parsed = StateCodes.TryParse(string.Concat(expected.ToString().Select(char.ToLowerInvariant)), out var state);

        // Assert
        Assert.True(parsed);
        Assert.Equal(expected, state);
    }

    [Fact]
    public void TryParse_IgnoresSurroundingWhitespace()
    {
        // Arrange
        var expected = TestValues.NewStateCode();

        // Act
        var parsed = StateCodes.TryParse($"{Generated.NewBlank()}{expected}{Generated.NewBlank()}", out var state);

        // Assert
        Assert.True(parsed);
        Assert.Equal(expected, state);
    }

    [Fact]
    public void TryParse_RejectsTheNumericFormEnumTryParseWouldAccept()
    {
        // Arrange
        var underlyingValue = ((int)TestValues.NewStateCode()).ToString(CultureInfo.InvariantCulture);

        // Act
        var parsed = StateCodes.TryParse(underlyingValue, out _);

        // Assert
        Assert.False(parsed);
    }

    [Fact]
    public void TryParse_RejectsATwoLetterStringThatIsNotAUspsCode()
    {
        // Act
        var parsed = StateCodes.TryParse(TestValues.NewNonUspsLetterPair(), out _);

        // Assert
        Assert.False(parsed);
    }

    [Fact]
    public void TryParse_RejectsAStringOfTheWrongLength()
    {
        // Act
        var parsed = StateCodes.TryParse(Generated.NewUnparseableStateCode(), out _);

        // Assert
        Assert.False(parsed);
    }

    [Fact]
    public void TryParse_RejectsBlank()
    {
        // Act
        var parsed = StateCodes.TryParse(Generated.NewBlank(), out _);

        // Assert
        Assert.False(parsed);
    }

    [Fact]
    public void TryParse_RejectsNull()
    {
        // Act
        var parsed = StateCodes.TryParse(null, out _);

        // Assert
        Assert.False(parsed);
    }
}
