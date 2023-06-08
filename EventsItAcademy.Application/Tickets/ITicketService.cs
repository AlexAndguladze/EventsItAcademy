using EventsItAcademy.Application.Tickets.Requests;
using EventsItAcademy.Application.Tickets.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Tickets
{
    public interface ITicketService
    {
        Task<int> AddAsync(CancellationToken cancellationToken, TicketRequestModel ticket, string userId);
        Task<int> CountTakenTicketsAsync(CancellationToken cancellationToken, int eventId);
        Task<TicketResponseModel> GetByDataAsync(CancellationToken cancellationToken, string userId, int eventId);
        Task<int> GetIdByDataAsync(CancellationToken cancellationToken, string userId, int eventId);
        Task<bool> ExistsAsync(CancellationToken cancellationToken, int id);
        Task<bool> ExistsByDataAsync(CancellationToken cancellationToken, string userId, int eventId);
        Task UpdateAsync(CancellationToken cancellationToken, TicketRequestModel ticket, int id, string userId);
        Task DeleteExpiredBookingsAsync(CancellationToken cancellationToken, int expireDateOffset);
    }
}
