using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using TaskNinjaHub.Desktop.Models;

namespace TaskNinjaHub.Desktop.Services;

public class UserProvider
{
    public static UserTokenModel User { get; set; } = null!;

    public static UserTokenModel GetUser(string token)
    {
        var jwtSecurityToken = new JwtSecurityToken(jwtEncodedString: token);
        var userToken = new UserTokenModel
        {
            Token = token
        };

        foreach (var claim in jwtSecurityToken.Claims)
        {
            switch (claim.Type)
            {
                case "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name":
                    userToken.Name = claim.Value;
                    break;
                case "first_name":
                    userToken.FirstName = claim.Value;
                    break;
                case "last_name":
                    userToken.LastName = claim.Value;
                    break;
                case "middle_name":
                    userToken.MiddleName = claim.Value;
                    break;
                case "short_name":
                    userToken.ShortName = claim.Value;
                    break;
                case "email":
                    userToken.Email = claim.Value;
                    break;
                case "claims":
                    userToken.Claims = JsonConvert.DeserializeObject<List<string>>(claim.Value) ?? new List<string>(); ;
                    break;
            }
        }
        
        User = userToken;
        
        return userToken;
    }
}