using Microsoft.AspNetCore.Mvc;
using IslamicPOS.Infrastructure.Services;
using IslamicPOS.Core.Models.IslamicFinance;

namespace IslamicPOS.API.Controllers;

public class ProfitSharingController : BaseApiController
{
    private readonly IProfitSharingService _profitSharingService;

    public ProfitSharingController(IProfitSharingService profitSharingService)
    {
        _profitSharingService = profitSharingService;
    }

    [HttpPost]
    public async Task<ActionResult<ProfitSharing>> CreateProfitDistribution([FromBody] DateRangeRequest request)
    {
        var distribution = await _profitSharingService.CreateProfitDistributionAsync(
            request.StartDate, 
            request.EndDate);
        return Ok(distribution);
    }

    [HttpPost("{id}/distribute")]
    public async Task<ActionResult> DistributeProfits(Guid id)
    {
        var success = await _profitSharingService.DistributeProfitsAsync(id);
        if (!success)
            return NotFound();
            
        return NoContent();
    }

    [HttpGet("{id}/shares")]
    public async Task<ActionResult<IDictionary<string, decimal>>> GetPartnerShares(Guid id)
    {
        var shares = await _profitSharingService.GetPartnerSharesAsync(id);
        return Ok(shares);
    }

    [HttpGet("{id}/charity")]
    public async Task<ActionResult<decimal>> GetCharityShare(Guid id)
    {
        var charityShare = await _profitSharingService.CalculateCharityShareAsync(id);
        return Ok(charityShare);
    }

    [HttpGet("{id}/details")]
    public async Task<ActionResult<IEnumerable<ProfitDistributionDetail>>> GetDistributionDetails(Guid id)
    {
        var details = await _profitSharingService.GetDistributionDetailsAsync(id);
        return Ok(details);
    }

    [HttpGet("undistributed")]
    public async Task<ActionResult<IEnumerable<ProfitSharing>>> GetUndistributedPeriods()
    {
        var periods = await _profitSharingService.GetUndistributedPeriodsAsync();
        return Ok(periods);
    }
}

public class DateRangeRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}