using TaskNinjaHub.Desktop.Models.Enums;

namespace TaskNinjaHub.Desktop.Models.User;

public class AuthorDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public LocalizationType LocalizationType { get; set; }
}