using EventsItAcademy.Application.Tickets.Repositories;
using EventsItAcademy.Domain.Tickets;
using EventsItAcademy.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Infrastructure.Tickets
{
    public class TicketRepository : ITicketRepository
    {
        private EventsItAcademyDbContext _context;

        public TicketRepository(EventsItAcademyDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(CancellationToken cancellationToken, Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return ticket.Id;
        }

        public async Task<int> CountTakenTicketsAsync(CancellationToken cancellationToken, int eventId)
        {
            return await _context.Tickets.Where(t=>t.EventId == eventId && t.IsDeleted==false).AsNoTracking().CountAsync();
        }

        public async Task<bool> ExistsAsync(CancellationToken cancellationToken, int id)
        {
            return await _context.Tickets.AnyAsync(t=>t.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsByDataAsync(CancellationToken cancellationToken, string userId, int eventId)
        {
            return await _context.Tickets.AnyAsync(t => t.UserId == userId && t.EventId == eventId);
        }

        public async Task<Ticket> GetByDataAsync(CancellationToken cancellationToken, string userId, int eventId)
        {
            return  await _context.Tickets.SingleOrDefaultAsync(t=>t.UserId == userId && t.EventId == eventId);
        }

        public async Task<int> GetIdByDataAsync(CancellationToken cancellationToken, string userId, int eventId)
        {
            var ticket = await _context.Tickets.AsNoTracking().SingleOrDefaultAsync(t => t.UserId == userId && t.EventId == eventId);
            if(ticket == null)
            {
                return 0;
            }
            return ticket.Id;
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, Ticket ticket)
        {
            _context.Update(ticket);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteExpiredBookingsAsync(CancellationToken cancellationToken, int expireDateOffset)
        {

            var tickets = _context.Tickets.Where(t => t.CreatedOn.AddDays(expireDateOffset) < DateTime.Now);
            _context.RemoveRange(tickets);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
