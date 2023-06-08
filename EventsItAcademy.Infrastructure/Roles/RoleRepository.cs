using EventsItAcademy.Application.Roles.Repositories;
using EventsItAcademy.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace EventsItAcademy.Infrastructure.Roles
{
    public class RoleRepository:IRoleRepository
    {
        private EventsItAcademyDbContext _context;

        public RoleRepository(EventsItAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<List<IdentityRole>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Roles.ToListAsync(cancellationToken);
        }
        public async Task<IdentityRole> GetByIdAsync(CancellationToken cancellationToken, string id)
        {
            return await _context.Roles.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
        }
    }
}
