using System.IdentityModel.Tokens.Jwt;
using Car.Helpers;
using Car.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Car.Services;

public interface IUserServices
{
    AuthResponse Authenticate(User model);
    IEnumerable<User> GetAll();
    User GetById(Guid Id);
}

public class UserServices : IUserServices
{
    private readonly carContext _context;

    private readonly AppSettings _appSettings;

    public UserServices(carContext context, IOptions<AppSettings> appSettings)
    {
        _context = context;
        _appSettings = appSettings.Value;
    }

    public AuthResponse Authenticate(User user)
    {       
        var token = GenerateJwtToken(user);
        return new AuthResponse(user, token);
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User GetById(Guid Id)
    {
        return _context.Users.FirstOrDefault(x => x.Userid == Id)!;
    }

    private string GenerateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret!);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new[]
                {
                    new Claim("Id", user.Userid.ToString())
                }
            ),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}