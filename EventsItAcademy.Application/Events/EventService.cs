using EventsItAcademy.Application.CustomExcepitons;
using EventsItAcademy.Application.Events.Repositories;
using EventsItAcademy.Application.Events.Requests;
using EventsItAcademy.Application.Events.Responses;
using EventsItAcademy.Domain.Events;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Events
{
    public class EventService : IEventService
    {
        private IEventRepository _repo;

        public EventService(IEventRepository repo)
        {
            _repo = repo;
        }
        public async Task<int> AddAsync(CancellationToken cancellationToken, EventRequestModel @event, string userId)
        {
            var toInsert = @event.Adapt<Event>();
            toInsert.OwnerId = userId;
            return await _repo.AddAsync(cancellationToken, toInsert);
        }

        public async Task ArchiveFinishedEventsAsync(CancellationToken cancellationToken)
        {
            await _repo.ArchiveFinishedEventsAsync(cancellationToken);
        }

        public async Task<List<EventResponseModel>> GetAllActiveAsync(CancellationToken cancellationToken)
        {
            var events = await _repo.GetAllActive(cancellationToken);
            return events.Adapt<List<EventResponseModel>>();
        }

        public async Task<List<EventResponseModel>> GetAllArchivedAsync(CancellationToken cancellationToken)
        {
            var events = await _repo.GetAllArchivedAsync(cancellationToken);
            return events.Adapt<List<EventResponseModel>>();
        }

        public async Task<List<EventResponseModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var events = await _repo.GetAllAsync(cancellationToken);
            return events.Adapt<List<EventResponseModel>>();

        }

        //public Task<List<EventResponseModel>> GetAllAsync(CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<List<EventResponseModel>> GetAllofUserAsync(CancellationToken cancellationToken, string id)
        {
            var events = await _repo.GetAllOfUser(cancellationToken, id);
            return events.Adapt<List<EventResponseModel>>();
        }

        public async Task<List<EventResponseModel>> GetAllPendingAsync(CancellationToken cancellationToken)
        {
            var events = await _repo.GetAllPendingAsync(cancellationToken);
            return events.Adapt<List<EventResponseModel>>();
        }

        public async Task<EventResponseModel> GetByIdAsync(CancellationToken cancellationToken, int id, string userId)
        {
            var @event = await _repo.GetAsync(cancellationToken, id, userId);
            if(@event == null)
            {
                throw new ItemNotFoundException($"Event with Id:{id} not found in database");
            }
            return @event.Adapt<EventResponseModel>();
        }

        public async Task<EventResponseModel> GetByIdAsync(CancellationToken cancellationToken, int id)
        {
            var @event = await _repo.GetAsync(cancellationToken, id);
            if (@event == null)
            {
                throw new ItemNotFoundException($"Event with Id:{id} not found in database");
            }
            return @event.Adapt<EventResponseModel>();
        }

        public async Task RemoveAsync(CancellationToken cancellationToken, int id)
        {
            var result =await _repo.RemoveAsync(cancellationToken, id);
            if(result == false)
            {
                throw new ItemCouldNotBeDeletedException("Item could not be deleted");
            }
        }

        public async Task RemoveAsync(CancellationToken cancellationToken, int id, string userId)
        {
            var result = await _repo.RemoveAsync(cancellationToken, id, userId);
            if(result == false)
            {
                throw new ItemCouldNotBeDeletedException("Item could not be deleted");
            }
        }

        public Task RemoveAsync(CancellationToken cancellationToken, EventRequestModel @event)
        {
            throw new NotImplementedException();
        }

        public async Task SetIsActiveStatusAsync(CancellationToken cancellationToken, int id, bool isActive)
        {
            var result = await _repo.SetIsActiveStatusAsync(cancellationToken, id, isActive);
            if (result == false)
            {
                throw new ItemNotFoundException("Item could not be found");
            }
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, EventRequestModel @event, int id, string userId)
        {
            var exists = await _repo.Exists(cancellationToken, id, userId);
            if(exists == false)
            {
                throw new ItemNotFoundException("Item could not be found");
            }
            var toUpdate = @event.Adapt<Event>();
            toUpdate.Id = id;
            toUpdate.OwnerId = userId;
            await _repo.UpdateAsync(cancellationToken, toUpdate);
        }
        //what if admin wants to update event? we have to take ownerId first and then update it 
    }
}
