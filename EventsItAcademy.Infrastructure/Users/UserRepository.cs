using EventsItAcademy.Application.Users.Repositories;
using EventsItAcademy.Domain.Users;
using EventsItAcademy.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Infrastructure.Users
{
    public class UserRepository:IUserRepository
    {
        private EventsItAcademyDbContext _context;

        public UserRepository(EventsItAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }
    }
}
