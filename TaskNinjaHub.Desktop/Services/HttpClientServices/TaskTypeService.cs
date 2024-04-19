using TaskNinjaHub.Desktop.Models.TaskTypes;
using TaskNinjaHub.Desktop.Services.Bases;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;

namespace TaskNinjaHub.Desktop.Services.HttpClientServices;

public class TaskTypeService(IHttpClientFactory httpClientFactory) : BaseService<CatalogTaskType>(httpClientFactory)
{
#if (DEBUG)

    protected override string BasePath => $"api/{nameof(CatalogTaskType).ToLower()}";

#elif (RELEASE)

    protected override string BasePath => $"task-api/api/{nameof(CatalogTaskType).ToLower()}";

#endif
}