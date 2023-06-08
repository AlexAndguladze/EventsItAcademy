using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Roles.Repositories
{
    public interface IRoleRepository
    {
        Task<List<IdentityRole>>GetAllAsync(CancellationToken cancellationToken);
        Task<IdentityRole>GetByIdAsync(CancellationToken cancellationToken, string id);
    }
}
