using EasyAgenda.Model;

namespace EasyAgenda.Data.Contracts
{
  public interface IAdminDAL
  {
    Task Register(Admin admin);
  }
}
