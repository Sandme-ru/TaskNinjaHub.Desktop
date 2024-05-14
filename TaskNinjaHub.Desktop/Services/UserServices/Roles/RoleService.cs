using System.Net.Http;
using System.Net.Http.Json;
using TaskNinjaHub.Desktop.Models.Role.Domain;
using TaskNinjaHub.Desktop.Models.User;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;

namespace TaskNinjaHub.Desktop.Services.UserServices.Roles;

public class RoleService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateAuthClient();

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<IEnumerable<Role>>("Admin/GetRoles")!;
        return result!;
    }

    public async Task<HttpResponseMessage> AddRoleAsync(string rolename)
    {
        var result = await _httpClient.PostAsync($"Admin/AddRole?roleName={rolename}", null);
        return result;
    }

    public async Task<HttpResponseMessage> EditRoleAsync(string roleId, string newName)
    {
        var result = await _httpClient.PutAsync($"Admin/EditRole?roleId={roleId}&newName={newName}", null);
        return result;
    }

    public async Task<HttpResponseMessage> DeleteRoleAsync(string roleId)
    {
        var result = await _httpClient.DeleteAsync($"Admin/DeleteRole?roleId={roleId}");
        return result;
    }
    public async Task<string> GetUserRoleAsync(string userId)
    {
        var response = await _httpClient.GetFromJsonAsync<IList<string>>($"Admin/GetUserRole?userId={userId}");
        var result = response.FirstOrDefault();

        return result;
    }
}