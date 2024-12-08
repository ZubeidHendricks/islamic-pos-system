using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IslamicPOS.Core.Ticketing.Interfaces;
using IslamicPOS.Core.Ticketing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPOS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TicketingController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketingController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket(CreateTicketRequest request)
        {
            var ticket = await _ticketService.CreateTicket(request.OrderId, request.CollectionLocation);
            return Ok(ticket);
        }

        [HttpGet("{ticketNumber}")]
        public async Task<ActionResult<Ticket>> GetTicket(string ticketNumber)
        {
            var ticket = await _ticketService.GetTicket(ticketNumber);
            if (ticket == null)
                return NotFound();
            return Ok(ticket);
        }

        [HttpPost("{ticketNumber}/validate")]
        public async Task<ActionResult<Ticket>> ValidateTicket(string ticketNumber)
        {
            try
            {
                var ticket = await _ticketService.ValidateTicket(ticketNumber);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{ticketNumber}/collect")]
        public async Task<ActionResult<Ticket>> CollectTicket(string ticketNumber, [FromBody] CollectTicketRequest request)
        {
            try
            {
                var ticket = await _ticketService.MarkTicketAsUsed(ticketNumber, request.CollectedBy);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pending/{location}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetPendingTickets(string location)
        {
            var tickets = await _ticketService.GetPendingTickets(location);
            return Ok(tickets);
        }

        [HttpPost("{ticketNumber}/cancel")]
        public async Task<ActionResult> CancelTicket(string ticketNumber)
        {
            var result = await _ticketService.CancelTicket(ticketNumber);
            if (!result)
                return NotFound();
            return Ok();
        }
    }

    public class CreateTicketRequest
    {
        public Guid OrderId { get; set; }
        public string CollectionLocation { get; set; }
    }

    public class CollectTicketRequest
    {
        public string CollectedBy { get; set; }
    }
}