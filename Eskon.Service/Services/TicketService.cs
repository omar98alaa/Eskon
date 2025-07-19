using Eskon.Domian.Models;
using Eskon.Service.Interfaces;
using Eskon.Infrastructure.Interfaces;

namespace Eskon.Service.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Ticket> GetTicketByIDAsync(Guid ticketID)
        {
            return await _ticketRepository.GetByIdAsync(ticketID);
        }

        public async Task<List<Ticket>> GetAllTicketsforAdminAsync(Guid adminID)
        {
            return await _ticketRepository.GetAllTicketsforAdminAsync(adminID);
        }
        public async Task<List<Ticket>> GetAllTicketsforUserAsync(Guid userID)
        {
            return await _ticketRepository.GetAllTicketsforUserAsync(userID);
        }

        public async Task<Ticket> CreateTicket(Ticket ticket)
        {
            return await _ticketRepository.AddAsync(ticket);
        }

        public async Task EditTicket(Ticket ticket)
        {
            await _ticketRepository.UpdateAsync(ticket);
        }

        public async Task DeleteTicket(Ticket ticket)
        {
            await _ticketRepository.DeleteAsync(ticket);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _ticketRepository.SaveChangesAsync();
        }
    }
}
