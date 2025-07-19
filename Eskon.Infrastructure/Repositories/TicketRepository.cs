using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Infrastructure.Repositories
{
    public class TicketRepository : GenericRepositoryAsync<Ticket>, ITicketRepository
    {
        #region Fields
        private readonly MyDbContext _context;
        #endregion

        #region Constructors
        public TicketRepository (MyDbContext context) : base (context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<Ticket?> GetTicketByIDAsync(Guid ticketId)
        {
            return await _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Admin)
                .FirstOrDefaultAsync(t => t.Id == ticketId);
        }
        public async Task<List<Ticket>> GetAllTicketsforUserAsync(Guid userID)
        {
            return await _context.Tickets
                .Where(t => t.UserId == userID)
                .Include(t => t.User)
                .Include(t => t.Admin)
                .ToListAsync();
        }
        public async Task<List<Ticket>> GetAllTicketsforAdminAsync(Guid adminID)
        {
            return await _context.Tickets
                .Where(t => t.AdminId == adminID)
                .Include(t => t.User)
                .Include(t => t.Admin)
                .ToListAsync();
        }
        #endregion


    }
}
