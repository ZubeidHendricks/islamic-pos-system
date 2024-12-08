namespace IslamicPOS.Tests.Services;

public class ProfitDistributionServiceTests
{
    private readonly Mock<IPartnerRepository> _partnerRepositoryMock;
    private readonly Mock<ISaleRepository> _saleRepositoryMock;
    private readonly ProfitDistributionService _service;

    public ProfitDistributionServiceTests()
    {
        _partnerRepositoryMock = new Mock<IPartnerRepository>();
        _saleRepositoryMock = new Mock<ISaleRepository>();

        _service = new ProfitDistributionService(
            _partnerRepositoryMock.Object,
            _saleRepositoryMock.Object,
            Mock.Of<ILogger<ProfitDistributionService>>()
        );
    }

    [Fact]
    public async Task CalculateDistribution_WithValidPartners_ReturnsCorrectShares()
    {
        // Arrange
        var startDate = DateTime.Today.AddDays(-30);
        var endDate = DateTime.Today;
        var netProfit = 10000m;

        var partners = new List<Partner>
        {
            new() { Id = 1, Name = "Partner A", SharePercentage = 40 },
            new() { Id = 2, Name = "Partner B", SharePercentage = 30 },
            new() { Id = 3, Name = "Partner C", SharePercentage = 30 }
        };

        _partnerRepositoryMock.Setup(x => x.GetActivePartnersAsync())
            .ReturnsAsync(partners);

        // Act
        var result = await _service.CalculateDistributionAsync(startDate, endDate);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Shares.Count);
        Assert.Contains(result.Shares, s => s.Amount == 4000m); // 40% of 10000
        Assert.Contains(result.Shares, s => s.Amount == 3000m); // 30% of 10000
    }
}