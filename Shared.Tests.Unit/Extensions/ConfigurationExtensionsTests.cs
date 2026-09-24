namespace Shared.Tests.Unit.Extensions;

using System.Globalization;
using Microsoft.Extensions.Configuration;
using Shared.Extensions;

[Trait("Category", "Unit")]
public sealed class ConfigurationExtensionsTests
{
    [Fact]
    public void GetRequired_WhenTheKeyIsPresent_ReturnsItsValue()
    {
        // Arrange
        var settingKey = $"key-{Guid.NewGuid():N}";
        var settingValue = $"value-{Guid.NewGuid():N}";
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([new KeyValuePair<string, string?>(settingKey, settingValue)])
            .Build();

        // Act
        var result = configuration.GetRequired<string>(settingKey);

        // Assert
        Assert.Equal(settingValue, result);
    }

    [Fact]
    public void GetRequired_WhenTheKeyIsMissing_ThrowsRatherThanReturningTheDefault()
    {
        // Arrange
        var missingSettingKey = $"key-{Guid.NewGuid():N}";
        var configuration = new ConfigurationBuilder().Build();

        // Act
        var missing = Record.Exception(() => configuration.GetRequired<string>(missingSettingKey));

        // Assert
        Assert.IsType<InvalidOperationException>(missing);
    }

    [Fact]
    public void GetRequired_WhenAnIntKeyIsMissing_ThrowsRatherThanReturningZero()
    {
        // Arrange
        var missingSettingKey = $"key-{Guid.NewGuid():N}";
        var configuration = new ConfigurationBuilder().Build();

        // Act
        var missing = Record.Exception(() => configuration.GetRequired<int>(missingSettingKey));

        // Assert
        Assert.IsType<InvalidOperationException>(missing);
    }

    [Fact]
    public void GetRequired_WhenABoolKeyIsMissing_ThrowsRatherThanReturningFalse()
    {
        // Arrange
        var missingSettingKey = $"key-{Guid.NewGuid():N}";
        var configuration = new ConfigurationBuilder().Build();

        // Act
        var missing = Record.Exception(() => configuration.GetRequired<bool>(missingSettingKey));

        // Assert
        Assert.IsType<InvalidOperationException>(missing);
    }

    [Fact]
    public void GetRequired_ForANonStringType_ConvertsTheConfiguredTextRatherThanFailing()
    {
        // Arrange
        var settingKey = $"key-{Guid.NewGuid():N}";
        var settingValue = Random.Shared.Next(1, 10_000);
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([
                new KeyValuePair<string, string?>(settingKey, settingValue.ToString(CultureInfo.InvariantCulture)),
            ])
            .Build();

        // Act
        var result = configuration.GetRequired<int>(settingKey);

        // Assert
        Assert.Equal(settingValue, result);
    }
}
