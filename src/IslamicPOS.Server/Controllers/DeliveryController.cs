using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IslamicPOS.Core.Services.Delivery;

namespace IslamicPOS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DeliveryController : ControllerBase
{
    private readonly IRouteOptimizationService _routeService;
    private readonly ILogger<DeliveryController> _logger;

    public DeliveryController(
        IRouteOptimizationService routeService,
        ILogger<DeliveryController> logger)
    {
        _routeService = routeService;
        _logger = logger;
    }

    [HttpPost("optimize")]
    public async Task<ActionResult<OptimizedRoute>> OptimizeRoute(
        [FromBody] RouteOptimizationRequest request)
    {
        try
        {
            var route = await _routeService.GenerateRouteAsync(
                request.DeliveryPoints,
                request.Vehicle
            );
            return Ok(route);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error optimizing route");
            return StatusCode(500, "An error occurred while optimizing the route");
        }
    }

    [HttpGet("daily/{date}")]
    public async Task<ActionResult<List<OptimizedRoute>>> GetDailyRoutes(DateTime date)
    {
        try
        {
            var routes = await _routeService.GenerateDailyRoutesAsync(date);
            return Ok(routes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating daily routes");
            return StatusCode(500, "An error occurred while generating daily routes");
        }
    }

    [HttpPost("validate-halal")]
    public async Task<ActionResult<bool>> ValidateHalalRequirements(
        [FromBody] OptimizedRoute route)
    {
        try
        {
            var isValid = await _routeService.ValidateHalalRequirementsAsync(route);
            return Ok(isValid);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating Halal requirements");
            return StatusCode(500, "An error occurred while validating Halal requirements");
        }
    }
}