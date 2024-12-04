using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IslamicPOS.Core.Logistics.Interfaces;
using IslamicPOS.Core.Logistics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPOS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LogisticsController : ControllerBase
    {
        private readonly IRouteOptimizationService _routeService;
        private readonly IVendorDeliveryService _deliveryService;

        public LogisticsController(
            IRouteOptimizationService routeService,
            IVendorDeliveryService deliveryService)
        {
            _routeService = routeService;
            _deliveryService = deliveryService;
        }

        [HttpPost("routes/optimize")]
        public async Task<ActionResult<OptimizedRoute>> OptimizeRoute(
            [FromBody] List<DeliveryPoint> deliveryPoints)
        {
            var route = await _routeService.GenerateOptimalRoute(deliveryPoints);
            return Ok(route);
        }

        [HttpGet("routes/daily/{date}")]
        public async Task<ActionResult<IEnumerable<OptimizedRoute>>> GetDailyRoutes(
            DateTime date)
        {
            var routes = await _routeService.GenerateDailyRoutes(date);
            return Ok(routes);
        }

        [HttpPost("delivery/window")]
        public async Task<ActionResult<TimeWindow>> RequestDeliveryWindow(
            [FromBody] VendorDeliveryRequest request)
        {
            var window = await _deliveryService.RequestDeliveryWindow(request);
            return Ok(window);
        }

        [HttpPut("delivery/{deliveryId}/status")]
        public async Task<ActionResult> UpdateDeliveryStatus(
            Guid deliveryId,
            [FromBody] DeliveryStatus status)
        {
            await _deliveryService.UpdateDeliveryStatus(deliveryId, status);
            return Ok();
        }

        [HttpGet("delivery/{deliveryId}")]
        public async Task<ActionResult<DeliveryPoint>> GetDeliveryDetails(
            Guid deliveryId)
        {
            var details = await _deliveryService.GetDeliveryDetails(deliveryId);
            return Ok(details);
        }
    }
}