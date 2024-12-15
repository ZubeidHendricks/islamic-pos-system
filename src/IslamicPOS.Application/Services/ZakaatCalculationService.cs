using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Finance.Models;
using IslamicPOS.Domain.Finance.Interfaces;
using Microsoft.EntityFrameworkCore;
using IslamicPOS.Application.Common.Interfaces;

namespace IslamicPOS.Application.Services;

public class ZakaatCalculationService : IZakaatService
{
    private readonly IApplicationDbContext _context;

    public ZakaatCalculationService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ZakaatCalculation> CalculateZakaat(ZakaatInput input)
    {
        var calculation = new ZakaatCalculation(
            input.Assets,
            input.Liabilities,
            input.BusinessAssets,
            input.Investments);

        _context.ZakaatCalculations.Add(calculation);
        await _context.SaveChangesAsync();

        return calculation;
    }

    public async Task<ZakaatCalculation?> GetZakaatCalculation(Guid id)
    {
        return await _context.ZakaatCalculations.FindAsync(id);
    }

    public async Task<List<ZakaatCalculation>> GetZakaatHistory(string userId)
    {
        return await _context.ZakaatCalculations
            .OrderByDescending(x => x.CreatedAt)
            .Take(10)
            .ToListAsync();
    }
}
