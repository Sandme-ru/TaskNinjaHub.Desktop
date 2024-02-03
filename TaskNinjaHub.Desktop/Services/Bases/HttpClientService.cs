using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TaskNinjaHub.Desktop.Services.Bases;

public class HttpService<TEntity> where TEntity : class, new()
{
    private static string Token => UserProvider.User.Token;

    private const string Scheme = "Bearer";

    private const string MediaType = "application/json";

    public static async Task<TEntity> GetAsync(string url, HttpClient httpClient)
    {
        using (httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, Token);

            var response = await httpClient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TEntity>(responseContent)!;
        }
    }

    public static async Task<HttpResponseMessage> PutAsync(string url, TEntity entity, HttpClient httpClient)
    {
        var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, MediaType);

        using (httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, Token);
            return await httpClient.PutAsync(url, content);
        }
    }

    public static async Task<HttpResponseMessage> PostAsync(string url, TEntity entity, HttpClient httpClient)
    {
        var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, MediaType);

        using (httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Scheme, Token);
            return await httpClient.PostAsync(url, content);
        }
    }
}