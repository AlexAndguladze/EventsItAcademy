using EventsItAcademy.Domain.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Tickets.Repositories
{
    public interface ITicketRepository
    {
        Task<int> AddAsync(CancellationToken cancellationToken, Ticket ticket);
        Task<int> CountTakenTicketsAsync(CancellationToken cancellationToken, int eventId);
        Task<Ticket>GetByDataAsync(CancellationToken cancellationToken, string userId, int eventId);
        Task<int> GetIdByDataAsync(CancellationToken cancellationToken, string userId, int eventId);
        Task<bool> ExistsByDataAsync(CancellationToken cancellationToken, string userId, int eventId);
        Task<bool> ExistsAsync(CancellationToken cancellationToken, int id);
        Task UpdateAsync(CancellationToken cancellationToken, Ticket ticket);
        Task DeleteExpiredBookingsAsync(CancellationToken cancellationToken, int expireDateOffset);
    }
}
