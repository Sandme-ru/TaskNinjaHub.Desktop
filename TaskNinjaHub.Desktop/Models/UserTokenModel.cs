using Newtonsoft.Json;

namespace TaskNinjaHub.Desktop.Models;

public class UserTokenModel
{
    [JsonProperty("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")]
    public string UserName { get; set; }

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("middle_name")]
    public string MiddleName { get; set; }

    [JsonProperty("short_name")]
    public string ShortName { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    public string Token { get; set; }

    public List<string> Claims { get; set; }
}