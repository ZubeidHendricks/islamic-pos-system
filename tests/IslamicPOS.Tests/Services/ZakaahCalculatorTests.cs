using Xunit;
using IslamicPOS.Core.Services;
using IslamicPOS.Infrastructure.Services;

namespace IslamicPOS.Tests.Services;

public class ZakaahCalculatorTests
{
    private readonly ZakaahCalculator _calculator;

    public ZakaahCalculatorTests()
    {
        _calculator = new ZakaahCalculator();
    }

    [Theory]
    [InlineData(5000, "USD", false)] // Below nisab
    [InlineData(5200, "USD", true)]  // At nisab
    [InlineData(6000, "USD", true)]  // Above nisab
    public async Task IsEligibleForZakaah_ReturnsCorrectResult(
        decimal assets,
        string currency,
        bool expected)
    {
        // Act
        var result = await _calculator.IsEligibleForZakaahAsync(assets, currency);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10000, 250)] // 2.5% of 10000
    [InlineData(20000, 500)] // 2.5% of 20000
    public async Task CalculateZakaah_ReturnsCorrectAmount(
        decimal assets,
        decimal expected)
    {
        // Act
        var result = await _calculator.CalculateZakaahAsync(
            assets, 0, 0, "USD");

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CalculateZakaah_WithMultipleAssets_SumsCorrectly()
    {
        // Arrange
        decimal cash = 5000;
        decimal inventory = 3000;
        decimal other = 2000;
        decimal expectedTotal = (cash + inventory + other) * 0.025m;

        // Act
        var result = await _calculator.CalculateZakaahAsync(
            cash, inventory, other, "USD");

        // Assert
        Assert.Equal(expectedTotal, result);
    }
}