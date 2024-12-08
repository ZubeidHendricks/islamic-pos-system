using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IslamicPOS.Core.Ticketing.Models;

namespace IslamicPOS.Core.Ticketing.Interfaces
{
    public interface ITicketService
    {
        Task<Ticket> CreateTicket(Guid orderId, string collectionLocation);
        Task<Ticket> GetTicket(string ticketNumber);
        Task<Ticket> ValidateTicket(string ticketNumber);
        Task<Ticket> MarkTicketAsUsed(string ticketNumber, string collectedBy);
        Task<IEnumerable<Ticket>> GetPendingTickets(string collectionLocation);
        Task<bool> CancelTicket(string ticketNumber);
    }
}