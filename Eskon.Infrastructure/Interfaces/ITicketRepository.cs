using Eskon.Infrastructure.Generics;
using Eskon.Domian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Domian.Models;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Infrastructure.Interfaces
{
    public interface ITicketRepository : IGenericRepositoryAsync<Ticket>
    {
        Task<Ticket?> GetTicketByIDAsync(Guid ticketId);
        Task<List<Ticket>> GetAllTicketsforUserAsync(Guid userID);
        Task<List<Ticket>> GetAllTicketsforAdminAsync(Guid adminID);
    }
}
