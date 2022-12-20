using EasyAgendaBase.Model;
using EasyAgendaService.Data.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyAgendaService
{
  public class TokenService: ITokenService
  {
    private readonly string _privateKey;
    public TokenService(IConfiguration configuration)
    {
      _privateKey = configuration["jwt:Key"];
    }
    public string GenerateToken(UserRole user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_privateKey);
      var tokenDescriptior = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Email, user.Email),
          new Claim(ClaimTypes.Role, user.RoleDescription)
        }),
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptior);
      return tokenHandler.WriteToken(token);
    }
  }
}
