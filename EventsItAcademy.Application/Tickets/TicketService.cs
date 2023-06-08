using EventsItAcademy.Application.CustomExcepitons;
using EventsItAcademy.Application.Tickets.Repositories;
using EventsItAcademy.Application.Tickets.Requests;
using EventsItAcademy.Application.Tickets.Responses;
using EventsItAcademy.Domain.Tickets;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Tickets
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repo;

        public TicketService(ITicketRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> AddAsync(CancellationToken cancellationToken, TicketRequestModel ticket, string userId)
        {
            var result = ticket.Adapt<Ticket>();
            result.UserId = userId;
            return await _repo.AddAsync(cancellationToken, result);
        }

        public async Task<int> CountTakenTicketsAsync(CancellationToken cancellationToken, int eventId)
        {
            return await _repo.CountTakenTicketsAsync(cancellationToken, eventId);
        }

        public async Task DeleteExpiredBookingsAsync(CancellationToken cancellationToken, int expireDateoffset)
        {
            await _repo.DeleteExpiredBookingsAsync(cancellationToken, expireDateoffset);
        }

        public async Task<bool> ExistsAsync(CancellationToken cancellationToken, int id)
        {
            return await _repo.ExistsAsync(cancellationToken, id);
        }

        public async Task<bool> ExistsByDataAsync(CancellationToken cancellationToken, string userId, int eventId)
        {
            return await _repo.ExistsByDataAsync(cancellationToken, userId, eventId);
        }

        public async Task<TicketResponseModel> GetByDataAsync(CancellationToken cancellationToken, string userId, int eventId)
        {
            var result=  await _repo.GetByDataAsync(cancellationToken, userId, eventId);
            if (result == null)
            {
                throw new ItemNotFoundException($"Ticket not found in database");
            }
            return result.Adapt<TicketResponseModel>();
        }
        public async Task<int> GetIdByDataAsync(CancellationToken cancellationToken, string userId, int eventId)
        {
            var id = await _repo.GetIdByDataAsync(cancellationToken, userId, eventId);
            if (id == 0)
            {
                throw new ItemNotFoundException($"{nameof(Ticket)} not found in database");
            }
            return id;
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, TicketRequestModel ticket, int id, string userId)
        {
            if (!await _repo.ExistsAsync(cancellationToken, id))
            {
                throw new ItemNotFoundException($"Ticket not found in database");
            }
            var toUpdate = ticket.Adapt<Ticket>();
            toUpdate.Id = id;
            toUpdate.UserId = userId;
            await _repo.UpdateAsync(cancellationToken, toUpdate);
        }
    }
}
