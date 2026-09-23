namespace Shared.Tests.Unit.Testing;

using System.Globalization;

[Trait("Category", "Unit")]
public sealed class EnvironmentSettingTests : IDisposable
{
    private const int Minimum = 1;
    private const int BelowMinimum = Minimum - 1;

    private static readonly int Maximum = Generated.NewCountCeiling();
    private static readonly int AboveMaximum = Maximum + 1;
    private static readonly double MaximumPercent = Generated.NewPercentCeiling();

    private readonly string _variableName = Generated.NewEnvironmentVariableName();

    public void Dispose() => Environment.SetEnvironmentVariable(_variableName, null);

    [Fact]
    public void Count_VariableUnset_ReturnsTheDeclaredDefault()
    {
        // Arrange
        var declaredDefault = Generated.NewCount();

        // Act
        var resolved = EnvironmentSetting.Count(_variableName, declaredDefault, Minimum, Maximum);

        // Assert
        Assert.Equal(declaredDefault, resolved);
    }

    [Fact]
    public void Count_VariableSetWithinBounds_OverridesTheDefault()
    {
        // Arrange
        var declaredDefault = Generated.NewCount();
        var configured = Generated.NewCount();
        Environment.SetEnvironmentVariable(_variableName, configured.ToString(CultureInfo.InvariantCulture));

        // Act
        var resolved = EnvironmentSetting.Count(_variableName, declaredDefault, Minimum, Maximum);

        // Assert
        Assert.Equal(configured, resolved);
    }

    [Fact]
    public void Count_VariableSurroundedByWhitespace_Parses()
    {
        // Arrange
        var configured = Generated.NewCount();
        Environment.SetEnvironmentVariable(_variableName, Generated.Padded(configured.ToString(CultureInfo.InvariantCulture)));

        // Act
        var resolved = EnvironmentSetting.Count(_variableName, Generated.NewCount(), Minimum, Maximum);

        // Assert
        Assert.Equal(configured, resolved);
    }

    [Fact]
    public void Count_NegativeVariable_StillThrowsNamingTheVariable()
    {
        // Arrange
        Environment.SetEnvironmentVariable(_variableName, (-Generated.NewCount()).ToString(CultureInfo.InvariantCulture));

        // Act
        var thrown = Assert.Throws<InvalidOperationException>(
            () => EnvironmentSetting.Count(_variableName, Generated.NewCount(), Minimum, Maximum));

        // Assert
        Assert.Contains(_variableName, thrown.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Count_VariableBelowMinimum_ThrowsNamingTheVariable()
    {
        // Arrange
        Environment.SetEnvironmentVariable(_variableName, BelowMinimum.ToString(CultureInfo.InvariantCulture));

        // Act
        var thrown = Assert.Throws<InvalidOperationException>(
            () => EnvironmentSetting.Count(_variableName, Generated.NewCount(), Minimum, Maximum));

        // Assert
        Assert.Contains(_variableName, thrown.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Count_VariableAboveMaximum_ThrowsNamingTheVariable()
    {
        // Arrange
        Environment.SetEnvironmentVariable(_variableName, AboveMaximum.ToString(CultureInfo.InvariantCulture));

        // Act
        var thrown = Assert.Throws<InvalidOperationException>(
            () => EnvironmentSetting.Count(_variableName, Generated.NewCount(), Minimum, Maximum));

        // Assert
        Assert.Contains(_variableName, thrown.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Count_VariableNotANumber_ThrowsRatherThanFallingBackToTheDefault()
    {
        // Arrange
        Environment.SetEnvironmentVariable(_variableName, Generated.NewUnparseableNumber());

        // Act
        var thrown = Assert.Throws<InvalidOperationException>(
            () => EnvironmentSetting.Count(_variableName, Generated.NewCount(), Minimum, Maximum));

        // Assert
        Assert.Contains(_variableName, thrown.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Percent_VariableSetWithinBounds_OverridesTheDefault()
    {
        // Arrange
        var configured = Generated.NewPercent();
        Environment.SetEnvironmentVariable(_variableName, configured.ToString(CultureInfo.InvariantCulture));

        // Act
        var resolved = EnvironmentSetting.Percent(_variableName, Generated.NewPercent(), MaximumPercent);

        // Assert
        Assert.Equal(configured, resolved);
    }

    [Fact]
    public void Percent_VariableSurroundedByWhitespace_Parses()
    {
        // Arrange
        var configured = Generated.NewPercent();
        Environment.SetEnvironmentVariable(_variableName, Generated.Padded(configured.ToString(CultureInfo.InvariantCulture)));

        // Act
        var resolved = EnvironmentSetting.Percent(_variableName, Generated.NewPercent(), MaximumPercent);

        // Assert
        Assert.Equal(configured, resolved);
    }

    [Fact]
    public void Percent_NegativeVariable_StillThrowsNamingTheVariable()
    {
        // Arrange
        Environment.SetEnvironmentVariable(_variableName, (-Generated.NewCount()).ToString(CultureInfo.InvariantCulture));

        // Act
        var thrown = Assert.Throws<InvalidOperationException>(
            () => EnvironmentSetting.Percent(_variableName, Generated.NewPercent(), MaximumPercent));

        // Assert
        Assert.Contains(_variableName, thrown.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Percent_VariableUnset_ReturnsTheDeclaredDefault()
    {
        // Arrange
        var declaredDefault = Generated.NewPercent();

        // Act
        var resolved = EnvironmentSetting.Percent(_variableName, declaredDefault, MaximumPercent);

        // Assert
        Assert.Equal(declaredDefault, resolved);
    }

    [Fact]
    public void Text_VariableSet_OverridesTheDefault()
    {
        // Arrange
        var configured = Generated.NewText();
        Environment.SetEnvironmentVariable(_variableName, configured);

        // Act
        var resolved = EnvironmentSetting.Text(_variableName, Generated.NewText());

        // Assert
        Assert.Equal(configured, resolved);
    }

    [Fact]
    public void Text_VariableIsWhitespace_ReturnsTheDeclaredDefault()
    {
        // Arrange
        var declaredDefault = Generated.NewText();
        var blank = Generated.NewBlank();
        Environment.SetEnvironmentVariable(_variableName, blank);

        // Act
        var resolved = EnvironmentSetting.Text(_variableName, declaredDefault);

        // Assert
        Assert.Equal(declaredDefault, resolved);
    }
}
