﻿namespace TaskNinjaHub.Desktop.Models.User;

public class AddUserDto
{
    public virtual string UserName { get; set; }
    public virtual string Email { get; set; }
    public virtual string PhoneNumber { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public string? Password { get; set; }

    public string Role { get; set; }
}