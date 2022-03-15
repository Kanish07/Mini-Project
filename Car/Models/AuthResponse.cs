using Car.Entities;
namespace Car.Models;

public class AuthResponse
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public Role Role { get; set; }
    public string? Token { get; set; }

    public AuthResponse(User user, string token)
    {
        Id = user.Userid;
        Username = user.Username;
        Role = user.UserRole;
        Token = token;
    }
};