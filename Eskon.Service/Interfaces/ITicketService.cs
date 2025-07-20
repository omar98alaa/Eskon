using Eskon.Domian.Models;


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
    }
}
