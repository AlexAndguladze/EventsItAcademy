using EventsItAcademy.Application.Events.Repositories;
using EventsItAcademy.Domain.Events;
using EventsItAcademy.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Infrastructure.Events
{
    public class EventRepository : IEventRepository
    {
        #region ibase
        /*
        private IBaseRepository<Event> _repo;

        public EventRepository(IBaseRepository<Event> repo)
        {
            _repo = repo;
        }
        public async Task<bool> Exists(CancellationToken cancellationToken, int id)
        {
           return await _repo.Table.AnyAsync(x=>x.Id == id, cancellationToken);
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, int id, string userId)
        {
            return await _repo.Table.AnyAsync(x=>x.Id==id && x.OwnerId==userId, cancellationToken);
        }

        public async Task<List<Event>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync(cancellationToken);
        }

        public async Task<List<Event>> GetAllOfUser(CancellationToken cancellationToken, string userId)
        {
            return await _repo.Table.Where(x => x.IsDeleted == false && x.OwnerId == userId).ToListAsync(cancellationToken);
        }

        public async Task<Event> GetAsync(CancellationToken cancellationToken, int id, string userId)
        {
            return await _repo.Table.SingleOrDefaultAsync(x=>x.Id==id &&x.OwnerId==userId, cancellationToken);
        }

        public async Task<Event> GetAsync(CancellationToken cancellationToken, int id)
        {
            return await _repo.GetAsync(cancellationToken, id);
        }

        public async Task RemoveAsync(CancellationToken cancelToken, int id)
        {
            await _repo.GetAsync(cancelToken, id);
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, Event @event)
        {
            await _repo.UpdateAsync(cancellationToken, @event);
        }*/
        #endregion

        private EventsItAcademyDbContext _context;

        public EventRepository(EventsItAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(CancellationToken cancellationToken, Event @event)
        {
            await _context.Events.AddAsync(@event,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return @event.Id;
        }

        public async Task ArchiveFinishedEventsAsync(CancellationToken cancellationToken)
        {
                //Parallel.ForEach(_context.Events, @event =>
                //{
                //    if (@event.EndDate <= DateTime.Now)
                //    {
                //        if (@event.IsArchived == false)
                //            @event.IsArchived = true;
                //    }
                //    if (cancellationToken.IsCancellationRequested)
                //    {
                //        return;
                //    }
                //});
                foreach(var @event in _context.Events)
                {
                    if (@event.EndDate <= DateTime.Now)
                    {
                        if (@event.IsArchived == false)
                            @event.IsArchived = true;
                    }
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, int id, string userId)
        {
           return await  _context.Events.AnyAsync(x => x.OwnerId == userId && x.Id==id, cancellationToken);
        }

        public async Task<List<Event>> GetAllActive(CancellationToken cancellationToken)
        {
            return await _context.Events.Where(x =>
            x.IsDeleted == false && 
            x.IsArchived == false &&
            x.IsActive == true).ToListAsync(cancellationToken);
        }

        public async Task<List<Event>> GetAllArchivedAsync(CancellationToken cancellationToken)
        {
            return await _context.Events.Where(x =>
           x.IsDeleted == false &&
           x.IsArchived == true).ToListAsync(cancellationToken);
        }

        public async Task<List<Event>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Events.Where(x => x.IsDeleted == false).ToListAsync(cancellationToken);
        }

        public async Task<List<Event>> GetAllOfUser(CancellationToken cancellationToken, string userId)
        {
            return await _context.Events.Where(x=>x.OwnerId==userId && x.IsDeleted==false).ToListAsync(cancellationToken);
        }

        public async Task<List<Event>> GetAllPendingAsync(CancellationToken cancellationToken)
        {
            return await _context.Events.Where(x=>
            x.IsActive == false && 
            x.IsDeleted == false && 
            x.IsArchived == false).ToListAsync(cancellationToken);
        }

        //returns null if not found
        public async Task<Event> GetAsync(CancellationToken cancellationToken, int id, string userId)
        {
            return await _context.Events.SingleOrDefaultAsync(x=>
            x.Id==id && 
            x.OwnerId==userId &&
            x.IsDeleted == false, cancellationToken);
        }

        public async Task<Event> GetAsync(CancellationToken cancellationToken, int id)
        {
            //return await _context.Events.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            return await _context.Events.Include(x => x.Tickets).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> RemoveAsync(CancellationToken cancellationToken, int id, string userId)
        {
            var result = await GetAsync(cancellationToken, id, userId);
            if (result == null)
            {
                return false;
            }
            result.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> RemoveAsync(CancellationToken cancellationToken, int id)
        {
            var result = await GetAsync(cancellationToken, id);
            if (result == null)
            {
                return false;
            }
            if(result.Tickets != null)
            {
                result.Tickets.ForEach(x =>x.IsDeleted = true);
               
            }
            result.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> SetIsActiveStatusAsync(CancellationToken cancellationToken, int id, bool status)
        {
            var result = await _context.Events.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (result == null)
            {
                return false;
            }
            result.IsActive = status;
            _context.ChangeTracker.DetectChanges();
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, Event @event)
        {
            _context.Update(@event);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
