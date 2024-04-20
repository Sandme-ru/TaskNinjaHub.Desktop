using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using TaskNinjaHub.Application.Entities.TaskTypes.Domain;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;
using TaskNinjaHub.Desktop.Utils.HttpHelperExtensions;
using TaskNinjaHub.Desktop.Utils.OperationResults;

namespace TaskNinjaHub.Desktop.Services.Bases;

public abstract class BaseService<TEntity>(IHttpClientFactory httpClientFactory) : IBaseService<TEntity> where TEntity : class
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateApiClient();

#if (DEBUG)

    protected virtual string BasePath => $"api/{nameof(TEntity).ToLower()}";

#elif (RELEASE)
    
    protected virtual string BasePath => $"task-api/api/{nameof(TEntity).ToLower()}";

#endif

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<IEnumerable<TEntity>>($"{BasePath}")!;
        return result!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllByFilterAsync(TEntity filterModel)
    {
        var requestUri = QueryHelpers.AddQueryString($"{BasePath}/filter", filterModel.ToPropertyDictionary());
        var result = await _httpClient.GetFromJsonAsync<IEnumerable<TEntity>>(requestUri);
        return result!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllByFilterByPageAsync(TEntity filterModel, int pageNumber = 1, int pageSize = 10)
    {
        var requestUri = $"{BasePath}/FilterByPage?pageNumber={pageNumber}&pageSize={pageSize}";
        var response = await _httpClient.PostAsJsonAsync(requestUri, filterModel.ToPropertyDictionary());
        var result = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(await response.Content.ReadAsStringAsync());

        return result!;
    }

    public virtual async Task<TEntity> GetIdAsync(int id)
    {
        var result = await _httpClient.GetFromJsonAsync<TEntity>($"{BasePath}/{id}");
        return result!;
    }

    public virtual async Task<HttpResponseMessage> DeleteAsync(int id)
    {
        var result = await _httpClient.DeleteAsync($"{BasePath}/{id}");
        return result;
    }

    public virtual async Task<OperationResult<TEntity>> CreateAsync(TEntity entity)
    {
        var response = await _httpClient.PostAsJsonAsync($"{BasePath}/Create", entity);
        var result = JsonConvert.DeserializeObject<OperationResult<TEntity>>(await response.Content.ReadAsStringAsync());

        return result!;
    }

    public virtual async Task<OperationResult<TEntity>> UpdateAsync(TEntity entity)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BasePath}/UpdateAsync", entity);
        var result = JsonConvert.DeserializeObject<OperationResult<TEntity>>(await response.Content.ReadAsStringAsync());

        return result!;
    }
}