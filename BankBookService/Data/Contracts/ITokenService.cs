using EasyAgendaBase.Model;

namespace EasyAgendaService.Data.Contracts
{
  public interface ITokenService
  {
    string GenerateToken(UserRole user);
  }
}
