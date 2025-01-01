namespace IslamicPOS.Infrastructure.Repositories;

public interface IZakatCalculationRepository
{
    Task<ZakatCalculation?> GetByIdAsync(Guid id);
    Task<IEnumerable<ZakatCalculation>> GetAllAsync();
    Task<ZakatCalculation> AddAsync(ZakatCalculation calculation);
    Task UpdateAsync(ZakatCalculation calculation);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<ZakatCalculation>> GetUnpaidCalculationsAsync();
    Task<ZakatCalculation?> GetLatestCalculationAsync();
    Task<decimal> GetTotalUnpaidZakatAsync();
}