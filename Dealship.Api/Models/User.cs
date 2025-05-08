﻿// файл: Dealship.Api/Models/User.cs
namespace Dealship.Api.Models;
public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PasswordHash { get; set; }
    public string Role { get; set; } = default!;
}
