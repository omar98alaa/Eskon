using Eskon.Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Eskon.Service.Interfaces
{
    public interface ITicketService
    {
        Task<Ticket> GetTicketByIDAsync(Guid ticketID);
        Task<List<Ticket>> GetAllTicketsforUserAsync(Guid userID);
        Task<List<Ticket>> GetAllTicketsforAdminAsync(Guid adminID);
        Task<Ticket> CreateTicket(Ticket ticket);
        Task EditTicket(Ticket ticket);
        Task DeleteTicket(Ticket ticket);
        Task<int> SaveChangesAsync();


    }
}
