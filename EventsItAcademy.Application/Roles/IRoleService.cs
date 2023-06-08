using EventsItAcademy.Application.Roles.Respones;


namespace EventsItAcademy.Application.Roles
{
    public interface IRoleService
    {
        Task<List<IdentityRoleResponseModel>>GetAllAsync(CancellationToken cancellationToken);
        Task<IdentityRoleResponseModel>GetById(CancellationToken cancellationToken, string id);
    }
}
