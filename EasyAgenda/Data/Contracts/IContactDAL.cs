using EasyAgenda.Model;
using EasyAgenda.Model.DTO;

namespace EasyAgenda.Data.Contracts
{
  public interface IContactDAL
  {
    Task Update (IList<Contact> contacts); 
  }
}
