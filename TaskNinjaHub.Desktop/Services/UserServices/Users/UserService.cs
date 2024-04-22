using IdentityModel;
using System.Net.Http;
using System.Net.Http.Json;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;

namespace TaskNinjaHub.Desktop.Services.UserServices.Users;

public class UserService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateAuthClient();

}