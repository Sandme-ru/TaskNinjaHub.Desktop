using TaskNinjaHub.Desktop.Models.User;

namespace TaskNinjaHub.Desktop.Services.UserServices.Roles;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetAllAsync();
}