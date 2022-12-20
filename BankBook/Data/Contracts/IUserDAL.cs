using EasyAgenda.Model;
using EasyAgenda.Model.DTO;

namespace EasyAgenda.Data.Contracts
{
  public interface IUserDAL
  {
    Task<User> GetByEmail(string email);
    Task<int> InsertReturn(UserDTO user);
  }
}
