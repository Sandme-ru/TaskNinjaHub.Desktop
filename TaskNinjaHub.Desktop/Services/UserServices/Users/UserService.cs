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
        try 
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<ApplicationUser>>("Admin/GetUsers")!;
            return result!;
        }
        catch
        {
            return new List<ApplicationUser>();
        }
    }

    public async Task<HttpResponseMessage> AddUserAsync(AddUserDto user)
    {
        try
        {
            var result = await _httpClient.PostAsJsonAsync($"Admin/AddUser", user);
            return result;
        }
        catch
        {
            return new HttpResponseMessage();
        }
    }

    public async Task<HttpResponseMessage> EditUserAsync(UserDto user)
    {

        try
        {
            var result = await _httpClient.PostAsJsonAsync($"Admin/EditUserInfo", user);
            return result;
        }
        catch
        {
            return new HttpResponseMessage();
        }
    }

    public async Task<HttpResponseMessage> DeleteUserAsync(string userId)
    {
        try
        {
            var result = await _httpClient.DeleteAsync($"Admin/DeleteUser?id={userId}");
            return result;
        }
        catch
        {
            return new HttpResponseMessage();
        }
    }

}