using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPOS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Manager")]
public class ProfitDistributionController : ControllerBase
{
    private readonly IProfitDistributionService _profitService;
    private readonly IPartnerRepository _partnerRepository;
    private readonly ILogger<ProfitDistributionController> _logger;

    public ProfitDistributionController(
        IProfitDistributionService profitService,
        IPartnerRepository partnerRepository,
        ILogger<ProfitDistributionController> logger)
    {
        _profitService = profitService;
        _partnerRepository = partnerRepository;
        _logger = logger;
    }

    [HttpPost("calculate")]
    public async Task<ActionResult<ProfitDistribution>> CalculateDistribution(
        [FromBody] DistributionRequest request)
    {
        try
        {
            var totalShares = await _partnerRepository.GetTotalSharePercentageAsync();
            if (totalShares != 100)
            {
                return BadRequest("Total partner shares must equal 100%");
            }

            var distribution = await _profitService.CalculateDistributionAsync(
                request.StartDate,
                request.EndDate
            );
            return Ok(distribution);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating profit distribution");
            return StatusCode(500, "An error occurred while calculating distribution");
        }
    }

    [HttpPost("process")]
    public async Task<ActionResult<DistributionResult>> ProcessDistribution(
        [FromBody] ProfitDistribution distribution)
    {
        try
        {
            var result = await _profitService.ProcessDistributionAsync(distribution);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing profit distribution");
            return StatusCode(500, "An error occurred while processing distribution");
        }
    }

    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<DistributionHistory>>> GetHistory(
        [FromQuery] DateTime? startDate)
    {
        try
        {
            var history = await _profitService.GetDistributionHistoryAsync(startDate);
            return Ok(history);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting distribution history");
            return StatusCode(500, "An error occurred while getting history");
        }
    }
}