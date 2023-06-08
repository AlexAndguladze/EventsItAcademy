using EventsItAcademy.Application.Events.Requests;
using EventsItAcademy.Application.Events.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Events
{
    public interface IEventService
    {
        Task<List<EventResponseModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<EventResponseModel>> GetAllActiveAsync(CancellationToken cancellationToken);
        Task<List<EventResponseModel>> GetAllPendingAsync(CancellationToken cancellationToken);
        Task<List<EventResponseModel>> GetAllArchivedAsync(CancellationToken cancellationToken);
        //Task<EventResponseModel> GetAsync(CancellationToken cancellationToken, int id);
        Task<EventResponseModel> GetByIdAsync(CancellationToken cancellationToken, int id, string userId);
        Task<EventResponseModel> GetByIdAsync(CancellationToken cancellationToken, int id);
        Task<int> AddAsync(CancellationToken cancellationToken, EventRequestModel @event, string userId);
        Task RemoveAsync(CancellationToken cancellationToken, int id);
        Task RemoveAsync(CancellationToken cancellationToken, int id, string userId);
        Task RemoveAsync(CancellationToken cancellationToken, EventRequestModel @event);
        Task UpdateAsync(CancellationToken cancellationToken, EventRequestModel @event, int id, string userId);
        Task<List<EventResponseModel>> GetAllofUserAsync(CancellationToken cancellationToken, string id);
        Task SetIsActiveStatusAsync(CancellationToken cancellationToken, int id, bool isActive);
        Task ArchiveFinishedEventsAsync(CancellationToken cancellationToken);
    }
}
