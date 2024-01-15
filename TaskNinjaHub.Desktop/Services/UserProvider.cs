using System.Security.Claims;

namespace TaskNinjaHub.Desktop.Services;

public class UserProvider
{
    public static IEnumerable<Claim> User { get; set; }
}