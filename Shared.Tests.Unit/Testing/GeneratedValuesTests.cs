namespace Shared.Tests.Unit.Testing;

using System.Globalization;
using Shared.Domain;

[Trait("Category", "Unit")]
public sealed class GeneratedValuesTests
{
    [Fact]
    public void NewPassword_Generated_CarriesEveryCharacterClassIdentityRequires()
    {
        // Act
        var password = Generated.NewPassword();

        // Assert
        Assert.Matches("[A-Z]", password);
        Assert.Matches("[a-z]", password);
        Assert.Matches("[0-9]", password);
        Assert.Matches("[^A-Za-z0-9]", password);
    }

    [Fact]
    public void NewUtcTimestamp_Generated_IsUtcAndInThePast()
    {
        // Act
        var timestamp = Generated.NewUtcTimestamp();

        // Assert
        Assert.Equal(TimeSpan.Zero, timestamp.Offset);
        Assert.True(timestamp < DateTimeOffset.UtcNow);
    }

    [Fact]
    public void NewTimestampWithNonZeroOffset_Generated_HasANonZeroOffset()
    {
        // Act
        var timestamp = Generated.NewTimestampWithNonZeroOffset();

        // Assert
        Assert.NotEqual(TimeSpan.Zero, timestamp.Offset);
    }

    [Fact]
    public void NewDayOfWeek_Generated_IsAValidDayIndex()
    {
        // Act
        var day = Generated.NewDayOfWeek();

        // Assert
        Assert.InRange(day, (byte)DayOfWeek.Sunday, (byte)DayOfWeek.Saturday);
    }

    [Fact]
    public void NewEmailAddress_Generated_UsesTheReservedInvalidDomain()
    {
        // Act
        var emailAddress = Generated.NewEmailAddress();

        // Assert
        Assert.Matches(@"^[a-z]+@[a-z]+\.invalid$", emailAddress);
    }

    [Fact]
    public void NewTokenFromFirstHalfOfAlphabet_Generated_DrawsOnlyFromAToM()
    {
        // Arrange
        var length = Generated.NewCount();

        // Act
        var token = Generated.NewTokenFromFirstHalfOfAlphabet(length);

        // Assert
        Assert.Matches($"^[a-m]{{{length}}}$", token);
    }

    [Fact]
    public void NewTokenFromSecondHalfOfAlphabet_Generated_DrawsOnlyFromNToZ()
    {
        // Arrange
        var length = Generated.NewCount();

        // Act
        var token = Generated.NewTokenFromSecondHalfOfAlphabet(length);

        // Assert
        Assert.Matches($"^[n-z]{{{length}}}$", token);
    }

    [Fact]
    public void UppercaseToken_Generated_DrawsOnlyFromAToZ()
    {
        // Arrange
        var length = Generated.NewCount();

        // Act
        var token = Generated.UppercaseToken(length);

        // Assert
        Assert.Matches($"^[A-Z]{{{length}}}$", token);
    }

    [Fact]
    public void NewEnvironmentVariableName_Generated_CannotNameARealVariable()
    {
        // Act
        var variableName = Generated.NewEnvironmentVariableName();

        // Assert
        Assert.Null(Environment.GetEnvironmentVariable(variableName));
        Assert.Matches("^CRGOLDEN_TESTING_[A-Z]+$", variableName);
    }

    [Fact]
    public void NewCountCeiling_Generated_ExceedsEveryCount()
    {
        // Act
        var count = Generated.NewCount();
        var ceiling = Generated.NewCountCeiling();

        // Assert
        Assert.True(ceiling > count);
    }

    [Fact]
    public void NewPercentCeiling_Generated_ExceedsEveryPercent()
    {
        // Act
        var percent = Generated.NewPercent();
        var ceiling = Generated.NewPercentCeiling();

        // Assert
        Assert.True(ceiling > percent);
    }

    [Fact]
    public void NewUnparseableNumber_Generated_IsNotANumber()
    {
        // Act
        var unparseable = Generated.NewUnparseableNumber();

        // Assert
        Assert.False(double.TryParse(unparseable, CultureInfo.InvariantCulture, out _));
    }

    [Fact]
    public void Padded_Generated_SurroundsTheValueWithBlanksOnly()
    {
        // Arrange
        var value = Generated.NewText();

        // Act
        var padded = Generated.Padded(value);

        // Assert
        Assert.NotEqual(value, padded);
        Assert.Equal(value, padded.Trim());
    }
    [Fact]
    public void NewDefinedValue_Generated_IsADefinedMemberOfTheEnum()
    {
        // Act
        var state = Generated.NewDefinedValue<StateCode>();

        // Assert
        Assert.True(Enum.IsDefined(state));
    }

    [Fact]
    public void NewUndefinedValue_Generated_IsNotAMemberOfTheEnum()
    {
        // Act
        var state = Generated.NewUndefinedValue<StateCode>();

        // Assert
        Assert.False(Enum.IsDefined(state));
    }

    [Fact]
    public void NewTokenOtherThan_Generated_NeverReturnsAnExcludedToken()
    {
        // Arrange
        var excluded = Generated.NewText();

        // Act
        var token = Generated.NewTokenOtherThan(excluded, excluded.ToUpperInvariant());

        // Assert
        Assert.NotEqual(excluded, token);
    }
}
