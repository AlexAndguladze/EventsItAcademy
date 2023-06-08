using EventsItAcademy.Application.Roles.Repositories;
using EventsItAcademy.Application.Roles.Respones;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.Roles
{
    public class RoleService : IRoleService
    {
        private IRoleRepository _repo;
        public RoleService(IRoleRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<IdentityRoleResponseModel>> GetAllAsync(CancellationToken cancellationToken)
        {
           var roles = await _repo.GetAllAsync(cancellationToken);
            return roles.Adapt<List<IdentityRoleResponseModel>>();
        }

        public async Task<IdentityRoleResponseModel> GetById(CancellationToken cancellationToken, string id)
        {
            var role = await _repo.GetByIdAsync(cancellationToken, id);
            return role.Adapt<IdentityRoleResponseModel>();
        }
    }
}
