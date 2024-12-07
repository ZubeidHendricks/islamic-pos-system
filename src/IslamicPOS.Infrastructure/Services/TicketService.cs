using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IslamicPOS.Core.Barcoding.Interfaces;
using IslamicPOS.Core.Ticketing.Interfaces;
using IslamicPOS.Core.Ticketing.Models;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBarcodeService _barcodeService;

        public TicketService(ApplicationDbContext context, IBarcodeService barcodeService)
        {
            _context = context;
            _barcodeService = barcodeService;
        }

        public async Task<Ticket> CreateTicket(Guid orderId, string collectionLocation)
        {
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                TicketNumber = GenerateTicketNumber(),
                CreatedAt = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                Status = TicketStatus.Active,
                CollectionLocation = collectionLocation
            };

            var qrContent = $"TICKET:{ticket.TicketNumber}|ORDER:{orderId}|LOC:{collectionLocation}";
            var qrCodeImage = await _barcodeService.GenerateQRCode(qrContent);
            ticket.QRCode = Convert.ToBase64String(qrCodeImage);

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }

        public async Task<Ticket> GetTicket(string ticketNumber)
        {
            return await _context.Tickets
                .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);
        }

        public async Task<Ticket> ValidateTicket(string ticketNumber)
        {
            var ticket = await GetTicket(ticketNumber);
            
            if (ticket == null)
                throw new Exception("Ticket not found");

            if (ticket.Status != TicketStatus.Active)
                throw new Exception($"Ticket is {ticket.Status}");

            if (ticket.ExpiryDate < DateTime.UtcNow)
            {
                ticket.Status = TicketStatus.Expired;
                await _context.SaveChangesAsync();
                throw new Exception("Ticket has expired");
            }

            return ticket;
        }

        public async Task<Ticket> MarkTicketAsUsed(string ticketNumber, string collectedBy)
        {
            var ticket = await ValidateTicket(ticketNumber);
            
            ticket.Status = TicketStatus.Used;
            ticket.CollectedAt = DateTime.UtcNow;
            ticket.CollectedBy = collectedBy;
            
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetPendingTickets(string collectionLocation)
        {
            return await _context.Tickets
                .Where(t => t.CollectionLocation == collectionLocation
                    && t.Status == TicketStatus.Active
                    && t.ExpiryDate > DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task<bool> CancelTicket(string ticketNumber)
        {
            var ticket = await GetTicket(ticketNumber);
            if (ticket == null || ticket.Status != TicketStatus.Active)
                return false;

            ticket.Status = TicketStatus.Cancelled;
            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerateTicketNumber()
        {
            return $"T{DateTime.UtcNow:yyyyMMdd}{Guid.NewGuid().ToString().Substring(0, 8)}".ToUpper();
        }
    }
}