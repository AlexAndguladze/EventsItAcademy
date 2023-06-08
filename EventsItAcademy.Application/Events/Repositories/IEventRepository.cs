using EventsItAcademy.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Events.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<Event>> GetAllPendingAsync(CancellationToken cancellationToken);
        Task<List<Event>> GetAllArchivedAsync(CancellationToken cancellationToken);
        Task<List<Event>> GetAllActive(CancellationToken cancellationToken);
        Task<int> AddAsync(CancellationToken cancellationToken, Event @event);
        Task<List<Event>>GetAllOfUser(CancellationToken cancellationToken, string userId);
        Task<Event> GetAsync(CancellationToken cancellationToken, int id, string userId);
        Task<Event> GetAsync(CancellationToken cancellationToken, int id);
        Task<bool> RemoveAsync(CancellationToken cancellationToken, int id, string userId);
        Task<bool> RemoveAsync(CancellationToken cancellationToken, int id);
        Task UpdateAsync(CancellationToken cancellationToken, Event @event);
        //Task<bool> Exists(CancellationToken cancellationToken, int id);
        Task<bool> Exists(CancellationToken cancellationToken, int id, string userId);
        Task<bool>SetIsActiveStatusAsync(CancellationToken cancellationToken, int id, bool status);
        Task ArchiveFinishedEventsAsync(CancellationToken cancellationToken);
    }
}
