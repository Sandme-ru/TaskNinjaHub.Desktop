using System.Net.Http;
using System.Net.Http.Json;
using TaskNinjaHub.Desktop.Models.User;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;

namespace TaskNinjaHub.Desktop.Services.UserServices.Users;

public class UserService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateAuthClient();

    public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<IEnumerable<ApplicationUser>>("Admin/GetUsers")!;
        return result!;
    }

    public async Task<HttpResponseMessage> AddUserAsync(UserDto user)
    {
        var result = await _httpClient.PostAsJsonAsync($"Admin/AddUser", user);
        return result;
    }

    public async Task<HttpResponseMessage> EditUserAsync(AuthorDto user)
    {
        var result = await _httpClient.PutAsJsonAsync($"Admin/EditUser", user);
        return result;
    }

    public async Task<HttpResponseMessage> DeleteUserAsync(string userId)
    {
        var result = await _httpClient.DeleteAsync($"Admin/DeleteUser?id={userId}");
        return result;
    }

}