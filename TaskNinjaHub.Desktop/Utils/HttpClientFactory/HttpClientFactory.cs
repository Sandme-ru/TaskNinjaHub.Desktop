using System.Net.Http;

namespace TaskNinjaHub.Desktop.Utils.HttpClientFactory;

public class HttpClientFactory : IHttpClientFactory
{
    public HttpClient CreateAuthClient()
    {

        var httpClient = new HttpClient();
#if DEBUG
        httpClient.BaseAddress = new Uri("https://localhost:7219");
#endif
#if RELEASE
        httpClient.BaseAddress = new Uri("https://auth.sandme.ru");
#endif
        return httpClient;
    }

    public HttpClient CreateApiClient()
    {
        var httpClient = new HttpClient();
#if DEBUG
        httpClient.BaseAddress = new Uri("https://localhost:7179");
#endif
#if RELEASE
        httpClient.BaseAddress = new Uri("https://sandme.ru/task-api");
#endif
        return httpClient;
    }

}