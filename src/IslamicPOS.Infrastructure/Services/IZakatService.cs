namespace IslamicPOS.Infrastructure.Services;

public interface IZakatService
{
    Task<ZakatCalculation> CalculateZakatAsync(DateTime calculationDate);
    Task<bool> IsEligibleForZakatAsync(decimal netWorth, DateTime calculationDate);
    Task<decimal> GetNisabThresholdAsync(DateTime date);
    Task<decimal> GetTotalAssetsValueAsync();
    Task<decimal> GetTotalLiabilitiesAsync();
    Task<IEnumerable<ZakatCalculation>> GetZakatHistoryAsync();
    Task<decimal> GetUnpaidZakatObligationAsync();
    Task<bool> MarkZakatAsPaidAsync(Guid calculationId);
}