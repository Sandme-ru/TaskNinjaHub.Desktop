using TaskNinjaHub.Desktop.Models.Priorities;
using TaskNinjaHub.Desktop.Services.Bases;
using TaskNinjaHub.Desktop.Utils.HttpClientFactory;

namespace TaskNinjaHub.Desktop.Services.HttpClientServices;

public class PriorityService(IHttpClientFactory httpClientFactory) : BaseService<Priority>(httpClientFactory)
{
#if (DEBUG)

    protected override string BasePath => $"api/{nameof(Priority).ToLower()}";

#elif (RELEASE)

    protected override string BasePath => $"task-api/api/{nameof(Priority).ToLower()}";

#endif
}