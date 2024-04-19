using System.Net.Http;

namespace TaskNinjaHub.Desktop.Utils.HttpClientFactory;

public interface IHttpClientFactory
{
    HttpClient CreateAuthClient();

    HttpClient CreateApiClient();
}